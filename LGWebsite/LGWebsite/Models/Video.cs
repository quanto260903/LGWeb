using System;
using System.Collections.Generic;

namespace LGWebsite.Models;

public partial class Video
{
    public int Id { get; set; }

    public int BlogCategoryId { get; set; }

    public string? LinkUrl { get; set; }

    public string? Description { get; set; }
}
