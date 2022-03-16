using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SellerFeature.Commands
{
    public class CreateSeller : IRequest<string>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public class CreateSellerHandler : IRequestHandler<CreateSeller, string>
        {
            private readonly IApplicationDbContext _context;
            public CreateSellerHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(CreateSeller command, CancellationToken cancellationToken)
            {
                var Seller = new Seller();
                Seller.Name = command.Name;
                Seller.IsActive = command.IsActive;

                _context.Sellers.Add(Seller);
                await _context.SaveChanges();
                return Seller.Id;
            }
        }
    }
}
