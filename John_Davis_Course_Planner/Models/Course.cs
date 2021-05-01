using SQLite;
using System;

namespace John_Davis_Course_Planner.Models
{
    public class Course
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int TermId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string Notes { get; set; }
        public string NotificationsEnabled { get; set; }

        public override string ToString()
        {
            return this.Name + " (" + this.StartDate.ToString("MM/dd/yyyy") + ") - " + "(" + this.EndDate.ToString("MM/dd/yyyy") + ")";
        }
    }
}
