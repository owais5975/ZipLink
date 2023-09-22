﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipLink.Core.Entities
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
