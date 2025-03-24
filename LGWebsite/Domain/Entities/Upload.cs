using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Upload
{
    public int Id { get; set; }

    public string? ImageUrl { get; set; }

    public string? ImageThumbUrl { get; set; }

    public string? ImageIconUrl { get; set; }

    public DateTimeOffset? DateCreated { get; set; }

    public DateTimeOffset? DateModified { get; set; }
}
