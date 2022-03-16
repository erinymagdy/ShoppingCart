using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SellerFeature.Commands
{
    public class UpdateSeller : IRequest<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public class UpdateSellerHandler : IRequestHandler<UpdateSeller, string>
        {
            private readonly IApplicationDbContext _context;
            public UpdateSellerHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(UpdateSeller command, CancellationToken cancellationToken)
            {
                var Seller = _context.Sellers.Where(a => a.Id == command.Id).FirstOrDefault();

                if (Seller == null)
                {
                    return default;
                }
                else
                {
                    Seller.Name = command.Name;
                    Seller.IsActive = command.IsActive;
                    await _context.SaveChanges();
                    return Seller.Id;
                }
            }
        }
    }
}
