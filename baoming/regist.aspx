<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="regist.aspx.cs" Inherits="regist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        table{border-left:1px solid #999;border-top:1px solid #999;}
        th,td{border-right:1px solid #999;border-bottom:1px solid #999;}
        .preview {
            position:absolute;
            margin-top:0px;
            margin-left:15em;
        }
        .imghead {  
        filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=image);}
    </style>
    <script type="text/javascript">  
        //图片上传预览  IE用滤镜。  
        function previewImage(file,previewDivID) {
            var MAXWIDTH = 130;
            var MAXHEIGHT = 90;
            var div = document.getElementById(previewDivID);
            var _id = file.id + 'imghead';
            div.innerHTML = '<img id='+_id+'>';
            var img = document.getElementById(_id);
            if (file.files && file.files[0]) {
                img.onload = function () {
                    var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
                    img.width = rect.width;
                    img.height = rect.height;
                    // img.style.marginLeft = rect.left+'px';  
                    img.style.marginTop = rect.top + 'px';
                }
                var reader = new FileReader();
                reader.onload = function (evt) { img.src = evt.target.result; }
                reader.readAsDataURL(file.files[0]);
            }
            else //兼容IE  
            {
                var sFilter = 'filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src="';
                file.select();
                var src = document.selection.createRange().text;
                img.filters.item('DXImageTransform.Microsoft.AlphaImageLoader').src = src;
                var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
                status = ('rect:' + rect.top + ',' + rect.left + ',' + rect.width + ',' + rect.height);
                div.innerHTML = "<div id=divhead style='width:" + rect.width + "px;height:" + rect.height + "px;margin-top:" + rect.top + "px;" + sFilter + src + "\"'></div>";
            }
        }
        function clacImgZoomParam(maxWidth, maxHeight, width, height) {
            var param = { top: 0, left: 0, width: width, height: height };
            if (width > maxWidth || height > maxHeight) {
                rateWidth = width / maxWidth;
                rateHeight = height / maxHeight;

                if (rateWidth > rateHeight) {
                    param.width = maxWidth;
                    param.height = Math.round(height / rateWidth);
                } else {
                    param.width = Math.round(width / rateHeight);
                    param.height = maxHeight;
                }
            }

            param.left = Math.round((maxWidth - param.width) / 2);
            param.top = Math.round((maxHeight - param.height) / 2);
            return param;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr><td align="center" colspan="3" style ="font-size:x-large">※注册新用户</td></tr>
        <tr><td colspan="3">
            <ul style="color:Red;margin-bottom:0.5em;margin-top:0.5em;">
                <li>图片中的身份证号必须与左边显示的身份证号要一致！</li>
                <li>请在<strong>上传前截好图（截掉无关部分，并确保图文清晰）。</strong></li>
                        <li>图片格式为jpg或png格式，不能大于4M。</li>
                        </ul>
            </td></tr>
        <tr><td>身份证正面图片：</td><td style="position:relative">
            <asp:FileUpload ID="FileUpload_ID" runat="server"  onchange="previewImage(this,'preview_img')"/>
            <span id="preview_img" class="preview"> </span>
            </td><td>
            </td></tr>
        <tr><td>身份证号(18位)：</td><td><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtUserName" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
            </td><td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtUserName" ErrorMessage="不是有效身份证！" ForeColor="#CC0000" 
                    ValidationExpression="\d{17}[\d|X]|\d{15}"></asp:RegularExpressionValidator>
            </td></tr>
        <tr><td>密码：</td><td><asp:TextBox ID="txtPassWord" runat="server" 
                TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtPassWord" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
            </td><td>&nbsp;</td></tr>
        <tr><td>确认密码：</td><td><asp:TextBox ID="txtPassWordCheck" runat="server" 
                TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="txtPassWordCheck" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
            </td><td>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtPassWord" ControlToValidate="txtPassWordCheck" 
                    ErrorMessage="两次输入密码不一致！" ForeColor="#CC0000"></asp:CompareValidator>
            </td></tr>
        <tr><td>验证码：</td>
        <td valign="middle">
            <asp:TextBox ID="txtValidateNum" runat="server" Width="98px"></asp:TextBox>
            <asp:Image ID="Image1" runat="server" Height="22px" Width="58px" style="vertical-align:bottom;" ImageUrl="~/ValidateNum.aspx" onclick='javascript:this.src="/ValidateNum.aspx?"+Math.random();'/>
        </td><td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="txtValidateNum" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
            <asp:Label ID="labInfo" runat="server" ForeColor="Red"></asp:Label>
            </td></tr>
        <tr><td></td><td colspan="2">
            <asp:Button ID="btnOK" runat="server" Text="注册" 
                onclick="btnOK_Click" />&nbsp;&nbsp;<asp:Button ID="Button2" runat="server" 
                Text="取消" CausesValidation="False" onclick="Button2_Click" /></td></tr>
        
    </table>
    </div>
    </form>
</body>
</html>
