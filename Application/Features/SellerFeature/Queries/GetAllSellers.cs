using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SellerFeature.Queries
{
    public class GetAllSellers : IRequest<IEnumerable<Seller>>
    {
        public class GetAllSellersHandler : IRequestHandler<GetAllSellers, IEnumerable<Seller>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllSellersHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Seller>> Handle(GetAllSellers query, CancellationToken cancellationToken)
            {
                var List = await _context.Sellers.ToListAsync();
                if (List == null)
                {
                    return null;
                }
                return List.AsReadOnly();
            }
        }
    }
}
