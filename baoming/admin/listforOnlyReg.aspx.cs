using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_listforOnlyReg : System.Web.UI.Page
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
            refreshList();
        }
    }

    private void refreshList()
    {
        using (var db = new teacherBaoMing_Entities())
        {
            //查询数据库，返回到Repeater控件中
            var lst = db.tb_admin.Where(t => t.Role == "0" && t.tb_userinfo.Count() <= 0);
            Repeater1.DataSource = lst.Select(t => new { t.UserName, roleName = (t.Role == "0") ? "" : "管理员" }).OrderBy(t => t.UserName).ToList();
            Repeater1.DataBind();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlModifyPassword.Visible = false;
        lstMain.Visible = true;
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        using (var db = new teacherBaoMing_Entities())
        {
            string UserName = hideID.Value;
            var _entity = db.tb_admin.FirstOrDefault(t => t.UserName == UserName);
            if (_entity != null)
            {
                string pass = txtPassWord.Text.Trim();
                if (pass == txtPassWordCheck.Text.Trim())
                {
                    _entity.PassWord = Input.MD5(pass);
                    db.SaveChanges();
                    pnlModifyPassword.Visible = false;
                    lstMain.Visible = true;
                }
                else
                    Message.alter(this, "两次密码输入不一致，请重新录入！");
            }
            else
                Message.alter(this, "没有找到对应的数据，请点取消！");
        }
    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string UserName = Convert.ToString(e.CommandArgument);
        using (var db = new teacherBaoMing_Entities())
        {
            var _entity = db.tb_admin.FirstOrDefault(t => t.UserName == UserName);
            if (_entity == null)
                refreshList();
            else
            {
                if (e.CommandName == "modifyPassword")
                {
                    pnlModifyPassword.Visible = true;
                    lstMain.Visible = false;
                    lbUserName.Text = _entity.UserName;
                    hideID.Value = _entity.UserName;
                    refreshList();
                }
                else if (e.CommandName == "del")
                {
                    if (db.tb_userinfo.Any(t => t.UserName == _entity.UserName))
                    {
                        Message.alter(this, "该用户已经提交报名表了，删除失败！");
                    }
                    else
                    {
                        db.tb_admin.Remove(_entity);
                        db.SaveChanges();
                    }
                    refreshList();
                }
            }
        }
    }
}