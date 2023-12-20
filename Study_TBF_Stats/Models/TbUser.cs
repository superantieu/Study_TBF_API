using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Study_TBF_Stats.Models;

[Table("tbUser")]
public partial class TbUser/* : IdentityUser*/
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    public string? FullName { get; set; }

    [StringLength(50)]
    public string? Discipline { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<TbTimeSheet> TbTimeSheets { get; set; } = new List<TbTimeSheet>();
}
