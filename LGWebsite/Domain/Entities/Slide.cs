using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public partial class Slide
{
    public int Id { get; set; }

    public string? ImageUrl { get; set; }

    public string? Url { get; set; }

    public bool? IsEnabled { get; set; }

    public DateTimeOffset? DateCreated { get; set; }

    public DateTimeOffset? DateModified { get; set; }

    public string? UpdateBy { get; set; }
    [NotMapped]
    [ValidateNever]
    public IFormFile ImageFile { get; set; }
    [NotMapped]
    public string? ThumbnailUrl { get; set; }
}
