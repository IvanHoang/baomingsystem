using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppBaoMing.Models;
using WebAppBaoMing.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppBaoMing.Controllers
{
    public class UserController : Controller

    {
        private readonly IXueKeService _xuekeService;
        private readonly IUserInfoService _userinfoService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IXueKeService xuekeService, IUserInfoService userinfoService)
        {
            _logger = logger;
            _xuekeService = xuekeService;
            _userinfoService = userinfoService;


        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BaoMingTable()
        {
            return View();
        }

        public async Task<IActionResult> TongJiAsync()
        {
            //var xk = await _xuekeService.GetListAsync();
            //ViewBag.DATA = xk;

            var lstall = await _userinfoService.GetListAsync();
            var grp= lstall.GroupBy(t => t.XueKeCode);//用学科代码进行分组
            grp.Select(t => new Tongji() { });//从Tongji这个model中取
            List<Tongji> datas = new List<Tongji>();
            foreach (var item in grp)
            {
                var tj = new Tongji();
                tj.XueKeCode = item.Key;
                var xk = await _xuekeService.GetItemAsync(tj.XueKeCode);
                tj.XueKeName = xk.XueKeName;
                tj.Num = item.Count();
                datas.Add(tj);

            }
            return View(datas);
        }

        public IActionResult EditPSW()
        {
            return View();
        }
    }
}
