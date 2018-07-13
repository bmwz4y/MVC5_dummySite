using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace WebApplication1test1.Common
{
    /*
     * Added on lesson 91
     */

    public class RemoteClientServerAttribute : RemoteAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Type controller =
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(
                        type => type.Name.ToLower() == $"{RouteData["controller"]}Controller".ToLower());

            if (controller != null)
            {
                MethodInfo action =
                    controller.GetMethods().FirstOrDefault(method => method.Name.ToLower() == RouteData["action"].ToString().ToLower());

                if (action != null)
                {
                    object instance = Activator.CreateInstance(controller);
                    object response = action.Invoke(instance, new[] { value });
                    var result = response as JsonResult;
                    if (result != null)
                    {
                        object jsonData = result.Data;
                        if (jsonData is bool)
                        {
                            return (bool)jsonData ? ValidationResult.Success : new ValidationResult(ErrorMessage);
                        }
                    }
                }
            }

            return ValidationResult.Success;
        }

        public RemoteClientServerAttribute(string routeName) : base(routeName)
        { }

        public RemoteClientServerAttribute(string action, string controller) : base(action, controller)
        { }

        public RemoteClientServerAttribute(string action, string controller, string areaName) : base(action, controller, areaName)
        { }
    }
}