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
                //接收页面传值id
                int id = 1;
                id = Convert.ToInt32(Request.QueryString["id"]);
                //查询数据库，返回到Repeater控件中
                //DB db = new DB();
                //SqlConnection conn = db.GetCon();
                //conn.Open();
                //string strSql = "select tb_userinfo.*,XueKeName,GangWeiName from tb_userinfo,tb_xueke,tb_gangwei where tb_userinfo.XueKeCode = tb_xueke.XueKeCode and tb_xueke.GangWeiCode = tb_gangwei.GangweiCode and tb_userinfo.id='" + id + "'";
                //SqlDataAdapter sda = new SqlDataAdapter(strSql, conn);
                //conn.Close();
                //DataSet ds = new DataSet();
                //sda.Fill(ds);
                var _user = dbHelper.getUserList().FirstOrDefault(t => t.id == id);
                if (_user != null)
                {
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
                    ltlXueLi.Text = dbHelper.getXueLiParameterName(_user.XueLiCode); //ds.Tables[0].Rows[0]["XueLi"].ToString();
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
                    ltlSex.Text = dbHelper.getXingBieParameterName(_user.SexCode);
                    ltlDuSheng.Text = _user.DuSheng; //ds.Tables[0].Rows[0]["DuSheng"].ToString();
                    ltlMobile.Text = _user.Mobile; //ds.Tables[0].Rows[0]["Mobile"].ToString();
                    ltlZiGeZhengCode.Text = _user.ZiGeZhengCode; //ds.Tables[0].Rows[0]["ZiGeZhengCode"].ToString();
                    ltlBiYeZhengShuCode.Text = _user.BiYeZhengShuCode; //ds.Tables[0].Rows[0]["BiYeZhengShuCode"].ToString();
                    ltlPTHLevel.Text = _user.PTHLevel;
                    ltlPTHNo.Text = _user.PTHZSNo;
                    imgPhoto.ImageUrl = ImgHelper.LoadImage(120, 160, _user.IDPhoto);
                }
            }
        }
    }
}