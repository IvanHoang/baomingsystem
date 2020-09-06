using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppBaoMing.Models
{
    [Table("tb_XueKe")]
    public class XueKe
    {
        [Key]
        //public List<XueKe> XueKes  { get; set; }
        public int XueKeCode { get; set; }
        public string XueKeName { get; set; }
        public int GangWeiCode { get; set; }
        public string kskmCode { get; set; }
       
    }
}
