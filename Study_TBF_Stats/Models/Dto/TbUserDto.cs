using System.ComponentModel.DataAnnotations;

namespace Study_TBF_Stats.Models.Dto
{
    public class TbUserDto
    {
        public int UserId { get; set; }

        public string? FullName { get; set; }

        public string? Discipline { get; set; }
    }
}
