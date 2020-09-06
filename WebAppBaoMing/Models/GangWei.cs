using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppBaoMing.Models
{
    [Table("tb_gangwei")]
    public class GangWei
    {
        [Key]
        public int GangWeiCode { get; set; }
        public string GangWeiName { get; set; }
        public bool bUsed { get; set; }
 
    }
}
