using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_audit : System.Web.UI.Page
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

        //★此处一定要加上if (!IsPostBack)，否则点击修改后，还是写入原来的值。
        if (!IsPostBack)
        {
            using (var dbHelper = new DBHelper())
            {
                var auditCodes = dbHelper.getAuditActionParameterList();
                controlHelper.DropDownList_DataBind(ddlAuditResult, auditCodes, null);
                litAuditRemark.Text = string.Join("<br/>", auditCodes.Where(t => t.itemRemark != null && t.itemRemark != "").Select(t => t.itemRemark));
                //接收页面传值id
                int id = 1;
                id = Convert.ToInt32(Request.QueryString["id"]);
                //查询数据库，返回到DataSet中
                var _user = dbHelper.getUserList().FirstOrDefault(t => t.id == id);
                if (_user != null)
                {
                    var _user0 = dbHelper.getAdminList().FirstOrDefault(t => t.UserName == _user.UserName);
                    lbXingMing.Text = _user.XingMing; //ds.Tables[0].Rows[0]["XingMing"].ToString();
                    lbUserName.Text = _user.UserName;
                    lbShengYuanDi.Text = _user.ShengYuanDi;
                    lbXueKeName.Text = _user.tb_xueke.XueKeName; //ds.Tables[0].Rows[0]["XueKeName"].ToString();
                    lbGangWeiName.Text = _user.tb_xueke.tb_gangwei.GangWeiName; //ds.Tables[0].Rows[0]["GangWeiName"].ToString();
                    ddlAuditResult.SelectedValue = _user.AuditCode;
                    txtAuditFeedback.Text = _user.AuditFeedback; //ds.Tables[0].Rows[0]["AuditFeedback"].ToString();

                    lbXueLi.Text = dbHelper.getXueLiParameterName(_user.XueLiCode);
                    lbShiFanLei.Text = _user.ShiFanLei;
                    lbZhuangye.Text = _user.ZhuanYe;
                    lbJiaoShiZGZ.Text = _user.ZiGeZheng;
                    lbJiaoShiZGZNo.Text = _user.ZiGeZhengCode;
                    lbPTHLevel.Text = _user.PTHLevel;

                    imgPhoto.ImageUrl = ImgHelper.LoadImage(120, 160, _user.IDPhoto);

                    imgIDCard.ImageUrl = ImgHelper.LoadImage(480, 480, _user0.IDCardPIC);
                    picResidenceBooklet.ImageUrl = ImgHelper.LoadImage(480, 480, _user.picResidenceBooklet);
                    picArchiveCertificate.ImageUrl= ImgHelper.LoadImage(480, 480, _user.picArchiveCertificate);
                    picDiploma.ImageUrl= ImgHelper.LoadImage(480, 480, _user.picDiploma);
                    picNewGraduates.ImageUrl= ImgHelper.LoadImage(480, 480, _user.picNewGraduates);
                    picZiGeZheng.ImageUrl= ImgHelper.LoadImage(480, 480, _user.picZiGeZheng);
                    picPTH.ImageUrl= ImgHelper.LoadImage(480, 480, _user.picPTH);
                    picKindergartenCommitment.ImageUrl= ImgHelper.LoadImage(480, 480, _user.picKindergartenCommitment);
                }
            }
        }
              
    }
    protected void btnSubmit(object sender, EventArgs e)
    {
        int id;
        id = Convert.ToInt32(Request.QueryString["id"]);
        string _AuditCode = ddlAuditResult.SelectedValue;
        string _AuditFeedback = txtAuditFeedback.Text.Trim();
        using (var db = new teacherBaoMing_Entities())
        {
            var _user = db.Set<tb_userinfo>().FirstOrDefault(t => t.id == id);
            if (_user != null)
            {
                _user.AuditCode = _AuditCode;
                _user.AuditFeedback = _AuditFeedback;
                db.SaveChanges();
                Response.Write("<script>alert('审核成功!');location.replace(location.href);</script>");
            }
            else
                Response.Write("<script>alert('审核写入失败!');</script>");
        }
    }
}