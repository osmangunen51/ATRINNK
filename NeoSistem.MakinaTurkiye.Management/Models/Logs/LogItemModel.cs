using System;

namespace NeoSistem.MakinaTurkiye.Management.Models.Logs
{
    public class LogItemModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string LogSituationText { get; set; }
        public string LogSituatinColor { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }

    }
}