namespace NeoSistem.MakinaTurkiye.Web.Models.Validation
{
  using System;
  using Properties;
  public class RangeValidationAttribute : System.ComponentModel.DataAnnotations.RangeAttribute
  {
    public RangeValidationAttribute(int minimum, int maximum)
      : base(minimum, maximum)
    {
      InitializeErrorMessageResource();
    }

    public RangeValidationAttribute(double minimum, double maximum)
      : base(minimum, maximum)
    {
      InitializeErrorMessageResource();
    }

    public RangeValidationAttribute(Type type, string minimum, string maximum)
      : base(type, minimum, maximum)
    {
      InitializeErrorMessageResource();
    }

    private void InitializeErrorMessageResource()
    {
      ErrorMessageResourceName = "RangeValidation";
      ErrorMessageResourceType = typeof(Resources);
    }
  }
}