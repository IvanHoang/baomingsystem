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

public partial class admin_list : System.Web.UI.Page
{
    string _Role = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断是否为管理员权限
        _Role=Convert.ToString(Session["Role2012_admin"]);
        if (string.IsNullOrWhiteSpace(_Role))
        {
            Response.Write("<script>alert('需要重新登录！');location='/AdminLogin.aspx'</script>");
            Response.End();
        }
        if (_Role == "0")
        {
            Response.Write("<script>alert('对不起，你无权访问此页面！');location='/Default.aspx'</script>");            
            Response.End();
        }
        if (!IsPostBack)
        {
            using (var dbHelper = new DBHelper())
            {
                //绑定报考学科列表
                var lstXueKe = dbHelper.getMyXueKeList(_Role);
                if (lstXueKe != null)
                {
                    if(!lstXueKe.Any(t=>t.XueKeCode==0))
                        lstXueKe.Insert(0, new tb_xueke() { XueKeCode = 0, XueKeName = "全部" });
                    ddlXueKeList.DataTextField = "XueKeName";
                    ddlXueKeList.DataValueField = "XueKeCode";
                    ddlXueKeList.DataSource = lstXueKe;
                    ddlXueKeList.SelectedIndex = 0;
                    ddlXueKeList.DataBind();
                }

                var lst = getDatas();
                rptList.DataSource = lst.OrderBy(t => t.id);
                rptList.DataBind();

                litInfo.Text = "记录数：" + lst.Count();
            }
        }
    }

    public List<UserListForAdmin> getDatas() {
        using (var dbHelper = new DBHelper())
        {
            var xuekeCode = Convert.ToInt32(ddlXueKeList.SelectedValue);
            var lst0 = dbHelper.getUserList().Include("tb_admin").AsQueryable();
            List<tb_userinfo> lst1 = null;
            if (_Role == "1")
            {
                if (xuekeCode > 0)
                    lst1 = lst0.Where(t => t.XueKeCode.HasValue && t.XueKeCode.Value == xuekeCode).ToList();
                else
                    lst1 = lst0.Where(t => t.XueKeCode.HasValue).ToList();
            }
            else
            {
                if (xuekeCode > 0)
                    lst1 = lst0.Where(t => t.XueKeCode.HasValue && t.XueKeCode.Value == xuekeCode).ToList();
                else
                {
                    var lstXueKe = dbHelper.getMyXueKeList(_Role).Select(t => t.XueKeCode).Distinct().ToList();
                    lst1 = lst0.Where(t => t.XueKeCode.HasValue).ToList().Where(t=>lstXueKe.Contains(t.XueKeCode.Value)).ToList();
                }
            }

            var lst = lst1.Select(t => new UserListForAdmin()
            {
                XingMing = t.XingMing,
                SexCode = t.SexCode,
                Sex = dbHelper.getXingBieParameterName(t.SexCode),
                UserName = t.UserName,
                Birthday = t.Birthday,
                ShengYuanDi = t.ShengYuanDi,
                Address = t.Address,
                BiYeSchool = t.BiYeSchool,
                XueLiCode = t.XueLiCode,
                XueLi = dbHelper.getXueLiParameterName(t.XueLiCode),
                BiYeTime = t.BiYeTime,
                QuanRiZhi = t.QuanRiZhi,
                ZhuanYe = t.ZhuanYe,
                ShiFanLei = t.ShiFanLei,
                ZiGeZheng = t.ZiGeZheng,
                ZiGeZhengCode = t.ZiGeZhengCode,
                DuSheng = t.DuSheng,
                Mobile = t.Mobile,
                Tel = t.Tel,
                BiYeZhengShuCode = t.BiYeZhengShuCode,
                XueQianWork = t.XueQianWork,
                GangWeiName = t.tb_xueke.tb_gangwei.GangWeiName,
                XueKeName = t.tb_xueke.XueKeName,
                XueKeCode = t.XueKeCode,
                AuditCode = t.AuditCode,
                AuditResult=dbHelper.getAuditActionParameterName(t.AuditCode),
                AuditFeedback = t.AuditFeedback,
                PTHLevel = t.PTHLevel,
                PTHZSNo = t.PTHZSNo,
                id = t.id,
                IDCardPIC = t.tb_admin.IDCardPIC,
                MinZuCode = t.MinZuCode,
                MinZu = dbHelper.getMinZuParameterName(t.MinZuCode),
                PoliticalOrientationCode = t.PoliticalOrientationCode,
                PoliticalOrientation = dbHelper.getZZMMParameterName(t.PoliticalOrientationCode),
                ZhiYeCode = t.ZhiYeCode,
                ZhiYe=dbHelper.getZhiYeParameterName(t.ZhiYeCode),
                CreateDT = t.CreateDT
            }).ToList();
            lst.ForEach(t => t.IDCardPIC = loadPic(t.IDCardPIC));
            return lst;
        }
    }

    public string loadPic(string IDCardPIC)
    {
        string ret = string.Empty;
        if (!string.IsNullOrWhiteSpace(IDCardPIC))
            ret = ImgHelper.LoadImage(300, 200, IDCardPIC).Replace("~", string.Empty);
        return ret;
    }

    protected void lbtnExport_Click(object sender, EventArgs e)
    {
        using (var dbHelper = new DBHelper())
        {
            //var _list = dbHelper.getUserList().Include("tb_admin").ToList().Select(t => new UserListForAdmin()
            //{
            //    XingMing = t.XingMing,
            //    SexCode = t.SexCode,
            //    Sex=dbHelper.getXingBieParameterName(t.SexCode),
            //    UserName = t.UserName,
            //    Birthday = t.Birthday,
            //    ShengYuanDi = t.ShengYuanDi,
            //    Address = t.Address,
            //    BiYeSchool = t.BiYeSchool,
            //    XueLiCode = t.XueLiCode,
            //    XueLi=dbHelper.getXueLiParameterName(t.XueLiCode),
            //    BiYeTime = t.BiYeTime,
            //    QuanRiZhi = t.QuanRiZhi,
            //    ZhuanYe = t.ZhuanYe,
            //    ShiFanLei = t.ShiFanLei,
            //    ZiGeZheng = t.ZiGeZheng,
            //    ZiGeZhengCode = t.ZiGeZhengCode,
            //    DuSheng = t.DuSheng,
            //    Mobile = t.Mobile,
            //    Tel = t.Tel,
            //    BiYeZhengShuCode = t.BiYeZhengShuCode,
            //    XueQianWork = t.XueQianWork,
            //    GangWeiName = t.tb_xueke.tb_gangwei.GangWeiName,
            //    XueKeName = t.tb_xueke.XueKeName,
            //    XueKeCode = t.XueKeCode,
            //    AuditCode=t.AuditCode,
            //    AuditResult =dbHelper.getAuditActionParameterName(t.AuditCode),
            //    AuditFeedback = t.AuditFeedback,
            //    PTHLevel = t.PTHLevel,
            //    PTHZSNo = t.PTHZSNo,
            //    id = t.id,
            //    IDCardPIC = t.tb_admin.IDCardPIC,
            //    MinZuCode = t.MinZuCode,
            //    MinZu=dbHelper.getMinZuParameterName(t.MinZuCode),
            //    PoliticalOrientationCode = t.PoliticalOrientationCode,
            //    PoliticalOrientation=dbHelper.getZZMMParameterName(t.PoliticalOrientationCode),
            //    ZhiYeCode = t.ZhiYeCode,
            //    ZhiYe=dbHelper.getZhiYeParameterName(t.ZhiYeCode),
            //    CreateDT = t.CreateDT
            //}).OrderByDescending(t => t.AuditCode).ThenBy(t => t.id).ToList();

            var _list = getDatas().OrderByDescending(t => t.AuditCode).ThenBy(t => t.id).ToList();
            if (_list == null || _list.Count() <= 0)
            {
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script type='text/javascript' defer>alert('没有找到需要导出的数据！');</script>");
                return;
            }
            else
            {
                #region 定义json头部

                string headJson = @"{ 'root':{'rowspan':2,'sheetname':'报名列表','defaultwidth':18,
'head':[ {'title':'教师招聘报名列表','cellregion':'0,0,0,36','height':40,'fontsize':23}, 
{'title':'编号','cellregion':'1,1,0,0'},
{'title':'姓名','cellregion':'1,1,1,1'},
{'title':'性别Code','cellregion':'1,1,2,2'}, {'title':'性别','cellregion':'1,1,3,3'}, 
{'title':'身份证','cellregion':'1,1,4,4'}, 
{'title':'出生年月','cellregion':'1,1,5,5'}, 
{'title':'民族Code','cellregion':'1,1,6,6'}, {'title':'民族','cellregion':'1,1,7,7'}, 
{'title':'政治面貌Code','cellregion':'1,1,8,8'},{'title':'政治面貌','cellregion':'1,1,9,9'}, 
{'title':'职业Code','cellregion':'1,1,10,10'}, {'title':'职业','cellregion':'1,1,11,11'}, 
{'title':'某生源','cellregion':'1,1,12,12'}, 
{'title':'家庭住址','cellregion':'1,1,13,13'}, 
{'title':'毕业学校','cellregion':'1,1,14,14'}, 
{'title':'学历Code','cellregion':'1,1,15,15'}, {'title':'学历','cellregion':'1,1,16,16'}, 
{'title':'毕业时间','cellregion':'1,1,17,17'}, 
{'title':'全日制','cellregion':'1,1,18,18'}, 
{'title':'专业','cellregion':'1,1,19,19'}, 
{'title':'师范类','cellregion':'1,1,20,20'}, 
{'title':'教师资格证类型','cellregion':'1,1,21,21'},
{'title':'教师资格证编号','cellregion':'1,1,22,22'},
{'title':'普通话等级','cellregion':'1,1,23,23'},
{'title':'普通话证号','cellregion':'1,1,24,24'},
{'title':'婚否','cellregion':'1,1,25,25'},
{'title':'手机','cellregion':'1,1,26,26'},
{'title':'电话','cellregion':'1,1,27,27'},
{'title':'毕业证书编号','cellregion':'1,1,28,28'},
{'title':'满5年','cellregion':'1,1,29,29'},
{'title':'报考岗位','cellregion':'1,1,30,30'},
{'title':'报考专业学科','cellregion':'1,1,31,31'},
{'title':'职位代码','cellregion':'1,1,32,32'},
{'title':'报名时间','cellregion':'1,1,33,33'},
{'title':'审核结果Code','cellregion':'1,1,34,34'},
{'title':'审核结果','cellregion':'1,1,35,35'},
{'title':'审核意见','cellregion':'1,1,36,36'}
]}
}";
                #endregion

                #region 开始导出
                new ExportBuilder<UserListForAdmin>().Column(a => a.id.ToString())
                    .Column(a => a.XingMing).Column(x => x.SexCode).Column(x => x.Sex)
                    .Column(a => a.UserName).Column(a => a.Birthday)
                    .Column(x => x.MinZuCode).Column(a => a.MinZu)
                    .Column(x => x.PoliticalOrientationCode).Column(a => a.PoliticalOrientation)
                    .Column(x => x.ZhiYeCode).Column(t => t.ZhiYe)
                    .Column(a => a.ShengYuanDi).Column(a => a.Address).Column(a => a.BiYeSchool)
                    .Column(x => x.XueLiCode).Column(a => a.XueLi)
                    .Column(a => a.BiYeTime.HasValue ? a.BiYeTime.Value.ToString("yyyy-MM-dd") : "").Column(a => a.QuanRiZhi).Column(a => a.ZhuanYe).Column(a => a.ShiFanLei).Column(a => a.ZiGeZheng).Column(a => a.ZiGeZhengCode)
                    .Column(a => a.PTHLevel).Column(a => a.PTHZSNo).Column(a => a.DuSheng).Column(a => a.Mobile).Column(a => a.Tel)
                    .Column(a => a.BiYeZhengShuCode).Column(a => a.XueQianWork).Column(a => a.GangWeiName).Column(a => a.XueKeName)
                    .Column(a => a.XueKeCode.HasValue ? a.XueKeCode.ToString() : "").Column(a => a.CreateDT.ToString("yyyy-MM-dd HH:mm:ss"))
                    .Column(a => a.AuditCode).Column(a => a.AuditResult).Column(a => a.AuditFeedback)
                    .Export(_list, DateTime.Now.Year + "教师招聘报名列表.xls", headJson);
                #endregion

            }
        }
    }

    protected void ddlXueKeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var lst = getDatas();
        rptList.DataSource = lst.OrderBy(t => t.id);
        rptList.DataBind();

        litInfo.Text = "记录数：" + lst.Count();

    }
}