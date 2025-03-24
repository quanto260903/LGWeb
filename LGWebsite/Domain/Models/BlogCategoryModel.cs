using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BlogCategoryModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }


        public string? Detail { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsDarft { get; set; }

        public bool? IsHome { get; set; }

        public int Styles { get; set; }

        public int Order { get; set; }

        public string? Color { get; set; }


        public string? ImageUrl { get; set; }

        public string? Position { get; set; }

        public string? Features { get; set; }

        [ValidateNever]
        public string? UpdateBy { get; set; }
        public DateTimeOffset? DateCreated { get; set; }

        public DateTimeOffset? DateModified { get; set; }

        [NotMapped] // Không lưu vào cơ sở dữ liệu
        [ValidateNever]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public string? ThumbnailUrl { get; set; }
        public List<Video> Videos { get; set; } = new List<Video>();

        public List<BlogCategoryModel> ListBlogCategory { get; set; } = new List<BlogCategoryModel>();
       
    }
}