using MakinaTurkiye.Entities.Tables.SearchEngine;
using System;

namespace MakinaTurkiye.Entities.Tables.Logs
{
    public class SearchEngineLog: BaseEntity
    {
        public int SearchEngineTypeId { get; set; }
        public SearchEngineType  SearchEngineType
        {
            get { return (SearchEngineType)SearchEngineTypeId; }
            set { SearchEngineTypeId = (int)value; }
        }
        public string ShortMessage { get; set; }
        public string FullMessage { get; set; }
        public string PageUrl { get; set; }
        public int MainPartyId { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
