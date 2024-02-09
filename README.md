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

To install `CodedByKay.SmartDialogue.Assistants`, add it to your project via NuGet Package Manager or using the .NET CLI:

```shell
dotnet add package CodedByKay.SmartDialogue.Assistants
```

### Configuration in Startup.cs

Incorporate CodedByKay.SmartDialogue.Assistants into your project's Startup.cs file to begin using its functionalities.


Register SmartDialogue Services
Then, add `CodedByKay.SmartDialogue.Assistants` to your service collection:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSmartDialogue(options =>
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
Inject ISmartDialogueAssistantsService into your controllers or services to utilize the library:

```csharp
using Microsoft.AspNetCore.Mvc;

public class ChatController : ControllerBase
{
    private readonly ISmartDialogueAssistantsService _smartDialogueAssistantsService;

    public ChatController(ISmartDialogueServiceFactory smartDialogueServiceFactory)
    {
        _smartDialogueAssistantsService = smartDialogueServiceFactory.Create();
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
            var response = await _smartDialogueAssistantsService.SendMessageAsync(request.SessionId, request.Message);
            return Ok(new { Response = response });
        }
        catch (Exception ex)
        {
            // Log the exception details
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
```

### Conclusion
With CodedByKay.SmartDialogue.Assistants, you can enhance your .NET web applications by integrating sophisticated chat functionalities powered by the OpenAI Assistant API. Follow this guide to set up and start leveraging this powerful library in your projects.