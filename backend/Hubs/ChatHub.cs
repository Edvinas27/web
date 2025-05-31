using backend.Models.Users;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;
    public ChatHub(ILogger<ChatHub> logger)
    {
        _logger = logger;
    }
    public async Task JoinChat(UserConnection conn)
    {
        await Clients.All.
        SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined");
    }

    public async Task JoinSpecificChatRoom(UserConnection conn)
    {
        _logger.LogInformation($"User {conn.Username} joined the chat room {conn.ChatRoom}");
        await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
        await Clients.Group(conn.ChatRoom)
        .SendAsync("JoinSpecificChatRoom", "admin", $"{conn.Username} has joined {conn.ChatRoom}");
    }
}