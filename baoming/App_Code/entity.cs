using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///entity 的摘要说明
/// </summary>
public class UserListForAdmin
{
    public string XingMing { get; set; }
    public string SexCode { get; set; }
    public string Sex { get; set; }
    public string UserName { get; set; }
    public string MinZuCode { get; set; }
    public string MinZu { get; set; }
    public string PoliticalOrientationCode { get; set; }
    public string PoliticalOrientation { get; set; }
    public string ZhiYeCode { get; set; }
    public string ZhiYe { get; set; }
    public string Birthday { get; set; }
    public string ShengYuanDi {get;set;}
    public string Address {get;set;}
    public string BiYeSchool {get;set;}
    public string XueLiCode {get;set;}
    public string XueLi { get; set; }
    public DateTime? BiYeTime {get;set;}
    public string QuanRiZhi {get;set;}
    public string ZhuanYe {get;set;}
    public string ShiFanLei {get;set;}
    public string ZiGeZheng {get;set;}
    public string ZiGeZhengCode {get;set;}
    public string DuSheng {get;set;}
    public string Mobile {get;set;}
    public string Tel {get;set;}
    public string BiYeZhengShuCode {get;set;}
    public string XueQianWork {get;set;}
    public string GangWeiName {get;set;}
    public string XueKeName {get;set;}
    public int? XueKeCode {get;set;}
    public string AuditCode {get;set;}
    public string AuditResult { get; set; }
    public string AuditFeedback {get;set;}
    public int id {get;set;}
    public string PTHLevel { get; set; }
    public string PTHZSNo { get; set; }
    public string IDCardPIC {get;set;}
    public DateTime CreateDT { get; set; }
}

public class ZhunKaoZhengListForAdmin
{
    //准考证号
    public string zkzCode { get; set; }
    //身份证号
    public string UserName { get; set; }
    //座位号
    public int zuoweiCode { get; set; }
    //考点
    public string kaoDian { get; set; }
    //试场号
    public string shiChangCode { get; set; }
    
    //考试时间
    public string kaoShiTime { get; set; }
    //考试日期
    public string kaoShiDate { get; set; }
    //考试科目
    public string Subject { get; set; }
    //姓名
    public string UserRealName { get; set; }
    //性别
    public string SexCode { get; set; }
    public string Sex { get; set; }
    //准考证照片
    public string IDPhoto { get; set; }
    //编号(用于布局)
    public int NO { get; set; }
}

public class ZhunKaoZhengListForAdmin1:tb_zunkaozheng
{
    //姓名
    public string UserRealName { get; set; }
    //性别
    public string SexCode { get; set; }
    public string Sex { get; set; }
    //准考证照片
    public string IDPhoto { get; set; }
    //编号(用于布局)
    public int NO { get; set; }
}

public class nameValueItem
{
    public string itemName { get; set; }
    public string itemValue { get; set; }
    public bool isDefault { get; set; }
    public int itemSort { get; set; }
    public string itemRemark { get; set; }
}