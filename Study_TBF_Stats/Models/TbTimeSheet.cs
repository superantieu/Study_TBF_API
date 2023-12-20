using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Study_TBF_Stats.Models;

[Table("tbTimeSheet")]
public partial class TbTimeSheet
{
    [Key]
    public int Id { get; set; }

    public int? TaskId { get; set; }

    public int? ProjectId { get; set; }

    public int? UserId { get; set; }

    [StringLength(50)]
    public string? UserDiscipline { get; set; }

    [Column("TSHours")]
    public int? Tshours { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("TbTimeSheets")]
    public virtual TbProject? Project { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TbTimeSheets")]
    public virtual TbUser? User { get; set; }
}
