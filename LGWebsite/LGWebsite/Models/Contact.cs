using System;
using System.Collections.Generic;

namespace LGWebsite.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTimeOffset? DateCreated { get; set; }

    public DateTimeOffset? DateModified { get; set; }

    public string? Phone { get; set; }

    public bool IsRead { get; set; }

    public string? UpdateBy { get; set; }
}
