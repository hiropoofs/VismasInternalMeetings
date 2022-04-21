using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalMeetings
{
    public class InOutUtils
    {
        /// <summary>
        /// Method used to save current Meeting data to json file
        /// </summary>
        /// <param name="register">List of meetings with corresponding participants</param>
        public static void SaveJSON(List<MeetingRegister> register)
        {
            List<Meeting> mlist = new List<Meeting>();
            List<Participant> plist = new List<Participant>();
            foreach (MeetingRegister regunit in register)
            {
                mlist.Add(regunit.meeting);
                regunit.ExportParticipants(plist);
            }
            File.WriteAllText(@"../../../mcont.json", JsonConvert.SerializeObject(mlist));
            File.WriteAllText(@"../../../pcont.json", JsonConvert.SerializeObject(plist));
        }
        /// <summary>
        /// Method used to read json file into a list of MeetingRegister data
        /// </summary>
        /// <returns>returns a list of MeetingRegister data</returns>
        public static List<MeetingRegister> ReadJSON()
        {
            List<MeetingRegister> registerlist = new List<MeetingRegister>();

            string participantsJson = File.ReadAllText(@"../../../pcont.json");
            List<Participant> participants = JsonConvert.DeserializeObject<List<Participant>>(participantsJson);

            string meetingsJson = File.ReadAllText(@"../../../mcont.json");
            List<Meeting> meetings = JsonConvert.DeserializeObject<List<Meeting>>(meetingsJson);

            foreach (Meeting meeting in meetings)
            {
                MeetingRegister reg = new MeetingRegister(meeting);

                foreach (Participant par in participants)
                {
                    if (par.EventName == meeting.Name)
                    {
                        reg.addParticipant(par);
                    }
                }
                registerlist.Add(reg);
            }
            return registerlist;
        }
        /// <summary>
        /// Method used to print to console meetings, which are filtered by a filter
        /// </summary>
        /// <param name="reglist">list of all the meetings</param>
        /// <param name="filter">Filter input by what to filter data</param>
        public static void PrintByFilter(List<MeetingRegister> reglist, FilterOptions filter)
        {
            switch (filter)
            {
                case FilterOptions.description:
                    string desc = Program.ReaderLine("\nType keyword to filter by description: (ex. \".NET\")", InputOptions.any, true);
                    FilterByDescription(reglist, desc);
                    break;
                case FilterOptions.responsiblePerson:
                    string resp = Program.ReaderLine("\nType a name to filter by responsible person: (ex. \"Simonas\")", InputOptions.onlyLetters, false);
                    FilterByResponsiblePerson(reglist, resp);
                    break;
                case FilterOptions.category:
                    Program.PrintEnum<Category>();
                    int cate = int.Parse(Program.ReaderLine("\nType a corresponding number to filter by category: (ex. \"1\")", InputOptions.onlyDigits, false));
                    FilterByCategory(reglist, cate);
                    break;
                case FilterOptions.type:
                    Program.PrintEnum<FilterOptions>();
                    int type = int.Parse(Program.ReaderLine("\nType a corresponding number to filter by type: (ex. \"1\")", InputOptions.onlyDigits, false));
                    FilterByType(reglist, type);
                    break;
                case FilterOptions.dates:
                    DateTime date1 = Convert.ToDateTime(Program.ReaderLine("\nType the beggining of a date: (ex. \"2000-05-10\")", InputOptions.onlyDate, false));
                    DateTime date2 = Convert.ToDateTime(Program.ReaderLine("\nType the beggining of a date: (ex. \"2001-07-12\")", InputOptions.onlyDate, false));
                    FilterByDates(reglist, date1, date2);
                    break;
                case FilterOptions.number:
                    int number = int.Parse(Program.ReaderLine("\nType a number that would specify minimum attendees count in a meeting: (ex. \"2\")", InputOptions.onlyDigits, false));
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Method used to print out meetings that only contain keyword in a description from filterText
        /// </summary>
        /// <param name="reglist">meetings list</param>
        /// <param name="filterText">description keyword to find meetings</param>
        private static void FilterByDescription(List<MeetingRegister> reglist, string filterText)
        {
            foreach (MeetingRegister reg in reglist)
            {
                if(reg.meeting.Description.Contains(filterText))
                {
                    reg.PrintMeeting();
                }
            }
        }
        /// <summary>
        /// Method used to print out meetings by filtering out a responsible persom from filterText
        /// </summary>
        /// <param name="reglist">meetings list</param>
        /// <param name="filterText">Responsible person's name</param>
        private static void FilterByResponsiblePerson(List<MeetingRegister> reglist, string filterText)
        {
            foreach (MeetingRegister reg in reglist)
            {
                if (reg.meeting.ResponsiblePerson.Equals(filterText))
                {
                    reg.PrintMeeting();
                }
            }
        }
        /// <summary>
        /// Method used to print out meetings by filtering out Category from filterText
        /// </summary>
        /// <param name="reglist">meetings list</param>
        /// <param name="filterText">Category's name</param>
        private static void FilterByCategory(List<MeetingRegister> reglist, int filterInt)
        {
            foreach (MeetingRegister reg in reglist)
            {
                if (reg.meeting.Category == (Category)filterInt)
                {
                    reg.PrintMeeting();
                }
            }
        }
        /// <summary>
        /// Method used to print out meetings by filtering out by Filter from filterText
        /// </summary>
        /// <param name="reglist">meetings list</param>
        /// <param name="filterText">Filter's name</param>
        private static void FilterByType(List<MeetingRegister> reglist, int filterInt)
        {
            foreach (MeetingRegister reg in reglist)
            {
                if (reg.meeting.Type == (EventType)filterInt)
                {
                    reg.PrintMeeting();
                }
            }
        }
        /// <summary>
        /// Method used to print out meeting that fall in between Date1 and Date2 brackets
        /// </summary>
        /// <param name="reglist">meetings list</param>
        /// <param name="date1">Start of a date</param>
        /// <param name="date2">End of a date</param>
        private static void FilterByDates(List<MeetingRegister> reglist, DateTime date1, DateTime date2)
        {
            Console.WriteLine("Meetings between dates " + date1.ToString("yyyy-mm-dd") + " and " + date2.ToString("yyyy-mm-dd"));
            foreach (MeetingRegister reg in reglist)
            {
                if (reg.meeting.StartDate >= date1 && reg.meeting.StartDate <= date2 || reg.meeting.EndDate >= date1 && reg.meeting.EndDate <= date2)
                {
                    reg.PrintMeeting();
                }
            }
        }
    }
}
