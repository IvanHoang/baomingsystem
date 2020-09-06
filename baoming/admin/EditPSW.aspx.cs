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
            //接收页面传值UserName            
            string _UserName = Request.QueryString["UserName"];
            //查询数据库，返回到Repeater控件中
            //DB db = new DB();
            //SqlConnection conn = db.GetCon();
            //conn.Open();
            string strSql = "select tb_userinfo.*,XueKeName,GangWeiName from tb_userinfo,tb_xueke,tb_gangwei where tb_userinfo.XueKeCode = tb_xueke.XueKeCode and tb_xueke.GangWeiCode = tb_gangwei.GangweiCode and tb_userinfo.UserName='" + _UserName + "'";
            //SqlDataAdapter sda = new SqlDataAdapter(strSql, conn);
            //conn.Close();
            //DataSet ds = new DataSet();
            //sda.Fill(ds);
            using (var db = new teacherBaoMing_Entities())
            {
                var _user = db.Set<tb_userinfo>().FirstOrDefault(t => t.UserName == _UserName);
                if (_user != null)
                {
                    lbXingMing.Text = _user.XingMing;
                    lbUserName.Text = _user.UserName;
                    lbXueKeName.Text = _user.tb_xueke.XueKeName;
                    lbGangWeiName.Text = _user.tb_xueke.tb_gangwei.GangWeiName;
                }
                //lbXingMing.Text = ds.Tables[0].Rows[0]["XingMing"].ToString();
                //lbUserName.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
                //lbXueKeName.Text = ds.Tables[0].Rows[0]["XueKeName"].ToString();
                //lbGangWeiName.Text = ds.Tables[0].Rows[0]["GangWeiName"].ToString();    
            }
        }
              
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //接收页面传值UserName            
        string _UserName = Request.QueryString["UserName"];       
        string _PassWord = txtPassWord.Text.Trim();
        using (var db = new teacherBaoMing_Entities())
        {
            var _admin = db.Set<tb_admin>().FirstOrDefault(t => t.UserName == _UserName);
            if (_admin != null)
            {
                _admin.PassWord = Input.MD5(_PassWord);
                db.SaveChanges();
                Response.Write("<script>alert('修改成功!');location.replace(location.href);</script>");
            }
            else
                Response.Write("<script>alert('修改失败！');'</script>");

            //string strSql = string.Format("update tb_admin set PassWord='{0}' where UserName='{1}'", _PassWord, _UserName);        
            //SqlConnection conn = db.GetCon();
            //SqlCommand cmd = new SqlCommand(strSql,conn);
            //conn.Open();
            //cmd.ExecuteNonQuery();
            //conn.Close();     
            //Response.Redirect("list.aspx");
        }
    }
}