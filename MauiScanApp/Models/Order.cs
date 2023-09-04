﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MauiScanApp.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderTime { get; set; }

    public int MemberId { get; set; }

    public string TaxId { get; set; }

    public int StatusId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? BillingAmount { get; set; }

    public int? CouponId { get; set; }

    public int? Count { get; set; }

    public virtual ICollection<Bonus> Bonus { get; set; } = new List<Bonus>();

    public virtual ICollection<Comment> Comment { get; set; } = new List<Comment>();

    public virtual CouponList Coupon { get; set; }

    public virtual CustomerInfomation Member { get; set; }

    public virtual ICollection<Message> Message { get; set; } = new List<Message>();

    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();

    public virtual Status Status { get; set; }
}