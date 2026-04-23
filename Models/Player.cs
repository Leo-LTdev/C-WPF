using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Player
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? LoginId { get; set; }

    public virtual Login? Login { get; set; }

    public virtual ICollection<Hero> Heroes { get; set; } = new List<Hero>();
}
