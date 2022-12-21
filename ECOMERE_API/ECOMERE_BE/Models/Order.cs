﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ECOMERE_BE.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public string Id { get; set; }
        public decimal? Total { get; set; }
        public string UserId { get; set; }
        public string PaymentTransactionId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}