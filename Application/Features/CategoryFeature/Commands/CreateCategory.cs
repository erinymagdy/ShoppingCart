using Application.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeature.Commands
{
    public class CreateCategory : IRequest<string>
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
       
        public class CreateCategoryHandler : IRequestHandler<CreateCategory, string>
        {
            private readonly IApplicationDbContext _context;
            public CreateCategoryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(CreateCategory command, CancellationToken cancellationToken)
            {
                var category = new Category();
                category.NameAr = command.NameAr;
                category.NameEn = command.NameEn;

                _context.Categories.Add(category);
                await _context.SaveChanges();
                return category.Id;
            }
        }
    }
}
