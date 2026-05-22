using BoxOffice.Flow.Identity;
using Microsoft.Extensions.Caching.Memory;
using Moq;

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
    public async Task GetUserAsyncWithCacheMissRequestsUser()
    {
        _mockUserDirectory.Setup(x => x.GetUserAsync("123")).ReturnsAsync(new User { DisplayName = "Test User", Email = "test@example.com" });

        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()));

        var result = await cachedUserDirectory.GetUserAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserAsync("123"), Times.Once);

        Assert.IsNotNull(result);
        Assert.AreEqual("Test User", result.DisplayName);
    }


    [TestMethod]
    public async Task GetUserAsyncWithCacheHitReturnsUserWithoutRequestingUser()
    {
        _mockUserDirectory.Setup(x => x.GetUserAsync("123")).ReturnsAsync(new User { DisplayName = "Cached User", Email = "cachedtest@example.com" });

        using var cache = new MemoryCache(new MemoryCacheOptions());
        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, cache);

        cache.Set("user:123", new User
        {
            DisplayName = "Cached User",
            Email = "cachedtest@example.com"
        });

        var result = await cachedUserDirectory.GetUserAsync("123");
        _mockUserDirectory.Verify(x => x.GetUserAsync("123"), Times.Never);

        Assert.AreEqual("Cached User", result?.DisplayName);
        Assert.AreEqual("cachedtest@example.com", result?.Email);
    }

    

    [TestMethod]
    public async Task GetUserAsyncWhenCalledTwiceOnlyCallsUserDirectoryOnce()
    {
        _mockUserDirectory.Setup(x => x.GetUserAsync("123")).ReturnsAsync(new User { DisplayName = "Cached User", Email = "cachedtest@example.com" });

        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()));

        await cachedUserDirectory.GetUserAsync("123");
        await cachedUserDirectory.GetUserAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserAsync("123"), Times.Once);
    }

    [TestMethod]
    public async Task GetUserAsyncWhenUserDirectoryReturnsNullDoesNotCache()
    {
        _mockUserDirectory.Setup(x => x.GetUserAsync("123")).ReturnsAsync((User?)null);

        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, new MemoryCache(new MemoryCacheOptions()));

        var result1 = await cachedUserDirectory.GetUserAsync("123");
        var result2 = await cachedUserDirectory.GetUserAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserAsync("123"), Times.Exactly(2));
        Assert.IsNull(result1);
        Assert.IsNull(result2);
    }

    [TestMethod]
    public async Task GetUserAsyncWhenCacheExpiredCallsUserDirectoryAgain()
    {
        _mockUserDirectory.Setup(x => x.GetUserAsync("123")).ReturnsAsync(new User { DisplayName = "Cached User", Email = "cachedtest@example.com" });

        using var cache = new MemoryCache(new MemoryCacheOptions());
        var cachedUserDirectory = new CachedUserDirectory(_mockUserDirectory.Object, cache);

        await cachedUserDirectory.GetUserAsync("123");

        cache?.Remove("user:123");

        await cachedUserDirectory.GetUserAsync("123");

        _mockUserDirectory.Verify(x => x.GetUserAsync("123"), Times.Exactly(2));
    }

}
