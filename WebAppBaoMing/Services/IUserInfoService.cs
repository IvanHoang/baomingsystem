using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBaoMing.Models;

namespace WebAppBaoMing.Services
{
    public interface IUserInfoService
    {
        Task<bool> AddItemAsync(UserInfo newItem);
        Task<bool> DeleteItemAsync(int id);
        Task<bool> UpdateItemAsync(UserInfo newItem);
        Task<UserInfo> GetItemAsync(int id);
        Task<List<UserInfo>> GetListAsync();
    }
}
