using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBaoMing.Models;

namespace WebAppBaoMing.Services
{
    public class XueKeService : IXueKeService
    {
        private readonly ApplicationDbContext _context;

        public XueKeService(ApplicationDbContext context)
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

        public async Task<bool> AddItemAsync(XueKe newItem)
        {
            _context.XueKe.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> DeleteItemAsync(int XueKeCode)
        {
            var item = _context.XueKe
                .FirstOrDefault(x => x.XueKeCode == XueKeCode);
            _context.XueKe.Remove(item);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<XueKe> GetItemAsync(int XueKeCode)
        {
            var item = await _context.XueKe
                .FirstOrDefaultAsync(x => x.XueKeCode == XueKeCode);
            return item;
        }
        public async Task<List<XueKe>> GetListAsync()
        {
            var item = await _context.XueKe.ToListAsync();
            return item;
        }

        public async Task<bool> UpdateItemAsync(XueKe newItem)
        {
            //var item = _context.Admin
            //    .FirstOrDefault(x => x.UserName == newItem.UserName);
            _context.XueKe.Update(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }


    }
}

