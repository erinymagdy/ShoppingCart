using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class DeleteProduct : IRequest<string>
    {
        public string Id { get; set; }
        public class DeleteProductHandler : IRequestHandler<DeleteProduct, string>
        {
            private readonly IApplicationDbContext _context;
            public DeleteProductHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(DeleteProduct command, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (product == null) return default;
                _context.Products.Remove(product);
                await _context.SaveChanges();
                return product.Id;
            }
        }
    }
}
