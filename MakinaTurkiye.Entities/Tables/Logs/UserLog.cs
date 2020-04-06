namespace MakinaTurkiye.Entities.Tables.Logs
{
    public class UserLog : BaseEntity
    {
        public int UserLogId { get; set; }
        public string LogName { get; set; }
        public string LogShortDescription { get; set; }
        public string LogDescription { get; set; }
        public int LogType { get; set; }
        public int LogStatus { get; set; }
        public string CreatedDate { get; set; }

    }
}
