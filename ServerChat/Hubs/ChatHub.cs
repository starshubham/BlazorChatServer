
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

public class ChatHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();

    public override async Task OnConnectedAsync()
    {
        var username = Context.GetHttpContext()?.Request.Query["username"].ToString();

        if (!string.IsNullOrEmpty(username))
        {
            Users.TryAdd(username, Context.ConnectionId);
            Console.WriteLine($"User connected: {username} with Connection ID: {Context.ConnectionId}");
        }
        else
        {
            Console.WriteLine("No username provided.");
        }

        await base.OnConnectedAsync();
    }

    public async Task SendMessage(string sender, string receiver, string message)
    {
        Console.WriteLine($"Sending message from {sender} to {receiver}");
        if (Users.TryGetValue(receiver, out var receiverConnectionId))
        {
            await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", sender, message);
        }
        else
        {
            Console.WriteLine($"{receiver} is not connected.");
            await Clients.Caller.SendAsync("ReceiveMessage", "System", $"{receiver} is not connected.");
        }
    }
}
