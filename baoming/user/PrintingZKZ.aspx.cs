using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class user_PrintingZKZ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string _UserName = Convert.ToString(Session["UserName2012"]);
            if (string.IsNullOrWhiteSpace(_UserName))
            {
                Response.Write("<script>alert('需要重新登录！');location='../default.aspx'</script>");
                Response.End();
            }
            else
            {
                using (var dbHelper = new DBHelper())
                {
                    //DateTime _ZKZPrintTimeStart = new SystemSetProvider().get准考证打印开始时间();
                    //if (DateTime.Compare(_ZKZPrintTimeStart, DateTime.Now) > 0)
                    if (!dbHelper.is打印准考证时间())
                    {
                        Response.Write("<script>alert('对不起，未到准考证打印时间！');location='User.aspx'</script>");
                    }
                    else
                    {
                        var _user = dbHelper.getUserList().FirstOrDefault(t => t.UserName == _UserName);
                        //判断是否已经提交报名表
                        if (_user != null)
                        {
                            lbSex.Text = dbHelper.getXingBieParameterName(_user.SexCode);
                            lbXingMing.Text = _user.XingMing;
                            image_Photo.ImageUrl = ImgHelper.LoadImage(120, 160, _user.IDPhoto);

                            var _zunkaozheng = dbHelper.getZKZList().FirstOrDefault(t => t.UserName == _user.UserName);
                            if (_zunkaozheng != null)
                            {
                                lbKaoDian.Text = _zunkaozheng.kaoDian;
                                lbKaoDian1.Text = _zunkaozheng.kaoDian1;
                                lbkaoShiDate.Text = _zunkaozheng.kaoShiDate;
                                lbkaoShiDate1.Text = _zunkaozheng.kaoShiDate1;
                                lbkaoShiTime.Text = _zunkaozheng.kaoShiTime;
                                lbkaoShiTime1.Text = _zunkaozheng.kaoShiTime1;
                                lbPrivateSubject.Text = _zunkaozheng.privateSubject;
                                lbPublicSubject.Text = _zunkaozheng.publicSubject;
                                lbshichangCode.Text = _zunkaozheng.shiChangCode;
                                lbshichangCode1.Text = _zunkaozheng.shiChangCode1;
                                lbUserName.Text = _zunkaozheng.UserName;
                                lbzkzCode.Text = _zunkaozheng.zkzCode;
                                lbzuoweiCode.Text = _zunkaozheng.zuoweiCode;
                                lbzuoweiCode1.Text = _zunkaozheng.zuoweiCode1;
                            }
                            else
                            {
                                Response.Write("<script>alert('人事科还没有导入准考证信息，若你已经通过了审核，请耐心等待！')</script>");
                            }
                        }
                        else
                            Response.Write("<script>alert('还没有准考证信息！')</script>");
                    }
                }
            }
        }
    }
}