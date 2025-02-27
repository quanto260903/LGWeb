using System;
using System.Collections.Generic;

namespace LGWebsite.Models;

public partial class Slide
{
    public int Id { get; set; }

    public string? ImageUrl { get; set; }

    public string? Url { get; set; }

    public bool? IsEnabled { get; set; }

    public DateTimeOffset? DateCreated { get; set; }

    public DateTimeOffset? DateModified { get; set; }

    public string? UpdateBy { get; set; }
}
