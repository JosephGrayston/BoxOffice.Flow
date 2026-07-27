// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Claims;

namespace BoxOffice.Flow.UnitTests.Identity
{
    internal static class ClaimPrincipalBuilder
    {
        internal static ClaimsPrincipal CreateUser(string objectId)
        {
            return new ClaimsPrincipal(
                new ClaimsIdentity(
                    [
                        new Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", objectId)
                    ], "Test"));
        }
    }
}
