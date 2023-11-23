using System.Collections.Generic;
using Commons.Types.SettingOptions;

namespace Localization
{
    public static class Localization
    {
        public static LanguageOption LanguageOption { get; set; }

        public static string GetSentence(string sentence)
        {
            return LanguageOption == LanguageOption.English
                ? _localizationUnitsBySentence[sentence].English
                : _localizationUnitsBySentence[sentence].Vietnamese;
        }

        private static Dictionary<string, LocalizationUnit> _localizationUnitsBySentence = new()
        {
            { Sentence.AuthenticationView.LOGIN, new LocalizationUnit("Login", "Đăng nhập") },
            { Sentence.AuthenticationView.USERNAME, new LocalizationUnit("Username", "Tên người dùng") },
            { Sentence.AuthenticationView.PASSWORD, new LocalizationUnit("Password", "Mật khẩu") },
            { Sentence.AuthenticationView.FORGOT_PASSWORD, new LocalizationUnit("Forgot Password", "Quên mật khẩu") },

            { Sentence.MapView.MAP, new LocalizationUnit("Map", "Bản đồ") },

            { Sentence.TasksView.TASKS, new LocalizationUnit("Tasks", "Nhiệm vụ") },
            { Sentence.TasksView.MCP_FILL_LEVEL, new LocalizationUnit("MCP Fill Level", "Mức đầy MCP") },
            { Sentence.TasksView.TASK_STATUS, new LocalizationUnit("Task Status", "Trạng thái nhiệm vụ") },
            { Sentence.TasksView.CREATED_TIME, new LocalizationUnit("Created Time", "Thời gian tạo") },
            { Sentence.TasksView.COMPLETE_BY_TIME, new LocalizationUnit("Complete By Time", "Hoàn thành trước thời gian") },
            { Sentence.TasksView.LAST_CHANGED_TIME, new LocalizationUnit("Last Changed Time", "Thời gian thay đổi lần cuối") },
            { Sentence.TasksView.MCP_ADDRESS, new LocalizationUnit("MCP Address", "Địa chỉ MCP") },
            { Sentence.TasksView.ASSIGNED_BY, new LocalizationUnit("Assigned By", "Được giao bởi") },
            { Sentence.TasksView.ASSIGNED_TO, new LocalizationUnit("Assigned To", "Được giao cho") },
            { Sentence.TasksView.CREATED_AT, new LocalizationUnit("Created At", "Được tạo lúc") },
            { Sentence.TasksView.COMPLETE_BY, new LocalizationUnit("Complete By", "Hoàn thành trước") },
            { Sentence.TasksView.LAST_STATUS, new LocalizationUnit("Last Status", "Trạng thái cuối cùng") },
            { Sentence.TasksView.LAST_STATUS_CHANGE_AT, new LocalizationUnit("Last Status Change At", "Thời gian thay đổi trạng thái cuối cùng") },
            {
                Sentence.TasksView.CHOOSE_THE_MCPS_THAT_YOU_WANT_TO_BE_COLLECTED,
                new LocalizationUnit("Choose the MCPS that you want to be collected", "Chọn những MCP mà bạn muốn thu gom")
            },
            {
                Sentence.TasksView.CHOOSE_THE_WORKERS_TO_ASSIGN,
                new LocalizationUnit("Choose the workers to assign", "Chọn những công nhân để giao việc")
            },
            {
                Sentence.TasksView.LEAVE_THIS_STEP_EMPTY_IF_YOU_WANT_TO_ASSIGN_THE_TASK_TO_ALL_WORKERS,
                new LocalizationUnit("Leave this step empty if you want to assign the task to all workers",
                    "Để trống bước này nếu bạn muốn giao việc cho tất cả công nhân")
            },
            {
                Sentence.TasksView.CHOOSE_THE_DATE_AND_TIME_TO_COLLECT_THE_SELECTED_MCPS,
                new LocalizationUnit("Choose the date and time to collect the selected MCPS", "Chọn ngày và giờ để thu gom những MCP đã chọn")
            },

            { Sentence.WorkersView.WORKERS, new LocalizationUnit("Workers", "Công nhân") },
            { Sentence.WorkersView.ROLE, new LocalizationUnit("Role", "Vai trò") },
            { Sentence.WorkersView.SUPERVISOR, new LocalizationUnit("Supervisor", "Người giám sát") },
            { Sentence.WorkersView.DRIVER, new LocalizationUnit("Driver", "Tài xế") },
            { Sentence.WorkersView.CLEANER, new LocalizationUnit("Cleaner", "Người làm vệ sinh") },
            { Sentence.WorkersView.GENDER, new LocalizationUnit("Gender", "Giới tính") },
            { Sentence.WorkersView.MALE, new LocalizationUnit("Male", "Nam") },
            { Sentence.WorkersView.FEMALE, new LocalizationUnit("Female", "Nữ") },
            { Sentence.WorkersView.OTHER, new LocalizationUnit("Other", "Khác") },
            { Sentence.WorkersView.DATE_OF_BIRTH, new LocalizationUnit("Date of Birth", "Ngày sinh") },
            { Sentence.WorkersView.ADDRESS, new LocalizationUnit("Address", "Địa chỉ") },
            { Sentence.WorkersView.ACCOUNT_CREATED, new LocalizationUnit("Account Created", "Tài khoản được tạo") },

            { Sentence.McpsView.MCPS, new LocalizationUnit("MCPS", "MCPS") },
            { Sentence.McpsView.FILL_LEVEL, new LocalizationUnit("Fill Level", "Mức đầy") },
            { Sentence.McpsView.TODAY, new LocalizationUnit("Today", "Hôm nay") },
            { Sentence.McpsView.LATITUDE, new LocalizationUnit("Latitude", "Vĩ độ") },
            { Sentence.McpsView.LONGITUDE, new LocalizationUnit("Longitude", "Kinh độ") },
            { Sentence.McpsView.LAST_EMPTIED, new LocalizationUnit("Last Emptied", "Lần hút chân không cuối cùng") },
            { Sentence.McpsView.NEVER, new LocalizationUnit("Never", "Chưa bao giờ") },

            { Sentence.VehiclesView.VEHICLES, new LocalizationUnit("Vehicles", "Xe cộ") },
            { Sentence.VehiclesView.SIDE_LOADER, new LocalizationUnit("Side Loader", "Xe nạp bên hông") },
            { Sentence.VehiclesView.REAR_LOADER, new LocalizationUnit("Rear Loader", "Xe nạp phía sau") },
            { Sentence.VehiclesView.FRONT_LOADER, new LocalizationUnit("Front Loader", "Xe nạp phía trước") },
            { Sentence.VehiclesView.MODEL, new LocalizationUnit("Model", "Mô hình") },
            { Sentence.VehiclesView.VEHICLE_TYPE, new LocalizationUnit("Vehicle Type", "Loại xe") },

            { Sentence.ReportingView.REPORTING, new LocalizationUnit("Reporting", "Báo cáo") },
            { Sentence.ReportingView.MCPS_COLLECTED, new LocalizationUnit("MCPS Collected", "MCPS đã thu gom") },
            { Sentence.ReportingView.DISTANCE_TRAVELED, new LocalizationUnit("Distance Traveled", "Quãng đường đã đi") },
            { Sentence.ReportingView.FUEL_CONSUMED, new LocalizationUnit("Fuel Consumed", "Nhiên liệu đã tiêu thụ") },
            { Sentence.ReportingView.CURRENT_TEMPERATURE, new LocalizationUnit("Current Temperature", "Nhiệt độ hiện tại") },
            { Sentence.ReportingView.CHANCE_OF_PRECIPITATION, new LocalizationUnit("Chance of Precipitation", "Cơ hội có mưa") },
            { Sentence.ReportingView.MCP_CAPACITY, new LocalizationUnit("MCP Capacity", "Dung lượng MCP") },
            { Sentence.ReportingView.TASKS_LEFT, new LocalizationUnit("Tasks Left", "Nhiệm vụ còn lại") },
            { Sentence.ReportingView.TASKS_CREATED, new LocalizationUnit("Tasks Created", "Nhiệm vụ đã tạo") },
            { Sentence.ReportingView.WORKERS_ONLINE, new LocalizationUnit("Workers Online", "Công nhân đang trực tuyến") },
            { Sentence.ReportingView.TOTAL_WORKERS, new LocalizationUnit("Total Workers", "Tổng số công nhân") },
            {
                Sentence.ReportingView.HOURLY_AGGREGATED_MCPS_FILL_LEVEL,
                new LocalizationUnit("Hourly Aggregated MCPS Fill Level", "Tổng hợp hàng giờ mức đầy MCPS")
            },
            { Sentence.ReportingView.HOURLY_MPCS_EMPTIED, new LocalizationUnit("Hourly MCPS Emptied", "Hàng giờ MCPS đã hút chân không") },

            { Sentence.MessagingView.MESSAGING, new LocalizationUnit("Messaging", "Tin nhắn") },
            { Sentence.MessagingView.ONLINE, new LocalizationUnit("Online", "Trực tuyến") },
            { Sentence.MessagingView.OFFLINE, new LocalizationUnit("Offline", "Ngoại tuyến") },

            { Sentence.SettingsView.SETTINGS, new LocalizationUnit("Settings", "Cài đặt") },
            { Sentence.SettingsView.ON, new LocalizationUnit("On", "Bật") },
            { Sentence.SettingsView.OFF, new LocalizationUnit("Off", "Tắt") },
            { Sentence.SettingsView.ENGLISH, new LocalizationUnit("English", "Tiếng Anh") },
            { Sentence.SettingsView.VIETNAMESE, new LocalizationUnit("Vietnamese", "Tiếng Việt") },
            { Sentence.SettingsView.INTERFACE_SETTINGS, new LocalizationUnit("Interface settings", "Cài đặt giao diện") },
            { Sentence.SettingsView.DARK_MODE, new LocalizationUnit("Dark mode", "Chế độ tối") },
            { Sentence.SettingsView.COLORBLIND_MODE, new LocalizationUnit("Colorblind mode", "Chế độ mù màu") },
            { Sentence.SettingsView.REDUCE_MOTION, new LocalizationUnit("Reduce motion", "Giảm chuyển động") },
            { Sentence.SettingsView.LANGUAGE, new LocalizationUnit("Language", "Ngôn ngữ") },
            { Sentence.SettingsView.NOTIFICATION_SETTINGS, new LocalizationUnit("Notification settings", "Cài đặt thông báo") },
            { Sentence.SettingsView.MESSAGES, new LocalizationUnit("Messages", "Tin nhắn") },
            { Sentence.SettingsView.EMPLOYEES_LOGGED_IN, new LocalizationUnit("Employees logged in", "Nhân viên đăng nhập") },
            { Sentence.SettingsView.EMPLOYEES_LOGGED_OUT, new LocalizationUnit("Employees logged out", "Nhân viên đăng xuất") },
            { Sentence.SettingsView.MCPS_ALMOST_FULL, new LocalizationUnit("Mcps almost full", "Mcps gần đầy") },
            { Sentence.SettingsView.MCPS_FULL, new LocalizationUnit("Mcps full", "Mcps đầy") },
            { Sentence.SettingsView.MCPS_EMPTIED, new LocalizationUnit("Mcps emptied", "Mcps đã hút chân không") },
            { Sentence.SettingsView.SOFTWARE_UPDATE_AVAILABLE, new LocalizationUnit("Software update available", "Cập nhật phần mềm có sẵn") },
            { Sentence.SettingsView.ACCOUNT_SETTINGS, new LocalizationUnit("Account settings", "Cài đặt tài khoản") },
            { Sentence.SettingsView.ONLINE_STATUS, new LocalizationUnit("Online status", "Trạng thái trực tuyến") },
            { Sentence.SettingsView.EXPORT_MESSAGES, new LocalizationUnit("Export messages", "Xuất tin nhắn") },
            { Sentence.SettingsView.EXPORT_WORK_LOGS, new LocalizationUnit("Export work logs", "Xuất nhật ký công việc") },
            { Sentence.SettingsView.CHANGE_PERSONAL_INFORMATION, new LocalizationUnit("Change personal information", "Thay đổi thông tin cá nhân") },
            { Sentence.SettingsView.CHANGE_PASSWORD, new LocalizationUnit("Change password", "Thay đổi mật khẩu") },
            { Sentence.SettingsView.REPORT_PROBLEM, new LocalizationUnit("Report problem", "Báo cáo vấn đề") },
            { Sentence.SettingsView.LOGOUT, new LocalizationUnit("Logout", "Đăng xuất") },


            { Sentence.ListControl.SEARCH, new LocalizationUnit("", "") },
            { Sentence.ListControl.SORT_BY, new LocalizationUnit("", "") },

            { Sentence.DateAndTime.JANUARY, new LocalizationUnit("January", "Tháng 1") },
            { Sentence.DateAndTime.FEBRUARY, new LocalizationUnit("February", "Tháng 2") },
            { Sentence.DateAndTime.MARCH, new LocalizationUnit("March", "Tháng 3") },
            { Sentence.DateAndTime.APRIL, new LocalizationUnit("April", "Tháng 4") },
            { Sentence.DateAndTime.MAY, new LocalizationUnit("May", "Tháng 5") },
            { Sentence.DateAndTime.JUNE, new LocalizationUnit("June", "Tháng 6") },
            { Sentence.DateAndTime.JULY, new LocalizationUnit("July", "Tháng 7") },
            { Sentence.DateAndTime.AUGUST, new LocalizationUnit("August", "Tháng 8") },
            { Sentence.DateAndTime.SEPTEMBER, new LocalizationUnit("September", "Tháng 9") },
            { Sentence.DateAndTime.OCTOBER, new LocalizationUnit("October", "Tháng 10") },
            { Sentence.DateAndTime.NOVEMBER, new LocalizationUnit("November", "Tháng 11") },
            { Sentence.DateAndTime.DECEMBER, new LocalizationUnit("December", "Tháng 12") },
            { Sentence.DateAndTime.SHORT_HAND_MONDAY, new LocalizationUnit("M", "T2") },
            { Sentence.DateAndTime.SHORT_HAND_TUESDAY, new LocalizationUnit("T", "T3") },
            { Sentence.DateAndTime.SHORT_HAND_WEDNESDAY, new LocalizationUnit("W", "T4") },
            { Sentence.DateAndTime.SHORT_HAND_THURSDAY, new LocalizationUnit("T", "T5") },
            { Sentence.DateAndTime.SHORT_HAND_FRIDAY, new LocalizationUnit("F", "T6") },
            { Sentence.DateAndTime.SHORT_HAND_SATURDAY, new LocalizationUnit("S", "T7") },
            { Sentence.DateAndTime.SHORT_HAND_SUNDAY, new LocalizationUnit("S", "CN") },
        };
    };
}