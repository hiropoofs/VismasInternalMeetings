using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalMeetings
{
    /// <summary>
    /// Class with a constructor that defines a Meeting
    /// </summary>
    public class Meeting :IEquatable<Meeting>
    {
        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public EventType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Meeting(string Name, string ResponsiblePerson, string Description, Category Category, EventType Type, DateTime StartDate, DateTime EndDate)
        {
            this.Name = Name;
            this.ResponsiblePerson = ResponsiblePerson;
            this.Description = Description;
            this.Category = Category;
            this.Type = Type;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", Name, ResponsiblePerson, StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd"));
        }

        public bool Equals(Meeting? other)
        {
            throw new NotImplementedException();
        }
    }
}
