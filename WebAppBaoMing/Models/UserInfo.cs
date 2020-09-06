using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppBaoMing.Models
{
    [Table("tb_UserInfo")]
    public class UserInfo
    {
        [Key]
        public int id { get; set; }
        public string UserName { get; set; }
        public string XingMing  { get; set; }
        public string Birthday { get; set; }
        public string ShengYuanDi  { get; set; }
        public string BiYeSchool  { get; set; }
        public string AuditCode { get; set; }
        public string AuditFeedback  { get; set; }
        public int XueKeCode { get; set; }
        public string XueLiCode { get; set; }
        public DateTime BiYeTime { get; set; }
        public string QuanRiZhi { get; set; }
        public string ZhuanYe { get; set; }
        public string ShiFanLei { get; set; }
        public string ZiGeZheng { get; set; }
        public string DuSheng { get; set; }
        public string Mobile { get; set; }
        public string ZiGeZhengCode { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string XueQianWork { get; set; }
        public string BiYeZhengShuCode { get; set; }
        public string SexCode { get; set; }
        public string PTHLevel { get; set; }
        public string PTHZSNo { get; set; }
        public string IDPhoto { get; set; }
        public string MinZuCode { get; set; }
        public string PoliticalOrientationCode { get; set; }
        public string ZhiYeCode { get; set; }
        public DateTime CreateDT  { get; set; }
        public string picResidenceBooklet { get; set; }
        public string picDiploma { get; set; }
        public string picArchiveCertificate { get; set; }
        public string picZiGeZheng { get; set; }
        public string picPTH { get; set; }
        public string picNewGraduates { get; set; }
        public string picKindergartenCommitment { get; set; }


    }
}
