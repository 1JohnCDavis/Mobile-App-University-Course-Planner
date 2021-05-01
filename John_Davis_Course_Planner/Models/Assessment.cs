using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace John_Davis_Course_Planner.Models
{
    public class Assessment
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public string NotificationsEnabled { get; set; }

        public override string ToString()
        {
            return this.Name + " (" + this.DueDate.ToString("MM/dd/yyyy") + ")";
        }
    }
}
