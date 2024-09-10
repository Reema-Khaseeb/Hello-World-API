using Xunit;
using HelloWorldAPI.Dtos;

namespace HelloWorldAPI.Tests;

public class HelloEndpointTests
{
    [Fact]
    public void Hello_ReturnsHelloWorld_WhenNameIsNull()
    {
        // Arrange
        string name = null;

        // Act
        var result = HelloEndpoint(name);

        // Assert
        Assert.Equal("Hello, World!", result.Greeting);
    }

    [Fact]
    public void Hello_ReturnsHelloWithName_WhenNameIsProvided()
    {
        // Arrange
        string name = "John";

        // Act
        var result = HelloEndpoint(name);

        // Assert
        Assert.Equal("Hello, John", result.Greeting);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Hello_ReturnsHelloWorld_WhenNameIsEmptyOrNull(string name)
    {
        // Act
        var result = HelloEndpoint(name);

        // Assert
        Assert.Equal("Hello, World!", result.Greeting);
    }

    // Helper method simulating /hello endpoint logic
    private HelloResponse HelloEndpoint(string name)
    {
        return new HelloResponse(
            string.IsNullOrWhiteSpace(name) ? "Hello, World!" : $"Hello, {name}"
        );
    }
}
