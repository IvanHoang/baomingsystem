<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintingZKZ.aspx.cs" Inherits="user_PrintingZKZ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>打印准考证</title>
</head>
<body>
    <style type="text/css">
        td {
            border-color: #000000;
            break-word: break-all;
            padding: 1px 2px;
        }

        .bold {
            font-weight: bold;
        }
        @media Print { .NoPrn { DISPLAY: none }}
    </style>
    <form id="form1" runat="server">
        <table width="800px" style="padding: 0px; margin: 0px" class="NoPrn">
            <tr>
                <td>
                    &nbsp;<a href="javascript:window.print()">打印准考证</a>&nbsp;
                </td>
            </tr>
        </table>
        <div id="div1">
            <table  style="border:1px solid #000000;line-height:160%; text-indent:1em;"  width="550px" >
                <tr>
                    <td colspan="2" align="center" style="font-family:SimHei; font-weight：bold; font-size:1.3em;line-height:160%;"><%=new SystemSetProvider().get年份()%>年某县招聘中小学（幼儿园）教师笔试</td>
                  
                </tr>
                <tr>
                    <td colspan="2" align="center"  style="font-family:SimHei; font-weight：bold; font-size:2.3em;line-height:160%;">准&nbsp;&nbsp;考&nbsp;&nbsp;证</td>
                  
                </tr>
                <tr  >
                    <td rowspan="5" align="center">
                        <asp:Image runat="server" ImageAlign="Middle" ID="image_Photo" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>姓名：</strong><asp:Label runat="server" ID="lbXingMing"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td >
                        <strong>性别：</strong><asp:Label runat="server" ID="lbSex"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td >
                        <strong>准考证号：</strong><asp:Label runat="server" ID="lbzkzCode"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td >
                        <strong>身份证号：</strong><asp:Label runat="server" ID="lbUserName"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table border="1" style="width:100%; border-color: #000000; border-collapse: collapse; text-indent:0; font-size: small; text-align: center;" cellpadding="0" cellspacing="0">
                            <tr>
                                <th>考试科目</th><th>日期时间</th><th>考点</th>
                            </tr>
                            <tr>
                                <td><asp:Label runat="server" ID="lbPublicSubject"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lbkaoShiDate"></asp:Label><br />
                                    <asp:Label runat="server" ID="lbkaoShiTime" ></asp:Label>

                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lbKaoDian"></asp:Label><br />
                                    <strong>试场号：</strong><asp:Label runat="server" ID="lbshichangCode"></asp:Label>&nbsp;&nbsp;  <strong>座位号：</strong><asp:Label runat="server" ID="lbzuoweiCode"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><asp:Label runat="server" ID="lbPrivateSubject"></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lbkaoShiDate1"></asp:Label><br />
                                    <asp:Label runat="server" ID="lbkaoShiTime1" ></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lbKaoDian1"></asp:Label><br />
                                    <strong>试场号：</strong><asp:Label runat="server" ID="lbshichangCode1"></asp:Label>&nbsp;&nbsp;  <strong>座位号：</strong><asp:Label runat="server" ID="lbzuoweiCode1"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                   <td colspan="2" align="center"><strong>(携带本人身份证和准考证提前15分钟到场)</strong></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
