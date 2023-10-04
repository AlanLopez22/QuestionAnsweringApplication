using System.Security.Claims;
using System.Security.Principal;

namespace QuestionAnsweringApplication
{
    public static class IdentityExtension
    {
        public static Guid GetUserId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            var value = GetValue(identity, ClaimTypes.NameIdentifier);
            if (value != null)
            {
                if (Guid.TryParse(value, out var userId))
                {
                    return userId;
                }
            }

            return default;
        }

        private static string GetValue(IIdentity identity, string key)
        {
            return GetValues(identity, key).FirstOrDefault()!;
        }

        private static IEnumerable<string> GetValues(IIdentity identity, string key)
        {
            if (identity is ClaimsIdentity claimsIdentity)
            {
                return claimsIdentity.FindAll(key)
                    .Select(x => x.Value);
            }

            return Array.Empty<string>();
        }
    }
}
