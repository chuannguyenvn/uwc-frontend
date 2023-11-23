namespace LocalizationNS
{
    public class Sentence
    {
        public class AuthenticationView
        {
            public const string LOGIN = "Authentication.Login";
            public const string USERNAME = "Authentication.Username";
            public const string PASSWORD = "Authentication.Password";
            public const string FORGOT_PASSWORD = "Authentication.ForgotPassword";
        }

        public class MapView
        {
            public const string MAP = "Map.Map";
        }

        public class TasksView
        {
            public const string TASKS = "Tasks.Tasks";
            public const string MCP_FILL_LEVEL = "Tasks.McpFillLevel";
            public const string TASK_STATUS = "Tasks.TaskStatus";
            public const string CREATED_TIME = "Tasks.CreatedTime";
            public const string COMPLETE_BY_TIME = "Tasks.CompleteByTime";
            public const string LAST_CHANGED_TIME = "Tasks.LastChangedTime";
            public const string MCP_ADDRESS = "Tasks.McpAddress";
            public const string ASSIGNED_BY = "Tasks.AssignedBy";
            public const string ASSIGNED_TO = "Tasks.AssignedTo";
            public const string CREATED_AT = "Tasks.CreatedAt";
            public const string COMPLETE_BY = "Tasks.CompleteBy";
            public const string LAST_STATUS = "Tasks.LastStatus";
            public const string LAST_STATUS_CHANGE_AT = "Tasks.LastStatusChangeAt";
            public const string CHOOSE_THE_MCPS_THAT_YOU_WANT_TO_BE_COLLECTED = "Tasks.ChooseTheMcpsThatYouWantToBeCollected";
            public const string CHOOSE_THE_WORKERS_TO_ASSIGN = "Tasks.ChooseTheWorkersToAssign";

            public const string LEAVE_THIS_STEP_EMPTY_IF_YOU_WANT_TO_ASSIGN_THE_TASK_TO_ALL_WORKERS =
                "Tasks.LeaveThisStepEmptyIfYouWantToAssignTheTaskToAllWorkers";

            public const string CHOOSE_THE_DATE_AND_TIME_TO_COLLECT_THE_SELECTED_MCPS = "Tasks.ChooseTheDateAndTimeToCollectTheSelectedMcps";
            public const string CLEANING_TASK = "Tasks.CleaningTask";
            public const string ONGOING = "Tasks.Ongoing";
            public const string PENDING = "Tasks.Pending";
            public const string COMPLETE = "Tasks.Complete";
            public const string COMPLETED = "Tasks.Completed";
            public const string REJECT = "Tasks.Reject";
            public const string REJECTED = "Tasks.Rejected";
            public const string ADDRESS = "Tasks.Address";
            public const string ETA = "Tasks.Eta";
            public const string EMPTYING_LOGS = "Tasks.EmptyingLogs";
            public const string CURRENT_LOAD = "Tasks.CurrentLoad";
        }

        public class StatusView
        {
            public const string STATUS = "Status.Status";
        }

        public class WorkersView
        {
            public const string WORKERS = "Workers.Workers";
            public const string ROLE = "Workers.Role";
            public const string SUPERVISOR = "Workers.Supervisor";
            public const string DRIVER = "Workers.Driver";
            public const string CLEANER = "Workers.Cleaner";
            public const string GENDER = "Workers.Gender";
            public const string MALE = "Workers.Male";
            public const string FEMALE = "Workers.Female";
            public const string OTHER = "Workers.Other";
            public const string DATE_OF_BIRTH = "Workers.DateOfBirth";
            public const string ADDRESS = "Workers.Address";
            public const string ACCOUNT_CREATED = "Workers.AccountCreated";
        }

        public class McpsView
        {
            public const string MCPS = "Mcps.Mcps";
            public const string FILL_LEVEL = "Mcps.FillLevel";
            public const string TODAY = "Mcps.Today";
            public const string LATITUDE = "Mcps.Latitude";
            public const string LONGITUDE = "Mcps.Longitude";
            public const string LAST_EMPTIED = "Mcps.LastEmptied";
            public const string NEVER = "Mcps.Never";
            public const string MAJOR_COLLECTION_POINT = "Mcps.MajorCollectionPoint";
            public const string ADDRESS = "Mcps.Address";
            public const string FILL_LEVEL_BY_HOUR = "Mcps.FillLevelByHour";
        }

        public class VehiclesView
        {
            public const string VEHICLES = "Vehicles.Vehicles";
            public const string SIDE_LOADER = "Vehicles.SideLoader";
            public const string REAR_LOADER = "Vehicles.RearLoader";
            public const string FRONT_LOADER = "Vehicles.FrontLoader";
            public const string MODEL = "Vehicles.Model";
            public const string VEHICLE_TYPE = "Vehicles.VehicleType";
        }

        public class ReportingView
        {
            public const string REPORTING = "Reporting.Reporting";
            public const string MCPS_COLLECTED = "Reporting.McpsCollected";
            public const string DISTANCE_TRAVELED = "Reporting.DistanceTraveled";
            public const string FUEL_CONSUMED = "Reporting.FuelConsumed";
            public const string CURRENT_TEMPERATURE = "Reporting.CurrentTemperature";
            public const string CHANCE_OF_PRECIPITATION = "Reporting.ChanceOfPrecipitation";
            public const string MCP_CAPACITY = "Reporting.McpCapacity";
            public const string TASKS_LEFT = "Reporting.TasksLeft";
            public const string TASKS_CREATED = "Reporting.TasksCreated";
            public const string WORKERS_ONLINE = "Reporting.WorkersOnline";
            public const string TOTAL_WORKERS = "Reporting.TotalWorkers";
            public const string HOURLY_AGGREGATED_MCPS_FILL_LEVEL = "Reporting.HourlyAggregatedMcpsFillLevel";
            public const string HOURLY_MCPS_EMPTIED = "Reporting.HourlyMcpsEmptied";
        }

        public class MessagingView
        {
            public const string MESSAGING = "Messaging.Messaging";
            public const string ONLINE = "Messaging.Online";
            public const string OFFLINE = "Messaging.Offline";
        }

        public class SettingsView
        {
            public const string SETTINGS = "Settings.Settings";
            public const string ON = "Settings.On";
            public const string OFF = "Settings.Off";
            public const string ENGLISH = "Settings.English";
            public const string VIETNAMESE = "Settings.Vietnamese";
            public const string INTERFACE_SETTINGS = "Settings.InterfaceSettings";
            public const string DARK_MODE = "Settings.DarkMode";
            public const string COLORBLIND_MODE = "Settings.ColorblindMode";
            public const string REDUCE_MOTION = "Settings.ReduceMotion";
            public const string LANGUAGE = "Settings.Language";
            public const string NOTIFICATION_SETTINGS = "Settings.NotificationSettings";
            public const string MESSAGES = "Settings.Messages";
            public const string EMPLOYEES_LOGGED_IN = "Settings.EmployeesLoggedIn";
            public const string EMPLOYEES_LOGGED_OUT = "Settings.EmployeesLoggedOut";
            public const string MCPS_ALMOST_FULL = "Settings.McpsAlmostFull";
            public const string MCPS_FULL = "Settings.McpsFull";
            public const string MCPS_EMPTIED = "Settings.McpsEmptied";
            public const string SOFTWARE_UPDATE_AVAILABLE = "Settings.SoftwareUpdateAvailable";
            public const string ACCOUNT_SETTINGS = "Settings.AccountSettings";
            public const string ONLINE_STATUS = "Settings.OnlineStatus";
            public const string ONLINE = "Settings.Online";
            public const string OFFLINE = "Settings.Offline";
            public const string EXPORT_MESSAGES = "Settings.ExportMessages";
            public const string EXPORT_WORK_LOGS = "Settings.ExportWorkLogs";
            public const string CHANGE_PERSONAL_INFORMATION = "Settings.ChangePersonalInformation";
            public const string CHANGE_PASSWORD = "Settings.ChangePassword";
            public const string REPORT_PROBLEM = "Settings.ReportProblem";
            public const string LOGOUT = "Settings.Logout";
            public const string REGISTER_FACIAL_RECOGNITION = "Settings.RegisterFacialRecognition";
        }

        public class ListControl
        {
            public const string SEARCH = "ListControl.Search";
            public const string SORT_BY = "ListControl.SortBy";
        }

        public class Procedure
        {
            public const string CONFIRM = "Procedure.Confirm";
        }

        public class DateAndTime
        {
            public const string JANUARY = "DateAndTime.January";
            public const string FEBRUARY = "DateAndTime.February";
            public const string MARCH = "DateAndTime.March";
            public const string APRIL = "DateAndTime.April";
            public const string MAY = "DateAndTime.May";
            public const string JUNE = "DateAndTime.June";
            public const string JULY = "DateAndTime.July";
            public const string AUGUST = "DateAndTime.August";
            public const string SEPTEMBER = "DateAndTime.September";
            public const string OCTOBER = "DateAndTime.October";
            public const string NOVEMBER = "DateAndTime.November";
            public const string DECEMBER = "DateAndTime.December";
            public const string SHORT_HAND_MONDAY = "DateAndTime.ShortHandMonday";
            public const string SHORT_HAND_TUESDAY = "DateAndTime.ShortHandTuesday";
            public const string SHORT_HAND_WEDNESDAY = "DateAndTime.ShortHandWednesday";
            public const string SHORT_HAND_THURSDAY = "DateAndTime.ShortHandThursday";
            public const string SHORT_HAND_FRIDAY = "DateAndTime.ShortHandFriday";
            public const string SHORT_HAND_SATURDAY = "DateAndTime.ShortHandSaturday";
            public const string SHORT_HAND_SUNDAY = "DateAndTime.ShortHandSunday";
        }

        public class MissingTranslation
        {
            public const string MISSING_TRANSLATION = "MissingTranslation.MissingTranslation";
        }
    }
}