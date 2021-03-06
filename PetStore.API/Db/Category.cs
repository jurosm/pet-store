﻿using System;
using System.Collections.Generic;

namespace PetStore.API.Db
{
    public partial class Category
    {
        public Category()
        {
            Toy = new HashSet<Toy>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Toy> Toy { get; set; }
    }
}
