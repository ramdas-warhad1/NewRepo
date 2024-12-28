﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Table("Stock")]
    public class Stock
    {
        public int Id { get; set; }
       
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
