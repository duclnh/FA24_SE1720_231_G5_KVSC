﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KVSC.Data.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; }

    public string Description { get; set; }

    public decimal? BasePrice { get; set; }

    public decimal? HomeVisitFee { get; set; }

    public int? Duration { get; set; }

    public int? CategoryId { get; set; }

    public bool? Availability { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ServiceCategory Category { get; set; }

    public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
}