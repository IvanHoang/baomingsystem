<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>首页_<%=new SystemSetProvider().get年份() %>年某县教师考录网上报名系统_某县教育局</title>
    <style type="text/css">
        table{border-left:1px solid #999;border-top:1px solid #999;line-height:220%;}
        th,td{border-right:1px solid #999;border-bottom:1px solid #999;}
    </style>
</head>
<body style="font-size: 11pt; text-align: center;">
    <form id="form1" runat="server" style="text-align:center">
        <table style="border-width: thin; border-color: #000000; width: 640px; background-color:#99CCFF; margin:0 auto;">
            <tr >
                <td style="font-size: 32px; text-align:center;line-height:220%;">
                    <%=new SystemSetProvider().get年份() %>年某县教师考录报名系统 <br /> 审核端</td>
            </tr>
            <tr>
                <td>
        <table style="width: 100%; color: #000000;">
            <tr>
                <td align="right">审核账号：</td>
                <td align="left">
                    <asp:TextBox ID="txtAdminName" runat="server" Width="180px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtAdminName" ErrorMessage="RequiredFieldValidator" 
                        ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">密码：</td>
                <td align="left">
                    <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password" Width="180px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtPassWord" ErrorMessage="RequiredFieldValidator" 
                        ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">验证码：</td>
                <td align="left" valign="middle">
                    <asp:TextBox ID="txtValidateNum" runat="server" Width="120px"></asp:TextBox>&nbsp;
                    <asp:Image ID="Image1" runat="server" Height="22px" Width="58px" style="vertical-align:bottom;" ImageUrl="~/ValidateNum.aspx"  onclick='javascript:this.src="/ValidateNum.aspx?"+Math.random();'/>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td align="left">
                    <asp:Button ID="btnLogin" runat="server" Text="审核人员 登录" ForeColor="DodgerBlue" 
                        onclick="btnLogin_Click" />
                </td>
            </tr>
            <tr><td colspan="2" >
            <p style="border-top-style: solid; border-width: 1px; padding-top: 10px;">联系方式：某县教育局人事科　null 林老师<br />
            Copyright© 2020 某县教育局 All rights reserved<br /> 
           null<br />
            </p>
            </td>
            </tr>
                </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
