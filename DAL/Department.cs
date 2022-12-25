﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public partial class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [Required]
    [StringLength(200)]
    [Unicode(false)]
    public string Name { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}