using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Hosting;
using System.Web.UI.HtmlControls;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Text;

public partial class admin_ImportZKZ : System.Web.UI.Page
{
    public string ErrMessage = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断是否为管理员权限
        string _Role = Session["Role2012_admin"].ToString();
        if (string.IsNullOrWhiteSpace(_Role))
        {
            Response.Write("<script>alert('需要重新登录！');location='../default.aspx'</script>");
            Response.End();
        }
        if (_Role == "0")
        {
            Response.Write("<script>alert('对不起，你无权访问此页面！');location='../Default.aspx'</script>");
            Response.End();
        }

    }
    /// <summary>
    /// 清空旧数据后导入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImportRefresh_Click(object sender, EventArgs e)
    {
        if (!fuExcel.HasFile)
        {
            ErrMessage = "请选择要导入的文件。";
            return;
        }
        if (fuExcel.PostedFile.ContentLength > 3145728)
        {
            ErrMessage = "导入的文件过大，请将文件控制在3MB以内。";
            return;
        }
        if (Path.GetExtension(fuExcel.PostedFile.FileName).ToUpper() != ".XLS")
        {
            ErrMessage = "导入的文件不是Excel文件。";
            return;
        }
        string _message = string.Empty;
        if (_导入(true, ref _message))
        {
            ErrMessage = "导入成功。";
        }
        else
        {
            ErrMessage = "导入结束：" + _message;
        }
    }
    /// <summary>
    /// 更新导入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImportOverride_Click(object sender, EventArgs e)
    {
        if (!fuExcel.HasFile)
        {
            ErrMessage = "请选择要导入的文件。";
            return;
        }
        if (fuExcel.PostedFile.ContentLength > 3145728)
        {
            ErrMessage = "导入的文件过大，请将文件控制在3MB以内。";
            return;
        }
        if (Path.GetExtension(fuExcel.PostedFile.FileName).ToUpper() != ".XLS")
        {
            ErrMessage = "导入的文件不是Excel文件。";
            return;
        }
        string _message = string.Empty;
        if (_导入(false, ref _message))
        {
            ErrMessage = "导入成功。";
        }
        else
        {
            ErrMessage = "导入结束：" + _message;
        }
    }

    protected class _列头Info
    {
        public _列头Info(string name, int r1, int r2, int c1, int c2)
        {
            Name = name;
            Row1 = r1;
            Row2 = r2;
            Col1 = c1;
            Col2 = c2;
        }
        public string Name { get; set; }
        public int Row1 { get; set; }
        public int Row2 { get; set; }

        public int Col1 { get; set; }
        public int Col2 { get; set; }
    }
    protected List<_列头Info> _列头 = new List<_列头Info>()
        {
            new _列头Info("姓名",0,1,0,0),
            new _列头Info("性别",0,1,1,1),
            new _列头Info("身份证号",0,1,2,2),
            new _列头Info("准考证号",0,1,3,3),
            new _列头Info("公共科目考试",0,1,4,4),
            new _列头Info("试场号",0,1,5,5),
            new _列头Info("座位号",0,1,6,6),
            new _列头Info("考试日期",0,1,7,7),
            new _列头Info("考试时间",0,1,8,8),
            new _列头Info("公共科目考试考点",0,1,9,9),
            new _列头Info("学科专业知识考试",0,1,10,10),
            new _列头Info("试场号",0,1,11,11),
            new _列头Info("座位号",0,1,12,12),
            new _列头Info("考试日期",0,1,13,13),
            new _列头Info("考试时间",0,1,14,14),
            new _列头Info("学科专业知识考试考点",0,1,15,15),
        };
    /// <summary>
    /// 数据导入
    /// </summary>
    /// <param name="isRefresh">是否为清空导入</param>
    /// <param name="Message"></param>
    /// <returns></returns>
    protected bool _导入(bool isRefresh, ref string Message)
    {
        IWorkbook _workbook = new HSSFWorkbook(fuExcel.PostedFile.InputStream);
        if (_workbook == null || _workbook.NumberOfSheets == 0)
        {
            Message = "导入的Excel文件没有内容或非Excel文件。";
            return false;
        }
        ISheet _sheet = _workbook.GetSheetAt(0);
        if (_sheet.LastRowNum < 1)
        {
            Message = "导入的Excel文件没有内容。";
            return false;
        }

        #region 列头判断
        //IRow _列头_R = _sheet.GetRow(0);
        foreach (_列头Info item in _列头)
        {
            IRow _tempR = _sheet.GetRow(item.Row1);
            ICell _tempC = _tempR.Cells[item.Col1];
            string name = "";
            if (_tempC.StringCellValue != null)
                name = _tempC.StringCellValue.Replace("\n", "");
            if (name != item.Name)
            {
                Message = "列头错误。请使用本系统提供的模板。";
                return false;
            }
        }
        #endregion
        #region 数据处理
        bool _isAllOK = true;
        using (var db = new teacherBaoMing_Entities())
        {
            StringBuilder _messageSB = new StringBuilder();
            List<tb_zunkaozheng> _zhunKaoZhengList = new List<tb_zunkaozheng>();
            _isAllOK = true;
            for (int i = 1; i <= _sheet.LastRowNum; i++)
            {
                string _ErrMesg = string.Empty;
                IRow _rowData = _sheet.GetRow(i);
                if (_rowData == null || !_rowData.Cells.Any(t => !string.IsNullOrWhiteSpace(t.ToString())))
                {
                    continue;
                }
                if (_rowData.Cells.Count < 11)
                {
                    continue;
                }
                #region 获取数据
                string _姓名 = _rowData.GetCell(0).ToString().Trim();
                string _性别 = _rowData.GetCell(1).ToString().Trim();
                string _身份证号 = _rowData.GetCell(2).ToString().Trim();
                string _准考证号 = _rowData.GetCell(3).ToString().Trim();
                string _公共科目 = _rowData.GetCell(4).ToString().Trim();
                string _试场号 = _rowData.GetCell(5).ToString().Trim();
                string _座位号 = _rowData.GetCell(6).ToString().Trim();
                string _考试日期 = _rowData.GetCell(7).ToString().Trim();
                string _考试时间 = _rowData.GetCell(8).ToString().Trim();
                string _考点 = _rowData.GetCell(9).ToString().Trim();
                string _学科科目 = _rowData.GetCell(10).ToString().Trim();
                string _试场号1 = _rowData.GetCell(11).ToString().Trim();
                string _座位号1 = _rowData.GetCell(12).ToString().Trim();
                string _考试日期1 = _rowData.GetCell(13).ToString().Trim();
                string _考试时间1 = _rowData.GetCell(14).ToString().Trim();
                string _考点1 = _rowData.GetCell(15).ToString().Trim();
                #endregion
                #endregion
                #region 检查数据
                if (string.IsNullOrWhiteSpace(_姓名))
                {
                    _ErrMesg += "姓名不能为空；";
                    _isAllOK = false;
                }
                if (string.IsNullOrWhiteSpace(_性别))
                {
                    _ErrMesg += "性别不能为空；";
                    _isAllOK = false;
                }
                if (string.IsNullOrWhiteSpace(_准考证号))
                {
                    _ErrMesg += "准考证号不能为空；";
                    _isAllOK = false;
                }
                if (string.IsNullOrWhiteSpace(_试场号) || string.IsNullOrWhiteSpace(_试场号1))
                {
                    _ErrMesg += "试场号不能为空；";
                    _isAllOK = false;
                }
                if (string.IsNullOrWhiteSpace(_座位号) || string.IsNullOrWhiteSpace(_座位号1))
                {
                    _ErrMesg += "座位号不能为空；";
                    _isAllOK = false;
                }
                if (string.IsNullOrWhiteSpace(_身份证号))
                {
                    _ErrMesg += "身份证号不能为空；";
                    _isAllOK = false;
                }
                if (string.IsNullOrWhiteSpace(_公共科目))
                {
                    _ErrMesg += "公共科目不能为空；";
                    _isAllOK = false;
                }
                if (string.IsNullOrWhiteSpace(_学科科目))
                {
                    _ErrMesg += "学科科目不能为空；";
                    _isAllOK = false;
                }
                if (string.IsNullOrWhiteSpace(_考试时间) || string.IsNullOrWhiteSpace(_考试时间1))
                {
                    _ErrMesg += "考试时间不能为空；";
                    _isAllOK = false;
                }
                if (string.IsNullOrWhiteSpace(_考点) || string.IsNullOrWhiteSpace(_考点1))
                {
                    _ErrMesg += "考点不能为空；";
                    _isAllOK = false;
                }
                if (string.IsNullOrWhiteSpace(_考试日期) || string.IsNullOrWhiteSpace(_考试日期1))
                {
                    _ErrMesg += "考试日期不能为空；";
                    _isAllOK = false;
                }

                #endregion

                #region 检查是否存在User信息
                var lst = db.Set<tb_userinfo>().Include("tb_admin");
                var _tempUser = lst.Where(t => t.UserName == _身份证号).FirstOrDefault();
                if (_tempUser == null)
                {
                    _ErrMesg += "未找到对应的数据，可能身份证号错误；";
                    _isAllOK = false;
                    _ErrMesg.TrimEnd('；');
                    _messageSB.AppendLine("第" + (i + 1) + "行：" + _ErrMesg + "。");
                    continue;
                }
                //else if (_tempUser.XingMing != _姓名 || _tempUser.Sex != _性别)
                //{
                //    _ErrMesg += "指定的人员信息不匹配，可能姓名或性别错误；";
                //    _isAllOK = false;
                //    _ErrMesg.TrimEnd('；');
                //    _messageSB.AppendLine("第" + (i + 1) + "行：" + _ErrMesg + "。");
                //    continue;
                //}
                #endregion

                #region 保存至数据库
                if (_isAllOK)
                {
                    if (isRefresh)//清空再更新
                    {
                        tb_zunkaozheng zhunKaoZhengEntnty = new tb_zunkaozheng()
                        {
                            UserName = _身份证号,
                            zkzCode = _准考证号,
                            publicSubject = _公共科目,
                            shiChangCode = _试场号,
                            zuoweiCode = _座位号,
                            kaoShiDate = _考试日期,
                            kaoShiTime = _考试时间,
                            kaoDian = _考点,
                            privateSubject = _学科科目,
                            shiChangCode1 = _试场号1,
                            zuoweiCode1 = _座位号1,
                            kaoShiDate1 = _考试日期1,
                            kaoShiTime1 = _考试时间1,
                            kaoDian1 = _考点1
                        };
                        _zhunKaoZhengList.Add(zhunKaoZhengEntnty);
                    }
                    else//覆盖
                    {
                        var zhunKaoZhengEntnty = db.Set<tb_zunkaozheng>().FirstOrDefault(t => t.UserName == _身份证号);
                        if (zhunKaoZhengEntnty != null)
                        {
                            if (db.Set<tb_zunkaozheng>().Any(t => t.zkzCode == _准考证号 && t.UserName != _身份证号))
                            {
                                _ErrMesg += "准考证号已存在；";
                                _isAllOK = false;
                            }
                            //zhunKaoZhengEntnty.zkzCode = _准考证号;
                            zhunKaoZhengEntnty.publicSubject = _公共科目;
                            zhunKaoZhengEntnty.shiChangCode = _试场号;
                            zhunKaoZhengEntnty.zuoweiCode = _座位号;
                            zhunKaoZhengEntnty.kaoShiTime = _考试时间;
                            zhunKaoZhengEntnty.kaoShiDate = _考试日期;
                            zhunKaoZhengEntnty.kaoDian = _考点;
                            zhunKaoZhengEntnty.privateSubject = _学科科目;
                            zhunKaoZhengEntnty.shiChangCode1 = _试场号1;
                            zhunKaoZhengEntnty.zuoweiCode1 = _座位号1;
                            zhunKaoZhengEntnty.kaoShiTime1 = _考试时间1;
                            zhunKaoZhengEntnty.kaoShiDate1 = _考试日期1;
                            zhunKaoZhengEntnty.kaoDian1 = _考点1;
                        }
                        else
                        {
                            if (db.Set<tb_zunkaozheng>().Any(t => t.zkzCode == _准考证号))
                            {
                                _ErrMesg += "准考证号已存在；";
                                _isAllOK = false;
                            }
                            zhunKaoZhengEntnty = new tb_zunkaozheng()
                            {
                                UserName = _身份证号,
                                zkzCode = _准考证号,
                                publicSubject = _公共科目,
                                shiChangCode = _试场号,
                                zuoweiCode = _座位号,
                                kaoShiDate = _考试日期,
                                kaoShiTime = _考试时间,
                                kaoDian = _考点,
                                privateSubject = _学科科目,
                                shiChangCode1 = _试场号1,
                                zuoweiCode1 = _座位号1,
                                kaoShiDate1 = _考试日期1,
                                kaoShiTime1 = _考试时间1,
                                kaoDian1 = _考点1
                            };
                            db.Set<tb_zunkaozheng>().Add(zhunKaoZhengEntnty);
                        }
                        _zhunKaoZhengList.Add(zhunKaoZhengEntnty);
                    }
                }
                else
                {
                }
                #endregion
                if (!_isAllOK && !string.IsNullOrWhiteSpace(_ErrMesg))
                {
                    _ErrMesg.TrimEnd('；');
                    _messageSB.AppendLine("第" + (i + 1) + "行：" + _ErrMesg + "。");
                }
            }
            if (_isAllOK)
            {
                var userNameList = _zhunKaoZhengList.Select(t => t.UserName);
                var zkzCodeList = _zhunKaoZhengList.Select(t => t.zkzCode);
                if (userNameList.Distinct().Count() != _zhunKaoZhengList.Count())
                {
                    _isAllOK = false;
                    Message = "Excel文件存在重复的身份证号，请检查文档内容！";
                    return false;
                }
                if (zkzCodeList.Distinct().Count() != _zhunKaoZhengList.Count())
                {
                    _isAllOK = false;
                    Message = "Excel文件存在重复的准考证号，请检查文档内容！";
                    return false;
                }
                if (isRefresh)
                {
                    if (_zhunKaoZhengList.Count == 0 && _messageSB.Length == 0)
                    {
                        _isAllOK = false;
                        Message = "Excel文件未能读取出数据，请检查文档内容。如果Excel拥有内容，请尝试先清除所有的边框后，再次上传。";
                        return false;
                    }
                    else
                    {
                        var zKZList_ForRefresh = db.Set<tb_zunkaozheng>();
                        if (zKZList_ForRefresh != null)//清空
                        {
                            db.Database.ExecuteSqlCommand("delete tb_zunkaozheng");
                        }
                        if (_zhunKaoZhengList != null)
                        {
                            foreach (var item in _zhunKaoZhengList)
                            {
                                db.tb_zunkaozheng.Add(item);
                            }
                        }
                        //db.DeleteObject(zKZList_ForRefresh);
                        //db.AddObject("tb_zunkaozheng", _zhunKaoZhengList);
                    }
                }
                try
                {
                    _isAllOK = db.SaveChanges() >= 0;
                }
                catch (Exception ex)
                {
                    _isAllOK = false;
                    Message = ex.ToString();
                }
            }
            else
            {

                Message = _messageSB.ToString();
            }
        }
        return _isAllOK;
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string _path = Server.MapPath("/准考证信息导入模版.xls");
        if (!File.Exists(_path))
        {
            ErrMessage = "内部错误：模板文件“准考证信息导入模版.xls”不存在";
            return;
        }
        Stream _fs = new FileStream(_path, FileMode.Open); //读入Excel模板
        IWorkbook workbook = new HSSFWorkbook(_fs);//创建Workbook对象
        string _name = HttpUtility.UrlEncode("准考证信息导入模版.xls", System.Text.Encoding.UTF8).Replace("+", "%20");
        Response.Clear();
        workbook.Write(Response.OutputStream);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + _name);
        Response.Flush();
    }

}