using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBaoMing.Models;

namespace WebAppBaoMing.Services
{
     public interface IXueKeService
    {
        Task<bool> AddItemAsync(XueKe newItem);
        Task<bool> DeleteItemAsync(int XueKeCode);
        Task<bool> UpdateItemAsync(XueKe newItem);
        Task<XueKe> GetItemAsync(int XueKeCode);
        Task<List<XueKe>> GetListAsync();
    }
}
