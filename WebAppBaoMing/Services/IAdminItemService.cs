using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBaoMing.Models;

namespace WebAppBaoMing.Services
{
     public interface IAdminItemService
    {
        Task<bool> AddItemAsync(AdminItem newItem);
        Task<bool> DeleteItemAsync(string UserName);
        Task<bool> UpdateItemAsync(AdminItem newItem);
        Task<AdminItem> GetItemAsync(string UserName);
        Task<List<AdminItem>> GetListAsync();
    }
}
