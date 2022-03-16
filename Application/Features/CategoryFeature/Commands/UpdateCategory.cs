using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeature.Commands
{
    public class UpdateCategory : IRequest<string>
    {
        public string Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public class UpdateCategoryHandler : IRequestHandler<UpdateCategory, string>
        {
            private readonly IApplicationDbContext _context;
            public UpdateCategoryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(UpdateCategory command, CancellationToken cancellationToken)
            {
                var category = _context.Categories.Where(a => a.Id == command.Id).FirstOrDefault();

                if (category == null)
                {
                    return default;
                }
                else
                {
   
                    category.NameEn = command.NameEn;
                    category.NameAr = command.NameAr;
                    await _context.SaveChanges();
                    return category.Id;
                }
            }
        }
    }
}
