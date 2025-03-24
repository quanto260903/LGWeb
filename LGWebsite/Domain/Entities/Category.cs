using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTimeOffset? DateCreated { get; set; }

    public DateTimeOffset? DateModified { get; set; }

    public string? UpdateBy { get; set; }

    public string? PositionTab { get; set; }
    [NotMapped] // Không lưu vào cơ sở dữ liệu
    [ValidateNever]
    public IFormFile ImageFile { get; set; } // File ảnh được upload
    [NotMapped]
    public string? ThumbnailUrl { get; set; }
}
