using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBaoMing.Models;

namespace WebAppBaoMing.Services
{
    public interface IGangWeiService
    {
        Task<bool> AddItemAsync(GangWei newItem);
        Task<bool> DeleteItemAsync(int GangWeiCode);
        Task<bool> UpdateItemAsync(GangWei newItem);
        Task<GangWei> GetItemAsync(int GangWeiCode);
        Task<List<GangWei>> GetListAsync();
    }
}
