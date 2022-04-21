using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalMeetings
{
    /// <summary>
    /// Class with a constructor that defines Participant
    /// </summary>
    public class Participant : IEquatable<Participant>
    {
        public string Name { get; set; }
        public DateTime ParticipationStartDate { get; set; }
        public DateTime ParticipationEndDate { get; set; }
        public string EventName { get; set; }
        public Participant(string Name, DateTime ParticipationStartDate, DateTime ParticipationEndDate, string EventName)
        {
            this.Name = Name;
            this.ParticipationStartDate = ParticipationStartDate;
            this.ParticipationEndDate = ParticipationEndDate;
            this.EventName = EventName;
        }
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", Name, ParticipationStartDate.ToString("yyyy-MM-dd"), ParticipationEndDate.ToString("yyyy-MM-dd"), EventName);
        }
        public bool Equals(Participant? other)
        {
            if (this.EventName == other.EventName && this.Name == other.Name && this.ParticipationStartDate == other.ParticipationStartDate 
                                                                                    && this.ParticipationEndDate == other.ParticipationEndDate)
            {
                return true;
            }
            return false;
        }

        public static bool operator <=(Participant left, Participant right)
        {
            return left.ParticipationStartDate <= right.ParticipationStartDate && left.ParticipationEndDate >= right.ParticipationEndDate;
        }
        public static bool operator >=(Participant left, Participant right)
        {
            return !(left.ParticipationStartDate == right.ParticipationStartDate && left.ParticipationEndDate == right.ParticipationEndDate);
        }
    }
}
