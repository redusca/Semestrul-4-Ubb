using System.Net.WebSockets;
using System.Text.Json;
using System.Text;

public class WebSocketHandler
{
    private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    private static readonly List<WebSocket> sockets = new List<WebSocket>();

    public static async Task HandleConnectionAsync(WebSocket webSocket)
    {
        sockets.Add(webSocket);

        var buffer = new byte[1024 * 4];
        var receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);

        try
        {
            while (!receiveResult.CloseStatus.HasValue)
            {
                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }
        catch (WebSocketException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            sockets.Remove(webSocket);
        }
    }

    public static async Task NotifyAll(string action, object data)
    {
        var notification = new
        {
            Action = action,
            Data = data,
            Timestamp = DateTime.UtcNow
        };

        var message = JsonSerializer.Serialize(notification, jsonOptions);
        var bytes = Encoding.UTF8.GetBytes(message);

        foreach (var socket in sockets)
        {
            try
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(
                        new ArraySegment<byte>(bytes),
                        WebSocketMessageType.Text,
                        true,
                        CancellationToken.None);
                }
                else
                {
                    sockets.Remove(socket);
                }
            }
            catch (Exception ex)
            {
                sockets.Remove(socket);
                Console.WriteLine(ex.Message);
            }
        }
    }
}