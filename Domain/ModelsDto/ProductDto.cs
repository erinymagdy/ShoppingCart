using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelsDto
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        [Display(Name = "Image Name")]
        public string ProductImgPath { get; set; }
        [Display(Name = "Image")]
        public IFormFile ProductImg { get; set; }
        public bool InStock { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
