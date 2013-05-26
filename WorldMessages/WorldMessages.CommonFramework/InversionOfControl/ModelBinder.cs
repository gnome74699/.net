using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WorldMessages.CommonFramework.InversionOfControl
{
    public class ModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            if (!modelType.IsValueType && modelType.Namespace != null && !modelType.Namespace.StartsWith("System"))
            {
                return DependencyResolver.Current.GetService(modelType);
            }

            return base.CreateModel(controllerContext, bindingContext, modelType);
        }
    }
}
