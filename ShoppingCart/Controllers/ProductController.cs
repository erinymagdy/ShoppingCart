using Application.Features.CategoryFeature.Queries;
using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using Application.Interfaces;
using Domain.Models;
using Domain.ModelsDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
 
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IMediator _mediator;
        public ProductController(ILogger<ProductController> logger , IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        // GET: ProductController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("get all products");
            IEnumerable<Product> list = await _mediator.Send(new GetAllProducts());
            return View(list);
        }

        // GET: ProductController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            _logger.LogInformation("get product by id ");
            Product item = await _mediator.Send(new GetProductById { Id = id });
            return View(item);
        }
        [Authorize(Roles = "Admin")]
        // GET: ProductController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.Categories = await _mediator.Send(new GetAllCategories {});
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]  CreateProduct command )
        {
            _logger.LogInformation("create product");
            //uploading the image
            if (command.ProductImg != null && command.ProductImg.Length >= 1)
            {
                var filename = $"{DateTime.Now.ToFileTime()}{Path.GetExtension(command.ProductImg.FileName)}";
                var imagepath = Path.GetFullPath( Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages", filename));
                using (var stream = new FileStream(imagepath, FileMode.Create))
                {
                    await command.ProductImg.CopyToAsync(stream);
                }
                command.ProductImgPath = filename;
            }
            // send request with Product model to mediatR to handle it
            var output = await _mediator.Send(command);
            _logger.LogInformation("Product Created");
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            _logger.LogInformation("get product by id ");
            Product item = await _mediator.Send(new GetProductById { Id = id });
            ProductDto productdto = new ProductDto();
            productdto.CategoryId = item.CategoryId;
            productdto.Description = item.Description;
            productdto.Id = item.Id;
            productdto.InStock = item.InStock;
            productdto.NameAr = item.NameAr;
            productdto.NameEn = item.NameEn;
            productdto.Price = item.Price;
            productdto.ProductImgPath = item.ProductImgPath;
            ViewBag.Categories = await _mediator.Send(new GetAllCategories { });
            return View(productdto);
        }
        [Authorize(Roles = "Admin")]
        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UpdateProduct command)
        {
            if (id != command.Id)
            {
                _logger.LogError("id not found");
                return BadRequest();
            }
            else
            {
                //uploading the image
                if (command.ProductImg != null && command.ProductImg.Length >= 1)
                {
                    var filename = $"{DateTime.Now.ToFileTime()}{Path.GetExtension(command.ProductImg.FileName)}";
                    var imagepath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages", filename));
                    using (var stream = new FileStream(imagepath, FileMode.Create))
                    {
                        await command.ProductImg.CopyToAsync(stream);
                    }
                    command.ProductImgPath = filename;
                }
                else
                {
                    command.ProductImgPath = command.ProductImgPath;
                }
                var output = await _mediator.Send(command);
                return RedirectToAction("Index");
            }

        }
        // POST: ProductController/Delete/5
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> Delete(string id)
        {
            var product = await _mediator.Send(new GetProductById { Id = id });
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        // POST: ProductController1/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                string output = await _mediator.Send(new DeleteProduct { Id = id });
                return View(output);
            }

        }
    }
}

