using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? PassWord { get; set; }

    public string? Email { get; set; }

    public DateTimeOffset? DateCreated { get; set; }

    public DateTimeOffset? DateModified { get; set; }

    public bool Active { get; set; }

    public string? ImageUrl { get; set; }

    public int Role { get; set; }
    [NotMapped] // Không lưu vào cơ sở dữ liệu
    [ValidateNever]
    public IFormFile ImageFile { get; set; }
    [NotMapped]
    public string? ThumbnailUrl { get; set; }
}
