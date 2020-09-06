<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="user_User" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://cdn.bootcss.com/jquery/1.12.3/jquery.min.js"></script>
    <%--<script type="text/javascript" src="../js/layer/layer.js"></script>--%>
     <style type="text/css">
        td { border-color: #000000;
            break-word:break-all;
            padding:1px 2px;
        }
        .bold{ font-weight:bold;}
        .notice{
            font-size:1.1em; color:red;margin:2em 2em 2em 1em;line-height:180%;width:50em;
        }
        .notice .head{font-weight:bold;text-align:center; line-height:180%;}
        .notice p{text-indent:2em;}
        .notice .foot{text-align:right;margin-right:3em;}
        @media Print { .NoPrn { DISPLAY: none }}
    </style>
<%--    <script type="text/javascript"> 
        function showMessage() {
            //iframe窗
            //layer.open({
            //    type: 2,
            //    title: '有关教师资格证要求的说明',
            //    //closeBtn: 1, //不显示关闭按钮
            //    shade: 0,
            //    maxmin:true,
            //    area: ['450px', '320px'],
            //    //offset: 'rb', //右下角弹出
            //    time: 2000, //2秒后自动关闭
            //    anim: 2,
            //    content: ['Notice.html', 'no'] //iframe的url，no代表不显示滚动条
            //    , btn: ['我知道了！'] //只是为了演示
            //    , yes: function () {
            //        //$(that).click(); //此处只是为了演示，实际使用可以剔除
            //        layer.closeAll();
            //    }
            //    , btn2: function () {
            //        layer.closeAll();
            //    }
            //});

            var i = 9; var interval;
            layer.confirm('是否提交？', {
                btn: [i + 1 + 's后可提交'], //按钮 
                skin: 'layui-layer-molv'
                , success: function (a, b) {
                    $(".layui-layer-btn0").css("backgroundColor", "#92B8B1");
                    var fn = function () {
                        // layer.title(i+' 秒后，系统将自动退出并关闭。',b); 
                        $(".layui-layer-btn0").text(i + 's'); i--;
                    }; interval = setInterval(function () {
                        fn();
                        if (i === 0)
                        {
                            $(".layui-layer-btn0").css("backgroundColor", "#019F95");
                            $(".layui-layer-btn0").text('我明白了！'); clearInterval(interval);
                        }
                    }, 1000);
                }
                , end: function () { clearInterval(interval); }
            }
                , function () {
                    clearInterval(interval);
                    //layer.msg('已关闭', { icon: 1 });
                });
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <table width="800px" style="padding: 0px; margin: 0px" class="NoPrn">
    <tr>
        <td>
            <%--<a id="btnFirst" runat="server" href="baoming.aspx">填写报名表&nbsp;|&nbsp;</a>--%>
            <asp:HyperLink ID="btnEdit" runat="server" NavigateUrl="~/user/Edit.aspx" Text="填写报名表 &nbsp;|&nbsp;" />
            <a href="../tongji.aspx" target="_blank">报名统计</a>&nbsp;|&nbsp;
            <a href="EditPSW.aspx">修改密码</a>&nbsp;|&nbsp;
            <a id="btnPrint" runat="server" href="javascript:window.print()" visible="false">打印报名表&nbsp;|</a>
            <asp:Literal ID="litZKZPrint" runat="server"></asp:Literal>
            <a href="../logout.aspx">退出</a>
        </td>
    </tr>
    </table>

    <asp:Panel ID="pnlInfo" runat="server">
        <div style="padding:5px;border:1px dashed red;" class="NoPrn">
        <asp:Label ID="lblMessage" runat="server" Text="提醒：" ForeColor="red"></asp:Label></div>

        <table border="1"  style="border-collapse: collapse;font-size:14px;float:left;margin-right:2em;"  cellpadding="0" width="710px" cellspacing="0">
        <tr><td colspan="2" style="border-right:0px;">报名编号：<asp:Literal ID="ltlid" runat="server"></asp:Literal> </td>
            <td colspan="8" style="font-size: 22px; height: 45px;border-left:0px;">
            某县<%=new SystemSetProvider().get年份() %>年教师招聘报名登记表</td>
        </tr>
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
                <p style="text-indent:2em;">我保证自己是某户籍（不含某市户籍）或某生源（不含某市生源）（报考高中政治、高中历史、高中地理、中学信息技术、心理健康、职高电子商务、舞蹈、特殊教育等岗位户籍不限，报考中小学语文、中小学体育、学前教育（全日制）需null户籍除外）、全日制普通高校毕业生(报考连续从事学前教育5学年及以上学前教育岗位的可以为国民教育序列的高校毕业生),2020年7月20日前取得毕业证书和教师资格证(职高专业课和舞蹈岗位除外);报名所填写报名表内容、提供的资料真实,若弄虚作假,我愿接受《某县2020年招聘中小学（幼儿园）教师的公告》有关规定处理。</p>
                
            <p align="right"><span style="margin-right:8em;">考生签名：</span>
                <%=new SystemSetProvider().get年份()%>&nbsp;年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;</p></td>          
        </tr>
        <tr>
            <td>现场审核</td><td colspan="9" width="600"><br />经审核符合：_____________________岗位_____________________学科
                <p align="right"><span style="margin-right:8em;">审核人：</span>
                    <%=new SystemSetProvider().get年份()%>&nbsp;年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;</p></td>
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
    </asp:Panel>
        <asp:Panel ID="pnlFee" runat="server" Visible="false">
            <div style="margin-top:1em; color:red;">
            你的报名已经初审通过！<br />
            请尽快扫描如下二维码，完成缴费：<br /><br />
            <asp:Image ID="imgFeeQRCode" runat="server" ImageUrl="~/images/feeQRCode.jpg"/>
                </div>
        </asp:Panel>

        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                <div class="notice">
                    <div class="head">温馨提醒 | 招聘条件</div>
<p>1．拥护中国共产党，遵纪守法，热爱教育事业，志愿从事教育工作。</p>
<p>2．具有与履行招考岗位相适应的职业道德素质、业务知识水平、教育教学能力和心理素质，身心健康。</p>
<p>3．专科毕业生年龄30周岁以下（1990年1月1日后出生），本科及以上毕业生年龄33周岁以下（1987年1月1日后出生）；符合报考学前教育工作满5学年岗位的，年龄可以放宽至35周岁以下（1985年1月1日后出生）。 </p>
<p>4．具有某户籍（不含某市）或某生源（指参加高考时户籍在某<《不含某市》>）的全日制普通高校毕业生（含2020年全日制普通高校应届毕业生），中小学语文、中小学体育、学前教育（全日制）岗位放宽至null户籍，高中政治、高中历史、高中地理、中学信息技术、心理健康、职高电子商务、舞蹈、特殊教育等岗位户籍不限，户籍确认时间截止：2020年3日7日。</p>
<p>5．未被机关、事业单位录用的人员。报考连续从事学前教育满5学年岗位的，必须在我县连续从事学前教育满5年的我县现任幼儿园长或教师，五学年时间计算到2020年6月30日，人员身份确认时间截止2020年3月7日。</p>
<p>6. 全日制普通高校专科及以上毕业生（含2020年应届毕业生），报考学前教育（全日制）、特殊教育岗位要求全日制专科及以上学历；报考中小学岗位要求全日制本科及以上学历（报考小学岗位对象，若具有学科相应专业且有对应学科教师资格证的可放宽至全日制大专学历）；报考连续从事学前教育满5学年岗位的可以为国民教育序列的高校毕业生。（具体见附件1）</p>
<p>7. 报考对象应具有相应类别的教师资格证（职高专业课、舞蹈除外），中学岗位都需高中教师资格证（初中社会、初中科学岗位需初中教师资格证及以上）；高学段教师资格证书适用于低学段岗位，学前教育岗位须持有幼儿园教师资格证；未取得教师资格证的可暂凭教育部考试中心的《中小学和幼儿园教师资格考试合格证明》报考。报考中小学语文岗位必须取得二级甲等及以上普通话等级证书，其余岗位持二级乙等及以上普通话等级证书。</p>
<p>8. 本科及以上毕业生可选择与学历证书专业相应学科或教师资格证（应届毕业生可提供是否师范类、所学专业、取得何种教师资格证（2020年7月20日前）对应的学科进行报考，物理、化学、生物、科学专业毕业生可报考初中或小学科学岗位，政治、历史、地理、社会专业毕业生可报考初中社会岗位，报考学前教育（全日制）、特殊教育、心理健康、舞蹈、职高专业课岗位要求对应专业。初等教育、小学教育专业毕业生若毕业证书没有注明方向且教师资格证没有明确学科的或具有小学全科教师资格证的，可自选相应学段一个学科进行报考。每位毕业生只能报考一个学科岗位，不得兼报。报考连续从事学前教育满5学年岗位的，专业可以不限。</p>
                </div>
            </asp:Panel>
    </form>
</body>
</html>
