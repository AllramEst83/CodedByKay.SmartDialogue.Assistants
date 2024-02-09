# CodedByKay.SmartDialogue.Assistant Setup Guide

## Overview

This project offers a comprehensive solution for integrating smart dialogue assistants and chat history management into .NET applications. Designed with modularity and extensibility in mind, it lays the groundwork for developers to create rich, interactive chat applications that can engage users with AI-powered conversations while maintaining an accessible history of these interactions.

At the heart of the project are four key components:

- **ChatHistoryService**: Manages the storage and retrieval of chat conversations, providing a seamless experience for users to access their chat histories.
- **SmartDialogueAssistantsService**: Powers the integration of AI-driven dialogue assistants, enhancing user interactions with smart, context-aware responses.
- **CodedByKaySmartDialogueAssistantsExtensions**: Offers extension methods to simplify the integration and management of dialogue assistants within the application, emphasizing ease of use and developer productivity.
- **SmartDialogueServiceFactory**: Implements the factory pattern to efficiently manage the creation and lifecycle of smart dialogue service instances, ensuring scalability and maintainability.

### Core Features

- **Middleware Integration**: Easily integrates into the middleware pipeline of a .NET web application.
- **Asynchronous Communication**: Communicates asynchronously with the OpenAI Assistant API.
- **Secure API Key Management**: Offers a secure way to manage and use your OpenAI API key.
- **Chat History Management**: Utilizes a `ConcurrentDictionary` for efficient and thread-safe chat history management.

### OpenAI API Communication

- **Send Messages**: Allows sending messages to the OpenAI Assistant API.
- **Receive and Forward Responses**: Receives responses from OpenAI and forwards them to the library user.
- **Maintain Chat History**: Keeps a record of the chat history that can be utilized in subsequent API requests for context.

## Setup and Configuration

### Prerequisites

- .NET Core 3.1 SDK or later.
- An OpenAI API key.

### Installation

To install `CodedByKay.SmartDialogue`, add it to your project via NuGet Package Manager or using the .NET CLI:

```shell
dotnet add package CodedByKay.SmartDialogue
```

### Configuration in Startup.cs

Incorporate CodedByKay.SmartDialogue into your project's Startup.cs file to begin using its functionalities.


Register SmartDialogue Services
Then, add `CodedByKay.SmartDialogue` to your service collection:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSmartDialogue(options =>
    {
        // All values are default for the AddSmartDialogue library
        options.OpenAiApiKey = "your_openai_api_key_here";
        options.Model = "gpt-3.5-turbo";
        options.OpenAIApiUrl = "https://api.openai.com";
        options.MaxTokens = 2000;
        options.Temperature = 1;
        options.TopP = 1;
        options.AverageTokeLenght = 2.85;
    });
}
```

### Usage
Inject ISmartDialogueService into your controllers or services to utilize the library:

```csharp
using Microsoft.AspNetCore.Mvc;

public class ChatController : ControllerBase
{
    private readonly ISmartDialogueService _smartDialogueService;

    public ChatController(ISmartDialogueServiceFactory smartDialogueServiceFactory)
    {
        _smartDialogueService = smartDialogueServiceFactory.Create();
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("Message is required.");
        }

        try
        {
            var response = await _smartDialogueService.SendMessageAsync(request.SessionId, request.Message);
            return Ok(new { Response = response });
        }
        catch (Exception ex)
        {
            // Log the exception details
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}

public class ChatRequest
{
    public string SessionId { get; set; }
    public string Message { get; set; }
}

```

### Conclusion
With CodedByKay.SmartDialogue, you can enhance your .NET web applications by integrating sophisticated chat functionalities powered by the OpenAI Assistant API. Follow this guide to set up and start leveraging this powerful library in your projects.


# Project Name

## Features
- Chat history management: Store and retrieve chat conversations efficiently.
- Smart dialogue assistants: Integrate AI-powered dialogue assistants to enhance user interaction.
- Extensibility: Easily extend the project with additional functionalities through well-defined interfaces and services.
- Factory pattern implementation: Utilize the factory pattern for creating instances of smart dialogue services, promoting scalability and maintainability.

## Setup and Configuration

### Prerequisites
- .NET Core SDK (version specific based on the project requirement)
- Any preferred IDE for .NET development (e.g., Visual Studio, Visual Studio Code)

### Installation
1. Clone the repository to your local machine.
2. Navigate to the project directory.
3. Restore the necessary packages by running `dotnet restore`.

### Configuration in Startup.cs
Incorporate CodedByKay.SmartDialogue.Assistants into your project's Startup.cs file to begin using its functionalities.


Register SmartDialogue Services
Then, add `CodedByKay.SmartDialogue.Assistants` to your service collection:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSmartDialogueAssistants(options =>
    {
        // All values are default for the AddSmartDialogue library
        options.OpenAiApiKey = "your_openai_api_key_here";
        options.Model = "gpt-3.5-turbo";
        options.OpenAIApiUrl = "https://api.openai.com/v1/";
        options.MaxTokens = 2000;
        options.AverageTokeLenght = 2.85;
    });
}
```

### Usage
Inject ISmartDialogueServiceFactory into your controllers or services to utilize the library:

```csharp
using Microsoft.AspNetCore.Mvc;

public class ChatController : ControllerBase
{
    private readonly ISmartDialogueService _smartDialogueService;

    public ChatController(ISmartDialogueServiceFactory smartDialogueServiceFactory)
    {
        _smartDialogueService = smartDialogueServiceFactory.Create();
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("Message is required.");
        }

        try
        {
            var response = await _smartDialogueService.SendMessageAsync(request.SessionId, request.Message);
            return Ok(new { Response = response });
        }
        catch (Exception ex)
        {
            // Log the exception details
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}

public class ChatRequest
{
    public string SessionId { get; set; }
    public string Message { get; set; }
}

### Conclusion
- A summary of the project's benefits and potential use cases.

## Contributing
- Guidelines for contributing to the project.

## License
- Details about the project's license.
