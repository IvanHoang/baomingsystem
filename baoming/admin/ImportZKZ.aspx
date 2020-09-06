<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportZKZ.aspx.cs" Inherits="admin_ImportZKZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>准考证导入</title>
</head>
<body>
    <style type="text/css">
*{margin:0;padding:0;}
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
*html,*html body {background-image:url(about:blank);background-attachment:fixed;}
*html .shortcut{position:absolute;top:expression(eval(document.documentElement.scrollTop));}
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
    <form id="form1" runat="server">
    <div>
        
    <table border="1" 
            style="border-color: #000000; border-collapse: collapse; font-size: small; text-align: center;"  
            cellpadding="0" cellspacing="0">
        <tr align="center"><td align="center" colspan="22" style ="font-size:x-large"><%=new SystemSetProvider().get年份() %>年某县教师考录网上报名准考证导入</td></tr>
        <asp:Repeater ID="rptList" runat="server">
        
            <ItemTemplate>
            
            </ItemTemplate>
        </asp:Repeater>
    </table>




    <div>
    选择要导入的Excel文件：<asp:FileUpload ID="fuExcel" runat="server" />
</div>
<div>
</div>
    <div>
        <asp:Button ID="btnImportOverride" runat="server" Text="覆盖导入" Visible="false" OnClick="btnImportOverride_Click" />
    </div>
        
    <div>
        <asp:Button ID="btnImportRefresh" runat="server" Text="清空导入" OnClick="btnImportRefresh_Click" />
    </div>
<div id="Message" style="color:red;">
    <%=ErrMessage %>
</div>
<br />
    <p>
        说明：<br />
        1、点击浏览选择要上传的Excel文件(*.xls)。<br />
        2、点击“清空导入”按钮，将会先清除所有以前导入的准考证信息，并从所导入的Excel中添加所有记录。<br />
        3、确认无误后，点击需要的对应按钮，上传数据。<br />
        4、如果导入发生不可意料的错误，可能是导入的Excel文件格式有问题，请<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">下载Excel模板文件</asp:LinkButton>
        使用！</p>
    </div>
    </form>
</body>
</html>
