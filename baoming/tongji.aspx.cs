using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class admin_list : System.Web.UI.Page
{
    static object lck = new object();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lock (lck)
            {
                //查询数据库，返回到Repeater控件中
                DataSet ds = Cache["__tongji"] as DataSet;
                if (ds == null)
                {
                    using (var db = new teacherBaoMing_Entities())
                    {
                        SqlConnection conn = new SqlConnection(db.Database.Connection.ConnectionString);
                        conn.Open();
                        string strSql = "SELECT xk.XueKeCode, xk.XueKeName, gw.GangWeiName, COUNT(u.id) AS num FROM tb_gangwei AS gw inner JOIN tb_xueke AS xk ON gw.GangweiCode = xk.GangWeiCode and gw.bUsed=1 LEFT JOIN tb_userinfo AS u ON xk.XueKeCode = u.XueKeCode GROUP BY xk.XueKeName, gw.GangWeiName, xk.XueKeCode ORDER BY xk.XueKeCode";
                        SqlDataAdapter sda = new SqlDataAdapter(strSql, conn);
                        ds = new DataSet();
                        sda.Fill(ds);
                        conn.Close();
                        Cache.Add("__tongji", ds, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(6), System.Web.Caching.CacheItemPriority.Default, null);
                    }
                }
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
        }
    }
}