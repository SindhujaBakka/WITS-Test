using System.Text.RegularExpressions;

namespace Services.Helpers
{
    public static class UsernameRules
    {
        private static readonly Regex Rx = new(@"^[A-Za-z0-9]{6,30}$", RegexOptions.Compiled);

        public static (bool IsValid, List<string> Errors) Validate(string? username)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(username))
            {
                errors.Add("Username is required.");
                return (false, errors);
            }

            if (!Rx.IsMatch(username))
            {
                errors.Add("Username must be 6-30 characters and alphanumeric only.");
            }

            return (errors.Count == 0, errors);
        }
    }
}
