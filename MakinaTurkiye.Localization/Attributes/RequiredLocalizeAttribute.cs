using System.ComponentModel.DataAnnotations;

namespace MakinaTurkiye.Localization.Attributes
{
    public class RequiredLocalizedAttribute : RequiredAttribute
    {
        protected string FormatErrorMessage()
        {
            return Localization.Localize("required");
        }
    }
}