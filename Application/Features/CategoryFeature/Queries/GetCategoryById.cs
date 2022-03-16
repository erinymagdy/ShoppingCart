using Application.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeature.Queries
{
    public class GetCategoryById : IRequest<Category>
    {
        public string Id { get; set; }
        public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, Category>
        {
            private readonly IApplicationDbContext _context;
            public GetCategoryByIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Category> Handle(GetCategoryById query, CancellationToken cancellationToken)
            {
                var category = _context.Categories.Where(a => a.Id == query.Id).FirstOrDefault();
                if (category == null) return null;
                return category;
            }
        }
    }
}
