﻿using System;
using System.Collections.Generic;

namespace MovieServices.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
