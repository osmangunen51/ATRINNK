using System.Collections.Generic;
using System.Web.Mvc;

namespace MakinaTurkiye.Utilities.Mvc
{
    public class BaseMakinaTurkiyeModel
    {
        public BaseMakinaTurkiyeModel()
        {
            this.CustomProperties = new Dictionary<string, object>();
        }

        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

        }

        /// <summary>
        /// Use this property to store any custom value for your models. 
        /// </summary>
        public Dictionary<string, object> CustomProperties { get; set; }

        public string Navigation { get; set; }
    }
}
