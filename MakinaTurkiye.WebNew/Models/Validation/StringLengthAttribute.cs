namespace NeoSistem.MakinaTurkiye.Web.Models.Validation
{
  using Properties;
  public class StringLengthValidationAttribute : System.ComponentModel.DataAnnotations.StringLengthAttribute
  {
    public StringLengthValidationAttribute(int maximumLength)
      : base(maximumLength)
    {
      ErrorMessageResourceName = "StringLengthValidation";
      ErrorMessageResourceType = typeof(Resources);
    }
  }
}