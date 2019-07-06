using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Infrastructure.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        private const string TempDataSuccessMessageKey = "SuccessMessage";
        public static void AddSuccessMessage(this ITempDataDictionary tempDate, string message)
        {
            tempDate[TempDataSuccessMessageKey] = string message
        }
    }
}
