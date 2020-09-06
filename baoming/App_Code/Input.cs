using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Hosting;
using System.Security.Cryptography;

/// <summary>
/// 外界输入处理类
/// </summary>
public class Input
{
    /// <summary>
    /// 检测是否整数型数据
    /// </summary>
    /// <param name="Num">待检查数据</param>
    /// <returns></returns>
    public static bool IsInteger(string Input)
    {
        if (Input == null)
        {
            return false;
        }
        else
        {
            return IsInteger(Input, true);
        }
    }
    /// <summary>
    /// 合并路径字符串
    /// </summary>
    /// <param name="paramesters"></param>
    /// <returns></returns>
    public static string Combine(params  string[] paramesters)
    {
        string result = string.Empty;
        if (paramesters.Length == 0)
            return string.Empty;
        result = paramesters[0];
        for (int i = 0; i < paramesters.Length - 1; i++)
        {
            string temp = paramesters[i + 1];
            if (!string.IsNullOrEmpty(temp) && (temp[0] == '/' || temp[0] == '\\'))
                temp = temp.Substring(1, temp.Length - 1);
            result += System.IO.Path.Combine(result[i].ToString(), temp);
        }
        return result;
    }
    /// <summary>
    /// 是否全是正整数
    /// </summary>
    /// <param name="Input"></param>
    /// <returns></returns>
    public static bool IsInteger(string Input, bool Plus)
    {
        if (Input == null)
        {
            return false;
        }
        else
        {
            string pattern = "^-?[0-9]+$";
            if (Plus)
                pattern = "^[0-9]+$";
            if (Regex.Match(Input, pattern, RegexOptions.Compiled).Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 判断输入是否为日期类型
    /// </summary>
    /// <param name="s">待检查数据</param>
    /// <returns></returns>
    public static bool IsDate(string s)
    {
        try
        {
            DateTime d = DateTime.Parse(s);
            return true;
        }
        catch
        {
            return false;
        }
    }


    /// <summary>
    /// 过滤字符串中的html代码
    /// </summary>
    /// <param name="Str"></param>
    /// <returns>返回过滤之后的字符串</returns>
    public static string LostHTML(string Str)
    {
        string Re_Str = "";
        if (Str != null)
        {
            if (Str != string.Empty)
            {
                string Pattern = "<\\/*[^<>]*>";
                Re_Str = Regex.Replace(Str, Pattern, "");
            }
        }
        return (Re_Str.Replace("\\r\\n", "")).Replace("\\r", "");
    }

    public static string LostPage(string Str)
    {
        string Re_Str = "";
        if (Str != null)
        {
            if (Str != string.Empty)
            {
                string Pattern = "\\[AK:PAGE\\/*[^<>]*\\$\\]";
                Re_Str = Regex.Replace(Str, Pattern, "");
            }
        }
        return Re_Str;
    }

    public static string LostVoteStr(string Str)
    {
        string Re_Str = "";
        if (Str != null)
        {
            if (Str != string.Empty)
            {
                string Pattern = "\\[AK:unLoop\\/*[^<>]*\\[\\/AK:unLoop\\]";
                Re_Str = Regex.Replace(Str, Pattern, "");
            }
        }
        return Re_Str;
    }

    /// <summary>
    /// 截取字符串函数
    /// </summary>
    /// <param name="Str">所要截取的字符串</param>
    /// <param name="Num">截取字符串的长度</param>
    /// <param name="Num">截取字符串后省略部分的字符串</param>
    /// <returns></returns>
    public static string GetSubString(string Str, int Num, string LastStr)
    {
        return (Str.Length > Num) ? Str.Substring(0, Num) + LastStr : Str;
    }

    /// <summary>
    /// 验证字符串是否是图片路径
    /// </summary>
    /// <param name="Input">待检测的字符串</param>
    /// <returns>返回true 或 false</returns>
    public static bool IsImgString(string Input)
    {
        return IsImgString(Input, "/{@dirfile}/");
    }

    public static bool IsImgString(string Input, string checkStr)
    {
        bool re_Val = false;
        if (Input != string.Empty)
        {
            string s_input = Input.ToLower();
            if (s_input.IndexOf(checkStr.ToLower()) != -1 && s_input.IndexOf(".") != -1)
            {
                string Ex_Name = s_input.Substring(s_input.LastIndexOf(".") + 1).ToString().ToLower();
                if (Ex_Name == "jpg" || Ex_Name == "gif" || Ex_Name == "bmp" || Ex_Name == "png")
                {
                    re_Val = true;
                }
            }
        }
        return re_Val;
    }


    /// <summary>
    ///  将字符转化为HTML编码
    /// </summary>
    /// <param name="str">待处理的字符串</param>
    /// <returns></returns>
    public static string HtmlEncode(string Input)
    {
        return HttpContext.Current.Server.HtmlEncode(Input);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Input"></param>
    /// <returns></returns>
    public static string HtmlDecode(string Input)
    {
        return HttpContext.Current.Server.HtmlDecode(Input);
    }

    /// <summary>
    /// URL地址编码
    /// </summary>
    /// <param name="Input"></param>
    /// <returns></returns>
    public static string URLEncode(string Input)
    {
        return HttpContext.Current.Server.UrlEncode(Input);
    }

    /// <summary>
    /// URL地址解码
    /// </summary>
    /// <param name="Input"></param>
    /// <returns></returns>
    public static string URLDecode(string Input)
    {
        return HttpContext.Current.Server.UrlDecode(Input);
    }
    /// <summary>
    /// 过滤字符
    /// </summary>
    /// <param name="Input"></param>
    /// <returns></returns>
    public static string Filter(string sInput)
    {
        if (sInput == null || sInput == "")
            return null;
        string sInput1 = sInput.ToLower();
        string output = sInput;
        string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
        if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
        {
            throw new Exception("字符串中含有非法字符!");
        }
        else
        {
            output = output.Replace("'", "''");
        }
        return output;
    }

    /// <summary>
    /// 过滤特殊字符/前台会员
    /// </summary>
    /// <param name="Input"></param>
    /// <returns></returns>
    public static string Htmls(string Input)
    {
        if (Input != string.Empty && Input != null)
        {
            string ihtml = Input.ToLower();
            ihtml = ihtml.Replace("<script", "&lt;script");
            ihtml = ihtml.Replace("script>", "script&gt;");
            ihtml = ihtml.Replace("<%", "&lt;%");
            ihtml = ihtml.Replace("%>", "%&gt;");
            ihtml = ihtml.Replace("<$", "&lt;$");
            ihtml = ihtml.Replace("$>", "$&gt;");
            return ihtml;
        }
        else
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 字符串字符处理
    /// </summary>
    /// <param name="chr">等待处理的字符串</param>
    /// <returns>处理后的字符串</returns>
    /// //把HTML代码转换成TXT格式
    public static String ToTxt(String Input)
    {
        StringBuilder sb = new StringBuilder(Input);
        sb.Replace("&nbsp;", " ");
        sb.Replace("<br>", "\r\n");
        sb.Replace("<br>", "\n");
        sb.Replace("<br />", "\n");
        sb.Replace("<br />", "\r\n");
        sb.Replace("&lt;", "<");
        sb.Replace("&gt;", ">");
        sb.Replace("&amp;", "&");
        return sb.ToString();
    }

    /// <summary>
    /// 字符串字符处理
    /// </summary>
    /// <param name="chr">等待处理的字符串</param>
    /// <returns>处理后的字符串</returns>
    /// //把HTML代码转换成TXT格式
    public static String ToshowTxt(String Input)
    {
        StringBuilder sb = new StringBuilder(Input);
        sb.Replace("&lt;", "<");
        sb.Replace("&gt;", ">");
        return sb.ToString();
    }

    /// <summary>
    /// 把字符转化为文本格式
    /// </summary>
    /// <param name="Input"></param>
    /// <returns></returns>
    public static string ForTXT(string Input)
    {
        StringBuilder sb = new StringBuilder(Input);
        sb.Replace("<font", " ");
        sb.Replace("<span", " ");
        sb.Replace("<style", " ");
        sb.Replace("<div", " ");
        sb.Replace("<p", "");
        sb.Replace("</p>", "");
        sb.Replace("<label", " ");
        sb.Replace("&nbsp;", " ");
        sb.Replace("<br>", "");
        sb.Replace("<br />", "");
        sb.Replace("<br />", "");
        sb.Replace("&lt;", "");
        sb.Replace("&gt;", "");
        sb.Replace("&amp;", "");
        sb.Replace("<", "");
        sb.Replace(">", "");
        return sb.ToString();
    }
    /// <summary>
    /// 字符串字符处理
    /// </summary>
    /// <param name="chr">等待处理的字符串</param>
    /// <returns>处理后的字符串</returns>
    /// //把TXT代码转换成HTML格式
    public static String ToHtml(string Input)
    {
        StringBuilder sb = new StringBuilder(Input);
        sb.Replace("&", "&amp;");
        sb.Replace("<", "&lt;");
        sb.Replace(">", "&gt;");
        sb.Replace("\r\n", "<br />");
        sb.Replace("\n", "<br />");
        sb.Replace("\t", " ");
        //sb.Replace(" ", "&nbsp;");
        return sb.ToString();
    }

    /// <summary>
    /// MD5加密字符串处理
    /// </summary>
    /// <param name="Half">加密是16位还是32位；如果为true为16位</param>
    /// <param name="Input">待加密码字符串</param>
    /// <returns></returns>
    public static string MD5(string Input, bool Half)
    {
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(Input));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        if (Half)//16位MD5加密（取32位加密的9~25字符）
        {
            var output = sBuilder.ToString().Substring(8, 16);
            return output;
        }
        else
            return sBuilder.ToString();
    }

    public static string MD5(string Input)
    {
        return MD5(Input, true);
    }

    /// <summary>
    /// 字符串加密  进行位移操作
    /// </summary>
    /// <param name="str">待加密数据</param>
    /// <returns>加密后的数据</returns>
    public static string EncryptString(string Input)
    {
        string _temp = "";
        int _inttemp;
        char[] _chartemp = Input.ToCharArray();
        for (int i = 0; i < _chartemp.Length; i++)
        {
            _inttemp = _chartemp[i] + 1;
            _chartemp[i] = (char)_inttemp;
            _temp += _chartemp[i];
        }
        return _temp;
    }

    /// <summary>
    /// 字符串解密
    /// </summary>
    /// <param name="str">待解密数据</param>
    /// <returns>解密成功后的数据</returns>
    public static string NcyString(string Input)
    {
        string _temp = "";
        int _inttemp;
        char[] _chartemp = Input.ToCharArray();
        for (int i = 0; i < _chartemp.Length; i++)
        {
            _inttemp = _chartemp[i] - 1;
            _chartemp[i] = (char)_inttemp;
            _temp += _chartemp[i];
        }
        return _temp;
    }

    /// <summary>
    /// 检测含中文字符串实际长度
    /// </summary>
    /// <param name="str">待检测的字符串</param>
    /// <returns>返回正整数</returns>
    public static int NumChar(string Input)
    {
        ASCIIEncoding n = new ASCIIEncoding();
        byte[] b = n.GetBytes(Input);
        int l = 0;
        for (int i = 0; i <= b.Length - 1; i++)
        {
            if (b[i] == 63)//判断是否为汉字或全脚符号
            {
                l++;
            }
            l++;
        }
        return l;
    }

    /// <summary>
    /// 检测是否合法日期
    /// </summary>
    /// <param name="str">待检测的字符串</param>
    /// <returns></returns>
    public static bool ChkDate(string Input)
    {
        DateTime t1;
        return DateTime.TryParse(Input, out t1);
    }

    /// <summary>
    /// 转换日期时间函数
    /// </summary>
    /// <returns></returns>        
    public static string ReDateTime()
    {
        return System.DateTime.Now.ToString("yyyyMMdd");
    }

    /// <summary>
    /// 去除字符串最后一个','号
    /// </summary>
    /// <param name="chr">:要做处理的字符串</param>
    /// <returns>返回已处理的字符串</returns>
    /// /// CreateTime:2007-03-26 Code By DengXi
    public static string CutComma(string Input)
    {
        return CutComma(Input, ",");
    }

    public static string CutComma(string Input, string indexStr)
    {
        if (Input.IndexOf(indexStr) >= 0)
            return Input.Remove(Input.LastIndexOf(indexStr));
        else
            return Input;
    }

    public static string checkID(string ID)
    {
        if (ID == null && ID == string.Empty)
            throw new Exception("参数传递错误!<li>参数不能为空</li>");
        else
            ID = Filter(ID);
        return ID;
    }


    /// <summary>
    /// 去除编号字符串中的'-1'
    /// </summary>
    /// <param name="id"></param>
    /// <returns>如果为空则返回'IsNull'</returns>

    public static string Losestr(string id)
    {
        if (id == null || id == "" || id == string.Empty)
            return "IsNull";

        id = id.Replace("'-1',", "");

        if (id == null || id == "" || id == string.Empty)
            return "IsNull";
        else
            return id;
    }

    public static string FilterHTML(string html)
    {
        if (html == null)
            return "";
        System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        html = regex1.Replace(html, ""); //过滤<script></script>标记
        html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
        html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
        html = regex4.Replace(html, ""); //过滤iframe
        html = regex5.Replace(html, ""); //过滤frameset
        html = regex6.Replace(html, ""); //过滤frameset
        html = regex7.Replace(html, ""); //过滤frameset
        html = regex8.Replace(html, ""); //过滤frameset
        html = regex9.Replace(html, "");
        html = html.Replace(" ", "");
        html = html.Replace("</strong>", "");
        html = html.Replace("<strong>", "");
        return html;
    }

    /// <summary>
    /// 转换日期时间,arjun
    /// </summary>
    /// <param name="datestr">一个预设的时间</param>
    /// <param name="str">要替换的字符串</param>
    /// <returns>替换后的字符串</returns>
    public static string replaceDateTimeStr(string datestr, string str)
    {
        DateTime dt = new DateTime();
        dt = DateTime.Parse(datestr);
        string _Str = str;
        string year02 = dt.ToString().PadRight(2);
        string year04 = dt.ToString();
        string month = dt.Month.ToString();
        string day = dt.Day.ToString();
        string hour = dt.Hour.ToString();
        string minute = dt.Minute.ToString();
        string second = dt.Second.ToString();
        _Str = _Str.Replace("{@year02}", year02);
        _Str = _Str.Replace("{@year04}", year04);
        _Str = _Str.Replace("{@month}", month);
        _Str = _Str.Replace("{@day}", day);
        _Str = _Str.Replace("{@hour}", hour);
        _Str = _Str.Replace("{@minute}", minute);
        _Str = _Str.Replace("{@second}", second);
        return _Str;
    }

    /// <summary>
    /// 判断字符创的格式只能为英文字母或数字
    /// </summary>
    /// <param name="Input">待检查的数据</param>
    /// <returns></returns>
    public static bool IsZimuAndShuzi(string Input)
    {
        if (Input == null)
        {
            return false;
        }
        else
        {
            string pattern = "^[A-Za-z0-9]+$";
            if (Regex.Match(Input, pattern).Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    #region 阿拉伯数字转中文大写数字
    /// <summary>
    /// 阿拉伯数字转中文大写数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ConvertSum(string str)
    {
        string _Fu = string.Empty;
        string rstr = string.Empty;
        if (!IsPositveDecimal(str))
        {
            _Fu = "负";
            str = str.TrimStart('-');
        }
        else { }
        if (Double.Parse(str) > 999999999999.99)
            return "数字太大，无法换算，请输入一万亿元以下的金额";
        char[] ch = new char[1];
        ch[0] = '.'; //小数点
        string[] splitstr = null; //定义按小数点分割后的字符串数组
        splitstr = str.Split(ch[0]);//按小数点分割字符串
        if (splitstr.Length == 1) //只有整数部分
            rstr = ConvertData(str) + "圆整";
        else //有小数部分
        {
            rstr = ConvertData(splitstr[0]) + "圆";//转换整数部分
            rstr += ConvertXiaoShu(splitstr[1]);//转换小数部分

        }
        return _Fu + rstr;
    }
    /// 
    /// 判断是否是正数字字符串
    /// 
    /// 判断字符串
    /// 如果是数字，返回true，否则返回false
    public static bool IsPositveDecimal(string str)
    {
        Decimal d;
        try
        {
            d = Decimal.Parse(str);

        }
        catch (Exception)
        {
            return false;
        }
        if (d >= 0)
            return true;
        else
            return false;
    }
    /// 
    /// 转换数字（整数）
    /// 
    /// 需要转换的整数数字字符串
    /// 转换成中文大写后的字符串
    public static string ConvertData(string str)
    {
        string tmpstr = "";
        string rstr = "";
        int strlen = str.Length;
        if (strlen <= 4)//数字长度小于四位
        {
            rstr = ConvertDigit(str);

        }
        else
        {

            if (strlen <= 8)//数字长度大于四位，小于八位
            {
                tmpstr = str.Substring(strlen - 4, 4);//先截取最后四位数字
                rstr = ConvertDigit(tmpstr);//转换最后四位数字
                tmpstr = str.Substring(0, strlen - 4);//截取其余数字
                //将两次转换的数字加上萬后相连接
                rstr = String.Concat(ConvertDigit(tmpstr) + "萬", rstr);
                rstr = rstr.Replace("零萬", "萬");
                rstr = rstr.Replace("零零", "零");

            }
            else
                if (strlen <= 12)//数字长度大于八位，小于十二位
                {
                    tmpstr = str.Substring(strlen - 4, 4);//先截取最后四位数字
                    rstr = ConvertDigit(tmpstr);//转换最后四位数字
                    tmpstr = str.Substring(strlen - 8, 4);//再截取四位数字
                    rstr = String.Concat(ConvertDigit(tmpstr) + "萬", rstr);
                    tmpstr = str.Substring(0, strlen - 8);
                    rstr = String.Concat(ConvertDigit(tmpstr) + "億", rstr);
                    rstr = rstr.Replace("零億", "億");
                    rstr = rstr.Replace("零萬", "零");
                    rstr = rstr.Replace("零零", "零");
                    rstr = rstr.Replace("零零", "零");
                }
        }
        strlen = rstr.Length;
        if (strlen >= 2)
        {
            switch (rstr.Substring(strlen - 2, 2))
            {
                case "佰零": rstr = rstr.Substring(0, strlen - 2) + "佰"; break;
                case "仟零": rstr = rstr.Substring(0, strlen - 2) + "仟"; break;
                case "萬零": rstr = rstr.Substring(0, strlen - 2) + "萬"; break;
                case "億零": rstr = rstr.Substring(0, strlen - 2) + "億"; break;

            }
        }

        return rstr;
    }
    /// 
    /// 转换数字（小数部分）
    /// 
    /// 需要转换的小数部分数字字符串
    /// 转换成中文大写后的字符串
    public static string ConvertXiaoShu(string str)
    {
        int strlen = str.Length;
        string rstr;
        if (strlen == 1)
        {
            rstr = ConvertChinese(str) + "角";
            return rstr;
        }
        else
        {
            string tmpstr = str.Substring(0, 1);
            rstr = ConvertChinese(tmpstr) + "角";
            tmpstr = str.Substring(1, 1);
            rstr += ConvertChinese(tmpstr) + "分";
            rstr = rstr.Replace("零分", "");
            rstr = rstr.Replace("零角", "");
            return rstr;
        }


    }

    /// 
    /// 转换数字
    /// 
    /// 转换的字符串（四位以内）
    /// 
    public static string ConvertDigit(string str)
    {
        int strlen = str.Length;
        string rstr = "";
        switch (strlen)
        {
            case 1: rstr = ConvertChinese(str); break;
            case 2: rstr = Convert2Digit(str); break;
            case 3: rstr = Convert3Digit(str); break;
            case 4: rstr = Convert4Digit(str); break;
        }
        rstr = rstr.Replace("拾零", "拾");
        strlen = rstr.Length;

        return rstr;
    }


    /// 
    /// 转换四位数字
    /// 
    public static string Convert4Digit(string str)
    {
        string str1 = str.Substring(0, 1);
        string str2 = str.Substring(1, 1);
        string str3 = str.Substring(2, 1);
        string str4 = str.Substring(3, 1);
        string rstring = "";
        rstring += ConvertChinese(str1) + "仟";
        rstring += ConvertChinese(str2) + "佰";
        rstring += ConvertChinese(str3) + "拾";
        rstring += ConvertChinese(str4);
        rstring = rstring.Replace("零仟", "零");
        rstring = rstring.Replace("零佰", "零");
        rstring = rstring.Replace("零拾", "零");
        rstring = rstring.Replace("零零", "零");
        rstring = rstring.Replace("零零", "零");
        rstring = rstring.Replace("零零", "零");
        return rstring;
    }
    /// 
    /// 转换三位数字
    /// 
    public static string Convert3Digit(string str)
    {
        string str1 = str.Substring(0, 1);
        string str2 = str.Substring(1, 1);
        string str3 = str.Substring(2, 1);
        string rstring = "";
        rstring += ConvertChinese(str1) + "佰";
        rstring += ConvertChinese(str2) + "拾";
        rstring += ConvertChinese(str3);
        rstring = rstring.Replace("零佰", "零");
        rstring = rstring.Replace("零拾", "零");
        rstring = rstring.Replace("零零", "零");
        rstring = rstring.Replace("零零", "零");
        return rstring;
    }
    /// 
    /// 转换二位数字
    /// 
    public static string Convert2Digit(string str)
    {
        string str1 = str.Substring(0, 1);
        string str2 = str.Substring(1, 1);
        string rstring = "";
        rstring += ConvertChinese(str1) + "拾";
        rstring += ConvertChinese(str2);
        rstring = rstring.Replace("零拾", "零");
        rstring = rstring.Replace("零零", "零");
        return rstring;
    }
    /// 
    /// 将一位数字转换成中文大写数字
    /// 
    public static string ConvertChinese(string str)
    {
        //"零壹贰叁肆伍陆柒捌玖拾佰仟萬億圆整角分"
        string cstr = "";
        switch (str)
        {
            case "0": cstr = "零"; break;
            case "1": cstr = "壹"; break;
            case "2": cstr = "贰"; break;
            case "3": cstr = "叁"; break;
            case "4": cstr = "肆"; break;
            case "5": cstr = "伍"; break;
            case "6": cstr = "陆"; break;
            case "7": cstr = "柒"; break;
            case "8": cstr = "捌"; break;
            case "9": cstr = "玖"; break;
        }
        return (cstr);
    }

    #endregion

    static string[] fileExtends = new string[] { ".jpg", ".gif", ".png" };  //可以上传的文件的后缀名

    //上传
    /// <summary>
    /// 上传文件（教师工作室中上传教学资源、设计...时用这个方法）
    /// </summary>
    /// <param name="upLoad">上传控件</param>
    /// <param name="oldName">原来上传的文件</param>
    /// <param name="fullPath">上传成功后返回文件保存的路径</param>
    /// <param name="reason">上传失败后返回错误信息</param>
    /// <param name="UserID">用户ID</param>
    /// <param name="FolderName">保存的文件夹</param>
    /// <returns></returns>
    public static bool UpLaodFile(FileUpload upLoad, ref string fullPath, ref string reason, string FolderName)
    {
        //string FileName = Guid.NewGuid().ToString() + "." + extend;
        //文件名称以及扩展名
        string fullFileName = upLoad.PostedFile.FileName;
        //文件名
        //string fileName = Path.GetFileNameWithoutExtension(fullFileName);
        string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        //文件扩展名
        string fileExtend = Path.GetExtension(fullFileName).ToLower();
        if (fileExtends != null)
        {
            if (!fileExtends.Any(t => t.ToLower() == fileExtend))
            {
                reason = " 不能上传后缀名为" + fileExtend + @"的文件";
                return false;
            }
        }
        if (upLoad.PostedFile.ContentLength > 0 && upLoad.PostedFile.ContentLength > 4 * 1024 * 1024)
        {
            reason = "上传的文件过大(大于4M)";
            return false;
        }

        //保存文件的名称
        string saveFileName = fileName + fileExtend;
        string _savePath = "~/Upload/" + FolderName + "/";//保存文件的相对路径

        try
        {
            //string _Path = System.Web.HttpContext.Current.Server.MapPath(_savePath);
            string _Path = HostingEnvironment.MapPath(_savePath);
            string saveFullPath = _Path + saveFileName;

            if (!Directory.Exists(_Path))//如果路径不存在就创建
            {
                Directory.CreateDirectory(_Path);
            }
            if (File.Exists(saveFullPath))
            {
                reason = "上传的文件已经存在,请修改一下文件名再上传！";
                return false;
            }
            else
            {
                upLoad.SaveAs(saveFullPath);
                fullPath = _savePath + saveFileName;
            }
            //if (File.Exists(_Path + oldName))//删除原来上传的文件
            //{
            //    File.Delete(_Path + oldName);
            //}
        }
        catch (Exception ex)
        {
            reason = "上传文件失败！错误原因：" + ex.Message;
            return false;
        }
        return true;
    }
}