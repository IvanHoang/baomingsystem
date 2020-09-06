<%@ Page Language="C#" AutoEventWireup="true" CodeFile="listforOnlyReg.aspx.cs" Inherits="admin_listforOnlyReg" MaintainScrollPositionOnPostback="True" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>已注册未报名的帐户列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="lstMain" runat="server">
    <table border="1" style="border-color: #000000; border-collapse: collapse; font-size: small; text-align: center;" cellpadding="0" cellspacing="0">
        <%--<tr><td colspan="25" align="left" style ="font-size:medium"><a href="../tongji.aspx" target="_blank">报考人数统计</a>&nbsp;|&nbsp;<a href="list.aspx">报名列表</a>&nbsp;|&nbsp;<a href="EditAdminPSW.aspx">修改管理员密码</a>&nbsp;|&nbsp;<a href="adminLogout.aspx">退出</a></td></tr>--%>
        <tr align="center"><td align="center" colspan="23" style ="font-size:x-large">未提交报名信息的注册帐户列表</td></tr>
        <tr><td>身份证</td><td>角色</td><td>操作</td></tr>
        <asp:Repeater ID="Repeater1" runat="server" 
            onitemcommand="Repeater1_ItemCommand">
            <ItemTemplate>
            <tr><td><%#Eval("UserName") %></td><td><%#Eval("roleName") %></td><td>
                <asp:Button ID="btnModifyPassword" runat="server" Text="修改密码" CommandName="modifyPassword" CommandArgument='<%#Eval("UserName") %>' />&nbsp;|&nbsp;<asp:Button ID="btnDelete" runat="server" Text="删除" CommandName="del" CommandArgument='<%#Eval("UserName") %>' OnClientClick='return confirm("确定要删除吗？");' /></td></tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </div>
       <table runat="server" id="pnlModifyPassword" visible="false">
        <tr><td align="center" colspan="2" style ="font-size:x-large">※修改用户密码密码</td></tr> 
        <tr><td>注册的身份证号：</td><td><asp:Label ID="lbUserName" runat="server" ></asp:Label><asp:HiddenField ID="hideID"
                runat="server" />
        </td></tr>
        <tr><td>密码修改为：</td><td>
            <asp:TextBox ID="txtPassWord" runat="server" 
                TextMode="Password" MaxLength="50"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtPassWord" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
            </td></tr>
        <tr><td>确认密码</td><td>
            <asp:TextBox ID="txtPassWordCheck" runat="server" 
                TextMode="Password" MaxLength="50"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="txtPassWordCheck" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtPassWord" ControlToValidate="txtPassWordCheck" 
                    ErrorMessage="两次输入密码不一致！" ForeColor="#CC0000"></asp:CompareValidator>
            </td></tr>
        <tr><td colspan="2" align="center">
            <asp:Button ID="btnOK" runat="server" Text="提交" onclick="btnOK_Click" />&nbsp;&nbsp;<asp:Button 
                ID="btnCancel" runat="server" Text="取消" CausesValidation="False" 
                onclick="btnCancel_Click"/></td></tr> 
   </table> 
    </form>
</body>
</html>
