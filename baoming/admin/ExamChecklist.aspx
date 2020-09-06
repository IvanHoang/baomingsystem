<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExamChecklist.aspx.cs" Inherits="admin_ExamChecklist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考试核对单</title>
     <script type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        body { font-family:Arial, Helvetica, sans-serif; font-size:12.5px; line-height:140%;      }
        div.each { width:18%;margin:0.25em; float:left; border:1px dotted #ccc; padding-top:2px; height:11em; overflow:hidden; }
            div.each img {border:1px solid #999; }
        @media Print { .NoPrn { DISPLAY: none }}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="NoPrn">
            <asp:DropDownList runat="server" ID="ddlKaoShiType" AutoPostBack="True" OnSelectedIndexChanged="ddlKaoShiType_SelectedIndexChanged">
                <asp:ListItem Text="教育基础知识考试" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="学科专业知识考试" Value="1"></asp:ListItem>
            </asp:DropDownList>&nbsp;
            试场号：<asp:DropDownList runat="server" ID="ddlshiChangCodeList"></asp:DropDownList>&nbsp;|
            <asp:LinkButton runat="server" ID="btnSelech" OnClick="btnSelech_Click">查询刷新</asp:LinkButton>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="javascript:window.print()">打印</a>
            &nbsp;&nbsp;|&nbsp;&nbsp;<a href="list.aspx">返回上一层功能</a>
        </div>
        <div style="text-align:center;">
            <h2><%=new SystemSetProvider().get年份() %>年某县招聘中小学(幼儿园)教师笔试核对单</h2>
            <asp:Label runat="server" ID="KaoShiTypeName"></asp:Label>&nbsp:&nbsp:试场号：<asp:Label runat="server" ID="shichangName"></asp:Label>
        </div>
        <div style="border:1px solid #999;">

        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <div class="each">
                   <div style="text-align:center;height:60px;"><img  src="<%#Eval("IDPhoto") %>"/></div>
                    <div>
                        <%#Eval("UserRealName") %>&nbsp;<%#Eval("Sex") %>&nbsp;&nbsp;座:<%#Eval("zuoweiCode") %><br />
                        <%#Eval("zkzCode") %><br />
                        签名：
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        </div>

       <iframe id="export" width="0" height="0"></iframe>
    
    </form>
</body>
</html>
