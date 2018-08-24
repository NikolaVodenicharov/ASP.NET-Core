namespace CarDealer.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        private const string NumberFormat = "F2";

        public static string ToPrice(this decimal price)
        {
            return $"${price.ToString(NumberFormat)}";
        }

        public static string ToPercentage(this double percentage)
        {
            percentage = percentage * 100;

            return $"{percentage.ToString(NumberFormat)}%";
        }
    }
}
