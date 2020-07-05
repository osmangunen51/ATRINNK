namespace MakinaTurkiye.Utilities.FormatHelpers
{
    public static class StringHelper
    {
        public static string Truncate(string value, int length, bool isEllipsis=false)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length <= length)
                {
                    return value;
                }
                value  =  value.Substring(0, length);
                if (isEllipsis)
                {
                    value += "...";
                }
                return value;
            }
            return "";
        }
    }
}
