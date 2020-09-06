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
    string username = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        username = Convert.ToString(Session["UserName2012"]);
        if (string.IsNullOrWhiteSpace(username))
        {
            Response.Write("<script>alert('需要重新登录！');location='../default.aspx'</script>");
            Response.End();
        }

        using (var dbHelper = new DBHelper())
        {
            if (!IsPostBack)
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
            }

            if (!dbHelper.is报名时间内() && !dbHelper.is再次报名时间内())
            {
                Response.Write("<script>alert('对不起，不在报名时间内！');location='../Default.aspx'</script>");
                Response.End();
            }
            else
            {
                //首先判断是否已经提交报名表，即tb_userinfo.UserName是否有等于Session["UserName2012"]记录
                var t_user = dbHelper.getUserList().FirstOrDefault(t => t.UserName == username);
                var t_admin = dbHelper.getAdminList().FirstOrDefault(t => t.UserName == username);
                if (dbHelper.is再次报名时间内())
                {
                    if (t_user == null)
                    {
                        Response.Write("<script>alert('报名时间已过！');location='../Default.aspx'</script>");
                        Response.End();
                    }
                    else
                    {
                        litTitle.Text = "※再次报考信息提交页";
                    }
                }
                else
                    litTitle.Text = "※用户报考信息提交页";

                if (t_user != null)
                {
                    if (t_user.AuditCode == "1")
                    {
                        Response.Write("<script>alert('前次的报名已经通过，不能再报名！');location='User.aspx'</script>");
                        Response.End();
                    }
                    if (t_user.AuditCode == "3")
                    {
                        Response.Write("<script>alert('你的报名信息已审核为不合适所有岗位，不能再报名！');location='User.aspx'</script>");
                        Response.End();
                    }
                }

                if (!IsPostBack)
                {
                    if (t_user != null)
                    {
                        txtXingMing.Text = t_user.XingMing; //ds.Tables[0].Rows[0]["XingMing"].ToString();
                        ddlSex.SelectedValue = t_user.SexCode; //ds.Tables[0].Rows[0]["Sex"].ToString();
                        litBirthday.Text = t_user.Birthday; //Convert.ToDateTime(ds.Tables[0].Rows[0]["Birthday"]).ToShortDateString();
                        ddlShengYuanDi.SelectedValue = t_user.ShengYuanDi; //ds.Tables[0].Rows[0]["ShengYuanDi"].ToString();
                        txtBiYeSchool.Text = t_user.BiYeSchool; //ds.Tables[0].Rows[0]["BiYeSchool"].ToString();
                        ddlGangWei.SelectedValue = t_user.tb_xueke.GangWeiCode.ToString(); //ds.Tables[0].Rows[0]["GangweiCode"].ToString();
                        ddlGangWei_SelectedIndexChanged(sender, e);
                        ddlXueKe.SelectedValue = t_user.XueKeCode.ToString(); //ds.Tables[0].Rows[0]["XueKeCode"].ToString();
                        ddlXueLi.SelectedValue = t_user.XueLiCode; //ds.Tables[0].Rows[0]["XueLi"].ToString();
                        txtBiYeTime.Text = t_user.BiYeTime.HasValue ? t_user.BiYeTime.Value.ToString("yyyy-MM-dd") : string.Empty; //Convert.ToDateTime(ds.Tables[0].Rows[0]["BiYeTime"]).ToShortDateString();
                        ddlQuanRiZhi.SelectedValue = t_user.QuanRiZhi; //ds.Tables[0].Rows[0]["QuanRiZhi"].ToString();
                        txtZhuanYe.Text = t_user.ZhuanYe; //ds.Tables[0].Rows[0]["ZhuanYe"].ToString();
                        ddlShiFanLei.SelectedValue = t_user.ShiFanLei; //ds.Tables[0].Rows[0]["ShiFanLei"].ToString();
                        ddlShiFanLei_SelectedIndexChanged(sender, e);
                        ddlDuSheng.SelectedValue = t_user.DuSheng; //ds.Tables[0].Rows[0]["DuSheng"].ToString();
                        txtMobile.Text = t_user.Mobile; //ds.Tables[0].Rows[0]["Mobile"].ToString();
                        txtAddress.Text = t_user.Address; //ds.Tables[0].Rows[0]["Address"].ToString();
                        txtTel.Text = t_user.Tel; //ds.Tables[0].Rows[0]["Tel"].ToString();
                        ddlXueQianWork.SelectedValue = t_user.XueQianWork; //ds.Tables[0].Rows[0]["XueQianWork"].ToString();
                        txtBiYeZhengShuCode.Text = t_user.BiYeZhengShuCode; //ds.Tables[0].Rows[0]["BiYeZhengShuCode"].ToString();
                        pnl非职高专业课.Visible = !helper._不需要资格证的岗位or学科.Contains(t_user.tb_xueke.GangWeiCode);

                        ddlMinZu.SelectedValue = t_user.MinZuCode;
                        ddlPoliticalOrientation.SelectedValue = t_user.PoliticalOrientationCode;
                        ddlZhiYe.SelectedValue = t_user.ZhiYeCode;

                        if (pnl非职高专业课.Visible)
                        {
                            txtZiGeZheng.Text = t_user.ZiGeZheng; //ds.Tables[0].Rows[0]["ZiGeZheng"].ToString();
                            txtZiGeZhengCode.Text = t_user.ZiGeZhengCode; //ds.Tables[0].Rows[0]["ZiGeZhengCode"].ToString();
                            ddlPTHLevel.SelectedValue = t_user.PTHLevel;
                            txtPTHNo.Text = t_user.PTHZSNo;

                            ddlXueKe_SelectedIndexChanged(sender, e);
                        }
                    }
                    else
                    {
                        //符合条件后，执行提交用户报考信息
                        if (username.Length == 18)
                            litBirthday.Text = username.Substring(6, 6).Insert(4,"-");
                    }

                    ltUserName.Text = username;
                    if (t_admin!=null)
                        imgIDCard.ImageUrl = ImgHelper.LoadImage(200, 150, t_admin.IDCardPIC);
                    else
                        imgIDCard.ImageUrl = ImgHelper.LoadImage(200, 150, null);
                    if (t_user != null)
                    {
                        imgPhoto.ImageUrl = ImgHelper.LoadImage(200, 150, t_user.IDPhoto);
                        picResidenceBooklet.ImageUrl = ImgHelper.LoadImage(200, 150, t_user.picResidenceBooklet);
                        picDiploma.ImageUrl= ImgHelper.LoadImage(200, 150, t_user.picDiploma);
                        picArchiveCertificate.ImageUrl= ImgHelper.LoadImage(200, 150, t_user.picArchiveCertificate);
                        picNewGraduates.ImageUrl= ImgHelper.LoadImage(200, 150, t_user.picNewGraduates);
                        picZiGeZheng.ImageUrl = ImgHelper.LoadImage(200, 150, t_user.picZiGeZheng);
                        picPTH.ImageUrl = ImgHelper.LoadImage(200, 150, t_user.picPTH);
                        picKindergartenCommitment.ImageUrl= ImgHelper.LoadImage(200, 150, t_user.picKindergartenCommitment);
                    }
                    else
                    {
                        imgPhoto.ImageUrl = ImgHelper.LoadImage(200, 150, null);
                        picResidenceBooklet.ImageUrl = ImgHelper.LoadImage(200, 150, null);
                        picDiploma.ImageUrl = ImgHelper.LoadImage(200, 150, null);
                        picArchiveCertificate.ImageUrl = ImgHelper.LoadImage(200, 150, null);
                        picNewGraduates.ImageUrl = ImgHelper.LoadImage(200, 150, null);
                        picZiGeZheng.ImageUrl = ImgHelper.LoadImage(200, 150, null);
                        picPTH.ImageUrl = ImgHelper.LoadImage(200, 150, null);
                        picKindergartenCommitment.ImageUrl = ImgHelper.LoadImage(200, 150, null);
                    }
                }
            }
        }
    }

    protected void btnSubmit(object sender, EventArgs e)
    {
        string _XingMing = txtXingMing.Text.Trim();
        string _Sex = ddlSex.SelectedValue;
        string _Birthday = username.Substring(6, 6);
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
            var _user = db.Set<tb_userinfo>().FirstOrDefault(t => t.UserName == username);
            bool needInsert = _user == null;
            if (needInsert)
            {
                _user = db.Set<tb_userinfo>().Create();
                _user.UserName = username;
            }

            _user.XingMing = _XingMing;
            _user.Birthday = _Birthday;
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
            _user.CreateDT = DateTime.Now;

            if (_GangWeiCode != 3)//职高
            {
                _user.ZiGeZheng = _ZiGeZheng;
                _user.ZiGeZhengCode = _ZiGeZhengCode;
                _user.PTHLevel = pthLevel;
                _user.PTHZSNo = pthNo;

                //判断 语文学科 必须是 二甲及以上
                string xueke = ddlXueKe.SelectedItem.Text;
                if (xueke == "语文" && pthLevel != "二甲及以上")
                {
                    //语文学科岗位要求普通话证书等级必须为二甲及以上
                    Response.Write("<script>alert('语文学科岗位要求普通话证书等级必须为二甲及以上!');</script>");
                    return;
                }
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
            var user0 = db.Set<tb_admin>().FirstOrDefault(t => t.UserName == username);
            string pic = string.Empty;
            if (user0 != null && FileUpload_ID.HasFile)
            {
                if (Input.UpLaodFile(FileUpload_ID, ref pic, ref reason, "IDCards"))
                {
                    user0.IDCardPIC = pic;
                }
                else
                    bsucc = false;
            }

            if (File_Photo.HasFile)
            {
                if (Input.UpLaodFile(File_Photo, ref pic, ref reason, "IDPhotos"))
                {
                    _user.IDPhoto = pic;
                }
                else
                    bsucc = false;
            }
            if(File_picResidenceBooklet.HasFile)
            {
                if (Input.UpLaodFile(File_picResidenceBooklet, ref pic, ref reason, "ResidenceBooklet"))
                {
                    _user.picResidenceBooklet = pic;
                }
                else
                    bsucc = false;
            }
            if (File_picDiploma.HasFile)
            {
                if (Input.UpLaodFile(File_picDiploma, ref pic, ref reason, "Diploma"))
                {
                    _user.picDiploma = pic;
                }
                else
                    bsucc = false;
            }
            if (File_picArchiveCertificate.HasFile)
            {
                if (Input.UpLaodFile(File_picArchiveCertificate, ref pic, ref reason, "ArchiveCertificate"))
                {
                    _user.picArchiveCertificate = pic;
                }
                else
                    bsucc = false;
            }
            if(File_picNewGraduates.HasFile)
            {
                if (Input.UpLaodFile(File_picNewGraduates, ref pic, ref reason, "NewGraduates"))
                {
                    _user.picNewGraduates = pic;
                }
                else
                    bsucc = false;
            }
            if(File_picZiGeZheng.HasFile)
            {
                if (Input.UpLaodFile(File_picZiGeZheng, ref pic, ref reason, "ZiGeZheng"))
                {
                    _user.picZiGeZheng = pic;
                }
                else
                    bsucc = false;
            }
            if (File_picPTH.HasFile)
            {
                if (Input.UpLaodFile(File_picPTH, ref pic, ref reason, "PTH"))
                {
                    _user.picPTH = pic;
                }
                else
                    bsucc = false;
            }
            if(File_picKindergartenCommitment.HasFile)
            {
                if (Input.UpLaodFile(File_picKindergartenCommitment, ref pic, ref reason, "KindergartenCommitment"))
                {
                    _user.picKindergartenCommitment = pic;
                }
                else
                    bsucc = false;
            }
            if (bsucc)
            {
                if (needInsert)
                    db.Set<tb_userinfo>().Add(_user);
                db.SaveChanges();
                Response.Write("<script>alert('修改成功!');location='user.aspx'</script>");
            }
            else
                Response.Write("<script>alert('提交失败！" + reason + "');location='user.aspx'</script>");
        }
    }

    protected void ddlGangWei_SelectedIndexChanged(object sender, EventArgs e)
    {
        int _GangWeiCode = Convert.ToInt32(ddlGangWei.SelectedValue);
        //取得所有2级类别：专业学科
        if (_GangWeiCode>0)
        {
            using (var dbHelper = new DBHelper())
            {
                var list = dbHelper.getXueKeList(_GangWeiCode);
                ddlXueKe.DataSource = list;
                ddlXueKe.DataTextField = "XueKeName";
                ddlXueKe.DataValueField = "XueKeCode";
                ddlXueKe.DataBind();
            }
            pnl非职高专业课.Visible = !helper._不需要资格证的岗位or学科.Contains(_GangWeiCode);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("User.aspx");
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

    protected void ddlXueKe_SelectedIndexChanged(object sender, EventArgs e)
    {
        string xueke = ddlXueKe.SelectedItem.Text;
        lblYWmust.Visible = xueke == "语文";

        int _XueKeCode = Convert.ToInt32(ddlXueKe.SelectedValue);
        pnl非职高专业课.Visible = !helper._不需要资格证的岗位or学科.Contains(_XueKeCode);
    }
}