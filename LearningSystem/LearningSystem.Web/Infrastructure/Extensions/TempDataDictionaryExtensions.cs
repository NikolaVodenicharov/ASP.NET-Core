using LearningSystem.Web.Infrastructure.Constants;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace LearningSystem.Web.Infrastructure.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        /// <summary>
        /// Can add to controllers success message that is displayed from the Layout view before rendering the body.
        /// </summary>
        public static void AddSuccessMessage(this ITempDataDictionary tempDate, string message)
        {
            tempDate[ExtensionConstants.TempDataSuccessMessageKey] = message;
        }
    }
}
