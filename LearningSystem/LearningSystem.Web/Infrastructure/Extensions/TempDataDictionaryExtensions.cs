using LearningSystem.Web.Infrastructure.Constants;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace LearningSystem.Web.Infrastructure.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        public const string SuccessMessageKey = "SuccessMessage";
        public const string ErrorMessageKey = "ErrorMessage";

        /// <summary>
        /// Can add to controllers success message that is displayed from the Layout view before rendering the body.
        /// </summary>
        public static void AddSuccessMessage(this ITempDataDictionary tempDate, string message)
        {
            tempDate[SuccessMessageKey] = message;
        }

        /// <summary>
        /// Can add to controllers error message that is displayed from the Layout view before rendering the body.
        /// </summary>
        public static void AddErrrorMessage(this ITempDataDictionary tempDate, string message)
        {
            tempDate[ErrorMessageKey] = message;
        }
    }
}
