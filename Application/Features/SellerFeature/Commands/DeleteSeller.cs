using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SellerFeature.Commands
{
    public class DeleteSeller : IRequest<string>
    {
        public string Id { get; set; }
        public class DeleteSellerHandler : IRequestHandler<DeleteSeller, string>
        {
            private readonly IApplicationDbContext _context;
            public DeleteSellerHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(DeleteSeller command, CancellationToken cancellationToken)
            {
                var Seller = await _context.Sellers.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (Seller == null) return default;
                _context.Sellers.Remove(Seller);
                await _context.SaveChanges();
                return Seller.Id;
            }
        }
    }
}
