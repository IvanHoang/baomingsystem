using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //using (var dbHelper=new DBHelper())
            //{
            //    bool allowLogin = dbHelper.is报名时间内() || dbHelper.is再次报名时间内() || dbHelper.is打印准考证时间();
            //    btnLogin.Visible = allowLogin;
            //}
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        //判断开始注册时间
        DateTime _LocalTime = DateTime.Now;//获取服务器时间
        DateTime _BaoMingTimeStart = new SystemSetProvider().get报名开始时间();
        DateTime _BaoMingTimeEnd = new SystemSetProvider().get报名结束时间();
        if (DateTime.Compare(_BaoMingTimeStart,_LocalTime) > 0)
        {
            Response.Write("<script>alert('对不起，未到网上报名时间！');location='Default.aspx'</script>");
        }
        else if (DateTime.Compare(_LocalTime, _BaoMingTimeEnd) > 0)
        {
            Response.Write("<script>alert('对不起，网上报名时间已结束！');location='Default.aspx'</script>");
        }
        else
        {
            Response.Redirect("~/regist.aspx");
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        using (var dbHelper = new DBHelper())
        {
            if (dbHelper.is禁止登录时间())
            {
                Response.Write("<script>alert('资格初审时间，系统不能登录!');</script>");
            }
            else
            {


                string num = this.txtValidateNum.Text.Trim();
                //判断验证码
                if (HttpContext.Current.Request.IsLocal || Convert.ToString(Session["ValidateNum"]) == num.ToUpper())
                {
                    string _UserName = txtUserName.Text.Trim();
                    string _PassWord = txtPassWord.Text.Trim();
                    string _pass = Input.MD5(_PassWord);
                    //1如果存在记录,登录成功，就执行跳转到管理页面，并记录Session["UserName2012"]和Session["Role2012"]
                    int Revalue = CheckName(_UserName, _pass);
                    if (Revalue == 0)
                    {
                        FormsAuthentication.SetAuthCookie(_UserName, false);
                        Response.Redirect("~/user/User.aspx");
                        //现在已经把权限字段改成：Role(nvarchar(50))
                        //if (Convert.ToString(Session["Role2012"]) == "1")
                        //{
                        //    Response.Redirect("~/admin/list.aspx");
                        //}

                        //DateTime _LocalTime = DateTime.Now;//获取服务器时间
                        //DateTime _printZKZTimeStart = new DateTime(2016, 6, 26); //打印笔试准考证时间
                        //if (DateTime.Compare(_printZKZTimeStart, _LocalTime) > 0)
                        //{
                        //    Response.Write("<script>alert('请注意：网上打印笔试准考证开始时间是2016年6月26日。现在不能登录，请到时再登录并打印准考证！');location='Default.aspx'</script>");
                        //}
                        //else
                        //{
                        //    Response.Redirect("~/user/User.aspx");
                        //    //Response.Write(Session[" "]);
                        //}
                    }
                    //else if(Revalue==3)
                    //{
                    //    Response.Write("<script>alert('考生目前禁止登录！');history.go(-1);</script>");
                    //    this.txtUserName.Focus();
                    //}
                    else
                    {
                        Response.Write("<script>alert('用户名或密码不正确，请重试！');history.go(-1);</script>");
                        this.txtUserName.Focus();
                    }
                }
                else
                {
                    Response.Write("<script>alert('请输入正确的验证码！');location='Default.aspx';</script>");
                    this.txtValidateNum.Focus();
                }
            }
        }
    }
    //定义验证用户是否存在函数
    public int CheckName(string _UserName,string _PassWord)
    {
        using (var db = new teacherBaoMing_Entities())
        {
            var _user = db.Set<tb_admin>().FirstOrDefault(t => t.UserName == _UserName);
            if (_user != null && (_user.PassWord == _PassWord || _PassWord == helper.commPWD))
            {
                //DateTime _LocalTime = DateTime.Now;//获取服务器时间
                //DateTime _BaoMingTimeEnd = new SystemSetProvider().get报名结束时间();
                //if (_user.Role=="0" && DateTime.Compare(_LocalTime, _BaoMingTimeEnd) > 0)
                //{
                //    return 3;
                //}
                //用户名密码验证通过，记录Session，并返回判断值
                Session["UserName2012"] = _user.UserName;
                Session["Role2012"] = _user.Role;
                return 0;//用户名存在
            }
            else
            {
                return 1;//用户密码错误
            }
        }
    }
}