using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_adminLogout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //FormsAuthentication.SignOut();
        //Session.Abandon();
        Session.Remove("UserName2012_admin");
        Session.Remove("Role2012_admin");
        Response.Redirect("~/AdminLogin.aspx");
    }
}