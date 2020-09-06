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

public partial class admin_ZhunKaoZhengList : System.Web.UI.Page
{

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
        if (!IsPostBack)
        {
            using (var dbHelper = new DBHelper())
            {
                var lst0 =dbHelper.getZKZList();
                var lst = lst0.Select(t => new ZhunKaoZhengListForAdmin1()
                {
                    UserName = t.UserName,
                    kaoDian = t.kaoDian,
                    kaoShiDate = t.kaoShiDate,
                    privateSubject = t.privateSubject,
                    zkzCode = t.zkzCode,
                    zuoweiCode = t.zuoweiCode,
                    shiChangCode = t.shiChangCode,
                    kaoDian1 = t.kaoDian1,
                    kaoShiDate1 = t.kaoShiDate1,
                    kaoShiTime = t.kaoShiTime,
                    kaoShiTime1 = t.kaoShiTime1,
                    zuoweiCode1 = t.zuoweiCode1,
                    publicSubject = t.publicSubject,
                    shiChangCode1 = t.shiChangCode1,
                    //UserRealName=
                }).ToList();

                if (lst != null && lst.Count() > 0)
                {
                    var _tb_userinfo =dbHelper.getUserList().Select(t => new { t.UserName, t.XingMing, t.SexCode, t.IDPhoto }).ToList();
                    foreach (var item in lst)
                    {
                        var userinfo_item = _tb_userinfo.FirstOrDefault(t => t.UserName == item.UserName);
                        if (userinfo_item != null)
                        {
                            item.UserRealName = userinfo_item.XingMing;
                            item.SexCode = userinfo_item.SexCode;
                            item.Sex = dbHelper.getXingBieParameterName(userinfo_item.SexCode);
                            item.IDPhoto = userinfo_item.IDPhoto;
                        }

                    }
                }
                rptList.DataSource = lst.OrderBy(t => t.zkzCode);
                rptList.DataBind();
            }
        }
    }
}