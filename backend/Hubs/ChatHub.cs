using backend.Models.Users;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    public async Task JoinChat(UserConnection conn)
    {
        //js deserializes this obj in invoke
        await Clients.All.
        SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined");
    }

    public async Task JoinSpecificChatRoom(UserConnection conn)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
        await Clients.Group(conn.ChatRoom)
        .SendAsync("JoinSpecificChatRoom", "admin", $"{conn.Username} has joined {conn.ChatRoom}");
    }
}