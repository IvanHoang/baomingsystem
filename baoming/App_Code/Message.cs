using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 网页页面上的消息提示类
/// </summary>
public class Message
{
    /// <summary>
    /// 显示模式消息
    /// </summary>
    /// <param name="page"></param>
    /// <param name="message"></param>
    public static void alter(System.Web.UI.Page page, string message)
    {
        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "info", "<script type=\"text/javascript\" defer>alert(\"" + message + "\");</script>");
    }

    /// <summary>
    /// 显示模式消息
    /// </summary>
    /// <param name="page"></param>
    /// <param name="message"></param>
    public static void alter1(System.Web.UI.Page page, string message)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "info", "<script type='text/javascript' defer>alert('" + message + "');</script>");
    }

    /// <summary>
    /// 显示模式消息，并跳转到页面
    /// </summary>
    /// <param name="page"></param>
    /// <param name="message"></param>
    public static void alter_Goto(System.Web.UI.Page page, string message, string location)
    {
        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "info", "<script type='text/javascript' defer>alert('" + message + "');window.location='" + location + "'</script>");
    }

    /// <summary>
    /// 执行代码
    /// </summary>
    /// <param name="page"></param>
    /// <param name="message"></param>
    public static void doRun(System.Web.UI.Page page, string message)
    {
        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "info", "<script type='text/javascript' defer>" + message + "</script>");
    }
}