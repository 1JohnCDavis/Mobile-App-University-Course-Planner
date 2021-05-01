using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace John_Davis_Course_Planner.Models
{
    public class Term
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return this.Name + " (" + this.StartDate.ToString("MM/dd/yyyy") + ") - " + "(" + this.EndDate.ToString("MM/dd/yyyy") + ")";
        }
    }
}
