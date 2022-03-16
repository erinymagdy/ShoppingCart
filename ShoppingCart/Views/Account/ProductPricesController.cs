using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Views.Account
{
    [Authorize(Roles = "Admin, Seller")]
    public class ProductPricesController : Controller
    {
        private readonly ILogger<ProductPricesController> _logger;
        private readonly IMediator _mediator;
        public ProductPricesController(ILogger<ProductPricesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        // GET: ProductPricesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductPricesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductPricesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductPricesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductPricesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductPricesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductPricesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductPricesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
