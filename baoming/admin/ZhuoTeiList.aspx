<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZhuoTeiList.aspx.cs" Inherits="admin_ZhuoTeiList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>准考证桌贴</title>   
    <script type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        body { font-family:Arial, Helvetica, sans-serif; font-size:14pt; line-height:160%;      }
        div.each { width:48%;margin:0.25em; float:left; border:1px dotted #ccc; padding-top:1em;padding-bottom:1em; height:10em;  }
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
        <div class="NoPrn"><asp:Label runat="server" ID="KaoShiTypeName"></asp:Label>&nbsp:&nbsp:试场号：<asp:Label runat="server" ID="shichangName"></asp:Label></div>
  <div style="border:1px solid #999;">
      <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <div class="each">
                    姓名：<%#Eval("UserRealName") %><br />
                    性别：<%#Eval("Sex") %><br />
                    准考证号：<%#Eval("zkzCode") %><br />
                    座位号：<%#Eval("zuoweiCode") %><br />
                    试场号：<%#Eval("shiChangCode") %><br />
                    考试科目：<%#Eval("Subject") %>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
       <iframe id="export" width="0" height="0"></iframe>
    
    </form>
</body>
</html>
