using System;
using System.Collections.Generic;
using Commons.Categories;
using Commons.Types;
using Commons.Types.SettingOptions;

namespace LocalizationNS
{
    public static class Localization
    {
        public static Action LanguageChanged;

        private static LanguageOption _languageOption = LanguageOption.English;

        private static Dictionary<string, LocalizationUnit> _localizationUnitsBySentence = new()
        {
            { Sentence.AuthenticationView.LOGIN, new LocalizationUnit("Login", "Đăng nhập") },
            { Sentence.AuthenticationView.USERNAME, new LocalizationUnit("Username", "Tên người dùng") },
            { Sentence.AuthenticationView.PASSWORD, new LocalizationUnit("Password", "Mật khẩu") },
            { Sentence.AuthenticationView.FORGOT_PASSWORD, new LocalizationUnit("Forgot password", "Quên mật khẩu") },

            { Sentence.MapView.MAP, new LocalizationUnit("Map", "Bản đồ") },

            { Sentence.TasksView.TASKS, new LocalizationUnit("Tasks", "Nhiệm vụ") },
            { Sentence.TasksView.MCP_FILL_LEVEL, new LocalizationUnit("MCP fill level", "Mức đầy MCP") },
            { Sentence.TasksView.TASK_STATUS, new LocalizationUnit("Task status", "Trạng thái nhiệm vụ") },
            { Sentence.TasksView.CREATED_TIME, new LocalizationUnit("Created time", "Thời gian tạo") },
            { Sentence.TasksView.COMPLETE_BY_TIME, new LocalizationUnit("Complete by time", "Hoàn thành trước thời gian") },
            { Sentence.TasksView.LAST_CHANGED_TIME, new LocalizationUnit("Last changed time", "Thời gian cập nhật cuối") },
            { Sentence.TasksView.MCP_ADDRESS, new LocalizationUnit("MCP address", "Địa chỉ MCP") },
            { Sentence.TasksView.ASSIGNED_BY, new LocalizationUnit("Assigned by", "Được giao bởi") },
            { Sentence.TasksView.ASSIGNED_TO, new LocalizationUnit("Assigned to", "Được giao cho") },
            { Sentence.TasksView.CREATED_AT, new LocalizationUnit("Created at", "Được tạo lúc") },
            { Sentence.TasksView.COMPLETE_BY, new LocalizationUnit("Complete by", "Hoàn thành trước") },
            { Sentence.TasksView.LAST_STATUS, new LocalizationUnit("Last status", "Trạng thái cuối") },
            { Sentence.TasksView.LAST_STATUS_CHANGE_AT, new LocalizationUnit("Last status change at", "Thời gian cập nhật trạng thái cuối") },
            {
                Sentence.TasksView.CHOOSE_THE_MCPS_THAT_YOU_WANT_TO_BE_COLLECTED,
                new LocalizationUnit("Choose the MCPs that you want to be collected", "Chọn các MCP bạn muốn thu gom")
            },
            { Sentence.TasksView.CHOOSE_THE_WORKERS_TO_ASSIGN, new LocalizationUnit("Choose the workers to assign", "Chọn nhân viên để giao việc") },
            {
                Sentence.TasksView.LEAVE_THIS_STEP_EMPTY_IF_YOU_WANT_TO_ASSIGN_THE_TASK_TO_ALL_WORKERS,
                new LocalizationUnit("Leave this step empty if you want to assign the task to all workers",
                    "Để trống bước này nếu bạn muốn giao nhiệm vụ cho tất cả nhân viên")
            },
            {
                Sentence.TasksView.CHOOSE_THE_DATE_AND_TIME_TO_COLLECT_THE_SELECTED_MCPS,
                new LocalizationUnit("Choose the date and time to collect the selected MCPS", "Chọn ngày và giờ để thu gom các MCP đã chọn")
            },
            { Sentence.TasksView.CLEANING_TASK, new LocalizationUnit("Cleaning task", "Nhiệm vụ") },
            { Sentence.TasksView.ONGOING, new LocalizationUnit("Ongoing", "Đang thực hiện") },
            { Sentence.TasksView.PENDING, new LocalizationUnit("Pending", "Đang chờ") },
            { Sentence.TasksView.COMPLETE, new LocalizationUnit("Complete", "Hoàn thành") },
            { Sentence.TasksView.COMPLETED, new LocalizationUnit("Completed", "Đã hoàn thành") },
            { Sentence.TasksView.REJECT, new LocalizationUnit("Reject", "Từ chối") },
            { Sentence.TasksView.REJECTED, new LocalizationUnit("Rejected", "Đã từ chối") },
            { Sentence.TasksView.ADDRESS, new LocalizationUnit("Address", "Địa chỉ") },
            { Sentence.TasksView.DISTANCE, new LocalizationUnit("Distance", "Khoảng cách") },
            { Sentence.TasksView.ETA, new LocalizationUnit("ETA", "Dự kiến đến nơi") },
            { Sentence.TasksView.EMPTYING_LOGS, new LocalizationUnit("Emptying logs", "Lịch sử thu gom") },
            { Sentence.TasksView.CURRENT_LOAD, new LocalizationUnit("Current load", "Tải hiện tại") },

            { Sentence.StatusView.STATUS, new LocalizationUnit("Status", "Trạng thái") },

            { Sentence.WorkersView.WORKERS, new LocalizationUnit("Workers", "Nhân viên") },
            { Sentence.WorkersView.ROLE, new LocalizationUnit("Role", "Chức vụ") },
            { Sentence.WorkersView.SUPERVISOR, new LocalizationUnit("Supervisor", "Quản lý") },
            { Sentence.WorkersView.DRIVER, new LocalizationUnit("Driver", "Tài xế") },
            { Sentence.WorkersView.CLEANER, new LocalizationUnit("Cleaner", "Lao công") },
            { Sentence.WorkersView.GENDER, new LocalizationUnit("Gender", "Giới tính") },
            { Sentence.WorkersView.MALE, new LocalizationUnit("Male", "Nam") },
            { Sentence.WorkersView.FEMALE, new LocalizationUnit("Female", "Nữ") },
            { Sentence.WorkersView.OTHER, new LocalizationUnit("Other", "Khác") },
            { Sentence.WorkersView.DATE_OF_BIRTH, new LocalizationUnit("Date of birth", "Ngày sinh") },
            { Sentence.WorkersView.ADDRESS, new LocalizationUnit("Address", "Địa chỉ") },
            { Sentence.WorkersView.ACCOUNT_CREATED, new LocalizationUnit("Account created", "Tạo tài khoản") },

            { Sentence.McpsView.MCPS, new LocalizationUnit("MCPs", "MCP") },
            { Sentence.McpsView.FILL_LEVEL, new LocalizationUnit("Fill level", "Mức đầy") },
            { Sentence.McpsView.TODAY, new LocalizationUnit("Today", "Hôm nay") },
            { Sentence.McpsView.LATITUDE, new LocalizationUnit("Latitude", "Vĩ độ") },
            { Sentence.McpsView.LONGITUDE, new LocalizationUnit("Longitude", "Kinh độ") },
            { Sentence.McpsView.LAST_EMPTIED, new LocalizationUnit("Last emptied", "Lần dọn dẹp cuối") },
            { Sentence.McpsView.NEVER, new LocalizationUnit("Never", "Chưa bao giờ") },
            { Sentence.McpsView.MAJOR_COLLECTION_POINT, new LocalizationUnit("Major collection point", "Điểm thu thập rác (MCP)") },
            { Sentence.McpsView.ADDRESS, new LocalizationUnit("Address", "Địa chỉ") },
            { Sentence.McpsView.FILL_LEVEL_BY_HOUR, new LocalizationUnit("Fill level by hour", "Độ đầy theo giờ") },

            { Sentence.VehiclesView.VEHICLES, new LocalizationUnit("Vehicles", "Phương tiện") },
            { Sentence.VehiclesView.SIDE_LOADER, new LocalizationUnit("Side loader", "Xe rác thùng bên") },
            { Sentence.VehiclesView.REAR_LOADER, new LocalizationUnit("Rear loader", "Xe rác thùng sau") },
            { Sentence.VehiclesView.FRONT_LOADER, new LocalizationUnit("Front loader", "Xe rác thùng trước") },
            { Sentence.VehiclesView.MODEL, new LocalizationUnit("Model", "Mẫu xe") },
            { Sentence.VehiclesView.VEHICLE_TYPE, new LocalizationUnit("Vehicle type", "Loại xe") },

            { Sentence.ReportingView.REPORTING, new LocalizationUnit("Reporting", "Báo cáo") },
            { Sentence.ReportingView.MCPS_COLLECTED, new LocalizationUnit("MCPS collected", "MCP đã thu gom") },
            { Sentence.ReportingView.DISTANCE_TRAVELED, new LocalizationUnit("Distance traveled", "Quãng đường đã đi") },
            { Sentence.ReportingView.FUEL_CONSUMED, new LocalizationUnit("Fuel consumed", "Nhiên liệu đã tiêu thụ") },
            { Sentence.ReportingView.CURRENT_TEMPERATURE, new LocalizationUnit("Current temperature", "Nhiệt độ") },
            { Sentence.ReportingView.CHANCE_OF_PRECIPITATION, new LocalizationUnit("Chance of precipitation", "Tỉ lệ mưa") },
            { Sentence.ReportingView.MCP_CAPACITY, new LocalizationUnit("MCP capacity", "Dung lượng MCP") },
            { Sentence.ReportingView.TASKS_LEFT, new LocalizationUnit("Tasks left", "Số nhiệm vụ còn lại") },
            { Sentence.ReportingView.TASKS_CREATED, new LocalizationUnit("Tasks created", "Số nhiệm vụ đã tạo") },
            { Sentence.ReportingView.WORKERS_ONLINE, new LocalizationUnit("Workers online", "Số công nhân đang trực tuyến") },
            { Sentence.ReportingView.TOTAL_WORKERS, new LocalizationUnit("Total workers", "Tổng số công nhân") },
            {
                Sentence.ReportingView.HOURLY_AGGREGATED_MCPS_FILL_LEVEL,
                new LocalizationUnit("Hourly aggregated MCPS fill level", "Tổng mức đầy mỗi giờ của MCP")
            },
            { Sentence.ReportingView.HOURLY_MCPS_EMPTIED, new LocalizationUnit("Hourly MCPS emptied", "MCP được thu gom mỗi giờ") },

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
            { Sentence.SettingsView.MCPS_EMPTIED, new LocalizationUnit("Mcps emptied", "Mcps đã được thu gom") },
            { Sentence.SettingsView.SOFTWARE_UPDATE_AVAILABLE, new LocalizationUnit("Software update available", "Cập nhật phần mềm") },
            { Sentence.SettingsView.ACCOUNT_SETTINGS, new LocalizationUnit("Account settings", "Cài đặt tài khoản") },
            { Sentence.SettingsView.ONLINE_STATUS, new LocalizationUnit("Online status", "Trạng thái trực tuyến") },
            { Sentence.SettingsView.ONLINE, new LocalizationUnit("Online", "Trực tuyến") },
            { Sentence.SettingsView.OFFLINE, new LocalizationUnit("Offline", "Ngoại tuyến") },
            { Sentence.SettingsView.EXPORT_MESSAGES, new LocalizationUnit("Export messages", "Xuất tin nhắn") },
            { Sentence.SettingsView.EXPORT_WORK_LOGS, new LocalizationUnit("Export work logs", "Xuất nhật ký công việc") },
            { Sentence.SettingsView.CHANGE_PERSONAL_INFORMATION, new LocalizationUnit("Change personal information", "Thay đổi thông tin cá nhân") },
            { Sentence.SettingsView.CHANGE_PASSWORD, new LocalizationUnit("Change password", "Thay đổi mật khẩu") },
            { Sentence.SettingsView.REPORT_PROBLEM, new LocalizationUnit("Report problem", "Báo cáo vấn đề") },
            { Sentence.SettingsView.LOGOUT, new LocalizationUnit("Logout", "Đăng xuất") },
            { Sentence.SettingsView.REGISTER_FACIAL_RECOGNITION, new LocalizationUnit("Register facial recognition", "Đăng ký nhận diện khuôn mặt") },

            { Sentence.ListControl.SEARCH, new LocalizationUnit("Search", "Tìm kiếm") },
            { Sentence.ListControl.SORT_BY, new LocalizationUnit("Sort by", "Sắp xếp theo") },

            { Sentence.Procedure.CONFIRM, new LocalizationUnit("Confirm", "Xác nhận") },

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

        public static LanguageOption LanguageOption
        {
            get => _languageOption;
            set
            {
                _languageOption = value;
                LanguageChanged?.Invoke();
            }
        }

        public static string GetSentence(string sentence)
        {
            if (!_localizationUnitsBySentence.ContainsKey(sentence)) return sentence;

            return LanguageOption == LanguageOption.English
                ? _localizationUnitsBySentence[sentence].English
                : _localizationUnitsBySentence[sentence].Vietnamese;
        }

        public static string GetMonth(int month)
        {
            var sentence = month switch
            {
                1 => Sentence.DateAndTime.JANUARY,
                2 => Sentence.DateAndTime.FEBRUARY,
                3 => Sentence.DateAndTime.MARCH,
                4 => Sentence.DateAndTime.APRIL,
                5 => Sentence.DateAndTime.MAY,
                6 => Sentence.DateAndTime.JUNE,
                7 => Sentence.DateAndTime.JULY,
                8 => Sentence.DateAndTime.AUGUST,
                9 => Sentence.DateAndTime.SEPTEMBER,
                10 => Sentence.DateAndTime.OCTOBER,
                11 => Sentence.DateAndTime.NOVEMBER,
                12 => Sentence.DateAndTime.DECEMBER,
                _ => Sentence.MissingTranslation.MISSING_TRANSLATION
            };

            return GetSentence(sentence);
        }

        public static string GetGender(Gender gender)
        {
            var sentence = gender switch
            {
                Gender.Male => Sentence.WorkersView.MALE,
                Gender.Female => Sentence.WorkersView.FEMALE,
                Gender.Other => Sentence.WorkersView.OTHER,
                _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
            };

            return GetSentence(sentence);
        }

        public static string GetUserRole(UserRole userRole)
        {
            var sentence = userRole switch
            {
                UserRole.Supervisor => Sentence.WorkersView.SUPERVISOR,
                UserRole.Driver => Sentence.WorkersView.DRIVER,
                UserRole.Cleaner => Sentence.WorkersView.CLEANER,
                _ => throw new ArgumentOutOfRangeException(nameof(userRole), userRole, null)
            };

            return GetSentence(sentence);
        }

        public static string GetVehicleType(VehicleType vehicleType)
        {
            var sentence = vehicleType switch
            {
                VehicleType.SideLoader => Sentence.VehiclesView.SIDE_LOADER,
                VehicleType.RearLoader => Sentence.VehiclesView.REAR_LOADER,
                VehicleType.FrontLoader => Sentence.VehiclesView.FRONT_LOADER,
                _ => throw new ArgumentOutOfRangeException(nameof(vehicleType), vehicleType, null)
            };

            return GetSentence(sentence);
        }
    };
}