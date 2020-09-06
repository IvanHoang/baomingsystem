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
        //★此处一定要加上if (!IsPostBack)，否则点击修改后，还是写入原来的值。
        if (!IsPostBack)
        {
            //取得session值            
            string _UserName = Session["UserName2012"].ToString();
            ////查询数据库，返回到Repeater控件中
            //DB db = new DB();
            //SqlConnection conn = db.GetCon();
            //conn.Open();
            //string strSql = "select * from tb_admin where UserName='" + _UserName + "'";
            //SqlDataAdapter sda = new SqlDataAdapter(strSql, conn);
            //conn.Close();
            //DataSet ds = new DataSet();
            //sda.Fill(ds);
            //lbUserName.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
            lbUserName.Text = _UserName;
        }
              
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //取得session值            
        string _UserName = Session["UserName2012"].ToString();      
        string _PassWord = txtPassWord.Text.Trim();
        using (var db = new teacherBaoMing_Entities())
        {
            var _user = db.Set<tb_admin>().FirstOrDefault(t => t.UserName == _UserName);
            if (_user != null)
            {
                _user.PassWord = Input.MD5(_PassWord);
                db.SaveChanges();
                Response.Write("<script>alert('修改成功！');location='User.aspx'</script>");
            }
            else
                Response.Write("<script>alert('修改失败！');</script>");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("User.aspx");
    }
}