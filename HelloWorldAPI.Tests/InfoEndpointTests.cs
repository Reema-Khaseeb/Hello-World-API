using Xunit;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Net;
using HelloWorldAPI.Dtos;
using Microsoft.Extensions.Primitives;

namespace HelloWorldAPI.Tests;

public class InfoEndpointTests
{
    [Fact]
    public void Info_ReturnsClientAddressAndHostName_WhenContextHasRemoteIpAndHeaders()
    {
        // Arrange
        var context = CreateMockHttpContext(IPAddress.Parse("127.0.0.1"), "Mozilla/5.0");

        // Act
        var result = InfoEndpoint(context);

        // Assert
        Assert.Equal("127.0.0.1", result.ClientAddress);
        Assert.NotNull(result.HostName);
        Assert.NotNull(result.Time);
        Assert.NotNull(result.Headers);
        Assert.Equal("Mozilla/5.0", result.Headers["User-Agent"]);
    }

    [Fact]
    public void Info_ReturnsUnknown_WhenClientAddressIsNull()
    {
        // Arrange
        var context = CreateMockHttpContext(null, "Mozilla/5.0");

        // Act
        var result = InfoEndpoint(context);

        // Assert
        Assert.Equal("Unknown", result.ClientAddress);
    }

    // Helper method to create the mock HttpContext
    private HttpContext CreateMockHttpContext(IPAddress ipAddress, string userAgent)
    {
        var mockContext = new Mock<HttpContext>();
        var mockRequest = new Mock<HttpRequest>();

        // Mock the RemoteIpAddress
        mockContext.Setup(c => c.Connection.RemoteIpAddress).Returns(ipAddress);

        // Mocking the Headers
        var headers = new HeaderDictionary
            {
                { "User-Agent", new StringValues(userAgent) }
            };
        mockRequest.Setup(r => r.Headers).Returns(headers);
        mockContext.Setup(c => c.Request).Returns(mockRequest.Object);

        return mockContext.Object;
    }

    // Helper method simulating /info endpoint logic
    private InfoResponse InfoEndpoint(HttpContext context)
    {
        var clientAddress = context.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
        var hostName = Dns.GetHostName();
        var requestTime = DateTime.UtcNow.ToString("o");
        var headers = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());

        return new InfoResponse(
            Time: requestTime,
            ClientAddress: clientAddress,
            HostName: hostName,
            Headers: headers
        );
    }
}
