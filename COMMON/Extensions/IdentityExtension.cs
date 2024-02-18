using System.Runtime.InteropServices.Marshalling;
using System.Security.Claims;
using System.Security.Principal;

namespace COMMON.Extensions;
public static class IdentityExtension
{
    public static uint PersonId(this IIdentity identity)
    {
        string personIdStr = ((ClaimsIdentity)identity).FindFirst("PersonId")?.Value ?? string.Empty;
        if (uint.TryParse(personIdStr, out uint personId)) return personId;
        return 0;
    }
    public static string UserName(this IIdentity identity)
    {
        return ((ClaimsIdentity)identity).FindFirst("UserName")?.Value ?? string.Empty;
    }
    public static string Email(this IIdentity identity)
    {
        return ((ClaimsIdentity)identity).FindFirst("Email")?.Value ?? string.Empty;
    }
}