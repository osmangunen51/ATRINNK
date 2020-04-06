namespace NeoSistem.MakinaTurkiye.Management.Models.Validation
{
    using Properties;
    using System;
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