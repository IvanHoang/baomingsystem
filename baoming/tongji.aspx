<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tongji.aspx.cs" Inherits="admin_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="1" 
            style="border-color: #000000; border-collapse: collapse; font-size: small; text-align: center;"  
            cellpadding="0" cellspacing="0">
        <tr align="center"><td align="center" colspan="23" style ="font-size:x-large"><%=new SystemSetProvider().get年份() %>年某县教师考录网上报名人数统计表<br />（只显示已报名学科）</td></tr>
        <tr><td>报考职位代码</td>
            <%--<td>报考专业岗位</td>--%>
            <td>报考专业学科岗位</td><td>报名总人数</td></tr>
        
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
            <tr><td><%#Eval("XueKeCode") %></td>
                <%--<td><%#Eval("GangWeiName") %></td>--%>
                <td><%#Eval("XueKeName") %></td><td><%#Eval("num") %></td></tr>
            </ItemTemplate>
        </asp:Repeater>
       <%-- Convert.ToDateTime(e.Value).ToShortDateString(); --%>
    </table>
    </div>
    </form>
</body>
</html>
