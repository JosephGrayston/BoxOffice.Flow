using BoxOffice.Flow.Identity;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Shouldly;

namespace BoxOffice.Flow.UnitTests.Identity;

[TestClass]
public sealed class CachedUserDirectoryTests
{
    private Mock<IUserDirectory> _mockUserDirectory = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockUserDirectory = new Mock<IUserDirectory>();
    }

    [TestMethod]
    public async Task GetUserAsyncWithCacheMissRequestsAndReturnsUser()
    {
        _mockUserDirectory.Setup(x => x.GetUserAsync("123")).ReturnsAsync(new UserProfile { DisplayName = "Test User", Email = "test@example.com" });

        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()));

        var result = await cachedUserDirectory.GetUserAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserAsync("123"), Times.Once);

        result.ShouldNotBeNull();
        result?.DisplayName.ShouldBe("Test User");
    }

    [TestMethod]
    public async Task GetUserAsyncWithCacheHitReturnsUserWithoutRequest()
    {
        var userProfile = new UserProfile
        {
            DisplayName = "Cached User",
            Email = "cachedtest@example.com"
        };

        _mockUserDirectory.Setup(x => x.GetUserAsync("123")).ReturnsAsync(userProfile);

        using var cache = new MemoryCache(new MemoryCacheOptions());
        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, cache);

        cache.Set("user:123", userProfile);

        var result = await cachedUserDirectory.GetUserAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserAsync("123"), Times.Never);
        result.ShouldBeSameAs(userProfile);
    }

    [TestMethod]
    public async Task GetUserAsyncWhenCalledTwiceOnlyCallsUserDirectoryOnce()
    {
        _mockUserDirectory.Setup(x => x.GetUserAsync("123")).ReturnsAsync(new UserProfile { DisplayName = "Cached User", Email = "cachedtest@example.com" });

        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()));

        await cachedUserDirectory.GetUserAsync("123");
        await cachedUserDirectory.GetUserAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserAsync("123"), Times.Once);
    }

    [TestMethod]
    public async Task GetUserAsyncWhenUserDirectoryReturnsNullDoesNotCache()
    {
        _mockUserDirectory.Setup(x => x.GetUserAsync("123")).ReturnsAsync((UserProfile?)null);

        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()));

        var result1 = await cachedUserDirectory.GetUserAsync("123");
        var result2 = await cachedUserDirectory.GetUserAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserAsync("123"), Times.Exactly(2));
        result1.ShouldBeNull();
        result2.ShouldBeNull();
    }

    [TestMethod]
    public async Task GetUserAsyncWhenCacheExpiredCallsUserDirectoryAgain()
    {
        _mockUserDirectory.Setup(x => x.GetUserAsync("123")).ReturnsAsync(new UserProfile { DisplayName = "Cached User", Email = "cachedtest@example.com" });

        using var cache = new MemoryCache(new MemoryCacheOptions());
        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, cache);

        await cachedUserDirectory.GetUserAsync("123");

        cache?.Remove("user:123");

        await cachedUserDirectory.GetUserAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserAsync("123"), Times.Exactly(2));
    }

    [TestMethod]
    public async Task GetUserPhotoAsyncWithCacheMissRequestsAndReturnsUserPhoto()
    {
        _mockUserDirectory.Setup(x => x.GetUserPhotoAsync("123")).ReturnsAsync("user-photo.jpg");

        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()));

        var result = await cachedUserDirectory.GetUserPhotoAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserPhotoAsync("123"), Times.Once);

        result.ShouldNotBeNull();
        result.ShouldBe("user-photo.jpg");
    }


    [TestMethod]
    public async Task GetUserPhotoAsyncWithCacheHitReturnsUserPhotoWithoutRequest()
    {
        var userPhoto = "user-photo.jpg";

        _mockUserDirectory.Setup(x => x.GetUserPhotoAsync("123")).ReturnsAsync(userPhoto);

        using var cache = new MemoryCache(new MemoryCacheOptions());
        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, cache);

        cache.Set("userPhoto:123", userPhoto);

        var result = await cachedUserDirectory.GetUserPhotoAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserPhotoAsync("123"), Times.Never);
        result.ShouldBeSameAs(userPhoto);
    }

    [TestMethod]
    public async Task GetUserPhotoAsyncWhenCalledTwiceOnlyCallsUserDirectoryOnce()
    {
        _mockUserDirectory.Setup(x => x.GetUserPhotoAsync("123")).ReturnsAsync("user-photo.jpg");

        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()));

        await cachedUserDirectory.GetUserPhotoAsync("123");
        await cachedUserDirectory.GetUserPhotoAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserPhotoAsync("123"), Times.Once);
    }

    [TestMethod]
    public async Task GetUserPhotoAsyncWhenUserDirectoryReturnsNullDoesNotCache()
    {
        _mockUserDirectory.Setup(x => x.GetUserPhotoAsync("123")).ReturnsAsync((string?)null);

        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()));

        var result1 = await cachedUserDirectory.GetUserPhotoAsync("123");
        var result2 = await cachedUserDirectory.GetUserPhotoAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserPhotoAsync("123"), Times.Exactly(2));
        result1.ShouldBeNull();
        result2.ShouldBeNull();
    }

    [TestMethod]
    public async Task GetUserPhotoAsyncWhenCacheExpiredCallsUserDirectoryAgain()
    {
        _mockUserDirectory.Setup(x => x.GetUserPhotoAsync("123")).ReturnsAsync("user-photo.jpg");

        using var cache = new MemoryCache(new MemoryCacheOptions());
        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, cache);

        await cachedUserDirectory.GetUserPhotoAsync("123");

        cache?.Remove("userPhoto:123");

        await cachedUserDirectory.GetUserPhotoAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserPhotoAsync("123"), Times.Exactly(2));
    }

}
