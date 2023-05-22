using System.ComponentModel.DataAnnotations;

namespace Trinnk.Localization.Attributes
{
    public class RequiredLocalizedAttribute : RequiredAttribute
    {
        protected string FormatErrorMessage()
        {
            return Localization.Localize("required");
        }
    }
}