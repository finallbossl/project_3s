using System;

namespace FSA_3S.Models.Respone
{
    public class WorkResponse
    {
        public int WorkId { get; set; }
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Monday { get; set; }
        public string? MondayTime { get; set; }
        public string? Tuesday { get; set; }
        public string? TuesdayTime { get; set; }
        public string? Wednesday { get; set; }
        public string? WednesdayTime { get; set; }
        public string? Thursday { get; set; }
        public string? ThursdayTime { get; set; }
        public string? Friday { get; set; }
        public string? FridayTime { get; set; }
        public string? Saturday { get; set; }
        public string? SaturdayTime { get; set; }
        public string? Sunday { get; set; }
        public string? SundayTime { get; set; }
    }
}