namespace BackOffice.Client.Extensions
{
    public static class StringExtensions
    {
        public static string ShowLess(this string value, int limit)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Length <= limit ? value : $"{value.Substring(0, limit)} ...";
        }
    }
}
