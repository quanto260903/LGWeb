using System;
using System.Collections.Generic;

namespace LGWebsite.Models;

public partial class WebConfiguration
{
    public int Id { get; set; }

    public DateTimeOffset? DateCreated { get; set; }

    public DateTimeOffset? DateModified { get; set; }

    public string ConfigKey { get; set; } = null!;

    public string ConfigValue { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? UpdateBy { get; set; }
}
