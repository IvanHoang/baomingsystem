<%@ Page Language="C#" AutoEventWireup="true" CodeFile="audit.aspx.cs" Inherits="admin_audit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/jquery.rotate.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var param = {
                right: $("#rotRight"),
                left: $("#rotLeft"),
                box: $("#imgBox"),
            };
            var fun = {
                right: function () {
                    $("#imgIDCard").rotate(90);
                    return false;
                },
                left: function () {
                    $("#imgIDCard").rotate(-90);
                    return false;
                }
            };
            param.right.click(function () {
                fun.right();
                return false;
            });
            param.left.click(function () {
                fun.left();
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="border:1px solid #999; min-width:1280px;width:100%;">
            <h2 style="text-indent:1em;">※资格审核模块</h2>
        <table border="1" cellpadding="5" cellspacing="0" style="float:left;margin-right:20px; width:350px;">
            <tr>
                <td align="center" colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Image ID="imgPhoto" runat="server" /></td>
            </tr>
            <tr>
                <td>姓名：</td>
                <td>
                    <asp:Label ID="lbXingMing" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>身份证：</td>
                <td>
                    <asp:Label ID="lbUserName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>某户籍或生源：</td>
                <td>
                    <asp:Label ID="lbShengYuanDi" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>报考岗位：</td>
                <td>
                    <asp:Label ID="lbGangWeiName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>报考专业学科：</td>
                <td>
                    <asp:Label ID="lbXueKeName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>专业：</td>
                <td>
                    <asp:Label ID="lbZhuangye" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>学历：</td>
                <td>
                    <asp:Label ID="lbXueLi" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>是否师范类：</td>
                <td>
                    <asp:Label ID="lbShiFanLei" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>教师资格证类型：</td>
                <td>
                    <asp:Label ID="lbJiaoShiZGZ" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>教师资格证编号：</td>
                <td>
                    <asp:Label ID="lbJiaoShiZGZNo" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>普通话等级：</td>
                <td>
                    <asp:Label ID="lbPTHLevel" runat="server"></asp:Label></td>
            </tr>
            <tr><td colspan="2" style="border:0px;">&nbsp;</td></tr>
            <tr>
                <td colspan="2">
                    <strong>审核结果：</strong>
                    <asp:DropDownList ID="ddlAuditResult" runat="server"></asp:DropDownList>
                <br />
                    <asp:Literal ID="litAuditRemark" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr><td colspan="2" style="border:0px;">&nbsp;</td></tr>
            <tr>
                <td colspan="2"><strong>审核意见：</strong><br />
                    <asp:TextBox ID="txtAuditFeedback" runat="server" TextMode="MultiLine" Rows="4" Width="98%" MaxLength="50"></asp:TextBox>
                    （建议35字以内！）</td>
            </tr>
            <tr><td colspan="2" style="border:0px;">&nbsp;</td></tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="Button1" runat="server"
                        Text="提交审核结果" OnClick="btnSubmit" />&nbsp;&nbsp;<a href="javascript:window.opener=null;window.open('','_self');window.close();">关闭页面</a></td>
            </tr>
        </table>

    <table border="1" cellspacing="0">
        <tr>
            <td align="center" valign="top">
                <div id="imgBox" class="image_box">
                    身份证照片：<br />
                    <asp:Image ID="imgIDCard" runat="server" /><br />
                    <input type="button" id="rotLeft" style="margin-right:4em;" value="&lt;&lt;向左旋转" /><input type="button" id="rotRight" value="向右旋转&gt;&gt;" />
                </div>
            </td>
            <td align="center" valign="top">
                <div class="image_box">
                    户口簿（本人页）原件照：<br />
                    <asp:Image ID="picResidenceBooklet" runat="server" />
                </div>
            </td>
            <td align="center" valign="top">
                <div class="image_box">
                    档案寄存单位的存档证明<br />（或档案查询手机截图）：<br />
                    <asp:Image ID="picArchiveCertificate" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <div class="image_box">
                    毕业证书原件照片：<br />
                    <asp:Image ID="picDiploma" runat="server" />
                </div>
            </td>
            <td align="center" valign="top">
                <div class="image_box">
                    应届毕业生学校证明：<br />
                    <asp:Image ID="picNewGraduates" runat="server" />
                </div>
            </td>
            <td align="center" valign="top">
                <div class="image_box">
                    教师资格证原件照：<br />
                    <asp:Image ID="picZiGeZheng" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <div class="image_box">
                    普通话证书原件照：<br />
                    <asp:Image ID="picPTH" runat="server" />
                </div>
            </td>
            <td align="center" valign="top">
                <div class="image_box">
                    幼儿园承诺书：<br />
                    <asp:Image ID="picKindergartenCommitment" runat="server" />
                </div>
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
    </table>
 
        </div>
   </form>
</body>
</html>
