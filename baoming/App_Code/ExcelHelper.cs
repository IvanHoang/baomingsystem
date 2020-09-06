using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using System.Drawing;
using NPOI.SS.Util;
using System.Text;
using NPOI.HPSF;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;

/*
 *使用方法
 *①最普通，行行
 *②大数据待解决
 *③支持可配置表头设计待解决
 *④支持模版导出待解决
 */
internal class ExcelHelper
{
    #region "构造函数"
    internal ExcelHelper() { }
    #endregion

    #region "私有字段"
    /// <summary>
    /// 自定义颜色
    /// </summary>
    HSSFPalette XlPalette = null;
    /// <summary>
    /// 要导出的excel对象
    /// </summary>
    private HSSFWorkbook workbook = null;
    /// <summary>
    /// 要导出的excel对象属性
    /// </summary>
    private HSSFWorkbook Workbook
    {
        get
        {
            if (workbook == null)
            {
                workbook = new HSSFWorkbook();
            }
            return workbook;
        }
        set { workbook = value; }
    }
    /// <summary>
    /// 要导出的excel对象中的一个表
    /// </summary>
    private ISheet sheet = null;
    /// <summary>
    /// 导出的内容部分的样式
    /// </summary>
    HSSFCellStyle cellStyle = null;
    /// <summary>
    /// 表头行数
    /// </summary>
    int rowHeadNum = 0;
    /// <summary>
    /// 行数，方便内容正确在行插入
    /// </summary>
    private List<IRow> rowList = new List<IRow>();

    private List<GroupClass> gCell = null;
    private List<GroupClass> GCell
    {
        get { return gCell; }
        set { gCell = value; }
    }

    /// <summary>
    /// 整个表格border样式，默认solid
    /// </summary>
    private NPOI.SS.UserModel.BorderStyle wholeBorderStyle = NPOI.SS.UserModel.BorderStyle.Thin;
    private NPOI.SS.UserModel.BorderStyle WholeBorderStyle
    {
        get { return wholeBorderStyle; }
        set { wholeBorderStyle = value; }
    }
    /// <summary>
    /// 整个表格border颜色，默认黑色
    /// </summary>
    private short wholeBorderColor = HSSFColor.Black.Index;
    public short WholeBorderColor
    {
        get { return wholeBorderColor; }
        set { wholeBorderColor = value; }
    }
    /// <summary>
    /// 表头单元格字体是否加粗
    /// </summary>
    private short headFontWeight = (short)FontBoldWeight.Bold;
    /// <summary>
    /// 表头单元格字体是否加粗
    /// </summary>
    public short HeadFontWeight
    {
        get { return headFontWeight; }
        set { headFontWeight = value; }
    }


    #endregion

    /// <summary>
    /// 创建表头
    /// </summary>
    /// <param name="json">类似json的字符串</param>
    internal void SetHead(string json, List<GroupClass> group, int column)
    {
        Root T = JsonUtility.DecodeObject<Root>(json);
        //确定表头行数
        if (group != null && column > -1)
        {
            int headRow = T.root.rowspan.Value;
            int indexs = headRow;
            foreach (var item in group)
            {
                item.column = column;
                if (indexs == headRow)
                {
                    item.index = headRow;
                }
                else
                {
                    item.index = indexs;
                }
                indexs = item.index.Value + item.groupCount.Value;
            }
            SetGroupCell(group);
        }
        if (sheet == null)
        {
            sheet = Workbook.CreateSheet(T.root.sheetname);
        }
        sheet.DisplayGridlines = true;
        if (T.root.defaultwidth.HasValue)
        {
            //设置表格默认宽高
            sheet.DefaultColumnWidth = T.root.defaultwidth.Value; //12
        }
        if (T.root.defaultheight.HasValue)
        {
            //设置表格默认行高
            sheet.DefaultRowHeight = (short)T.root.defaultheight.Value; //25
        }
        if (!string.IsNullOrEmpty(T.root.borderstyle))
        {
            string bStyle = T.root.borderstyle.Trim();
            if (!string.IsNullOrEmpty(bStyle))
            {
                switch (bStyle)
                {
                    case "none":
                        WholeBorderStyle = NPOI.SS.UserModel.BorderStyle.None;
                        break;
                    case "solid":
                        WholeBorderStyle = NPOI.SS.UserModel.BorderStyle.Thin;
                        break;
                    case "dashed":
                        WholeBorderStyle = NPOI.SS.UserModel.BorderStyle.Dashed;
                        break;
                    case "dotted":
                        WholeBorderStyle = NPOI.SS.UserModel.BorderStyle.Dotted;
                        break;
                    case "double":
                        WholeBorderStyle = NPOI.SS.UserModel.BorderStyle.Double;
                        break;
                    default:
                        WholeBorderStyle = NPOI.SS.UserModel.BorderStyle.Thin;
                        break;
                }
            }
        }
        XlPalette = Workbook.GetCustomPalette();
        if (!string.IsNullOrEmpty(T.root.bordercolor))
        {
            Color co = ColorTranslator.FromHtml(T.root.bordercolor);
            XlPalette.SetColorAtIndex(HSSFColor.Plum.Index, (byte)co.R, (byte)co.G, (byte)co.B);
            WholeBorderColor = NPOI.HSSF.Util.HSSFColor.Plum.Index;//这句代码根据16进制不起作用，起到颜色初始化
        }


        int rowN = Convert.ToInt32(T.root.rowspan);
        rowHeadNum = rowN;
        //创建行
        for (int i = 0; i < rowN; i++)
        {
            IRow temp = sheet.CreateRow(i);
            rowList.Add(temp);

        }
        //合并单元格
        //填充内容
        for (int i = 0; i < T.root.head.Count; i++)
        {
            //读取最重要的区域,0=fromRow,1=toRow,2=fromColumn,3=toColumn
            AttributeList al = T.root.head[i];
            int[] c = al.cellregion.Split(',').ToIntArray();
            if (c[0] < c[1] || c[2] < c[3])   //例如1,1,2,2 第二行中的第3列,例如1,1,2,7 第二行中的(第3列到8列),合并列
            {
                CellRangeAddress cellr = new CellRangeAddress(c[0], c[1], c[2], c[3]);
                sheet.AddMergedRegion(cellr);
                //设置边框
                ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(cellr, WholeBorderStyle, WholeBorderColor);
            }
        }
        //填充内容
        for (int i = 0; i < T.root.head.Count; i++)
        {
            //读取最重要的区域,0=fromRow,1=toRow,2=fromColumn,3=toColumn
            AttributeList al = T.root.head[i];
            int[] c = al.cellregion.Split(',').ToIntArray();
            //计算title要插入的位置的索引
            int txtIndex = -1;
            int txtRow = -1;

            if ((c[0] == c[1] && c[2] == c[3]) || (c[0] == c[1] && c[2] < c[3]))
            { //例如1,1,2,2 第二行中的第3列,例如1,1,2,7 第二行中的(第3列到8列)
                txtIndex = c[2];
                txtRow = c[0];
                ICell cell1 = rowList[txtRow].CreateCell(txtIndex);

                //设置单元格的高度
                if (!T.root.defaultheight.HasValue && al.height.HasValue)
                {
                    rowList[txtRow].HeightInPoints = (short)al.height.Value;
                }
                SetHeadCellBold(al);
                cell1.SetCellValue(al.title);
                cell1.CellStyle = SetCellStyle(al);

            }
            if (c[0] < c[1] && c[2] == c[3]) //合并c[0]到c[1]行 ，列没变 ，   'cellregion':'0,1,1,1',
            {
                txtIndex = c[2];
                txtRow = c[0];
                ICell cell1 = rowList[txtRow].CreateCell(txtIndex);
                //设置单元格的高度
                if (!T.root.defaultheight.HasValue && al.height.HasValue)
                {
                    rowList[txtRow].Height = (short)(al.height.Value * 20);
                }
                SetHeadCellBold(al);
                cell1.SetCellValue(al.title);
                cell1.CellStyle = SetCellStyle(al);
            }
            if (c[0] < c[1] && c[2] < c[3]) //合并c[0]到c[1]行 ，列没变 ，   'cellregion':'4,5,2,4',
            {
                txtIndex = c[2];
                txtRow = c[0];
                ICell cell1 = rowList[txtRow].CreateCell(txtIndex);
                SetHeadCellBold(al);
                //设置单元格的高度
                if (!T.root.defaultheight.HasValue && al.height.HasValue)
                {
                    rowList[txtRow].Height = (short)(al.height.Value * 20);
                }
                cell1.SetCellValue(al.title);
                cell1.CellStyle = SetCellStyle(al);
            }

            //设置单元格的宽度
            //if (!T.root.defaultwidth.HasValue && al.width.HasValue)
            if (al.width.HasValue)
            {
                sheet.SetColumnWidth(i, al.width.Value * 256);
            }
        }


    }
    /// <summary>
    /// 设置表头单元格字体是否加粗，默认加粗
    /// </summary>
    /// <param name="al"></param>
    private void SetHeadCellBold(AttributeList al)
    {
        if (string.IsNullOrEmpty(al.fontweight))
        {
            HeadFontWeight = (short)FontBoldWeight.Bold;
        }
        else
        {
            switch (al.fontweight)
            {
                case "bold":
                    HeadFontWeight = (short)FontBoldWeight.Bold;
                    break;
                case "none":
                    HeadFontWeight = (short)FontBoldWeight.None;
                    break;
                case "normal":
                    HeadFontWeight = (short)FontBoldWeight.Normal;
                    break;
                default:
                    HeadFontWeight = (short)FontBoldWeight.Bold;
                    break;
            }
        }
    }

    /// <summary>
    /// web导出excel
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    /// <param name="list">导出的列表对象</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="titles">标题</param>
    /// <param name="fieldFuncs">字段委托，如果不传则T的全部属性</param>
    internal void ExportToWeb<T>(List<T> list, string fileName, string[] titles, string headJson, params Func<T, string>[] fieldFuncs)
    {
        ///创建表头
        SetHead(headJson, null, -1);
        ///转换数据源
        System.Data.DataTable dtSource = list.ToDataTable(titles, fieldFuncs);
        ///开始导出
        WebCommonExport(dtSource, fileName);
        System.GC.Collect();
    }
    /// <summary>
    /// web导出excel
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    /// <param name="list">导出的列表对象</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="titles">标题</param>
    /// <param name="fieldFuncs">字段委托，如果不传则T的全部属性</param>
    internal void ExportToWeb<T>(List<T> list, string fileName, string[] titles, string headJson, List<GroupClass> group, int groupColumn, params Func<T, string>[] fieldFuncs)
    {
        ///创建表头
        SetHead(headJson, group, groupColumn);
        ///转换数据源
        System.Data.DataTable dtSource = list.ToDataTable(titles, fieldFuncs);
        ///开始导出
        WebCommonExport(dtSource, fileName);
        System.GC.Collect();
    }

    /// <summary>
    /// 保存到本地
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    /// <param name="list">导出的列表对象</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="titles">标题</param>
    /// <param name="fieldFuncs">字段委托，如果不传则T的全部属性</param>
    internal void ExportToLocal<T>(List<T> list, string fileName, string[] titles, string headJson, params Func<T, string>[] fieldFuncs)
    {
        ///创建表头
        SetHead(headJson, null, -1);
        ///转换数据源
        System.Data.DataTable dtSource = list.ToDataTable(titles, fieldFuncs);
        ///开始导出
        LocalCommonExport(dtSource, fileName);
        System.GC.Collect();
    }
    /// <summary>
    /// 保存到本地
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    /// <param name="list">导出的列表对象</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="titles">标题</param>
    /// <param name="fieldFuncs">字段委托，如果不传则T的全部属性</param>
    internal void ExportToLocal<T>(List<T> list, string fileName, string[] titles, string headJson, List<GroupClass> group, int groupColumn, params Func<T, string>[] fieldFuncs)
    {
        ///创建表头
        SetHead(headJson, group, groupColumn);
        ///转换数据源
        System.Data.DataTable dtSource = list.ToDataTable(titles, fieldFuncs);
        ///开始导出
        LocalCommonExport(dtSource, fileName);
        System.GC.Collect();
    }
    /// DataTable导出到Excel的MemoryStream,#CommonExport,全部都是字符串处理
    /// </summary>
    /// <param name="dtSource">源DataTable</param>
    /// <param name="strHeaderText">表头文本</param>
    internal void WebCommonExport(System.Data.DataTable dtSource, string fileName)
    {
        BeforeExport(dtSource);
        //转换好后开始提供下载
        Workbook.ExportToWeb(fileName);
    }

    /// DataTable导出到Excel的MemoryStream,#CommonExport,全部都是字符串处理
    /// </summary>
    /// <param name="dtSource">源DataTable</param>
    /// <param name="fileName">文件存储路径</param>
    internal void LocalCommonExport(System.Data.DataTable dtSource, string fileName)
    {
        BeforeExport(dtSource);
        //转换好后开始保存本地
        Workbook.ExportToLocal(fileName);
    }

    /// <summary>
    /// 整合数据
    /// </summary>
    /// <param name="dtSource"></param>
    private void BeforeExport(System.Data.DataTable dtSource)
    {
        HSSFCellStyle dateStyle = (HSSFCellStyle)Workbook.CreateCellStyle();
        cellStyle = SetContentFormat(600, 10);
        //取得列宽
        int[] arrColWidth = new int[dtSource.Columns.Count];
        foreach (DataColumn item in dtSource.Columns)
        {
            arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
        }
        for (int i = 0; i < dtSource.Rows.Count; i++)
        {
            for (int j = 0; j < dtSource.Columns.Count; j++)
            {
                int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                if (intTemp > arrColWidth[j])
                {
                    arrColWidth[j] = intTemp;
                }
            }
        }
        int rowIndex = rowList.Count();
        dtSource.Rows.RemoveAt(0); //移除第一行，因为有表头了



        foreach (DataRow row in dtSource.Rows)
        {
            IRow dataRow = sheet.CreateRow(rowIndex);
            foreach (DataColumn column in dtSource.Columns)
            {
                ICell newCell = dataRow.CreateCell(column.Ordinal);
                newCell.CellStyle = cellStyle;
                string drValue = row[column].ToString();
                newCell.SetCellValue(drValue);

            }
            rowIndex++;
        }

        try
        {
            int temp = 0;
            if (GCell != null && dtSource.Rows.Count > 0)
            {
                foreach (GroupClass gCell in GCell)
                {

                    IRow row = sheet.GetRow(gCell.index.Value);//获取工作表第一行
                    ICell cell = row.GetCell(gCell.column);//获取行的第COLUMN列
                    string cellValue = cell.ToString();//获取列的值
                    //如果设置了分组，目前只能一种
                    temp = gCell.index.Value + gCell.groupCount.Value - 1;
                    CellRangeAddress cellr = new CellRangeAddress(gCell.index.Value, temp, gCell.column, gCell.column);
                    sheet.AddMergedRegion(cellr);
                    //设置边框
                    ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(cellr, WholeBorderStyle, WholeBorderColor);
                    ICell ce = row.CreateCell(gCell.column);
                    ce.CellStyle = cellStyle;
                    ce.SetCellValue(cellValue);
                }
            }
        }
        catch
        {
            throw new Exception("GroupCell某些属性可能为空了！");
        }

    }


    #region 辅助帮助，设置样式和 转换颜色
    static bool cellColorBug = true; //关于NPOI自定义颜色设置有个bug，这个可以保证第一次单元格设置不会始终黑色
    /// <summary>
    /// 设置单元格基本样式
    /// </summary>
    /// <param name="al"></param>
    private HSSFCellStyle SetCellStyle(AttributeList al)
    {
        HSSFCellStyle headStyle = (HSSFCellStyle)Workbook.CreateCellStyle();
        XlPalette = Workbook.GetCustomPalette();
        headStyle.Alignment = string.IsNullOrEmpty(al.align) ? NPOI.SS.UserModel.HorizontalAlignment.Center : al.align.ToHorAlign(); //默认水平居中
        headStyle.VerticalAlignment = string.IsNullOrEmpty(al.valign) ? VerticalAlignment.Center : al.valign.ToVerAlign();//垂直居中
        headStyle.FillPattern = FillPattern.SolidForeground; //默认填充整个背景颜色
        bool forc = string.IsNullOrEmpty(al.bgcolor); //是否有背景颜色
        if (forc)
        {
            headStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index; //默认灰色
        }
        else
        {
            headStyle.FillForegroundColor = GetColorIndex(Workbook, al.bgcolor);//这句代码根据16进制不起作用，起到颜色初始化
            if (cellColorBug)
            {
                Color co = ColorTranslator.FromHtml(al.bgcolor);
                XlPalette.SetColorAtIndex(HSSFColor.Pink.Index, (byte)co.R, (byte)co.G, (byte)co.B);
                headStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Pink.Index;
                cellColorBug = false;
            }
        }

        //设置单元格border
        headStyle.BorderRight = headStyle.BorderLeft = headStyle.BorderBottom = headStyle.BorderTop = WholeBorderStyle;
        headStyle.BottomBorderColor = headStyle.RightBorderColor = headStyle.LeftBorderColor = headStyle.TopBorderColor = WholeBorderColor;
        bool fontc = string.IsNullOrEmpty(al.fontcolor); //是否有字体颜色
        //设置单元格字体
        HSSFFont font = (HSSFFont)Workbook.CreateFont();
        if (fontc)
        {
            font.Color = 8; //默认黑色
        }
        else
        {
            font.Color = GetColorIndex(Workbook, al.fontcolor);//这句代码根据16进制不起作用，起到颜色初始化
            if (cellColorBug)
            {
                Color co = ColorTranslator.FromHtml(al.fontcolor);
                XlPalette.SetColorAtIndex(HSSFColor.Pink.Index, (byte)co.R, (byte)co.G, (byte)co.B);
                font.Color = NPOI.HSSF.Util.HSSFColor.Pink.Index;
                cellColorBug = false;
            }
        }


        font.FontHeightInPoints = al.fontsize ?? 11; //设置字体大小
        font.Boldweight = HeadFontWeight;
        font.FontName = string.IsNullOrWhiteSpace(al.fontName) ? "宋体" : al.fontName;//设置字体为宋体
        font.IsItalic = al.IsItalic.HasValue && al.IsItalic.Value ? true : false;//是否是斜体
        font.IsStrikeout = al.IsStrikeout.HasValue && al.IsStrikeout.Value ? true : false;//是否有中间线
        font.Underline = al.Underline.HasValue && al.Underline.Value ? FontUnderlineType.Single : FontUnderlineType.None;//设置下划线
        headStyle.SetFont(font);
        return headStyle;
    }

    /// <summary>
    /// 根据十六进制颜色获得颜色索引
    /// </summary>
    /// <param name="workbook"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    private short GetColorIndex(HSSFWorkbook workbook, string color)
    {
        Color co = ColorTranslator.FromHtml(color);

        return GetXLColour(workbook, co);
    }

    //获得excel中的颜色索引
    private short GetXLColour(HSSFWorkbook workbook, System.Drawing.Color SystemColour)
    {
        short s = 0;
        HSSFColor XlColour = XlPalette.FindColor(SystemColour.R, SystemColour.G, SystemColour.B);
        if (XlColour == null)
        {
            if (NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE < 255)
            {
                if (NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE < 64)
                {
                    //NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE = 64;
                    //NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE += 1;
                    XlColour = XlPalette.AddColor(SystemColour.R, SystemColour.G, SystemColour.B);
                }
                else
                {
                    XlColour = XlPalette.FindSimilarColor(SystemColour.R, SystemColour.G, SystemColour.B);
                }
                s = XlColour.Indexed;
            }
        }
        else
            s = XlColour.Indexed;
        return s;
    }

    #endregion

    #region 设置excel文件基本属性
    /// <summary>
    /// 文件基本属性
    /// </summary>
    /// <param name="company">公司名称，默认 慧择网</param>
    /// <param name="author">作者信息，默认 慧择</param>
    /// <param name="ApplicationName">创建程序信息</param>
    /// <param name="LastAuthor">xls文件最后保存者信息</param>
    /// <param name="Comments">填加xls文件作者信息，备注</param>
    /// <param name="title">填加xls文件标题信息</param>
    /// <param name="Subject">填加文件主题信息</param>
    /// <returns>一个初始化的Excel Workbook对象</returns>
    internal void SetWorkbook(ExcelProperty ep)
    {
        #region 右击文件 属性信息
        DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
        ///公司
        dsi.Company = ep.Company;
        dsi.Manager = ep.Manager;
        dsi.Category = ep.Catagory;
        Workbook.DocumentSummaryInformation = dsi;
        SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
        si.Author = ep.Author; //填加xls文件作者信息
        si.ApplicationName = ep.ApplicationName; //填加xls文件创建程序信息
        si.LastAuthor = ep.LastAuthor; //填加xls文件最后保存者信息
        si.Comments = ep.Comments; //填加xls文件作者信息
        si.Title = ep.Title; //填加xls文件标题信息
        si.Subject = ep.Subject;//填加文件主题信息
        si.Keywords = ep.KeyWord;
        si.CreateDateTime = DateTime.Now;
        si.Comments = ep.Comments;
        Workbook.SummaryInformation = si;
        #endregion
    }
    /// <summary>
    /// 初始化显示的内容的单元格统一的样式
    /// </summary>
    /// <param name="fontweight">字体粗细</param>
    /// <param name="fontsize">字体大小</param>
    internal HSSFCellStyle SetContentFormat(short fontweight = 600, short fontsize = 10)
    {
        cellStyle = (HSSFCellStyle)Workbook.CreateCellStyle();
        //设置单元格border
        cellStyle.BorderRight = cellStyle.BorderLeft = cellStyle.BorderBottom = cellStyle.BorderTop = WholeBorderStyle;
        cellStyle.BottomBorderColor = cellStyle.RightBorderColor = cellStyle.LeftBorderColor = cellStyle.TopBorderColor = WholeBorderColor;
        cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        cellStyle.VerticalAlignment = VerticalAlignment.Center;
        IFont font = Workbook.CreateFont();
        font.FontHeightInPoints = fontsize;

        //去掉导出文件中内容的加粗
        //font.Boldweight = fontweight;

        cellStyle.SetFont(font);
        return cellStyle;
    }
    #endregion

    #region 设置分组信息
    internal void SetGroupCell(List<GroupClass> ce)
    {
        GCell = ce;
    }
    #endregion

    #region 导入功能
    public bool Import(string filePath, ref DataTable table, ref string ErrMsg)
    {
        bool bSucc = true;
        if (File.Exists(filePath))
        {
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            HSSFWorkbook workbook = null;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
            }

            //获取excel的第一个sheet
            var sheet = workbook.GetSheetAt(0);

            if (table == null)
                table = new DataTable();
            //获取sheet的首行
            var headerRow = sheet.GetRow(0);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }

            workbook = null;
            sheet = null;
        }
        else
        {
            bSucc = false;
            ErrMsg = "文件不存在！" + filePath;
        }
        return bSucc;
    }

    #endregion
}

/// <summary>
/// 拓展类
/// </summary>
public static class Extend
{

    /// <summary>
    /// 泛型列表转成DataTable
    /// </summary>
    /// <typeparam name="T">泛型实体</typeparam>
    /// <param name="list">要转换的列表</param>
    /// <param name="titles">标题</param>
    /// <param name="fieldFuncs">字段委托</param>
    /// <returns></returns>
    internal static System.Data.DataTable ToDataTable<T>(this List<T> list, string[] titles, params Func<T, string>[] fieldFuncs)
    {
        if (fieldFuncs.Length > 0)
        {
            if (titles == null || fieldFuncs.Length != titles.Length)
            {
                throw new Exception("titles不能为空且必须与导出字段一一对应");
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            //标题行
            DataRow headerDataRow = dt.NewRow();
            for (int i = 0; i < fieldFuncs.Length; i++)
            {
                dt.Columns.Add(new DataColumn());
                headerDataRow[i] = titles[i];
            }
            dt.Rows.Add(headerDataRow);

            //内容行
            foreach (T item in list)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < fieldFuncs.Length; i++)
                {
                    dr[i] = fieldFuncs[i](item);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        else
        {
            Type listType = typeof(T);
            PropertyInfo[] properties = listType.GetProperties();
            if (properties.Length != titles.Length)
            {
                throw new Exception("titles不能为空且必须与导出字段一一对应");
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            //标题行
            DataRow headerDataRow = dt.NewRow();
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                dt.Columns.Add(new DataColumn());
                headerDataRow[i] = titles[i];
            }
            dt.Rows.Add(headerDataRow);

            //内容行
            foreach (T item in list)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dr[i] = properties[i].GetValue(item, null);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }

    /// <summary>
    /// List转DataTable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    internal static DataTable ToDataTable<T>(this IEnumerable<T> list, string tableName)
    {
        //创建属性的集合    
        List<PropertyInfo> pList = new List<PropertyInfo>();
        //获得反射的入口    
        Type type = typeof(T);
        System.Data.DataTable dt = new System.Data.DataTable();
        dt.TableName = tableName;
        //把所有的public属性加入到集合 并添加DataTable的列    
        System.Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
        foreach (var item in list)
        {
            //创建一个DataRow实例    
            DataRow row = dt.NewRow();
            //给row 赋值    
            pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
            //加入到DataTable    
            dt.Rows.Add(row);
        }
        return dt;
    }

    /// <summary>
    /// 将WorkBook对象转换成内存流
    /// </summary>
    /// <param name="wv"></param>
    /// <returns></returns>
    public static MemoryStream SaveToStream(this HSSFWorkbook wv)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            wv.Write(ms);
            ms.Flush();
            ms.Position = 0;
            return ms;
        }
    }

    internal static int[] ToIntArray(this string[] region)
    {
        ArrayList aList = new ArrayList();
        foreach (string i in region)
            aList.Add(Convert.ToInt32(i));
        return (int[])aList.ToArray(typeof(int));
    }

    internal static NPOI.SS.UserModel.HorizontalAlignment ToHorAlign(this string str)
    {
        NPOI.SS.UserModel.HorizontalAlignment ret = NPOI.SS.UserModel.HorizontalAlignment.Center;
        switch (str.ToLower())
        {
            case "center":
                ret = NPOI.SS.UserModel.HorizontalAlignment.Center;
                break;
            case "left":
                ret = NPOI.SS.UserModel.HorizontalAlignment.Left;
                break;
            case "right":
                ret = NPOI.SS.UserModel.HorizontalAlignment.Right;
                break;
            default:
                ret = NPOI.SS.UserModel.HorizontalAlignment.Center;
                break;
        }
        return ret;
    }

    internal static VerticalAlignment ToVerAlign(this string str)
    {
        switch (str.ToLower())
        {
            case "center":
                return VerticalAlignment.Center;
            case "top":
                return VerticalAlignment.Top;
            case "bottom":
                return VerticalAlignment.Bottom;
            default:
                return VerticalAlignment.Center;
        }
        //return VerticalAlignment.CENTER;
    }

    /// <summary>
    ///  web导出excel
    /// </summary>
    /// <param name="hssf">已经被处理好的HSSFWorkbook对象</param>
    /// <param name="fileName">将要下载显示的名字</param>
    public static void ExportToWeb(this HSSFWorkbook hssf, string fileName)
    {
        byte[] buffers = hssf.SaveToStream().GetBuffer();
        ExportToWebExcel(buffers, fileName);
    }

    /// <summary>
    /// 本地存储到excel
    /// </summary>
    /// <param name="hssf">已经被处理好的HSSFWorkbook对象</param>
    /// <param name="fileName">文件名称，请自己包含路径，例如C:\\test.xls</param>
    public static void ExportToLocal(this HSSFWorkbook hssf, string fileName)
    {
        byte[] buffers = hssf.SaveToStream().GetBuffer();
        ExportToLocalExcel(buffers, fileName);
    }

    /// <summary>
    ///  本地存储到excel
    /// </summary>
    /// <param name="buffers">文件二进制流</param>
    /// <param name="fileName">文件目录例如C:\\test.xls</param>
    public static void ExportToLocalExcel(byte[] buffers, string fileName)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
            fs.Write(buffers, 0, buffers.Length);
            fs.Flush();
        }
    }

    /// <summary>
    /// web导出excel
    /// </summary>
    /// <param name="buffers">文件二进制流</param>
    /// <param name="fileName">文件名称</param>
    public static void ExportToWebExcel(byte[] buffers, string fileName)
    {
        if (HttpContext.Current.Request.Browser.Type.IndexOf("IE") > -1)
        {
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" +
                HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
        }
        else
        {
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename*=utf-8'zh_cn'{0}", HttpUtility.UrlEncode(fileName)));
        }
        HttpContext.Current.Response.Charset = "gb2312";
        HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");

        HttpContext.Current.Response.Clear();

        HttpContext.Current.Response.BinaryWrite(buffers);
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("zh-CN", true);
        HttpContext.Current.Response.End();
    }


}

#region Excel属性类
/// <summary>
/// 用于定义导出的excel属性
/// </summary>
public class ExcelProperty
{
    public ExcelProperty() { }
    /// <summary>
    /// 文件基本属性
    /// </summary>
    /// <param name="company">公司名称 默认AaronYang</param>
    /// <param name="author">作者信息，默认 杨洋</param>
    /// <param name="ApplicationName">创建程序信息</param>
    /// <param name="LastAuthor">xls文件最后保存者信息</param>
    /// <param name="Comments">填加xls文件作者信息，备注</param>
    /// <param name="title">填加xls文件标题信息</param>
    /// <param name="Subject">填加文件主题信息</param>
    /// <param name="keyWord">关键词</param>
    /// <param name="catagory">类别</param>
    /// <param name="manager">经理</param>
    public ExcelProperty(string company, string author, string applicationName, string lastAuthor, string comments, string title, string subject, string keyWord, string catagory, string manager)
    {
        this.Company = company;
        this.Author = author;
        this.ApplicationName = applicationName;
        this.LastAuthor = lastAuthor;
        this.Comments = comments;
        this.Title = title;
        this.Subject = subject;
        this.Manager = manager;
        this.KeyWord = keyWord;
        this.Catagory = catagory;
    }
    /// <summary>
    /// 公司名称
    /// </summary>
    private string company = "";
    /// <summary>
    /// 公司名称
    /// </summary>
    public string Company
    {
        get { return company; }
        set { company = value; }
    }
    /// <summary>
    /// 作者信息
    /// </summary>
    private string author = "系统管理员";
    /// <summary>
    /// 作者信息
    /// </summary>
    public string Author
    {
        get { return author; }
        set { author = value; }
    }
    /// <summary>
    /// 创建程序信息
    /// </summary>
    private string applicationName = "";
    /// <summary>
    /// 创建程序信息
    /// </summary>
    public string ApplicationName
    {
        get { return applicationName; }
        set { applicationName = value; }
    }
    /// <summary>
    /// xls文件最后保存者信息
    /// </summary>
    private string lastAuthor = "";
    /// <summary>
    /// xls文件最后保存者信息
    /// </summary>
    public string LastAuthor
    {
        get { return lastAuthor; }
        set { lastAuthor = value; }
    }
    /// <summary>
    ///填加xls文件作者信息，备注
    /// </summary>
    private string comments = "";
    /// <summary>
    ///填加xls文件作者信息，备注
    /// </summary>
    public string Comments
    {
        get { return comments; }
        set { comments = value; }
    }
    /// <summary>
    /// 填加xls文件标题信息
    /// </summary>
    private string title = "";
    /// <summary>
    /// 填加xls文件标题信息
    /// </summary>
    public string Title
    {
        get { return title; }
        set { title = value; }
    }
    /// <summary>
    /// 填加文件主题信息
    /// </summary>
    private string subject = "";
    /// <summary>
    /// 填加文件主题信息
    /// </summary>
    public string Subject
    {
        get { return subject; }
        set { subject = value; }
    }
    /// <summary>
    /// 关键字
    /// </summary>
    private string keyWord = "";
    /// <summary>
    /// 关键字
    /// </summary>
    public string KeyWord
    {
        get { return keyWord; }
        set { keyWord = value; }
    }
    /// <summary>
    /// 类别
    /// </summary>
    private string catagory = "";
    /// <summary>
    /// 类别
    /// </summary>
    public string Catagory
    {
        get { return catagory; }
        set { catagory = value; }
    }
    /// <summary>
    /// 经理
    /// </summary>
    private string manager = "";
    /// <summary>
    /// 经理
    /// </summary>
    public string Manager
    {
        get { return manager; }
        set { manager = value; }
    }

}
#endregion

#region 定义Json表头的格式
/// <summary>
/// 关于表头单元格设置属性：字体默认：黑体，字体大小默认12
/// </summary>
internal class AttributeList
{
    /// <summary>
    /// 显示的文字
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 显示方式
    /// </summary>
    public string align { get; set; }
    /// <summary>
    /// 垂直显示方式
    /// </summary>
    public string valign { get; set; }
    /// <summary>
    /// 背景颜色.例如#000000
    /// </summary>
    public string bgcolor { get; set; }
    /// <summary>
    /// 字体大小
    /// </summary>
    public short? fontsize { get; set; }
    /// <summary>
    /// 字体颜色,例如#000000
    /// </summary>
    public string fontcolor { get; set; }
    /// <summary>
    /// 单元格合并位置，（fromRow,toRow,fromColumn,toColumn）
    /// </summary>
    public string cellregion { get; set; }
    /// <summary>
    /// 字体名称
    /// </summary>
    public string fontName { get; set; }
    /// <summary>
    /// 表头文字是否加粗，默认加粗
    /// </summary>
    public string fontweight { get; set; }
    /// <summary>
    /// 宽度
    /// </summary>
    public int? width { get; set; }
    /// <summary>
    /// 高度
    /// </summary>
    public int? height { get; set; }

    /// <summary>
    ///是否是斜体
    /// </summary>
    public bool? IsItalic { get; set; }
    /// <summary>
    /// 是否有中间线
    /// </summary>
    public bool? IsStrikeout { get; set; }
    /// <summary>
    /// 设置下划线
    /// </summary>
    public bool? Underline { get; set; }

}

/// <summary>
/// 合并组，暂时支持一列
/// </summary>
public class GroupClass
{
    /// <summary>
    /// 从哪一行开始
    /// </summary>
    public int? index { get; set; }
    /// <summary>
    /// 分组后每组中多少个值
    /// </summary>
    public int? groupCount { get; set; }
    /// <summary>
    /// 要合并的那一列的索引
    ///  </summary>
    public int column { get; set; }

}
/// <summary>
/// 报表表格头部信息
/// </summary>
internal class HeadInfo
{
    public IList<AttributeList> head { get; set; }
    public int? rowspan { get; set; }
    public string sheetname { get; set; }
    /// <summary>
    /// 默认单元格宽度
    /// </summary>
    public int? defaultwidth { get; set; }
    /// <summary>
    /// 默认行高度
    /// </summary>
    public int? defaultheight { get; set; }
    /// <summary>
    /// 默认黑色,表格边框颜色
    /// </summary>
    public string bordercolor { get; set; }
    /// <summary>
    /// 边框风格，默认 thin
    /// </summary>
    public string borderstyle { get; set; }
}
/// <summary>
/// 根节点
/// </summary>
internal class Root
{
    public HeadInfo root { get; set; }
}

#endregion

/// <summary>
/// excel 构建
/// 仿照AutoMaper 调用方式
/// 调用方式
/// new ExportBuilder<CustomerInsureModel>()
///         .Column(c => c.InsureNum)
///         .Column(c => c.Applicant)
///         .Column(c => c.Insurant)
///         .Column(c => c.CompanyName)
///         .Column(c => c.ProdName)
///         .Column(c => c.InsureTime, c => c.InsureTime.Value.ToString("yyyy-MM-dd HH:mm"))
///         .Column(c => c.IsMergePay, c => c.IsMergePay ? c.OrderNum : string.Empty)
///         .Column(c => c.BuySinglePrice, c => c.BuySinglePrice.Value.ToString("0.00"))
///         .Column(c => c.OnlinePaymnet, c => c.OnlinePaymnet.GetDescription())
///         .Export(vdata.ToList(), "用户投保信息.xls");
/// </summary>
/// <typeparam name="T"></typeparam>
public class ExportBuilder<T>
{
    List<string> titles = new List<string>();
    List<Func<T, string>> funcs = new List<Func<T, string>>();
    private ExcelHelper excel;
    internal ExcelHelper Excel
    {
        get
        {
            if (excel == null)
            {
                excel = new ExcelHelper();
            }
            return excel;
        }
        set { excel = value; }
    }

    /// <summary>
    /// 定义列
    /// 2020-08-15 支持 .Column(m => m.Platform01.ToString()) 写法
    /// </summary>
    /// <param name="member">解析Display特性得到列名，如不存在，则使用列名</param>
    /// <returns></returns>
    public ExportBuilder<T> Column(Expression<Func<T, string>> member)
    {
        //var memberParam = member.Body as MemberExpression;
        titles.Add(GetDisplayName(member.Body));
        var convert = member.Compile();
        funcs.Add(convert);
        return this;
    }

    /// <summary>
    /// 定义列
    /// </summary>
    /// <param name="member"></param>
    /// <param name="title">列名</param>
    /// <returns></returns>
    public ExportBuilder<T> Column(Expression<Func<T, string>> member, string title)
    {
        var memberParam = member.Body as MemberExpression;
        titles.Add(title);
        var convert = member.Compile();
        funcs.Add(convert);
        return this;
    }

    /// <summary>
    /// 定义列 
    /// </summary>
    /// <param name="member">解析Display特性得到列名，如不存在，则使用列名</param>
    /// <param name="convert">定义数据输出格式</param>
    /// <returns></returns>
    public ExportBuilder<T> Column(Expression<Func<T, object>> member, Func<T, string> convert)
    {
        //var memberParam = member.Body as MemberExpression;
        titles.Add(GetDisplayName(member.Body));
        funcs.Add(convert);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="member"></param>
    /// <param name="title">列名</param>
    /// <param name="convert">定义数据输出格式</param>
    /// <returns></returns>
    public ExportBuilder<T> Column(Expression<Func<T, object>> member, string title, Func<T, string> convert)
    {
        var memberParam = member.Body as MemberExpression;
        titles.Add(title);
        funcs.Add(convert);
        return this;
    }

    /// <summary>
    /// 编辑已经添加的转换
    /// </summary>
    /// <param name="name"></param>
    /// <param name="convert"></param>
    /// <returns></returns>
    public ExportBuilder<T> Edit(string name, Func<T, string> convert)
    {
        var index = titles.FindIndex(m => m == name);
        if (index > -1)
        {
            funcs[index] = convert;
        }
        return this;
    }

    /// <summary>
    /// 设置Excel属性
    /// </summary>
    /// <param name="ext"></param>
    /// <returns></returns>
    public ExportBuilder<T> SetExcelProperty(ExcelProperty ext)
    {
        Excel.SetWorkbook(ext);
        return this;
    }

    /// <summary>
    /// 导出WEB
    /// </summary>
    /// <param name="list">数据源</param>
    /// <param name="fileName">文件名</param>
    /// <param name="headJosn">表头JSON</param>
    public void Export(List<T> list, string fileName, string headJson)
    {
        Excel.ExportToWeb<T>(list, fileName, titles.ToArray(), headJson, funcs.ToArray());
    }
    /// <summary>
    /// 导出WEB，含分组
    /// </summary>
    /// <param name="list">数据源（分组后的）</param>
    /// <param name="fileName">文件名</param>
    /// <param name="headJosn">表头JSON</param>
    /// <param name="group">分组集合</param>
    /// <param name="groupColumn">分组所在的列，索引从0开始</param>
    public void Export(List<T> list, string fileName, string headJson, List<GroupClass> group, int groupColumn)
    {
        Excel.ExportToWeb<T>(list, fileName, titles.ToArray(), headJson, group, groupColumn, funcs.ToArray());
    }
    /// <summary>
    /// 导出到本地存储
    /// </summary>
    /// <param name="list">数据源</param>
    /// <param name="fileName">文件名</param>
    /// <param name="headJson">表头JSON</param>
    public void ExportToLocal(List<T> list, string fileName, string headJson)
    {
        Excel.ExportToLocal<T>(list, fileName, titles.ToArray(), headJson, funcs.ToArray());
    }
    /// <summary>
    /// 导出到本地存储，含分组
    /// </summary>
    /// <param name="list">数据源（分组后的）</param>
    /// <param name="fileName">文件名</param>
    /// <param name="headJson">表头JSON</param>
    /// <param name="group">分组集合</param>
    /// <param name="groupColumn">分组所在的列，索引从0开始</param>
    public void ExportToLocal(List<T> list, string fileName, string headJson, List<GroupClass> group, int groupColumn)
    {
        Excel.ExportToLocal<T>(list, fileName, titles.ToArray(), headJson, group, groupColumn, funcs.ToArray());
    }

    /// <summary>
    /// [Display(Name = "")]
    /// 获得类属性中标记的名称
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public string GetDisplayName(Expression expr)
    {
        var memberParam = expr as MemberExpression;
        if (memberParam != null)
        {
            return GetDisplayName(memberParam);
        }
        var unary = expr as UnaryExpression;
        if (unary != null)
        {
            return GetDisplayName(unary.Operand as MemberExpression);
        }
        var call = expr as MethodCallExpression;
        if (call != null)
        {
            return GetDisplayName(call.Object as MemberExpression);
        }

        return string.Empty;

    }

    /// <summary>
    /// [Display(Name = "记住帐号")]
    /// 获得类属性中标记的中文名
    /// </summary>
    /// <param name="memberParam"></param>
    /// <returns></returns>
    private string GetDisplayName(MemberExpression memberParam)
    {
        var name = memberParam.Member.Name;
        var property = memberParam.Member.ReflectedType.GetProperty(name);
        var displays = property.GetCustomAttributes(typeof(DisplayAttribute), false);
        if (displays == null || displays.Length == 0)
            return property.Name;
        else
            return (displays[0] as DisplayAttribute).Name;
    }

}