﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KVSC.Data.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? CustomerId { get; set; }

    public int? DoctorId { get; set; }

    public int? FishId { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public string Status { get; set; }

    public string PaymentStatus { get; set; }

    public bool? HomeVisit { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AppointmentDetail> AppointmentDetails { get; set; } = new List<AppointmentDetail>();

    public virtual Customer Customer { get; set; }

    public virtual Doctor Doctor { get; set; }

    public virtual Fish Fish { get; set; }
}