using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{

    public class UpdateProduct : IRequest<string>
    {
        public string Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        [Display(Name = "Image")]
        public IFormFile ProductImg { get; set; }

        [Display(Name = "Image Name")]
        public string ProductImgPath { get; set; }
        public bool InStock { get; set; }
        public string CategoryId { get; set; }
        public class UpdateProductHandler : IRequestHandler<UpdateProduct, string>
        {
            private readonly IApplicationDbContext _context;
            public UpdateProductHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(UpdateProduct command, CancellationToken cancellationToken)
            {
                var product = _context.Products.Where(a => a.Id == command.Id).FirstOrDefault();

                if (product == null)
                {
                    return default;
                }
                else
                {
                    product.NameAr = command.NameAr;
                    product.NameEn = command.NameEn;
                    product.Price = command.Price;
                    product.Description = command.Description;
                    product.ProductImgPath = command.ProductImgPath;
                    await _context.SaveChanges();
                    return product.Id;
                }
            }
        }
    }
}
