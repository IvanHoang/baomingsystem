using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBaoMing.Models;

namespace WebAppBaoMing.Services
{
    public class UserInfoService : IUserInfoService
        {
            private readonly ApplicationDbContext _context;

            public UserInfoService(ApplicationDbContext context)
            {
                _context = context;
            }

    
            public async Task<bool> AddItemAsync(UserInfo newItem)
            {
                _context.UserInfo.Add(newItem);

                var saveResult = await _context.SaveChangesAsync();
                return saveResult == 1;
            }

            public async Task<bool> DeleteItemAsync(int id)
            {
                var item = _context.UserInfo
                    .FirstOrDefault(x => x.XueKeCode == id);
                _context.UserInfo.Remove(item);

                var saveResult = await _context.SaveChangesAsync();
                return saveResult == 1;
            }

            public async Task<UserInfo> GetItemAsync(int id)
            {
                var item = await _context.UserInfo
                    .FirstOrDefaultAsync(x => x.id == id);
                return item;
            }
            public async Task<List<UserInfo>> GetListAsync()
            {
                var item = await _context.UserInfo.ToListAsync();
                return item;
            }

            public async Task<bool> UpdateItemAsync(UserInfo newItem)
            {
                //var item = _context.Admin
                //    .FirstOrDefault(x => x.UserName == newItem.UserName);
                _context.UserInfo.Update(newItem);

                var saveResult = await _context.SaveChangesAsync();
                return saveResult == 1;
            }


        }
    }



