using Application.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeature.Queries
{
    public class GetAllCategories : IRequest<IEnumerable<Category>>
    {
        public class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, IEnumerable<Category>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCategoriesHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Category>> Handle(GetAllCategories query, CancellationToken cancellationToken)
            {
                var categoryList = await _context.Categories.ToListAsync();
                if (categoryList == null)
                {
                    return null;
                }
                return categoryList.AsReadOnly();
            }
        }
    }
}
