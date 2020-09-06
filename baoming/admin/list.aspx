<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="admin_list" MaintainScrollPositionOnPostback="True" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
a,img{border:0;}
ul,li{list-style-type:none;}
/* demo */
.demo{width:600px;margin:40px auto;}
.demo h2{font-size:14px;height:62px;color:#3366cc;}
.demo a{font-size:14px;color:#333;text-decoration:none;}
.demo a img{border:solid 1px #ddd;margin:0 5px;}
/* previewShowWindow */
.previewShowWindow{position:absolute;border:1px solid #dadade;background:#95959d;padding:5px;display:none;color:#fff;text-align:center;z-index:999;}
/* 自己调用的时候·可以删除下面的样式 */
body {background-image:url(about:blank);background-attachment:fixed;}
.shortcut{position:absolute;top:expression(eval(document.documentElement.scrollTop));}
.shortcut{height:28px;line-height:28px;font-size:12px;background:#EEEEEE;text-transform:uppercase;box-shadow:1px 0px 2px rgba(0,0,0,0.2);border-bottom:1px solid #DDDDDD;}
.shortcut h1{font-size:14px;font-family:"微软雅黑","宋体";}
.shortcut a,.shortcut h1{padding:0px 10px;letter-spacing:1px;color:#333;text-shadow:0px 1px 1px #fff;display:block;float:left;}
.shortcut a:hover{background:#fff;}
.shortcut span.right{float:right;}
.shortcut span.right a{float:left;display:block;color:#ff6600;font-weight:800;}
.headeline{height:40px;overflow:hidden;}
.adv960x90{width:960px;height:90px;overflow:hidden;border:solid 1px #E6E6E6;margin:0 auto;}
.adv728x90{width:728px;height:90px;overflow:hidden;border:solid 1px #E6E6E6;margin:0 auto;}
</style>

<script type="text/javascript" src="/js/jquery.min.js"></script>
<script type="text/javascript" src="/js/preview.js"></script>
<script type="text/javascript">
    $(function () {
        if ($('a.preview').length) {
            var img = preloadIm();
            imagePreview(img);
        }
    })
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="1" style="width:100%;border-color: #000000; border-collapse: collapse; font-size: small; text-align: center;" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left" style="font-size: medium;padding:0.5em;">
                    <a href="../tongji.aspx" target="_blank">报考人数统计</a>&nbsp;|&nbsp;
                    <a href="listforOnlyReg.aspx" target="_blank">未提交报名信息的注册帐户列表</a>&nbsp;|&nbsp;
                    <a href="EditAdminPSW.aspx" target="_blank">修改管理员密码</a>&nbsp;|&nbsp;
            <asp:LinkButton ID="lbtnExport" runat="server" OnClick="lbtnExport_Click">导出列表&nbsp;|&nbsp;</asp:LinkButton>
                    <a href="ZhunKaoZhengList.aspx">准考证信息</a>&nbsp;|&nbsp;
                    <a href="ExamChecklist.aspx">考试核对单</a>&nbsp;|&nbsp; 
                    <a href="ZhuoTeiList.aspx">准考证桌贴</a>&nbsp;|&nbsp;
                    <a href="OutputUserPic.aspx">报名照片导出</a>&nbsp;|&nbsp;
                    <a href="adminLogout.aspx">退出审核权限</a></td>
            </tr>
            <tr align="center" style="padding:0.5em;">
                <td align="center" style="font-size: x-large"><%=new SystemSetProvider().get年份() %>年某县教师考录网上报名汇总表</td>
            </tr>
            <tr><td style="padding:0.5em;">
                报考专业学科：<asp:DropDownList runat="server" ID="ddlXueKeList" AutoPostBack="True" OnSelectedIndexChanged="ddlXueKeList_SelectedIndexChanged"></asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Literal ID="litInfo" runat="server"></asp:Literal>
                </td></tr>
        </table>
        <asp:Repeater ID="rptList" runat="server" ItemType="UserListForAdmin">
        <HeaderTemplate>
         <table border="1" style="width:100%;border-color: #000000; border-collapse: collapse; text-align: center;" cellpadding="0" cellspacing="0">
        <tr><td>编号</td><td>姓名、性别、婚否、民族<br />身份证</td><td>出生年月</td>
            <td style="min-width:5em;">政治面貌<br />职业</td>
            <td>某生源</td><td>家庭住址</td>
            <td>毕业学校<br />学历</td><td>毕业时间<br />毕业证书编号</td>
            <td>专业<br />师范类</td><td>教师资格证类型<br />教师资格证编号</td>
            <td>普通话等级<br />普通话证号</td><td>手机<br />电话</td><td>满5年</td>
            <td>报考岗位</td><td>报考专业学科</td><td>报名时间</td>
            <td>审核结果</td><td>审核意见</td><td style="width:4em;">操作</td>
        </tr>
        </HeaderTemplate>
            <ItemTemplate>
            <tr>
                <td><%#Eval("id") %></td>
                <td>
                    <a href="AdminPrint.aspx?id=<%#Eval("id") %>" target="_blank" class="preview" path='<%#Eval("IDCardPIC") %>'><%#Eval("XingMing") %></a>
                 &nbsp;<%# Item.Sex +"&nbsp;"+Item.DuSheng+"&nbsp;"+Item.MinZu %><br /><%#Eval("UserName") %></td>
                <td><%#Eval("Birthday")%></td>
                <td><%# Item.PoliticalOrientation %><br /><%#Eval("ZhiYe")%></td>
                <td><%#Eval("ShengYuandi")%></td><td><%#Eval("Address") %></td>
                <td><%#Eval("BiYeSchool") %><br /><%#Eval("XueLi") %></td>
                <td><%#Convert.ToDateTime(Eval("BiYeTime")).ToShortDateString()%><br /><%#Eval("BiYeZhengShuCode")%></td>
                <td><%#Eval("ZhuanYe") %><br /><%# (Item.QuanRiZhi=="是"?"全日制":"非全日制") +"&nbsp;"+(Item.ShiFanLei=="是"?"师范类":"非师范") %></td><td><%#Eval("ZiGeZheng") %><br /><%#Eval("ZiGeZhengCode")%></td>
                <td><%#Eval("PTHLevel")%><br /><%#Eval("PTHZSNo")%></td>
                <td><%#Eval("Mobile") %><br /><%#Eval("Tel") %></td>
                <td><%#Eval("XueQianWork") %></td><td><%#Eval("GangWeiName") %></td>
                <td><%# Item.XueKeCode + Item.XueKeName %></td><td><%#Eval("CreateDT","{0:g}") %></td>
                <td><%#Eval("AuditResult") %></td><td><%#Eval("AuditFeedback") %></td><td><a href="audit.aspx?id=<%#Eval("id") %>" target="_blank">审核</a><br /><a href="EditPSW.aspx?UserName=<%#Eval("UserName") %>" target="_blank">修改密码</a><br /><a href="Edit.aspx?ID=<%#Eval("ID") %>" target="_blank">修改信息</a></td></tr>
            </ItemTemplate>
           <FooterTemplate>
                </table>
           </FooterTemplate>
        </asp:Repeater>
       <iframe id="export" width="0" height="0"></iframe>
    </div>
    </form>
</body>
</html>
