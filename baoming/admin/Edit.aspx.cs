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
                controlHelper.DropDownList_DataBind(ddlSex, dbHelper.getXingBieParameterList(), null);
                controlHelper.DropDownList_DataBind(ddlPoliticalOrientation, dbHelper.getZZMMParameterList(), null);
                controlHelper.DropDownList_DataBind(ddlZhiYe, dbHelper.getZhiYeParameterList(), null);
                controlHelper.DropDownList_DataBind(ddlXueLi, dbHelper.getXueLiParameterList(), null);
                controlHelper.DropDownList_DataBind(ddlMinZu, dbHelper.getMinZuParameterList(), null);

                var gangweiList = dbHelper.getGangWeiList();
                ddlGangWei.DataSource = gangweiList;
                ddlGangWei.DataTextField = "GangWeiName";
                ddlGangWei.DataValueField = "GangWeiCode";
                ddlGangWei.DataBind();

                ddlGangWei_SelectedIndexChanged(sender, e);

                //符合条件后，执行修改用户报考信息之绑定原有信息到页面功能
                string _idtmp = Request["ID"];
                int _id = -1;
                if (int.TryParse(_idtmp, out _id))
                {
                    var _user =dbHelper.getUserList().FirstOrDefault(t => t.id == _id);
                    if (_user != null)
                    {
                        var _user0 =dbHelper.getAdminList().FirstOrDefault(t => t.UserName == _user.UserName);

                        imgPhoto.ImageUrl = ImgHelper.LoadImage(200, 300, _user.IDPhoto);
                        txtXingMing.Text = _user.XingMing; //ds.Tables[0].Rows[0]["XingMing"].ToString();
                        ddlSex.SelectedValue = _user.SexCode; //ds.Tables[0].Rows[0]["Sex"].ToString();
                        litBirthday.Text = _user.Birthday; //Convert.ToDateTime(ds.Tables[0].Rows[0]["Birthday"]).ToShortDateString();
                        ddlShengYuanDi.SelectedValue = _user.ShengYuanDi; //ds.Tables[0].Rows[0]["ShengYuanDi"].ToString();
                        txtBiYeSchool.Text = _user.BiYeSchool; //ds.Tables[0].Rows[0]["BiYeSchool"].ToString();
                        ddlGangWei.SelectedValue = _user.tb_xueke.GangWeiCode.ToString(); //ds.Tables[0].Rows[0]["GangweiCode"].ToString();
                        ddlGangWei_SelectedIndexChanged(sender, e);
                        ddlXueKe.SelectedValue = _user.XueKeCode.ToString(); //ds.Tables[0].Rows[0]["XueKeCode"].ToString();
                        ddlXueLi.SelectedValue = _user.XueLiCode; //ds.Tables[0].Rows[0]["XueLi"].ToString();
                        txtBiYeTime.Text = _user.BiYeTime.HasValue ? _user.BiYeTime.Value.ToString("yyyy-MM-dd") : string.Empty; //Convert.ToDateTime(ds.Tables[0].Rows[0]["BiYeTime"]).ToShortDateString();
                        ddlQuanRiZhi.SelectedValue = _user.QuanRiZhi; //ds.Tables[0].Rows[0]["QuanRiZhi"].ToString();
                        txtZhuanYe.Text = _user.ZhuanYe; //ds.Tables[0].Rows[0]["ZhuanYe"].ToString();
                        ddlShiFanLei.SelectedValue = _user.ShiFanLei; //ds.Tables[0].Rows[0]["ShiFanLei"].ToString();
                        ddlShiFanLei_SelectedIndexChanged(sender, e);
                        ddlDuSheng.SelectedValue = _user.DuSheng; //ds.Tables[0].Rows[0]["DuSheng"].ToString();
                        txtMobile.Text = _user.Mobile; //ds.Tables[0].Rows[0]["Mobile"].ToString();
                        txtAddress.Text = _user.Address; //ds.Tables[0].Rows[0]["Address"].ToString();
                        txtTel.Text = _user.Tel; //ds.Tables[0].Rows[0]["Tel"].ToString();
                        ddlXueQianWork.SelectedValue = _user.XueQianWork; //ds.Tables[0].Rows[0]["XueQianWork"].ToString();
                        txtBiYeZhengShuCode.Text = _user.BiYeZhengShuCode; //ds.Tables[0].Rows[0]["BiYeZhengShuCode"].ToString();

                        ddlMinZu.SelectedValue = _user.MinZuCode;
                        ddlPoliticalOrientation.SelectedValue = _user.PoliticalOrientationCode;
                        ddlZhiYe.SelectedValue = _user.ZhiYeCode;

                        ltUserName.Text = _user.UserName; //ds.Tables[0].Rows[0]["UserName"].ToString();
                        imgIDCard.ImageUrl = ImgHelper.LoadImage(350, 350, _user0.IDCardPIC);
                        pnl非职高专业课.Visible = !helper._不需要资格证的岗位or学科.Contains( _user.tb_xueke.GangWeiCode);
                        if (pnl非职高专业课.Visible)
                        {
                            txtZiGeZheng.Text = _user.ZiGeZheng; //ds.Tables[0].Rows[0]["ZiGeZheng"].ToString();
                            txtZiGeZhengCode.Text = _user.ZiGeZhengCode; //ds.Tables[0].Rows[0]["ZiGeZhengCode"].ToString();
                            ddlPTHLevel.SelectedValue = _user.PTHLevel;
                            txtPTHNo.Text = _user.PTHZSNo;
                        }
                    }
                    else
                        Response.Write("用户信息不存在！");
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //符合条件后，执行修改用户报考信息之绑定原有信息到页面功能
        string _idtmp = Request["ID"];
        int _id = -1;
        if (int.TryParse(_idtmp, out _id))
        {
            //修改数据库操作
            //string _UserName = Session["UserName2012_admin"].ToString();
            string _XingMing = txtXingMing.Text.Trim();
            string _Sex = ddlSex.SelectedValue;
            string _ShengYuanDi = ddlShengYuanDi.SelectedValue;
            string _BiYeSchool = txtBiYeSchool.Text.Trim();
            string _XueKe = ddlXueKe.SelectedValue;
            string _XueLi = ddlXueLi.SelectedValue;
            string __BiYeDT = txtBiYeTime.Text.Trim();
            string _QuanRiZhi = ddlQuanRiZhi.SelectedValue;
            string _ZhuanYe = txtZhuanYe.Text.Trim();
            string _ShiFanLei = ddlShiFanLei.SelectedValue;
            string _ZiGeZheng = txtZiGeZheng.Text.Trim();
            string _DuSheng = ddlDuSheng.SelectedValue;
            string _Mobile = txtMobile.Text.Trim();
            string _ZiGeZhengCode = txtZiGeZhengCode.Text.Trim();
            string _Address = txtAddress.Text.Trim();
            string _Tel = txtTel.Text.Trim();
            string _XueQianWork = ddlXueQianWork.SelectedValue;
            string _BiYeZhengShuCode = txtBiYeZhengShuCode.Text.Trim();
            int _GangWeiCode = Convert.ToInt32(ddlGangWei.SelectedValue);
            string pthLevel = ddlPTHLevel.SelectedValue;
            string pthNo = txtPTHNo.Text.Trim();
            using (var db = new teacherBaoMing_Entities())
            {
                var _user = db.Set<tb_userinfo>().FirstOrDefault(t => t.id == _id);
                if (_user != null)
                {
                    _user.XingMing = _XingMing;
                    _user.Birthday = _user.UserName.Substring(6, 6);
                    _user.ShengYuanDi = _ShengYuanDi;
                    _user.BiYeSchool = _BiYeSchool;
                    _user.XueKeCode = Convert.ToInt32(_XueKe);
                    _user.XueLiCode = _XueLi;
                    //_user.BiYeTime = _BiYeTime;
                    DateTime _BiYeDT = DateTime.MinValue;
                    if (!DateTime.TryParse(__BiYeDT, out _BiYeDT))
                    {
                        Response.Write("<script>alert('毕业时间，请给出一个恰当的日期!');</script>");
                        return;
                    }
                    else
                        _user.BiYeTime = _BiYeDT;
                    _user.QuanRiZhi = _QuanRiZhi;
                    _user.ZhuanYe = _ZhuanYe;
                    _user.ShiFanLei = _ShiFanLei;
                    _user.DuSheng = _DuSheng;
                    _user.Mobile = _Mobile;
                    _user.Address = _Address;
                    _user.Tel = _Tel;
                    _user.XueQianWork = _XueQianWork;
                    _user.BiYeZhengShuCode = _BiYeZhengShuCode;
                    _user.SexCode = _Sex;

                    _user.MinZuCode = ddlMinZu.SelectedValue;
                    _user.PoliticalOrientationCode = ddlPoliticalOrientation.SelectedValue;
                    _user.ZhiYeCode = ddlZhiYe.SelectedValue;

                    if (_GangWeiCode != 3)//职高
                    {
                        _user.ZiGeZheng = _ZiGeZheng;
                        _user.ZiGeZhengCode = _ZiGeZhengCode;
                        _user.PTHLevel = pthLevel;
                        _user.PTHZSNo = pthNo;
                    }
                    else
                    {
                        _user.ZiGeZheng = null;
                        _user.ZiGeZhengCode = null;
                        _user.PTHLevel = null;
                        _user.PTHZSNo = null;
                    }
                    bool bsucc = true;
                    string reason = string.Empty;
                    var user0 = db.Set<tb_admin>().FirstOrDefault(t => t.UserName == _user.UserName);
                    if (user0 != null && FileUpload_ID.HasFile)
                    {
                        string pic = string.Empty;
                        if (Input.UpLaodFile(FileUpload_ID, ref pic, ref reason, "IDCards"))
                        {
                            user0.IDCardPIC = pic;
                        }
                        else
                            bsucc = false;
                    }
                    if (File_Photo.HasFile)
                    {
                        string pic = string.Empty;
                        if (Input.UpLaodFile(File_Photo, ref pic, ref reason, "IDPhotos"))
                        {
                            _user.IDPhoto = pic;
                        }
                        else
                            bsucc = false;
                    }
                    if (bsucc)
                    {
                        db.SaveChanges();
                        Response.Write("<script>alert('修改成功!');location.replace(location.href);</script>");
                    }
                    else
                        Response.Write("<script>alert('修改失败！" + reason + "');'</script>");
                }
                else
                    Response.Write("<script>alert('修改失败!')</script>");
            }
        }
    }

    protected void ddlGangWei_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (var db = new teacherBaoMing_Entities())
        {
            int _GangWeiCode = Convert.ToInt32(ddlGangWei.SelectedValue);
            if (_GangWeiCode > 0)
            {
                ddlXueKe.DataSource = db.Set<tb_xueke>().Where(t => t.GangWeiCode == _GangWeiCode).OrderBy(t => t.XueKeCode).ToList();
                ddlXueKe.DataTextField = "XueKeName";
                ddlXueKe.DataValueField = "XueKeCode";
                ddlXueKe.DataBind();
                pnl非职高专业课.Visible =!helper._不需要资格证的岗位or学科.Contains( _GangWeiCode);
            }
        }
    }

    protected void ddlShiFanLei_SelectedIndexChanged(object sender, EventArgs e)
    {
        string isSFL = ddlShiFanLei.SelectedValue;
        if (isSFL == "是")
        {
            rfvZiGeZhengCode.EnableClientScript = false;
            rfvPTHNo.EnableClientScript = false;
        }
        else
        {
            rfvZiGeZhengCode.EnableClientScript = true;
            rfvPTHNo.EnableClientScript = true;
        }
    }
}