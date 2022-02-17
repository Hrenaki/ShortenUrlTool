
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetName(this Type controller)
        {
            if (!controller.IsSubclassOf(typeof(ControllerBase)))
            {
                return null;
            }

            var fullname = controller.Name;
            return fullname.Substring(0, fullname.Length - 10);
        }
    }
}