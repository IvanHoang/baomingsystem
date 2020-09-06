<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditPSW.aspx.cs" Inherits="admin_audit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   <table>
        <tr><td align="center" colspan="2" style ="font-size:x-large">※用户修改密码模块</td></tr>
        <tr><td align="center" colspan="2" style ="font-size:x-large"></td></tr> 
        <tr><td>身份证：</td><td><asp:Label ID="lbUserName" runat="server" ></asp:Label></td></tr>
        <tr><td>密码修改为：</td><td><asp:TextBox ID="txtPassWord" runat="server" 
                TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtPassWord" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
            </td></tr>
        <tr><td>确认密码</td><td><asp:TextBox ID="txtPassWordCheck" runat="server" 
                TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="txtPassWordCheck" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtPassWord" ControlToValidate="txtPassWordCheck" 
                    ErrorMessage="两次输入密码不一致！" ForeColor="#CC0000"></asp:CompareValidator>
            </td></tr>
        <tr><td colspan="2" align="center">
            <asp:Button ID="Button1" runat="server" 
                Text="提交" onclick="Button1_Click" />&nbsp;&nbsp;<asp:Button ID="Button2" 
                runat="server" Text="取消" CausesValidation="False" onclick="Button2_Click" /></td></tr> 
   </table> 
    </div>
    </form>
</body>
</html>
