using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SellerFeature.Queries
{
    public class GetSellerById : IRequest<Seller>
    {
        public string Id { get; set; }
        public class GetSellerByIdHandler : IRequestHandler<GetSellerById, Seller>
        {
            private readonly IApplicationDbContext _context;
            public GetSellerByIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Seller> Handle(GetSellerById query, CancellationToken cancellationToken)
            {
                var Seller = _context.Sellers.Where(a => a.Id == query.Id).FirstOrDefault();
                if (Seller == null) return null;
                return Seller;
            }
        }
    }
}
