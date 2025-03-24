using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public partial class Menu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public string? Icon { get; set; }

    public int? ParentId { get; set; }

    public int? SortOrder { get; set; }

    public int? PositionType { get; set; }

    public DateTimeOffset? DateCreated { get; set; }

    public DateTimeOffset? DateModified { get; set; }

    public string? UpdateBy { get; set; }

    public string? LinkUrl { get; set; }
    [NotMapped]
    [ValidateNever]
    public IFormFile ImageFile { get; set; }

    [NotMapped]
    public string? ThumbnailUrl { get; set; }

    [NotMapped]
    [ValidateNever]
    public IFormFile IconFile { get; set; }
}
