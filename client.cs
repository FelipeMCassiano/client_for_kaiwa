using Microsoft.AspNetCore.SignalR.Client;

class ChatClient
{
    private HubConnection _connection;

    public ChatClient(string hubUrl)
    {
        // Initialize the SignalR connection
        _connection = new HubConnectionBuilder()
            .WithUrl(hubUrl) // The URL of your SignalR hub
            .WithAutomaticReconnect() // Automatically reconnect on disconnection
            .Build();

        // Set up a handler for receiving messages
        _connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Console.WriteLine($"[{user}]: {message}");
        });
    }

    public async Task StartAsync()
    {
        try
        {
            await _connection.StartAsync();
            Console.WriteLine("Connected to SignalR server.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while connecting: {ex.Message}");
        }
    }

    public async Task StopAsync()
    {
        await _connection.StopAsync();
        Console.WriteLine("Disconnected from SignalR server.");
    }

    public async Task SendMessageAsync(string user, string message)
    {
        try
        {
            await _connection.InvokeAsync("SendMessage", user, message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while sending message: {ex.Message}");
        }
    }
}
