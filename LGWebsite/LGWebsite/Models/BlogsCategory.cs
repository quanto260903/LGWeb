﻿using System;
using System.Collections.Generic;

namespace LGWebsite.Models;

public partial class BlogsCategory
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Detail { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsDraft { get; set; }

    public bool? IsHome { get; set; }

    public string? ImageUrl { get; set; }

    public DateTimeOffset? DateCreated { get; set; }

    public DateTimeOffset? DateModified { get; set; }

    public string? UpdateBy { get; set; }

    public int Order { get; set; }

    public string? Color { get; set; }

    public string? Position { get; set; }

    public string? Features { get; set; }
}
