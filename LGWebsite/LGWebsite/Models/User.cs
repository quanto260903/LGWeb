using System;
using System.Collections.Generic;

namespace LGWebsite.Models;

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
}
