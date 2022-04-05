using System;

namespace MakinaTurkiye.Entities.Tables.Logs
{
    public class ApplicationLog : BaseEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
