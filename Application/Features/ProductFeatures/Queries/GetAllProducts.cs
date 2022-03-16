using Application.Interfaces;
using Domain.Models;
using Domain.ModelsDto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Queries
{
    public class GetAllProducts : IRequest<IEnumerable<Product>>
    {
        public class GetAllProductsHandler : IRequestHandler<GetAllProducts, IEnumerable<Product>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllProductsHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetAllProducts query, CancellationToken cancellationToken)
            {
                var productList = await _context.Products.Include(x=>x.Category).ToListAsync();
                if (productList == null)
                {
                    return null;
                }
     
                    return productList.AsReadOnly();
            }
        }
    }
}
