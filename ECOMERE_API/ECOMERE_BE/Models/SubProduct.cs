﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ECOMERE_BE.Models
{
    public partial class SubProduct
    {
        public SubProduct()
        {
            SubSubProduct = new HashSet<SubSubProduct>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public byte[] ModifiedBy { get; set; }
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<SubSubProduct> SubSubProduct { get; set; }
    }
}