var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var hubUrl = "http://localhost:5066/hub";
var chatClient = new ChatClient(hubUrl);

await chatClient.StartAsync();

Console.WriteLine("Enter your name");
var userName = Console.ReadLine();
Console.WriteLine("Enter messages to send. Type 'exit' to quit.");
while (true)
{
    var message = Console.ReadLine();
    if (message == "exit")
        break;

    if (userName == null || message == null)
    {
        Console.WriteLine($"username: {userName} message: {message}");
        break;
    }
    await chatClient.SendMessageAsync(userName, message);
}


app.Run();
