namespace NeoSistem.Trinnk.Web.Models.Validation
{
    using Properties;
    public class RequiredValidationAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {
        public RequiredValidationAttribute()
        {
            ErrorMessageResourceName = "RequiredValidation";
            ErrorMessageResourceType = typeof(Resources);
        }
    }
}