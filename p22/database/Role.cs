using System;
using System.Collections.Generic;

namespace p22.database;

public partial class Role
{
    public int Idrole { get; set; }

    public string Role1 { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
