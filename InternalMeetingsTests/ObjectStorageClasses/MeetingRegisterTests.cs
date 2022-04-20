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
    public class MeetingRegisterTests
    {
        [TestMethod()]
        public void ExportParticipantsTest()
        {
            // Arrange
            Meeting meet = new Meeting("Event", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 05, 09), new DateTime(2000, 05, 12));
            Participant newParticipant1 = new Participant("Igoris", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            Participant newParticipant2 = new Participant("Simonas", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            MeetingRegister meetingReg = new MeetingRegister(meet);
            meetingReg.addParticipant(newParticipant1);
            meetingReg.addParticipant(newParticipant2);

            Participant newParticipant3 = new Participant("Igoris", new DateTime(2000, 06, 13), new DateTime(2000, 06, 15), "Event2");
            Participant newParticipant4 = new Participant("Simonas", new DateTime(2000, 06, 13), new DateTime(2000, 06, 15), "Event2");
            List<Participant> plist = new List<Participant>();
            plist.Add(newParticipant3);
            plist.Add(newParticipant4);

            // Act
            meetingReg.ExportParticipants(plist);

            // assert
            Assert.IsTrue(plist.Count == 5);
        }
        [TestMethod()]
        public void CheckIfContains_Firstelement()
        {
            // Arrange
            Meeting meet = new Meeting("Event", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 05, 09), new DateTime(2000, 05, 12));
            Participant newParticipant1 = new Participant("Igoris", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            Participant newParticipant2 = new Participant("Simonas", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            MeetingRegister meetingReg = new MeetingRegister(meet);
            meetingReg.addParticipant(newParticipant1);
            meetingReg.addParticipant(newParticipant2);

            // Act
            bool result = meetingReg.checkIfContains(newParticipant1);

            // assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void CheckIfContains_SecondElement()
        {
            // Arrange
            Meeting meet = new Meeting("Event", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 05, 09), new DateTime(2000, 05, 12));
            Participant newParticipant1 = new Participant("Igoris", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            Participant newParticipant2 = new Participant("Simonas", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            MeetingRegister meetingReg = new MeetingRegister(meet);
            meetingReg.addParticipant(newParticipant1);
            meetingReg.addParticipant(newParticipant2);

            // Act
            bool result = meetingReg.checkIfContains(newParticipant2);

            // assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void CheckIfContains_UnknownElement()
        {
            // Arrange
            Meeting meet = new Meeting("Event", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 05, 09), new DateTime(2000, 05, 12));
            Participant newParticipant1 = new Participant("Igoris", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            Participant newParticipant2 = new Participant("Simonas", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            MeetingRegister meetingReg = new MeetingRegister(meet);
            meetingReg.addParticipant(newParticipant1);
            meetingReg.addParticipant(newParticipant2);

            Participant newParticipant3 = new Participant("Igoris", new DateTime(2000, 06, 13), new DateTime(2000, 06, 15), "Event2");

            // Act
            bool result = meetingReg.checkIfContains(newParticipant3);

            // assert
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void FindParticipantByNameTest_ExistantParticipant()
        {
            // Arrange
            Meeting meet = new Meeting("Event", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 05, 09), new DateTime(2000, 05, 12));
            Participant newParticipant1 = new Participant("Igoris", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            Participant newParticipant2 = new Participant("Simonas", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            MeetingRegister meetingReg = new MeetingRegister(meet);
            meetingReg.addParticipant(newParticipant1);
            meetingReg.addParticipant(newParticipant2);

            Participant newParticipant3 = new Participant("Igoris", new DateTime(2000, 06, 13), new DateTime(2000, 06, 15), "Event2");

            // Act
            Participant result = meetingReg.findParticipantByName("Simonas");

            // assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void FindParticipantByNameTest_NonExistantParticipant()
        {
            // Arrange
            Meeting meet = new Meeting("Event", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 05, 09), new DateTime(2000, 05, 12));
            Participant newParticipant1 = new Participant("Igoris", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            Participant newParticipant2 = new Participant("Simonas", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            MeetingRegister meetingReg = new MeetingRegister(meet);
            meetingReg.addParticipant(newParticipant1);
            meetingReg.addParticipant(newParticipant2);

            // Act
            Participant result = meetingReg.findParticipantByName("Jurgis");

            // assert
            Assert.IsNull(result);
        }
        [TestMethod()]
        public void removeParticipantTest_success()
        {
            // Arrange
            Meeting meet = new Meeting("Event", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 05, 09), new DateTime(2000, 05, 12));
            Participant newParticipant1 = new Participant("Igoris", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            MeetingRegister meetingReg = new MeetingRegister(meet);
            meetingReg.addParticipant(newParticipant1);

            // Act
            meetingReg.removeParticipant(newParticipant1);

            // assert
            Assert.IsTrue(meetingReg.participantCount() == 1);
        }
        [TestMethod()]
        public void removeParticipantTest_cantRemoveHost()
        {
            // Arrange
            Meeting meet = new Meeting("Event", "Aglirdas", "", Category.TeamBuilding, EventType.InPerson, new DateTime(2000, 05, 09), new DateTime(2000, 05, 12));
            Participant newParticipant1 = new Participant("Igoris", new DateTime(2000, 05, 13), new DateTime(2000, 05, 15), "Event");
            MeetingRegister meetingReg = new MeetingRegister(meet);
            meetingReg.addParticipant(newParticipant1);

            Participant host = new Participant("Algirdas", new DateTime(2000, 05, 09), new DateTime(2000, 05, 12), "Event");
            // Act
            meetingReg.removeParticipant(host);

            // assert
            Assert.IsTrue(meetingReg.participantCount() == 2);
        }
        /*
         public void ExportParticipants(List<Participant> listToAddTo)
        {
            foreach (var item in Participants)
            {
                listToAddTo.Add(item);
            }
        }
         */
    }
}