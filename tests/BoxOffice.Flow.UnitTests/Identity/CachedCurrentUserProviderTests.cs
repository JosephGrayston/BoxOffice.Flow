using BoxOffice.Flow.Identity;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Shouldly;

namespace BoxOffice.Flow.UnitTests.Identity;

[TestClass]
public sealed class CachedCurrentUserProviderTests
{
    private const string CachedUser = "testuser";

    private Mock<ICurrentUserProvider> _mockUserDirectory = null!;
    private CurrentPrincipalAccessor _principalAccessor = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockUserDirectory = new Mock<ICurrentUserProvider>();

        var authProvider = new AuthenticationStateProviderFake(ClaimPrincipalBuilder.CreateUser(CachedUser));
        _principalAccessor = new CurrentPrincipalAccessor(authProvider);
    }

    [TestMethod]
    public async Task GetCurrentUserAsyncWithCacheMissRequestsAndReturnsUser()
    {
        _mockUserDirectory.Setup(x => x.GetCurrentUserAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new UserProfile { Name = "Test User", Email = "test@example.com" });

        var cachedUserDirectory = new CachedCurrentUserProvider(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()), _principalAccessor);

        var result = await cachedUserDirectory.GetCurrentUserAsync(CancellationToken.None);

        _mockUserDirectory.Verify(x => x.GetCurrentUserAsync(It.IsAny<CancellationToken>()), Times.Once);

        result.ShouldNotBeNull();
        result?.Name.ShouldBe("Test User");
    }

    [TestMethod]
    public async Task GetCurrentUserAsyncWithCacheHitReturnsUserWithoutRequest()
    {
        var userProfile = new UserProfile
        {
            Name = "Cached User",
            Email = "cachedtest@example.com"
        };

        _mockUserDirectory.Setup(x => x.GetCurrentUserAsync(It.IsAny<CancellationToken>())).ReturnsAsync(userProfile);

        using var cache = new MemoryCache(new MemoryCacheOptions());
        var cachedUserDirectory = new CachedCurrentUserProvider(_mockUserDirectory.Object, cache, _principalAccessor);

        cache.Set($"user:{CachedUser}", userProfile);

        var result = await cachedUserDirectory.GetCurrentUserAsync(CancellationToken.None);

        _mockUserDirectory.Verify(x => x.GetCurrentUserAsync(It.IsAny<CancellationToken>()), Times.Never);
        result.ShouldBeSameAs(userProfile);
    }

    [TestMethod]
    public async Task GetCurrentUserAsyncWhenCalledTwiceOnlyCallsUserDirectoryOnce()
    {
        _mockUserDirectory.Setup(x => x.GetCurrentUserAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new UserProfile { Name = "Cached User", Email = "cachedtest@example.com" });

        var cachedUserDirectory = new CachedCurrentUserProvider(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()), _principalAccessor);

        await cachedUserDirectory.GetCurrentUserAsync(CancellationToken.None);
        await cachedUserDirectory.GetCurrentUserAsync(CancellationToken.None);

        _mockUserDirectory.Verify(x => x.GetCurrentUserAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task GetCurrentUserAsyncWhenUserDirectoryReturnsNullDoesNotCache()
    {
        _mockUserDirectory.Setup(x => x.GetCurrentUserAsync(It.IsAny<CancellationToken>())).ReturnsAsync((UserProfile?)null);

        var cachedUserDirectory = new CachedCurrentUserProvider(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()), _principalAccessor);

        var result1 = await cachedUserDirectory.GetCurrentUserAsync(CancellationToken.None);
        var result2 = await cachedUserDirectory.GetCurrentUserAsync(CancellationToken.None);

        _mockUserDirectory.Verify(x => x.GetCurrentUserAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        result1.ShouldBeNull();
        result2.ShouldBeNull();
    }

    [TestMethod]
    public async Task GetCurrentUserPhotoAsyncWithCacheMissRequestsAndReturnsUserPhoto()
    {
        _mockUserDirectory.Setup(x => x.GetCurrentUserPhotoAsync(It.IsAny<CancellationToken>())).ReturnsAsync("user-photo.jpg");

        var cachedUserDirectory = new CachedCurrentUserProvider(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()), _principalAccessor);

        var result = await cachedUserDirectory.GetCurrentUserPhotoAsync(CancellationToken.None);

        _mockUserDirectory.Verify(x => x.GetCurrentUserPhotoAsync(It.IsAny<CancellationToken>()), Times.Once);

        result.ShouldNotBeNull();
        result.ShouldBe("user-photo.jpg");
    }


    [TestMethod]
    public async Task GetCurrentUserPhotoAsyncWithCacheHitReturnsUserPhotoWithoutRequest()
    {
        var userPhoto = "user-photo.jpg";

        _mockUserDirectory.Setup(x => x.GetCurrentUserPhotoAsync(It.IsAny<CancellationToken>())).ReturnsAsync(userPhoto);

        using var cache = new MemoryCache(new MemoryCacheOptions());
        var cachedUserDirectory = new CachedCurrentUserProvider(_mockUserDirectory.Object, cache, _principalAccessor);

        cache.Set($"photo:{CachedUser}", userPhoto);

        var result = await cachedUserDirectory.GetCurrentUserPhotoAsync(CancellationToken.None);

        _mockUserDirectory.Verify(x => x.GetCurrentUserPhotoAsync(It.IsAny<CancellationToken>()), Times.Never);
        result.ShouldBeSameAs(userPhoto);
    }

    [TestMethod]
    public async Task GetCurrentUserPhotoAsyncWhenCalledTwiceOnlyCallsUserDirectoryOnce()
    {
        _mockUserDirectory.Setup(x => x.GetCurrentUserPhotoAsync(It.IsAny<CancellationToken>())).ReturnsAsync("user-photo.jpg");

        var cachedUserDirectory = new CachedCurrentUserProvider(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()), _principalAccessor);

        await cachedUserDirectory.GetCurrentUserPhotoAsync(CancellationToken.None);
        await cachedUserDirectory.GetCurrentUserPhotoAsync(CancellationToken.None);

        _mockUserDirectory.Verify(x => x.GetCurrentUserPhotoAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task GetCurrentUserPhotoAsyncWhenUserDirectoryReturnsNullDoesNotCache()
    {
        _mockUserDirectory.Setup(x => x.GetCurrentUserPhotoAsync(It.IsAny<CancellationToken>())).ReturnsAsync((string?)null);

        var cachedUserDirectory = new CachedCurrentUserProvider(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()), _principalAccessor);

        var result1 = await cachedUserDirectory.GetCurrentUserPhotoAsync(CancellationToken.None);
        var result2 = await cachedUserDirectory.GetCurrentUserPhotoAsync(CancellationToken.None);

        _mockUserDirectory.Verify(x => x.GetCurrentUserPhotoAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        result1.ShouldBeNull();
        result2.ShouldBeNull();
    }
}
