namespace NeoSistem.MakinaTurkiye.Management.Models.Validation
{
    using Properties;
    using System.ComponentModel.DataAnnotations;

    public class EmailValidationAttribute : RegularExpressionAttribute
  {
    public EmailValidationAttribute()
      : base("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$")
    {
      ErrorMessageResourceName = "EmailValidation";
      ErrorMessageResourceType = typeof(Resources);
    }
  }
}