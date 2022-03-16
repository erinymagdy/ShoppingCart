using Application.Features.SellerFeature.Commands;
using Application.Features.SellerFeature.Queries;
using Domain.models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class SellerController : Controller
    {
        private readonly ILogger<SellerController> _logger;

        private readonly IMediator _mediator;

        public SellerController(ILogger<SellerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        // GET: SellerController
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("get all Sellers");
            IEnumerable<Seller> list = await _mediator.Send(new GetAllSellers());
            return View(list);
        }

        // GET: SellerController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            _logger.LogInformation("get Seller by id ");
            Seller item = await _mediator.Send(new GetSellerById { Id = id });
            return View(item);
        }

        // GET: SellerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SellerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSeller command)
        {
            _logger.LogInformation("create Seller");
            string id = await _mediator.Send(command);
            return View(id);
        }

        // GET: SellerController/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: SellerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UpdateSeller command)
        {
            if (id != command.Id)
            {
                _logger.LogError("id not found");
                return BadRequest();
            }
            else
            {
                _logger.LogInformation("edit Seller");
                string output = await _mediator.Send(command);
                return View(id);
            }

        }

        // POST: SellerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                _logger.LogError("id not found");
                return BadRequest();
            }
            else
            {
                _logger.LogInformation("delete Seller");
                string output = await _mediator.Send(new DeleteSeller { Id = id });
                return View(output);
            }

        }
    }
}
