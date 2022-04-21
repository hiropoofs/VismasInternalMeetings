using InternalMeetings;

internal class Program
{
    #region GlobalValues

    public static List<MeetingRegister> mlist = new List<MeetingRegister>();
    public static bool running = true;
    public static string CurrentUser = "";

    #endregion

    #region Main Body
    static void Main(string[] args)
    {
        Execute();
    }
    /// <summary>
    /// Method used to initiate User and keep the app running until command "exit" is executed
    /// </summary>
    static void Execute()
    {
        while (CurrentUser == "")
        {
            mlist = InOutUtils.ReadJSON();
            Console.WriteLine("Please type your name:");
            CurrentUser = Console.ReadLine();
            Console.Clear();
        }
        Console.WriteLine("Hello " + CurrentUser);
        while (running)
        {
            save();
            Console.WriteLine("Type in command or \"help\" for more options");
            string commandline = Console.ReadLine();
            Command(commandline);
        }
    }
    /// <summary>
    /// Method with Switch statement to initiate corresponding commands typed in console
    /// </summary>
    /// <param name="line">command input</param>
    static void Command(string line)
    {
        switch (line.ToLower())
        {
            case "help":
                Console.Clear();
                help();
                break;
            case "exit":
                Console.Clear();
                exit();
                break;
            case "create":
                Console.Clear();
                create();
                break;
            case "delete":
                Console.Clear();
                delete();
                break;
            case "add":
                Console.Clear();
                add();
                break;
            case "remove":
                Console.Clear();
                remove();
                break;
            case "list":
                Console.Clear();
                list();
                break;
            default:
                break;
        }
    }

    #endregion

    #region Button Fuctions
    /// <summary>
    /// Command to list all the meetings
    /// </summary>
    static void list()
    {
        Console.WriteLine("Filter list:");
        foreach (FilterOptions val in Enum.GetValues(typeof(FilterOptions)))
        {
            Console.WriteLine((int)val + " - " + val.ToString());
        }
        FilterOptions Filter = (FilterOptions)int.Parse(ReaderLine("\nSelect Filter by typing corresponding number: (ex. 1)", InputOptions.onlyDigits, false));
        InOutUtils.PrintByFilter(mlist, Filter);

    }
    /// <summary>
    /// Command to remove a person from the meeting
    /// </summary>
    static void remove()
    {
        Console.WriteLine("List of the events: ");
        PrintMeetingList();
        Console.WriteLine("\nList of Participants: ");
        PrintParticipantList();

        string EventName = ReaderLine("\nWrite event name from which to remove a person from:", InputOptions.any, false);
        Console.Clear();
        Console.WriteLine(EventName + " participant list:");
        bool flag = false;
        foreach (var p in mlist)
        {
            if (p.meeting.Name == EventName)
            {
                flag = true;
                p.PrintParticipants();
                string PersonNameInput = ReaderLine("Write a name of the person to remove from the event (leave empty if it is you): ", InputOptions.onlyLetters, true);
                string PersonName = PersonNameInput == "" ? CurrentUser : PersonNameInput;

                if (PersonName != p.meeting.ResponsiblePerson)
                {
                    Participant participant = p.findParticipantByName(PersonName);
                    if (participant != null)
                        p.removeParticipant(participant);
                    else
                        Console.WriteLine("Cannot remove participant with such name, because it does not exist");
                }
                else
                {
                    Console.WriteLine("Cannot remove a person from a meeting that is responsible for the meeting");
                }
            }
        }
        if (!flag)
        {
            Console.WriteLine("empty\n");
            Console.WriteLine("Wrong Event name input?");
        }
    }
    /// <summary>
    /// Command to add a person to the meeting
    /// </summary>
    static void add()
    {
        Console.WriteLine("List of the events: ");
        PrintMeetingList();

        Console.WriteLine("\nList of Participants: ");
        PrintParticipantList();

        string EventName = ReaderLine("\nWrite event name which event to add a person to: ", InputOptions.any, false);
        string PersonNameInput = ReaderLine("Person's name to add to the event?: ", InputOptions.any, false);

        string StartDateInput = ReaderLine("Start date of the participation? (ex. YYYY-MM-DD): ", InputOptions.onlyDate, false);
        DateTime StartDate = DateTime.Parse(StartDateInput);

        string EndDateInput = ReaderLine("End date of the participation? (ex. YYYY-MM-DD): ", InputOptions.onlyDate, false);
        DateTime EndDate = DateTime.Parse(EndDateInput);

        Participant participant = new Participant(PersonNameInput, StartDate, EndDate, EventName);
        
        if (!TaskUtils.DoesTheDatesClash(mlist, participant))
        {
            TaskUtils.AddParticipantToCorrespondingMeeting(mlist, participant);
        }
        else
        {
            string answer = ReaderLine("Participant can't be added due to him/her having that date planned out\nShould he/her still be added?: (Y/N)", InputOptions.onlySymbol, false);
            if (answer.ToLower() == "y")
            {
                TaskUtils.AddParticipantToCorrespondingMeeting(mlist, participant);
            }
        }
    }
    /// <summary>
    /// Command to delete a meeting
    /// </summary>
    static void delete()
    {
        Console.Clear();
        Console.WriteLine("List of events: ");
        foreach (var item in mlist)
        {
            item.PrintMeeting();
        }
        string EventName = ReaderLine("\nType the name of the event to delete: ", InputOptions.any, false);

        bool deletedFlag = false;
        foreach (var meeting in mlist)
        {
            if (meeting.meeting.Name == EventName && CurrentUser == meeting.meeting.ResponsiblePerson)
            {
                deletedFlag = true;
                mlist.Remove(meeting);
                break;
            }
        }
        if (deletedFlag)
        {
            Console.WriteLine("Event successfuly deleted");
        }
        else
        {
            Console.WriteLine("Could not delete provided event name.");
        }
    }
    /// <summary>
    /// command to create a new meeting
    /// </summary>
    static void create()
    {
        string Name = ReaderLine("Type the name of the event: ", InputOptions.any, false);

        string ResponsiblePersonInput = ReaderLine("Responsible person for the event? (leave empty if it is you): ", InputOptions.onlyLetters, true);
        string ResponsiblePerson = ResponsiblePersonInput == "" ? CurrentUser : ResponsiblePersonInput;

        string Description = ReaderLine("Description of the event: ", InputOptions.any, true);
        
        PrintEnum<Category>();
        string categoryString = ReaderLine("Category of the event? (Type corresponding number): ", InputOptions.onlyDigits, false);
        Category category = (Category)int.Parse(categoryString);

        PrintEnum<EventType>();
        string EventTypeString = ReaderLine("Type of the event? (Type corresponding number):", InputOptions.onlyDigits, false);
        EventType eventType = (EventType)int.Parse(EventTypeString);

        string StartDateInput = ReaderLine("Start of the event? (ex. YYYY-MM-DD): ", InputOptions.onlyDate, false);
        DateTime StartDate = DateTime.Parse(StartDateInput);

        string EndDateInput = ReaderLine("End of the event? (ex. YYYY-MM-DD): ", InputOptions.onlyDate, false);
        DateTime EndDate = DateTime.Parse(EndDateInput);

        MeetingRegister reg = new MeetingRegister(new Meeting(Name, ResponsiblePerson, Description, category, eventType, StartDate, EndDate));
        mlist.Add(reg);
    }
    /// <summary>
    /// sub-command that only the console initiates whenever "exit" command is initiated or before another command input
    /// </summary>
    static void save()
    {
        InOutUtils.SaveJSON(mlist);
    }
    /// <summary>
    /// command used to exit the application
    /// </summary>
    static void exit()
    {
        save();
        running = false;
    }
    /// <summary>
    /// command used to list all possible user commands
    /// </summary>
    static void help()
    {
        Console.WriteLine("List of commands:");
        Console.WriteLine("\"create\" - Command to create a new meeting");
        Console.WriteLine("\"delete\" - Command to delete a meeting");
        Console.WriteLine("\"add\" - Command to add a person to the meeting");
        Console.WriteLine("\"remove\" - Command to remove a person from the meeting");
        Console.WriteLine("\"list\" - Command to list all the meetings");
        Console.WriteLine("\"exit\" - Command to exit the program");
        Console.WriteLine("\"help\" - Command to list all the commands");
        Console.WriteLine();
    }

    #endregion

    #region UtilityMethods
    /// <summary>
    /// command used to print out corresponding text to "defaulttxt", it also allows the programmer to asign what kind of ReadLine()
    /// line is expected to get and prevents the user from entering wrong data
    /// </summary>
    /// <param name="defaulttxt">header before asking for user input</param>
    /// <param name="typeOfInput">enumerator to assign what kind of string as an output is expected</param>
    /// <param name="EmptyString">boolean that allows empty string to be input by user</param>
    /// <returns>a ReadLine() string</returns>
    public static string ReaderLine(string defaulttxt, InputOptions? typeOfInput, bool EmptyString)
    {
        while (true)
        {
            Console.WriteLine(defaulttxt);
            string line = Console.ReadLine();

            if (!EmptyString && line == "")
            {
                Console.WriteLine("Input cannot be empty, try again");
                continue;
            }
            switch (typeOfInput)
            {
                case InputOptions.onlyDigits:
                    if (line.All(char.IsDigit))
                    {
                        return line;
                    }
                    else Console.WriteLine("Input can only contain digits, try again");
                    break;
                case InputOptions.onlyLetters:
                    if (line.All(char.IsLetter))
                    {
                        return line;
                    }
                    else Console.WriteLine("Input can only contain digits, try again");
                    break;
                case InputOptions.onlyDate:
                    DateTime test;
                    if (DateTime.TryParse(line, out test))
                    {
                        return line;
                    }
                    else Console.WriteLine("Wrong Date Input, please use \"yyyy-mm-dd\" format");
                    break;
                case InputOptions.onlySymbol:
                    if (line.Length == 1)
                    {
                        return line;
                    }
                    else
                    {
                        Console.WriteLine("Input only one letter!");
                    }
                    break;
                default:
                    return line;
                    break;
            }
        }
    }
    /// <summary>
    /// Outputs all participants in List<MeetingRegister>
    /// </summary>
    public static void PrintParticipantList()
    {
        foreach (var p in mlist)
        {
            p.PrintParticipants();
        }
    }
    /// <summary>
    /// Outputs all Meetings in List<MeetingRegister>
    /// </summary>
    public static void PrintMeetingList()
    {
        foreach (var p in mlist)
        {
            p.PrintMeeting();
        }
    }
    /// <summary>
    /// Outputs all enum values specified Enum
    /// </summary>
    public static void PrintEnum<T>()
    {
        Console.WriteLine("List of Categories:");
        foreach (T t in Enum.GetValues(typeof(T)))
        {
            Console.WriteLine((Convert.ToInt16(t) + " - " + t));
        }
    }

    #endregion
}
