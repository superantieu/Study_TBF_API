using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Study_TBF_Stats.Models.Dto
{
    public class TbProjectDto
    {

        public int ProjectId { get; set; }


        public string? ProjectName { get; set; }

        public int? TotalHours { get; set; }

        public int? Tasks { get; set; }

        public int? FloorAreas { get; set; }


        public DateTime? StartDate { get; set; }


        public DateTime? CompletedDate { get; set; }


        public DateTime? TargetDate { get; set; }

        public int? Completion { get; set; }

        public bool? IsComplete { get; set; }

        public string? Listmember { get; set; }

        public int? UsedHours { get; set; }

        public object? Members { get; set; }

    }
}
