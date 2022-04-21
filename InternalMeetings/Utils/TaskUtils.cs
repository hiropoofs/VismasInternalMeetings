using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalMeetings
{
    public static class TaskUtils
    {
        public static bool DoesTheDatesClash(List<MeetingRegister> mReg, Participant newParticipant)
        {
            List<Participant> plist = AddAllParticipants(mReg);
            foreach (var participant in plist)
            {
                if (participant.Name == newParticipant.Name && participant.EventName == newParticipant.EventName)
                {
                    if (participant <= newParticipant)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static List<Participant> AddAllParticipants(List<MeetingRegister> mReg)
        {
            List<Participant> participants = new List<Participant>();
            foreach (var m in mReg)
            {
                m.ExportParticipants(participants);
            }
            return participants;
        }
        public static void AddParticipantToCorrespondingMeeting(List<MeetingRegister> meetinglist, Participant newParticipant)
        {
            foreach (var mReg in meetinglist)
            {
                if (mReg.meeting.Name == newParticipant.EventName)
                {
                    mReg.addParticipant(newParticipant);
                }
            }
        }
    }
}
