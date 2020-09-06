using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppBaoMing.Models;

namespace WebAppBaoMing.Services
{
    public class AdminItemService : IAdminItemService
    {
        private readonly ApplicationDbContext _context;

        public AdminItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<AdminItem[]> GetIncompleteItemsAsync(IdentityUser user)
        //{
        //    var items = await _context.Items
        //        //.Where(x => x.IsDone == false)
        //        .Where(x => x.UserId == user.Id)
        //        .ToArrayAsync();
        //    return items;
        //}

        public async Task<bool> AddItemAsync(AdminItem newItem)
        {
            _context.Users.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> DeleteItemAsync(string UserName)
        {
            var item = _context.Users
                .FirstOrDefault(x => x.UserName == UserName);
            _context.Users.Remove(item);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<AdminItem> GetItemAsync(string UserName)
        {
            var item = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == UserName);
            return item;
        }

        public async Task<List<AdminItem>> GetListAsync()
        {
            var items = await _context.Users.ToListAsync();
            return items;
        }


        public async Task<bool> UpdateItemAsync(AdminItem newItem)
        {
            //var item = _context.Admin
            //    .FirstOrDefault(x => x.UserName == newItem.UserName);
            _context.Users.Update(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        //public async Task<bool> MarkDoneAsync(Guid id, IdentityUser user)
        //{
        //    var item = await _context.Items
        //        .Where(x => x.Id == id && x.UserId == user.Id)
        //        .SingleOrDefaultAsync();

        //    if (item == null) return false;

        //    item.IsDone = !item.IsDone;

        //    var saveResult = await _context.SaveChangesAsync();
        //    return saveResult == 1; // One entity should have been updated
        //}

    }
}
