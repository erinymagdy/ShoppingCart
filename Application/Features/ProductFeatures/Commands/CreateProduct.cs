using Application.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{

    public class CreateProduct : IRequest<Product>
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public IFormFile ProductImg { get; set; }
        public string ProductImgPath { get; set; }
        public bool InStock { get; set; }
        public string CategoryId { get; set; }
        public class CreateProductHandler : IRequestHandler<CreateProduct, Product>
        {
            private readonly IApplicationDbContext _context;
            public CreateProductHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Product> Handle(CreateProduct command, CancellationToken cancellationToken)
            {
                var product = new Product();
                product.NameAr = command.NameAr;
                product.NameEn = command.NameEn;
                product.Price = command.Price;
                product.InStock = command.InStock;
                product.Description = command.Description;
                product.ProductImgPath = command.ProductImgPath;
                product.CategoryId = command.CategoryId;
                _context.Products.Add(product);
                await _context.SaveChanges();
                return product;
            }
        }
    }
}
