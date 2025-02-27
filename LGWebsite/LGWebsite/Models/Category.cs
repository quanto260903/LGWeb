using System;
using System.Collections.Generic;

namespace LGWebsite.Models;

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
}
