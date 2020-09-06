using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppBaoMing.Models
{
    [Table("tb_Admin")]
    public class AdminItem
    {
        [Key]
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Role { get; set; }
        public string IDCardPIC { get; set; }
    }
}
