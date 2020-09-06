<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>首页_<%=new SystemSetProvider().get年份() %>年某县教师考录网上报名系统_某县教育局</title>
    <style type="text/css">
        table{border-left:1px solid #999;border-top:1px solid #999;}
        th,td{border-right:1px solid #999;border-bottom:1px solid #999;}
    </style>
</head>
<body style="font-size: 11pt; text-align: center;">
    <form id="form1" runat="server" style="text-align:center">
    <table width="100%">
        <tr>
            <td align="center">
        <table  bgcolor="#99CCFF" 
                    style="border-width: thin; border-color: #000000; width: 640px;">
            <tr >
                <td style="height: 80px; font-size: 32px; text-align:center">
                    <%=new SystemSetProvider().get年份() %>年某县教师考录网上报名系统</td>
            </tr>
            <tr>
                <td>
        <table style="width: 100%; color: #000000;">
            <tr>
                <td align="right">
                    身份证号：</td>
                <td align="left" class="style2">
                    <asp:TextBox ID="txtUserName" runat="server" Width="180px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtUserName" ErrorMessage="RequiredFieldValidator" 
                        ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtUserName" ErrorMessage="不是有效身份证！" ForeColor="#CC0000" 
                        ValidationExpression="\d{17}[\d|X]|\d{15}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    密码：</td>
                <td align="left" class="style2">
                    <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password" Width="180px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtPassWord" ErrorMessage="RequiredFieldValidator" 
                        ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <!-- <tr>
                <td align="right">
                    验证码：</td>
                <td align="left" class="style2" valign="middle">
                    <asp:TextBox ID="txtValidateNum" runat="server" Width="120px"></asp:TextBox>&nbsp;
                    <asp:Image ID="Image1" runat="server" Height="22px" Width="58px" style="vertical-align:bottom;" ImageUrl="~/ValidateNum.aspx"  onclick='javascript:this.src="/ValidateNum.aspx?"+Math.random();'/>
                </td>
            </tr> -->
            <tr>
                <td class="style1" style="border-width: 1px; border-color: #000000;">
                </td>
                <td align="left" class="style2">
                    <asp:Button ID="btnLogin" runat="server" Text="登录" ForeColor="DodgerBlue" 
                        onclick="btnLogin_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnRegister"
                        runat="server" Text="注册" ForeColor="DodgerBlue" 
                        onclick="btnRegister_Click" CausesValidation="False" /></td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                <strong> 报考流程：</strong><br />
                1、<strong>网上注册报名。</strong>时间：<%=new SystemSetProvider().get报名开始时间().ToString("yyyy年MM月dd日 00:00") %>--<%=new SystemSetProvider().get报名结束时间().ToString("yyyy年MM月dd日 24:00") %><br />
                    ①使用个人身份证注册账号。②登录系统，并填写报考信息。<br />
                2、<strong>资格初审。</strong>时间：<%=new SystemSetProvider().get年份() %>年3月06日0:00--3月07日24:00<br />
                3、<strong>查询初审结果并再次报名。</strong>时间：<%=new SystemSetProvider().get年份() %>年3月08日0:00—6月21日24:00<br />
                4、<strong>打印报名表。</strong>网上审核通过后自行打印。<br />
                5、<strong>现场确认。</strong>时间：<%=new SystemSetProvider().get年份() %>年？月22日--？月24日（上午8:30--11:30，下午14:00--17:00），逾期不予确认。地点：某县教育局<br />
                6、<strong>网上打印笔试准考证。</strong>时间：<%=new SystemSetProvider().get年份() %>年？月29日0:00:00至<%=new SystemSetProvider().get年份() %>年？月30日24:00<br />
                </td>
            </tr>
            <tr><td colspan="2" align="center" style="height: 60px;" >
            <p style="border-top-style: solid; border-width: 1px; padding-top: 10px;">联系方式：某县教育局<br />
            </p>
            </td>
            </tr>
                </table>
                </td>
            </tr>
        </table>
        </td>
        </tr>
    </table>
    </form>
</body>
</html>
