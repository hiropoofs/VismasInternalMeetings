using Microsoft.VisualStudio.TestTools.UnitTesting;
using InternalMeetings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalMeetings.Tests
{
    [TestClass()]
    public class TaskUtilsTests
    {
        [TestMethod()]
        public void DoesTheDatesClashTest_DatesClash()
        {
            // Arrange
            Meeting meet = new Meeting("Event", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 05, 09), new DateTime(2000, 05, 12));
            MeetingRegister meetingReg = new MeetingRegister(meet);
            List<MeetingRegister> meetingregList = new List<MeetingRegister>();
            meetingregList.Add(meetingReg);
            Participant newParticipant = new Participant("Aglirdas", new DateTime(2000, 05, 09), new DateTime(2000, 05, 10), "Event");
            bool expectedResult = true;

            // Act
            bool result = TaskUtils.DoesTheDatesClash(meetingregList, newParticipant);

            // assert
            Assert.AreEqual(result, expectedResult);
        }
        [TestMethod()]
        public void DoesTheDatesClashTest_DatesDoNotClash()
        {
            // Arrange
            Meeting meet = new Meeting("Event", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 05, 09), new DateTime(2000, 05, 12));
            MeetingRegister meetingReg = new MeetingRegister(meet);
            List<MeetingRegister> meetingregList = new List<MeetingRegister>();
            meetingregList.Add(meetingReg);
            Participant newParticipant = new Participant("Aglirdas", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            bool expectedResult = false;

            // Act
            bool result = TaskUtils.DoesTheDatesClash(meetingregList, newParticipant);

            // assert
            Assert.AreEqual(result, expectedResult);
        }
        [TestMethod()]
        public void AddAllParticipantsTest()
        {
            // Arrange
            Meeting meet = new Meeting("Event", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 05, 09), new DateTime(2000, 05, 12));
            Participant meetCurrator = new Participant("Algirdas", new DateTime(2000, 05, 09), new DateTime(2000, 05, 12), "Event");
            Participant newParticipant = new Participant("Igoris", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            Participant newParticipant2 = new Participant("Simonas", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            MeetingRegister meetingReg = new MeetingRegister(meet);
            meetingReg.addParticipant(newParticipant);
            meetingReg.addParticipant(newParticipant2);

            Meeting meet2 = new Meeting("Event2", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 06, 09), new DateTime(2000, 06, 12));
            Participant meetCurrator2 = new Participant("Algirdas", new DateTime(2000, 06, 09), new DateTime(2000, 06, 12), "Event2");
            Participant newParticipant3 = new Participant("Igoris", new DateTime(2000, 06, 13), new DateTime(2000, 06, 15), "Event2");
            Participant newParticipant4 = new Participant("Simonas", new DateTime(2000, 06, 13), new DateTime(2000, 06, 15), "Event2");
            MeetingRegister meetingReg2 = new MeetingRegister(meet2);
            meetingReg2.addParticipant(newParticipant);
            meetingReg2.addParticipant(newParticipant2);

            List<MeetingRegister> meetingregList = new List<MeetingRegister>();
            meetingregList.Add(meetingReg);
            meetingregList.Add(meetingReg2);

            List<Participant> expectedResult = new List<Participant>();
            expectedResult.Add(meetCurrator);
            expectedResult.Add(newParticipant);
            expectedResult.Add(newParticipant2);
            expectedResult.Add(meetCurrator2);
            expectedResult.Add(newParticipant3);
            expectedResult.Add(newParticipant4);

            // Act
            List<Participant> plist = TaskUtils.AddAllParticipants(meetingregList);

            // assert
            Assert.AreEqual(expectedResult.Count, plist.Count);
        }
        [TestMethod()]
        public void AddParticipantToCorrespondingMeetingTest()
        {
            // Arrange
            Meeting meet = new Meeting("Event", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 05, 09), new DateTime(2000, 05, 12));
            Participant newParticipant1 = new Participant("Igoris", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            Participant newParticipant2 = new Participant("Simonas", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            MeetingRegister meetingReg = new MeetingRegister(meet);
            meetingReg.addParticipant(newParticipant1);
            meetingReg.addParticipant(newParticipant2);

            Meeting meet2 = new Meeting("Event2", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 06, 09), new DateTime(2000, 06, 12));
            Participant newParticipant3 = new Participant("Igoris", new DateTime(2000, 06, 13), new DateTime(2000, 06, 15), "Event2");
            Participant newParticipant4 = new Participant("Simonas", new DateTime(2000, 06, 13), new DateTime(2000, 06, 15), "Event2");
            MeetingRegister meetingReg2 = new MeetingRegister(meet2);
            meetingReg2.addParticipant(newParticipant1);
            meetingReg2.addParticipant(newParticipant2);

            List<MeetingRegister> meetingregList = new List<MeetingRegister>();
            meetingregList.Add(meetingReg);
            meetingregList.Add(meetingReg2);

            Participant addingParticipant = new Participant("Hans", new DateTime(2000, 06, 13), new DateTime(2000, 06, 13), "Event");
            bool expectedResult = true;

            // Act
            TaskUtils.AddParticipantToCorrespondingMeeting(meetingregList, addingParticipant);
            bool Result = meetingregList[1].participantCount() == 3;

            // assert
            Assert.IsTrue(Result);
        }
    }
}