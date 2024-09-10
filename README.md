# HelloWorldApi

# Table of Contents
1. [Features](#features)
2. [Prerequisites](#prerequisites)
3. [Setup and Run](#setup-and-run)
4. [Endpoints](#endpoints)
   - [/hello](#hello)
   - [/info](#info)
5. [Example Requests](#example-requests)
6. [Test Coverage](#test-coverage)

This is a simple minimal API built using .NET that provides two endpoints:
- `/hello`: Returns a greeting message.
- `/info`: Provides information about the client making the request.

## Features

- Simple minimal API structure.
- Swagger documentation.
- Two endpoints to demonstrate basic API functionality.
- No database or complex business logic required.

## Prerequisites

- .NET SDK (version 6.0 or higher)
- `xUnit`
- `Moq` (if mocking dependencies is necessary, as in the `/info` endpoint tests)
- `Serilog` for logging

## Setup and Run

1. Clone the repository
    ```bash
    git clone https://github.com/Reema-Khaseeb/Hello-World-API.git
    ```
2. Navigate to the project folder:
    ```
    cd Hello-World-API/HelloWorldAPI
    ```
3. Open the project in Visual Studio or your preferred IDE
4. Restore the dependencies
    ```bash
    dotnet restore
    ```
5. Run the application using the "Run" button or by executing the command
    ```
    dotnet run
    ```
6. Access API endpoints via any HTTP client (browser, Postman, cURL, etc.).
Open a browser and navigate to:
- Swagger UI: `http://localhost:5000/swagger`
- `GET /hello` endpoint: `http://localhost:5000/hello`
- `GET /info` endpoint: `http://localhost:5000/info`
7. Run Tests
    ```bash
    dotnet test
    ```

## Endpoints

### `/hello`

- **Method**: GET
- **Description**: Returns a greeting. If a name is passed in as a query parameter, the greeting will include the name; otherwise, it defaults to "Hello, World!".
- **Parameters**:
  - `name` (optional): A query parameter for providing a name.
- **Response**:
  - **200 OK**: `{ "greeting": "Hello, [name]" }`
  - If `name` is not provided, it will return `{ "greeting": "Hello, World!" }`.

#### Example Requests:

- Without name:  
  `GET /hello`

  **Response**:
  ```json
  {
    "greeting": "Hello, World!"
  }
  ```

### `/info`

- **Method**: GET
- **Description**: Returns detailed information about the client making the request, such as their IP address, server hostname, request time, and request headers.
- **Response**:
  - **200 OK**: JSON response containing:
    - `time`: Request time in ISO8601 format.
    - `client_address`: IP address of the client.
    - `host_name`: Server host name.
    - `headers`: All request headers.

#### Example Requests:

- Without name:  
  `GET /info`

  **Response**:
  ```json
  {
    "time": "2023-09-10T12:34:56Z",
    "client_address": "127.0.0.1",
    "host_name": "my-server",
    "headers": {
    "User-Agent": "Mozilla/5.0...",
    "Accept": "application/json",
    ...
    }
  }
  ```

## Test Coverage
The tests cover the following scenarios:
1. `/hello` endpoint:
    - Returns "Hello, World!" when no name is provided.
    - Returns "Hello, [name]" when a name is passed as a query parameter.
    - Handles edge cases such as an empty or null name.
`/info` endpoint:
    - Mocks HttpContext to test that the correct client address, host name, and request time are returned.
