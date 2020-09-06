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

public partial class admin_ExamChecklist : System.Web.UI.Page
{
    /// <summary>
    /// 是 教育基础知识考试
    /// </summary>
    bool isPublicKaoShi = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断是否为管理员权限
        string _Role = Convert.ToString(Session["Role2012_admin"]);
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
        isPublicKaoShi = ddlKaoShiType.SelectedValue == "0";//0教育基础知识考试  1学科专业知识考试
        if (!IsPostBack)
        {
            ddlKaoShiType_SelectedIndexChanged(sender, e);
        }
    }

    public void bangDing()
    {
        string _shichangCode = ddlshiChangCodeList.SelectedValue;
        if (!string.IsNullOrWhiteSpace(_shichangCode))
        {
            using (var dbHelper = new DBHelper())
            {
                shichangName.Text = ddlshiChangCodeList.SelectedValue;
                List<tb_zunkaozheng> lst0 = null;
                List<ZhunKaoZhengListForAdmin> lst = new List<ZhunKaoZhengListForAdmin>();
                if (isPublicKaoShi)
                {
                    lst0 = dbHelper.getZKZList().Where(p => p.shiChangCode == _shichangCode).ToList();
                    lst0.ForEach(t =>
                    {
                        ZhunKaoZhengListForAdmin item = new ZhunKaoZhengListForAdmin();
                        item.UserName = t.UserName;
                        item.kaoDian = t.kaoDian;
                        item.kaoShiDate = t.kaoShiDate;
                        item.kaoShiTime = t.kaoShiTime;
                        item.Subject = t.publicSubject;
                        item.zkzCode = t.zkzCode;
                        item.zuoweiCode = Convert.ToInt32(t.zuoweiCode);
                        item.shiChangCode = t.shiChangCode;
                        lst.Add(item);
                    });
                }
                else
                {
                    lst0 = dbHelper.getZKZList().Where(p => p.shiChangCode1 == _shichangCode).ToList();
                    lst0.ForEach(t =>
                    {
                        ZhunKaoZhengListForAdmin item = new ZhunKaoZhengListForAdmin();
                        item.UserName = t.UserName;
                        item.kaoDian = t.kaoDian1;
                        item.kaoShiDate = t.kaoShiDate1;
                        item.kaoShiTime = t.kaoShiTime1;
                        item.Subject = t.privateSubject;
                        item.zkzCode = t.zkzCode;
                        item.zuoweiCode = Convert.ToInt32(t.zuoweiCode1);
                        item.shiChangCode = t.shiChangCode1;
                        lst.Add(item);
                    });
                }
                if (lst != null && lst.Count() > 0)
                {
                    var _tb_userinfo = dbHelper.getUserList().Select(t => new { t.UserName, t.XingMing, t.SexCode, t.IDPhoto }).ToList();
                    foreach (var item in lst)
                    {
                        var userinfo_item = _tb_userinfo.FirstOrDefault(t => t.UserName == item.UserName);
                        if (userinfo_item != null)
                        {
                            item.UserRealName = userinfo_item.XingMing;
                            item.SexCode = userinfo_item.SexCode;
                            item.Sex = dbHelper.getXingBieParameterName(userinfo_item.SexCode);
                            item.IDPhoto = loadPic(userinfo_item.IDPhoto);
                        }
                    }
                }
                rptList.DataSource = lst.OrderBy(t => t.zuoweiCode);
                rptList.DataBind();
            }
        }
        else
        {
            Message.alter(this.Page,"请选择试场号！");
        }
    }
    public string loadPic(string IDCardPIC)
    {
        string ret = string.Empty;
        if (!string.IsNullOrWhiteSpace(IDCardPIC))
            ret = ImgHelper.LoadImage(60, 60, IDCardPIC).Replace("~", string.Empty);
        return ret;
    }
    
    protected void btnSelech_Click(object sender, EventArgs e)
    {
        bangDing();
    }

    protected void ddlKaoShiType_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (var db = new teacherBaoMing_Entities())
        {
            if (isPublicKaoShi) //教育基础知识考试
            {
                KaoShiTypeName.Text = "教育基础知识考试";
                var lst = db.Set<tb_zunkaozheng>().Select(p => p.shiChangCode).Distinct().ToList();
                ddlshiChangCodeList.DataSource = lst.OrderBy(t => t);
                ddlshiChangCodeList.DataBind();
            }
            else //学科专业知识考试
            {
                KaoShiTypeName.Text = "学科专业知识考试";
                var lst = db.Set<tb_zunkaozheng>().Select(p => p.shiChangCode1).Distinct().ToList();
                ddlshiChangCodeList.DataSource = lst.OrderBy(t => t);
                ddlshiChangCodeList.DataBind();
            }
        }
        if (!string.IsNullOrWhiteSpace(ddlshiChangCodeList.SelectedValue))
            bangDing();
    }
}