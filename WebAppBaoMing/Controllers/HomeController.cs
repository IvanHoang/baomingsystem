using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppBaoMing.Models;
using WebAppBaoMing.Services;

namespace WebAppBaoMing.Controllers
{
    public class HomeController : Controller
    {
       

        private readonly ILogger<HomeController> _logger;
        private readonly IAdminItemService _adminItemService;
        private readonly IXueKeService _xuekeService;
        private readonly IGangWeiService _gangweiService;


        public HomeController(ILogger<HomeController> logger, IAdminItemService adminItemService, IXueKeService xuekeService, IGangWeiService gangweiService)
        {
            _logger = logger;
            _adminItemService = adminItemService;
            _xuekeService = xuekeService;
            _gangweiService = gangweiService;

        }

        //public IActionResult Index()
        //{
            //var lst = _adminItemService.GetListAsync().GetAwaiter().GetResult();
           // var fir = lst.FirstOrDefault();
            //var xk = _xuekeService.GetListAsync().GetAwaiter().GetResult();
            //var fir2 = xk.FirstOrDefault();
            //return View();

        //}
        public async Task<IActionResult> Index()
        {
            var lst = await _adminItemService.GetListAsync();
            var first = lst.FirstOrDefault().UserName;
            var xk = await _xuekeService.GetListAsync();
            var xkfst = xk.FirstOrDefault().XueKeName;
            var gw = await _gangweiService.GetListAsync();
            var gwfst = gw.FirstOrDefault().GangWeiName;

            return View();
        }




        public IActionResult Register()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
