﻿<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="regist" MaintainScrollPositionOnPostback="True" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
        table{border-left:1px solid #999;border-top:1px solid #999;}
        th,td{border-right:1px solid #999;border-bottom:1px solid #999;}
        .preview {
            position:absolute;
            margin-top:-60px;
            margin-left:1em;
        }
        .imghead {  
        filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=image);}
    </style>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
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
        <tr><td align="center" colspan="3" style ="font-size:x-large">※用户报考信息提交页</td></tr>
        <tr><td align="center" colspan="3" style ="font-size:x-large"></td></tr>        
        <tr><td align="right">身份证：</td><td><asp:Literal ID="ltUserName" runat="server"></asp:Literal></td>
            <td rowspan="10" style="position:relative;">
                <asp:Image ID="imgIDCard" runat="server" /><br />
                身份证正面图片：<asp:FileUpload ID="FileUpload_ID" runat="server"  onchange="previewImage(this,'preview_imgIDCard')"/>
                <span id="preview_imgIDCard" class="preview"> </span><br />
                <ul style="color:Red;">
                    <li>图片中的身份证号必须与左边显示的身份证号要一致！</li>
                    <li>请在<strong>上传前截好图（截掉无关部分，并确保图文清晰）。</strong></li>
                    <li>图片格式为jpg或png格式，不能大于4M。</li>
                </ul>
            </td></tr>
        <tr><td align="right">姓名：</td><td><asp:TextBox ID="txtXingMing" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtXingMing" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr><td align="right">性别：</td><td><asp:DropDownList ID="ddlSex" runat="server"></asp:DropDownList></td>
        </tr>
        <tr><td align="right">出生年月：</td><td>
            <asp:Literal ID="litBirthday" runat="server"></asp:Literal>
                </td></tr>
        <tr><td align="right">民族：</td><td><asp:DropDownList ID="ddlMinZu" runat="server"></asp:DropDownList></td></tr>
        <tr><td align="right">政治面貌：</td><td><asp:DropDownList ID="ddlPoliticalOrientation" runat="server"></asp:DropDownList></td>
        </tr>
        <tr><td align="right">职业：</td><td><asp:DropDownList ID="ddlZhiYe" runat="server"></asp:DropDownList></td>
        </tr>
        <tr><td align="right">毕业学校：</td><td><asp:TextBox ID="txtBiYeSchool" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="txtBiYeSchool" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
            </td></tr>
        <tr><td align="right">学历：</td><td><asp:DropDownList ID="ddlXueLi" runat="server"></asp:DropDownList></td></tr>
        <tr><td align="right">毕业时间：</td><td><asp:TextBox ID="txtBiYeTime" runat="server" onClick="WdatePicker()"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="txtBiYeTime" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                </td></tr>
        <tr><td align="right">是否全日制：</td><td><asp:DropDownList ID="ddlQuanRiZhi" runat="server">
            <asp:ListItem>是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
            </asp:DropDownList></td></tr>
        <tr><td align="right">专业：</td><td><asp:TextBox ID="txtZhuanYe" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="txtZhuanYe" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
            </td></tr>
        <tr><td align="right">是否师范类：</td><td><asp:DropDownList ID="ddlShiFanLei" 
                runat="server" onselectedindexchanged="ddlShiFanLei_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem>是</asp:ListItem>
            <asp:ListItem>否</asp:ListItem>
            </asp:DropDownList></td></tr>
        <tr><td align="right">毕业证书电子注册号：</td><td><asp:TextBox ID="txtBiYeZhengShuCode" runat="server"></asp:TextBox></td>
            <td rowspan="6" style="position:relative;">
                <asp:Image ID="imgPhoto" runat="server" /><br />
                证件照片：<asp:FileUpload ID="File_Photo" runat="server"  onchange="previewImage(this,'preview_imgPhoto')"/>
                <span id="preview_imgPhoto" class="preview"> </span><br />
               <ul style="color:Red;"><li>此照片将用于报名表头像和准考证头像，必须上传。</li>
                    <li>请上传标准证件照(单寸照)：近照、免冠。</li>
                    <li>请在<strong>上传前截好图（截掉与照片无关部分，并确保图片清晰）。</strong></li>
                    <li>图片格式为jpg或png格式，不能大于4M。</li></ul>
            </td></tr>
        <tr><td align="right">婚否：</td><td><asp:DropDownList ID="ddlDuSheng" runat="server">
            <asp:ListItem Value="已婚" Selected="True">已婚</asp:ListItem>
            <asp:ListItem Value="未婚">未婚</asp:ListItem>
            </asp:DropDownList></td><td></td></tr>
        <tr><td align="right">联系手机：</td><td><asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ControlToValidate="txtMobile" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                        ControlToValidate="txtMobile" ErrorMessage="请正确输入11位手机号！" ForeColor="#CC0000" 
                        ValidationExpression="^(1[0-9][0-9]|15[0|3|6|8|9])\d{8}$"></asp:RegularExpressionValidator>
                </td><td></td></tr>
        <tr><td align="right">家庭住址：</td><td><asp:TextBox ID="txtAddress" runat="server" 
                Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ControlToValidate="txtAddress" ErrorMessage="RequiredFieldValidator" 
                ForeColor="#CC0000">*</asp:RequiredFieldValidator>
            </td><td></td></tr>
        <tr><td align="right">联系电话：</td><td><asp:TextBox ID="txtTel" runat="server"></asp:TextBox></td><td></td></tr>
        
        <tr><td align="right">报考岗位：</td><td><asp:DropDownList ID="ddlGangWei" 
                runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlGangWei_SelectedIndexChanged"></asp:DropDownList></td><td></td></tr>
        <tr><td align="right">报考专业学科：</td><td><asp:DropDownList ID="ddlXueKe" runat="server"></asp:DropDownList></td><td></td></tr>

        <asp:Panel ID="pnl非职高专业课" runat="server">
        <tr><td align="right">取得何种教师资格证：</td><td colspan="2"><asp:TextBox ID="txtZiGeZheng" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                ID="RequiredFieldValidator9" runat="server" ErrorMessage="此项除职高专业课外，必须输入！" ControlToValidate="txtZiGeZheng" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />（如：高中语文、初中数学、小学音乐、幼儿园或或填写已取得XXX学科合格证明）</td></tr>
        <tr><td align="right">教师资格证编号：</td><td colspan="2"><asp:TextBox ID="txtZiGeZhengCode" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                ID="rfvZiGeZhengCode" runat="server" ErrorMessage="此项除职高专业课外，必须输入！" ControlToValidate="txtZiGeZhengCode" ForeColor="Red"></asp:RequiredFieldValidator><br />
            (教师资格证编号或填写已取得全国中小学教师资格证XXX学科合格证编号)</td></tr>
        <tr><td align="right">取得普通话证书等级：</td><td><asp:DropDownList ID="ddlPTHLevel" 
                runat="server">
            <asp:ListItem Selected="True">二乙</asp:ListItem>
            <asp:ListItem>二甲及以上</asp:ListItem>
            </asp:DropDownList></td><td>&nbsp;</td></tr>
        <tr><td align="right">普通话证书号：</td><td><asp:TextBox ID="txtPTHNo" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                ID="rfvPTHNo" runat="server" ErrorMessage="此项除职高专业课外，必须输入！" ControlToValidate="txtPTHNo" ForeColor="Red"></asp:RequiredFieldValidator></td><td></td></tr>
        </asp:Panel>

        <tr>
            <td align="right">是否某生源或户籍：</td>
            <td><asp:DropDownList ID="ddlShengYuanDi" runat="server">
                <asp:ListItem>是</asp:ListItem>
                <asp:ListItem>否</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td align="right">是否在我县从事幼儿教育满5年：</td>
            <td><asp:DropDownList ID="ddlXueQianWork" runat="server">
                <asp:ListItem Selected="True">否</asp:ListItem>
                <asp:ListItem>是</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr><td></td><td colspan="2"><asp:Button ID="Button1" runat="server" Text="提交" 
                onclick="Button1_Click" />&nbsp;&nbsp;<a href="javascript:window.opener=null;window.open('','_self');window.close();">关闭页面</a></td></tr>
        
    </table>
    </div>
    </form>
</body>
</html>
