using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class AdminLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string num = this.txtValidateNum.Text.Trim();
        //判断验证码
        if (HttpContext.Current.Request.IsLocal || Convert.ToString(Session["ValidateNum"]) == num.ToUpper())
        {
            string _UserName = txtAdminName.Text.Trim();
            string _PassWord = txtPassWord.Text.Trim();
            string _pass = Input.MD5(_PassWord);

            using (var dbHelper = new DBHelper())
            {
                var admin = dbHelper.CheckName(_UserName, _pass, true);
                if (admin == null)
                {
                    Response.Write("<script>alert('审核人员登录不成功，请重试！');</script>");
                    this.txtAdminName.Focus();
                }
                else
                {
                    //用户名密码验证通过，记录Session，并返回判断值
                    Session["UserName2012_admin"] = admin.UserName;
                    Session["Role2012_admin"] = admin.Role;

                    FormsAuthentication.SetAuthCookie(_UserName, false);
                    Response.Redirect("~/admin/list.aspx");
                }
            }
        }
        else
        {
            Response.Write("<script>alert('请输入正确的验证码！');</script>");
            this.txtValidateNum.Focus();
        }
    }
}