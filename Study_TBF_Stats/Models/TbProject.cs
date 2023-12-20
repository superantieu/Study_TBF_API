using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Study_TBF_Stats.Models;

[Table("tbProject")]
public partial class TbProject
{
    [Key]
    public int ProjectId { get; set; }

    [StringLength(50)]
    public string? ProjectName { get; set; }

    public int? TotalHours { get; set; }

    public int? Tasks { get; set; }

    public int? FloorAreas { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CompletedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TargetDate { get; set; }

    public int? Completion { get; set; }

    public bool? IsComplete { get; set; }

    public string? Listmember { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<TbTimeSheet> TbTimeSheets { get; set; } = new List<TbTimeSheet>();
}
