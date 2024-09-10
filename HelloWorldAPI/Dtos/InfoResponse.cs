namespace HelloWorldAPI.Dtos;

public record InfoResponse(
        string Time,
        string ClientAddress,
        string HostName,
        IDictionary<string, string> Headers
    );
