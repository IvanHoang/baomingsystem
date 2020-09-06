using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBaoMing.Models;

namespace WebAppBaoMing.Services
{
    public class GangWeiService : IGangWeiService 
    {
        private readonly ApplicationDbContext _context;

        public GangWeiService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddItemAsync(GangWei newItem)
        {
            _context.GangWei.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> DeleteItemAsync(int GangWeiCode)
        {
            var item = _context.GangWei
                .FirstOrDefault(x => x.GangWeiCode == GangWeiCode);
            _context.GangWei.Remove(item);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<GangWei> GetItemAsync(int GangWeiCode)
        {
            var item = await _context.GangWei
                .FirstOrDefaultAsync(x => x.GangWeiCode == GangWeiCode);
            return item;
        }
        public async Task<List<GangWei>> GetListAsync()
        {
            var item = await _context.GangWei.ToListAsync();
            return item;
        }

        public async Task<bool> UpdateItemAsync(GangWei newItem)
        {
            //var item = _context.Admin
            //    .FirstOrDefault(x => x.UserName == newItem.UserName);
            _context.GangWei.Update(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
