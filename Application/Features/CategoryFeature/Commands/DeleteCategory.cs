using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeature.Commands
{
    public class DeleteCategory : IRequest<string>
    {
        public string Id { get; set; }
        public class DeleteCategoryHandler : IRequestHandler<DeleteCategory, string>
        {
            private readonly IApplicationDbContext _context;
            public DeleteCategoryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(DeleteCategory command, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (category == null) return default;
                _context.Categories.Remove(category);
                await _context.SaveChanges();
                return category.Id;
            }
        }
    }
}
