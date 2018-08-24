namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("sale")]
    public class SaleController : Controller
    {
        private readonly ISaleService saleService;

        public SaleController(ISaleService saleService)
        {
            this.saleService = saleService;
        }

        [Route("")]
        public IActionResult All ()
        {
            return View(saleService.All());
        }

        [Route("{id}")]
        public IActionResult Details(int id)
        {
            return View(saleService.Details(id));
        }
    }
}
