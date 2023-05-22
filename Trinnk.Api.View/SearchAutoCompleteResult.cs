using System.Collections.Generic;

namespace Trinnk.Api.View
{
    public class SearchAutoCompleteResult
    {
        public List<SearchAutoCompleteItem> suggestions { get; set; } = new List<SearchAutoCompleteItem>();
    }
    public class SearchAutoCompleteItem
    {
        public string Name { get; set; } = "";
        public string Url { get; set; } = "";
        public data data { get; set; } = new data();
        public string Value { get; set; } = "";
    }

    public class data
    {
        public string category { get; set; } = "None";
    }
}
