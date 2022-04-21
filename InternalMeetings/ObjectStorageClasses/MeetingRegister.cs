using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalMeetings
{
    public class MeetingRegister
    {
        public Meeting meeting { get; private set; }
        private List<Participant> Participants { get; set; }
        /// <summary>
        /// Constructor that sets the first participant to be the host and assigns meeting values
        /// </summary>
        /// <param name="meeting"></param>
        public MeetingRegister(Meeting meeting)
        {
            this.meeting = meeting;
            Participants = new List<Participant>();
            Participants.Add(new Participant(meeting.ResponsiblePerson, meeting.StartDate, meeting.EndDate, meeting.Name));
        }
        /// <summary>
        /// Method used to print Current meeting
        /// </summary>
        public void PrintMeeting()
        {
            Console.WriteLine(meeting.ToString()); 
        }
        /// <summary>
        /// Method used to print Current participants in a meeting
        /// </summary>
        public void PrintParticipants()
        {
            foreach (var participant in Participants)
                Console.WriteLine(participant.ToString());
        }
        /// <summary>
        /// Method used to prepare a final list for json file save
        /// </summary>
        /// <param name="listToAddTo">list of incrementing list of participants</param>
        public void ExportParticipants(List<Participant> listToAddTo)
        {
            foreach (var item in Participants)
            {
                listToAddTo.Add(item);
            }
        }
        /// <summary>
        /// Method that adds a new participant if it is already not in the list, avoids duplicates
        /// </summary>
        /// <param name="participant">new participant</param>
        public void addParticipant(Participant participant)
        {
            if (!checkIfContains(participant))
            {
                Participants.Add(participant);
            }
        }
        /// <summary>
        /// Method that checks if the passed participant already exists in the meeting
        /// </summary>
        /// <param name="participant">new participant</param>
        /// <returns>returns true if it contains that participant, otherwise not</returns>
        public bool checkIfContains(Participant participant)
        {
            foreach (var p in Participants)
            {
                if (p.Equals(participant))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Method used to find Participant in a meeting by name
        /// </summary>
        /// <param name="name">participant's name</param>
        /// <returns>returns participant, otherwise null</returns>
        public Participant findParticipantByName(string name)
        {
            for (int i = 0; i < Participants.Count; i++)
            {
                if (Participants[i].Name == name)
                    return Participants[i];
            }
            return null;
        }
        /// <summary>
        /// Method that removes a passed Participant argument
        /// Does not delete participant, if he is thehost
        /// </summary>
        /// <param name="participant">participant that should be deleted</param>
        public void removeParticipant(Participant participant)
        {
            if (!Participants.Contains(participant))
            {
                Console.WriteLine("Error, no such participant to remove");
            }
            else
            {
                if (participant.Name != meeting.ResponsiblePerson)
                {
                    Participants.Remove(participant);
                }
                else
                {
                    Console.WriteLine("Not allowed to remove Host participant");
                }
            }
        }
        /// <summary>
        /// Method that returns the current count of participants
        /// </summary>
        /// <returns>count of participants</returns>
        public int participantCount()
        {
            return Participants.Count;
        }
    }
}
