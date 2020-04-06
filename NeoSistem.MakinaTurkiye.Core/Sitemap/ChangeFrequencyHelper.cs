namespace NeoSistem.MakinaTurkiye.Core.Sitemap
{
    public static class ChangeFrequencyHelper
    {
        public static ChangeFrequency ToChangeFrequency(this double days)
        {
            if (days < 30)
            {
                //Biray
                return ChangeFrequency.daily;
            }
            else if (days < 270)
            {
                //Altiay
                return ChangeFrequency.weekly;
            }
            else
            {
                //Diger
                return ChangeFrequency.monthly;
            }
        }

        public static float ToPriority(this double days)
        {
            if (days <= 14)
            {
                return 0.7f;
            }
            else if (days > 365)
            {
                return 0.5f;
            }
            else
            {
                return 0.6f;
            }
        }
    }
}