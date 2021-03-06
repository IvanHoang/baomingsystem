﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminPrint.aspx.cs" Inherits="user_User" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        td { border-color: #000000;
            break-word:break-all;           
            padding:1px 2px;    
        }
        .bold{ font-weight:bold;}
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="div1" >
    <table border="1"  style="border-color: #000000; border-collapse: collapse;font-size:14px;"  cellpadding="0" width="670px" cellspacing="0">
        <tr><td colspan="10" align="right" style="height: 20px;">报名编号：<asp:Literal ID="ltlid" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
        <tr><td colspan="10" align="center" style="font-size: large; height: 30px;">某县<%=new SystemSetProvider().get年份() %>年教师招聘报名登记表</td></tr>
        <tr>
            <td height="60px" width="65px" >姓名</td>
            <td width="98px" class="bold"><asp:Literal ID="ltlXingMing" runat="server"></asp:Literal></td>
            <td width="65px">性别</td>
            <td width="40px" class="bold"><asp:Literal ID="ltlSex" runat="server"></asp:Literal></td>
            <td width="65px">出生年月</td>
            <td width="88px" class="bold"><asp:Literal ID="ltlBirthday" runat="server"></asp:Literal></td>
            <td width="68px">学历</td>
            <td width="65px" class="bold"><asp:Literal ID="ltlXueLi" runat="server"></asp:Literal></td>
            <td width="96px" rowspan="2" colspan="2" align="center" ><asp:Image ID="imgPhoto" runat="server" /></td>
        </tr>
        <tr>
            <td height="80px">毕业学校</td>
            <td class="bold" width="98px"><asp:Literal ID="ltlBiYeSchool" runat="server"></asp:Literal></td>
            <td width="65px">是否某生源或户籍</td>
            <td class="bold"><asp:Literal ID="ltlShengYuanDi" runat="server"></asp:Literal></td>
            <td>毕业时间</td><td class="bold"><asp:Literal ID="ltlBiYeTime" runat="server"></asp:Literal></td>
            <td width="68px">是否全日制</td>
            <td class="bold"><asp:Literal ID="ltlQuanRiZhi" runat="server"></asp:Literal></td>            
        </tr>
        <tr>
            <td>专业</td>
            <td class="bold" width="98px"><asp:Literal ID="ltlZhuanYe" runat="server"></asp:Literal></td>
            <td>是否师范类</td>
            <td class="bold"><asp:Literal ID="ltlShiFanLei" runat="server"></asp:Literal></td>
            <td width="65px">取得何种教师资格证</td>
            <td colspan="2" width="156" class="bold"><asp:Literal ID="ltlZiGeZheng" runat="server"></asp:Literal></td>
            <td width="65px" colspan="2">婚否</td>
            <td class="bold"><asp:Literal ID="ltlDuSheng" runat="server"></asp:Literal></td>            
        </tr>
        <tr>
            <td>身份证</td>
            <td colspan="3" class="bold"><asp:Literal ID="ltlUserName" runat="server"></asp:Literal></td>
            <td>联系手机</td>
            <td class="bold"><asp:Literal ID="ltlMobile" runat="server"></asp:Literal></td>
            <td width="68px">教师资格证编号</td>
            <td colspan="3" class="bold"><asp:Literal ID="ltlZiGeZhengCode" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td>家庭住址</td>
            <td colspan="3" class="bold" width="205px"><asp:Literal ID="ltlAddress" runat="server"></asp:Literal></td>
            <td>联系电话</td>
            <td class="bold"><asp:Literal ID="ltlTel" runat="server"></asp:Literal></td>
            <td width="68px">毕业证书电子注册号</td>
            <td colspan="3" class="bold"><asp:Literal ID="ltlBiYeZhengShuCode" runat="server"></asp:Literal></td>            
        </tr>
        <tr>
            <td>报考岗位</td>
            <td colspan="3"  class="bold" width="205px">
                报考职位代码：<asp:Literal ID="ltlXueKeCode" runat="server"></asp:Literal><br />
                报考专业岗位：<asp:Literal ID="ltlGangWeiName" runat="server"></asp:Literal><br />
                报考专业学科：<asp:Literal ID="ltlXueKeName" runat="server"></asp:Literal></td>
            <td colspan="6">
                <table width="100%" cellspacing="0">
                    <tr>
                        <td style="width:50px;">
                            是否在某幼教工作满5年</td>
                        <td style="border-left:1px solid black;border-right:1px solid black;width:30px" class="bold">
                            <asp:Literal ID="ltlXueQianWork" runat="server"></asp:Literal></td>
                        <td  style="width:50px;">
                            ▲网上资格初审结果</td>
                        <td style="border-left:1px solid black;" class="bold" width="242px">
                            初审结果：<asp:Literal ID="ltlAuditResult" runat="server"></asp:Literal>
                            <br />
                            审核意见：<asp:Literal ID="ltlAuditFeedback" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">取得普通话证书等级</td>
            <td colspan="2"  class="bold">
                            <asp:Literal ID="ltlPTHLevel" runat="server"></asp:Literal></td>
            <td colspan="2">证书号</td>
            <td colspan="3"  class="bold">
                            <asp:Literal ID="ltlPTHNo" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td>考生签名保证</td><td colspan="9" width="592px">
                <p style="text-indent:2em;">我保证自己是某户籍（不含某市户籍）或某生源（不含某生源）（报考高中政治、高中历史、高中地理、中学信息技术、心理健康、职高电子商务、舞蹈、特殊教育等岗位户籍不限，报考中小学语文、中小学体育、学前教育（全日制）需null户籍除外）、全日制普通高校毕业生(报考连续从事学前教育5学年及以上学前教育岗位的可以为国民教育序列的高校毕业生),2020年7月20日前取得毕业证书和教师资格证(职高专业课和舞蹈岗位除外);报名所填写报名表内容、提供的资料真实,若弄虚作假,我愿接受《某县2020年招聘中小学（幼儿园）教师的公告》有关规定处理。</p>
            <p align="center">考生签名：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
            <p align="center"><%=new SystemSetProvider().get年份() %>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</p></td>          
        </tr>
        <tr>
            <td>现场审核</td><td colspan="9" width="600"><br />经审核符合：_____________________岗位_____________________学科<p 
                align="center">审核人：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
            <p align="center"><%=new SystemSetProvider().get年份() %>年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日</p></td>
        </tr>
        <tr>
           <td align="center">资格复审<br />需携带材料<br />说明</td>
           <td colspan="9" width="592px">
1、下载打印的《某县<%=new SystemSetProvider().get年份() %>年教师招聘报名登记表》一式一份并由考生本人签名；<br />
2、本人身份证、户口本（或户口迁出底册）等原件及复印件；<br />
3、毕业证书原件及复印件。未取得毕业证书的应届毕业生要提供学生证和学校证明（学校证明需写明生源地、专业、学历、是否师范类、教师资格类别及学科等），在7月20日之前须将证书送到县教育局人事科，否则取消录用资格。<br />
4、教师资格证原件及复印件。已取得教育部考试中心的《中小学和幼儿园教师资格考试合格证明》的考生可直接下载打印考生合格证明，并提供中国教师资格网中教师资格认定网上申报已受理的凭证。7月20日之前须将教师资格证送到县教育局人事科，否则取消录用资格，职高专业课、舞蹈除外。<br />
5、普通话证书原件及复印件。<br />
6、未被机关事业单位录用的有关证明材料：往届毕业生须提供个人档案存档证明（由个人档案保管单位开具；个人档案在某县人才市场的可在现场扫描二维码查询，不需开具证明）；应届毕业生须提供《全国普通高校就业协议书》原件及复印件。<br />
7、报考连续从事学前教育5学年及以上学前教育岗位除提供上述材料外,还要提供幼儿园工作的5学年工资册（每学年提供3、6、10、12月四个月）及幼儿园聘任证明（需园长签字和承诺书）。
            </td>
        </tr>        
    </table>
    </div>
    </form>
</body>
</html>
