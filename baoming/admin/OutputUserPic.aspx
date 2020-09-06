<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutputUserPic.aspx.cs" Inherits="admin_OutputUserPic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>报名照片导出</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="1" style="border-color: #000000; border-collapse: collapse;">
                <tr>
                    <td align="left" style="font-size: medium"><a href="list.aspx">返回上一层功能</a></td>
                </tr>
                <tr>
                    <td>
                        报名照片可以以身份证号命名导出。<br />
                        此处操作，耗时较久，请耐心等待。<br />
                        生成成功以后，点“下载”可以下载到打包的文件！<br /><br />
                        <asp:Literal ID="litMessage" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="重新生成" OnClick="btnRefresh_Click" />该处理需要大约3-8分钟，请耐心等待，不要重复点击！报名结束以后，只需执行一次即可。<br />
                        <asp:Button ID="btnOutput" runat="server" Text="打包下载"  OnClick="btnOutput_Click"/>该处理需要大约半分钟，请在“重新生成”执行完成以后使用。
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
