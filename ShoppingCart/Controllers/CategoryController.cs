using Application.Features.CategoryFeature.Commands;
using Application.Features.CategoryFeature.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMediator _mediator;
        public CategoryController(ILogger<CategoryController> logger , IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> list = await _mediator.Send(new GetAllCategories());
            return View(list);
        }
        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            Category item = await _mediator.Send(new GetCategoryById { Id = id });
            return View(item);
        }
        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategory command)
        {

            string id = await _mediator.Send(command);
            return RedirectToAction("Index");
        }
        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            _logger.LogInformation("get category by id ");
            Category item = await _mediator.Send(new GetCategoryById { Id = id });
            return View(item);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UpdateCategory command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            else {
                string output = await _mediator.Send(command);
                return View(id);
            }
           
        }
        public async Task<ActionResult> Delete(string id)
        {
            var category = await _mediator.Send(new GetCategoryById { Id = id });
            return View(category);
        }

        // POST: CategoryController1/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else {
                string output = await _mediator.Send(new DeleteCategory { Id = id });
                return View(output);
            }
            
        }
    }
}
