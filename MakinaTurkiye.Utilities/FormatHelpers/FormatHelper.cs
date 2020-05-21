namespace MakinaTurkiye.Utilities.FormatHelpers
{
    public static class FormatHelper
    {
        public static string ToPlural(string value)
        {
            //if (!string.IsNullOrEmpty(value))
            //{
            //    if (value.Length >= 3)
            //    {
            //        if (value.Substring(value.Length - 3) == "ler" || value.Substring(value.Length - 3) == "lar")
            //        {
            //            return value;
            //        }
            //    }
            //    string letter = value.Substring(value.Length - 2).ToString();
            //    if (letter.Contains("e") || letter.Contains("i") || letter.Contains("ö") || letter.Contains("ü"))
            //    {
            //        return value + "ler";
            //    }
            //    return value + "lar";
            //}
            //return "";
            return value;
        }
        public static string GetMemberDescriptionText(string memberDescription)
        {
            string text = "";
            if (!string.IsNullOrEmpty(memberDescription) && !memberDescription.Contains("<img"))
            {
                if (memberDescription.IndexOf("~") > 0)
                {


                    string[] members = memberDescription.Split('~');
                    int counter = 1;
                    string container = "<div id='description-container' style='height:200px; width:100%; position: relative;  overflow: scroll; '>{0}</div>";
                    foreach (var item in members)
                    {
                        text += item.Replace("<p>", "").Replace("</p>", "") + "<br>";
                        counter++;
                    }
                    if (counter > 3)
                    {
                        text = string.Format(container, text);
                    }
                }
                else
                {
                    text = memberDescription;
                }
            }
            if (memberDescription.Contains("<img"))
            {
                text = "Mail";
            }
            return !string.IsNullOrEmpty(text) ? text : memberDescription;

        }
        public static string GetCategoryNameWithSynTax(string categoryName, CategorySyntaxType syntaxType)
        {

            switch (syntaxType)
            {
                case CategorySyntaxType.Store:
                    return string.Format("{0} {1}", categoryName, "Firmaları");
                default:
                    return categoryName;
            }
        }
        public static string GetSearchText(string searchText)
        {
            int indexChr = searchText.IndexOf("|");
            if (indexChr != -1)
            {

                searchText = searchText.Substring(0, indexChr);
            }
            return searchText;
        }

        public static bool ValueIsInt(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            var split = value.Split(',');
            int parse = 0;
            var result = int.TryParse(split[0], out parse);
            return result;
        }
    }
}
