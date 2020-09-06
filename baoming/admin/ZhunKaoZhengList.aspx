<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZhunKaoZhengList.aspx.cs" Inherits="admin_ZhunKaoZhengList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
        }

        a, img {
            border: 0;
        }

        ul, li {
            list-style-type: none;
        }
        /* demo */
        .demo {
            width: 600px;
            margin: 40px auto;
        }

            .demo h2 {
                font-size: 14px;
                height: 62px;
                color: #3366cc;
            }

            .demo a {
                font-size: 14px;
                color: #333;
                text-decoration: none;
            }

                .demo a img {
                    border: solid 1px #ddd;
                    margin: 0 5px;
                }
        /* previewShowWindow */
        .previewShowWindow {
            position: absolute;
            border: 1px solid #dadade;
            background: #95959d;
            padding: 5px;
            display: none;
            color: #fff;
            text-align: center;
            z-index: 999;
        }
        /* 自己调用的时候·可以删除下面的样式 */
        *html, *html body {
            background-image: url(about:blank);
            background-attachment: fixed;
        }

        *html .shortcut {
            position: absolute;
            top: expression(eval(document.documentElement.scrollTop));
        }

        .shortcut {
            height: 28px;
            line-height: 28px;
            font-size: 12px;
            background: #EEEEEE;
            text-transform: uppercase;
            box-shadow: 1px 0px 2px rgba(0,0,0,0.2);
            border-bottom: 1px solid #DDDDDD;
        }

            .shortcut h1 {
                font-size: 14px;
                font-family: "微软雅黑","宋体";
            }

            .shortcut a, .shortcut h1 {
                padding: 0px 10px;
                letter-spacing: 1px;
                color: #333;
                text-shadow: 0px 1px 1px #fff;
                display: block;
                float: left;
            }

                .shortcut a:hover {
                    background: #fff;
                }

            .shortcut span.right {
                float: right;
            }

                .shortcut span.right a {
                    float: left;
                    display: block;
                    color: #ff6600;
                    font-weight: 800;
                }

        .headeline {
            height: 40px;
            overflow: hidden;
        }

        .adv960x90 {
            width: 960px;
            height: 90px;
            overflow: hidden;
            border: solid 1px #E6E6E6;
            margin: 0 auto;
        }

        .adv728x90 {
            width: 728px;
            height: 90px;
            overflow: hidden;
            border: solid 1px #E6E6E6;
            margin: 0 auto;
        }
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
                <tr>
                    <td colspan="28" align="left" style="font-size: medium"><a href="../admin/ImportZKZ.aspx" target="_blank">准考证导入</a>&nbsp;|&nbsp;<a href="list.aspx">返回上一层功能</a></td>
                </tr>
                <tr align="center">
                    <td align="center" colspan="28" style="font-size: x-large"><%=new SystemSetProvider().get年份() %>年某县教师考录准考证列表<small>(按F5键会刷新以下列表)</small></td>
                </tr>

                <asp:Repeater ID="rptList" runat="server">
                    <HeaderTemplate>
                        <tr>
                            <td>姓名</td>
                            <td>性别</td>
                            <td>身份证号</td>
                            <td>准考证号</td>
                            <td></td>
                            <td>公共科目考试</td>
                            <td>试场号</td>
                            <td>座位号</td>
                            <td>考试日期</td>
                            <td>考试时间</td>
                            <td>公共科目考试考点</td>
                            <td></td>
                            <td>学科专业知识考试</td>
                            <td>试场号</td>
                            <td>座位号</td>
                            <td>考试日期</td>
                            <td>考试时间</td>
                            <td>学科专业知识考试考点</td>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("UserRealName") %></td>
                            <td><%#Eval("Sex")%></td>
                            <td><%#Eval("UserName") %></td>
                            <td><%#Eval("zkzCode")%></td>
                            <td></td>
                            <td><%#Eval("publicSubject") %></td>
                            <td><%#Eval("shiChangCode") %></td>
                            <td><%#Eval("zuoweiCode") %></td>
                            <td><%#Eval("kaoShiDate") %></td>
                            <td><%#Eval("kaoShiTime")%></td>
                            <td><%#Eval("kaoDian")%></td>
                            <td></td>
                            <td><%#Eval("privateSubject") %></td>
                            <td><%#Eval("shiChangCode1") %></td>
                            <td><%#Eval("zuoweiCode1") %></td>
                            <td><%#Eval("kaoShiDate1") %></td>
                            <td><%#Eval("kaoShiTime1")%></td>
                            <td><%#Eval("kaoDian1")%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <iframe id="export" width="0" height="0"></iframe>
        </div>
    </form>
</body>
</html>
