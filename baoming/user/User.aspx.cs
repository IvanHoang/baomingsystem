using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class user_User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //已经报名，执行下面查询
        if (!IsPostBack)
        {
            using (var dbHelper = new DBHelper())
            {
                string userName = Convert.ToString(Session["UserName2012"]);

                btnEdit.Visible = dbHelper.is报名时间内() || dbHelper.is再次报名时间内();

                var _user = dbHelper.getUserList().FirstOrDefault(t => t.UserName == userName);
                bool isExist = _user != null;
                //btnFirst.Visible = !isExist;
                btnPrint.Visible = isExist && _user.AuditCode == "1";

                //判断是否已经提交报名表
                if (isExist)
                {
                    btnEdit.Visible &= "0,2".Contains(_user.AuditCode);
                    pnlFee.Visible= _user.AuditCode == "1";

                    if (dbHelper.is再次报名时间内())
                        btnEdit.Text = "二次报名 &nbsp;|&nbsp;";
                    else
                        btnEdit.Text = "修改报名表 &nbsp;|&nbsp;";

                    //接收页面传值id
                    //查询数据库，返回到Repeater控件中
                    //string strSql = "select tb_userinfo.*,XueKeName,GangWeiName from tb_userinfo,tb_xueke,tb_gangwei where tb_userinfo.XueKeCode = tb_xueke.XueKeCode and tb_xueke.GangWeiCode = tb_gangwei.GangweiCode and tb_userinfo.UserName='" + _UserName + "'";
                    ltlid.Text = _user.id.ToString(); //ds.Tables[0].Rows[0]["id"].ToString();
                    ltlXingMing.Text = _user.XingMing; //ds.Tables[0].Rows[0]["XingMing"].ToString();
                    ltlUserName.Text = _user.UserName; //ds.Tables[0].Rows[0]["UserName"].ToString();
                    ltlXueKeName.Text = _user.tb_xueke.XueKeName; //ds.Tables[0].Rows[0]["XueKeName"].ToString();
                    ltlGangWeiName.Text = _user.tb_xueke.tb_gangwei.GangWeiName; //ds.Tables[0].Rows[0]["GangWeiName"].ToString();
                    ltlAuditResult.Text = dbHelper.getAuditActionParameterName(_user.AuditCode);
                    ltlAuditFeedback.Text = _user.AuditFeedback; //ds.Tables[0].Rows[0]["AuditFeedback"].ToString();
                    ltlBirthday.Text = _user.Birthday; //Convert.ToDateTime(ds.Tables[0].Rows[0]["Birthday"].ToString()).ToShortDateString();
                    ltlShengYuanDi.Text = _user.ShengYuanDi; //ds.Tables[0].Rows[0]["ShengYuanDi"].ToString();
                    ltlBiYeSchool.Text = _user.BiYeSchool; //ds.Tables[0].Rows[0]["BiYeSchool"].ToString();
                    ltlXueLi.Text =dbHelper.getXueLiParameterName(_user.XueLiCode); //ds.Tables[0].Rows[0]["XueLi"].ToString();
                    ltlBiYeTime.Text = _user.BiYeTime.HasValue ? _user.BiYeTime.Value.ToShortDateString() : string.Empty; //Convert.ToDateTime(ds.Tables[0].Rows[0]["BiYeTime"].ToString()).ToShortDateString();
                    ltlQuanRiZhi.Text = _user.QuanRiZhi; //ds.Tables[0].Rows[0]["QuanRiZhi"].ToString();
                    ltlZhuanYe.Text = _user.ZhuanYe; //ds.Tables[0].Rows[0]["ZhuanYe"].ToString();
                    ltlShiFanLei.Text = _user.ShiFanLei; //ds.Tables[0].Rows[0]["ShiFanLei"].ToString();
                    ltlZiGeZheng.Text = _user.ZiGeZheng; //ds.Tables[0].Rows[0]["ZiGeZheng"].ToString();
                    ltlAddress.Text = _user.Address; //ds.Tables[0].Rows[0]["Address"].ToString();
                    ltlTel.Text = _user.Tel; //ds.Tables[0].Rows[0]["Tel"].ToString();
                    ltlXueQianWork.Text = _user.XueQianWork; //ds.Tables[0].Rows[0]["XueQianWork"].ToString();
                    ltlBiYeZhengShuCode.Text = _user.BiYeZhengShuCode; //ds.Tables[0].Rows[0]["BiYeZhengShuCode"].ToString();
                    ltlXueKeCode.Text = _user.XueKeCode.ToString(); //ds.Tables[0].Rows[0]["XueKeCode"].ToString();
                    ltlSex.Text = dbHelper.getXingBieParameterName(_user.SexCode); //ds.Tables[0].Rows[0]["Sex"].ToString();
                    ltlDuSheng.Text = _user.DuSheng; //ds.Tables[0].Rows[0]["DuSheng"].ToString();
                    ltlMobile.Text = _user.Mobile; //ds.Tables[0].Rows[0]["Mobile"].ToString();
                    ltlZiGeZhengCode.Text = _user.ZiGeZhengCode; //ds.Tables[0].Rows[0]["ZiGeZhengCode"].ToString();
                    ltlBiYeZhengShuCode.Text = _user.BiYeZhengShuCode; //ds.Tables[0].Rows[0]["BiYeZhengShuCode"].ToString();
                    ltlPTHLevel.Text = _user.PTHLevel;
                    ltlPTHNo.Text = _user.PTHZSNo;
                    imgPhoto.ImageUrl = ImgHelper.LoadImage(120, 160, _user.IDPhoto);

                    var zkz = dbHelper.get准考证(_user.UserName);
                    if (zkz != null && dbHelper.is打印准考证时间())
                        litZKZPrint.Text = "&nbsp;<a href=\"../user/PrintingZKZ.aspx\" target=\"_blank\">打印准考证</a>&nbsp;|";
                    else
                        litZKZPrint.Text = string.Empty;

                    bool showNotice = string.IsNullOrWhiteSpace(_user.XingMing);
                    pnlMessage.Visible = showNotice;
                    pnlInfo.Visible = !showNotice;
                }
                else
                {
                    bool showNotice = true;
                    pnlMessage.Visible = showNotice;
                    pnlInfo.Visible = !showNotice;

                    btnEdit.Text = "填写报名表 &nbsp;|&nbsp;";
                }

                f报名信息验证(dbHelper, userName);
            }
        }
    }

    private void f报名信息验证(DBHelper dbHelper,string userName)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        var _user = dbHelper.getUserList().FirstOrDefault(t => t.UserName == userName);
        if (_user == null)
            sb.AppendLine("请填写报名表；");
        else
        {
            if(string.IsNullOrWhiteSpace( _user.IDPhoto))
                sb.AppendLine("需要上传免冠头像照片；");

            if(_user.AuditCode == "1")
                sb.AppendLine("初审已经通过，请及时完成缴费！");
            else if (_user.AuditCode == "2")
                sb.AppendLine("初审未通过，你可以在二次报名期间，转其它岗位学科！");
            else if (_user.AuditCode == "3")
                sb.AppendLine("审核认为，你不合适全部的岗位学科，不能继续报名！");
            if (!string.IsNullOrWhiteSpace(_user.AuditFeedback))
                sb.AppendLine("审核人员反馈：" + _user.AuditFeedback);

            if (dbHelper.get准考证(_user.UserName) != null)
            {
                if(dbHelper.is打印准考证时间())
                    sb.AppendLine("已有准考证信息，可以打印准考证了！");
                else
                    sb.AppendLine("已有准考证信息，但未到允许打印时间！");
            }
        }
        if (sb.Length > 0)
        {
            sb.Insert(0, "提示：" + Environment.NewLine);
            lblMessage.Text = sb.ToString().Replace(Environment.NewLine, "<br/>");
        }
        else
            lblMessage.Text = "目前没有提示！";
    }
}