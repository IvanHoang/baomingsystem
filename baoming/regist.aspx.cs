using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class regist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断开始注册时间
        DateTime _LocalTime = DateTime.Now;//获取服务器时间
        DateTime _BaoMingTimeStart = new SystemSetProvider().get报名开始时间();
        DateTime _BaoMingTimeEnd = new SystemSetProvider().get报名结束时间();
        if (DateTime.Compare(_BaoMingTimeStart, _LocalTime) > 0)
        {
            Response.Write("<script>alert('对不起，未到网上报名时间！');location='Default.aspx'</script>");
        }
        else if (DateTime.Compare(_LocalTime, _BaoMingTimeEnd) > 0)
        {
            Response.Write("<script>alert('对不起，网上报名时间已结束！');location='Default.aspx'</script>");
        }

    }
    //定义验证用户是否存在函数
    public int CheckName(string _UserName)
    {
        using (var db = new teacherBaoMing_Entities())
        {
            bool isExist = db.Set<tb_admin>().Any(t => t.UserName == _UserName);
            if (isExist)
                return 0;
            else
                return 1;
            //DataTable dt = db.reDt(strSql);
            //try
            //{
            // if (dt.Rows[0][0].ToString() != "0")
            //        {
            //            return 0;//用户名存在
            //        }
            //        else
            //        {
            //            return 1;//用户名不存在
            //        }
            //}
            //catch (Exception ee)
            //{
            //    return 2;
            //}
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string num = this.txtValidateNum.Text.Trim();
        string _UserName = txtUserName.Text.Trim();
        string _PassWord = txtPassWord.Text.Trim();
        if (_UserName.Length != 18)
        {
            labInfo.Text = "身份证号码必须是18位！";
            return;
        }
        //判断验证码
        if (Session["ValidateNum"].ToString() == num.ToUpper())
        {
            if (FileUpload_ID.HasFile)
            {
                int Revalue = CheckName(_UserName);
                if (Revalue == 0)
                {
                    Response.Write("<script>alert('该身份证已经存在！');</script>");
                    this.txtUserName.Focus();
                }
                else
                {
                    string _pass = Input.MD5(_PassWord);
                    string pic = string.Empty;
                    string reason = string.Empty;
                    if (Input.UpLaodFile(FileUpload_ID, ref pic, ref reason, "IDCards"))
                    {
                        using (var db = new teacherBaoMing_Entities())
                        {
                            var sets = db.Set<tb_admin>();
                            tb_admin addEntity = new tb_admin() { PassWord = _pass, UserName = _UserName, Role = "0", IDCardPIC = pic };
                            sets.Add(addEntity);
                            db.SaveChanges();
                            //string strSql = "insert into tb_admin(UserName,PassWord) values ('" + _UserName + "','" + _PassWord + "')";
                            //db.sqlEx(strSql);
                            Response.Write("<script>alert('注册成功，请牢记你的密码，并请登录后提交其他报名信息！');location='Default.aspx'</script>");
                        }
                    }
                    else
                        labInfo.Text = "身份证图片文件上传失败!" + reason;
                }
            }
            else
                labInfo.Text = "请指定一个身份证正面图片文件!";
        }
        else
        {
            //Response.Write("<script>alert('验证码输入错误！');location='regist.aspx'</script>");               
            labInfo.Text = "验证码输入错误，请重新输入!";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/");
    }
}