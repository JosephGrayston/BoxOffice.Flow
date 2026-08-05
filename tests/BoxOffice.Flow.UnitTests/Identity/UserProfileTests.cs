using BoxOffice.Flow.Identity;
using Shouldly;

namespace BoxOffice.Flow.UnitTests.Identity;

[TestClass]
public sealed class UserProfileTests
{
    [TestMethod]
    [DataRow("Test User", "T")]
    [DataRow("User", "U")]
    public void InitialsWithNameReturnsFirstLetterOfNameInUpperCase(string name, string expectedInitial)
    {
        var userProfile = new UserProfile
        {
            Name = name
        };

        userProfile.Initials.ShouldBe(expectedInitial);
    }

    [TestMethod]
    public void InitialsWithNullNameReturnsEmptyString()
    {
        var userProfile = new UserProfile();

        userProfile.Initials.ShouldBe(string.Empty);
    }
}
