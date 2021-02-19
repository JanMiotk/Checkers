using Interfaces;
using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Application.Middlewares
{
    public class CommunicationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;
        private ITableService _tableService;
        private Dictionary<string,WebSocketRoom> _webSocketRooms;
        public CommunicationMiddleware(RequestDelegate next, IUserService userService, ITableService tableService, Dictionary<string, WebSocketRoom> webSocketRooms)
        {
            _next = next;
            _userService = userService;
            _tableService = tableService;
            _webSocketRooms = webSocketRooms;
        }
        public async Task Invoke(HttpContext context)
        {
            var buffer = new byte[1024 * 4];

            if (context.WebSockets.IsWebSocketRequest)
            {
                var roomName = context.Request.Query.FirstOrDefault(x => x.Key == "name");
                var room = _webSocketRooms.FirstOrDefault(x => x.Key == roomName.Value);
                var socketId = Guid.NewGuid().ToString();

                if (room.Key == null)
                {
                    _webSocketRooms.Add(roomName.Value,new WebSocketRoom() );
                }

                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                _webSocketRooms[roomName.Value].Connections.Add(socketId, webSocket);
                await Echo(context, webSocket, roomName.Value,socketId);
            }
            else
            {
                var key = context.Request.Query["connection"].ToString();
                var roomName = context.Request.Query["name"].ToString(); 
                if (key != "" && roomName != "")
                {
                    _webSocketRooms[roomName].Connections.Remove(key);

                    var isOccupied = _webSocketRooms[roomName].Connections.Count();
                    if (isOccupied == 0)
                    {
                        _webSocketRooms.Remove(roomName);
                    }
                    context.Response.Redirect("/Game");
                }
                await _next.Invoke(context);
            }
        }

        private async Task Echo(HttpContext context, WebSocket webSocket, string roomName, string sockedId)
        {
            bool visitor = true;
            var buffer = new byte[1024 * 4];
            List<byte[]> data = new List<byte[]>();
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                var value5 = Encoding.Default.GetString(buffer); 
                dynamic command = JObject.Parse(value5);

                var l = Buffer.ByteLength(buffer);
                var key = Convert.ToString(command["type"]);
                string key2 = Convert.ToString(command["position"]);

                if (key == "Position1" || key == "Position2")
                {
                    
                    if (!_webSocketRooms[roomName].Positions.ContainsKey(key))
                    {

                        _webSocketRooms[roomName].Positions.Add(key, buffer);
                    }
                    visitor = false;
                }

                if (key == "Board")
                {
                    _webSocketRooms[roomName].Board.Add(buffer);
                }

                if (key == "Leave")
                {
                    _webSocketRooms[roomName].Board.Clear();
                    _webSocketRooms[roomName].Positions.Remove(key2);
                }

                foreach (var socket in _webSocketRooms[roomName].Connections)
                {
                    var size = 0;

                    if(visitor == true)
                    {
                        if (_webSocketRooms[roomName].Positions.Count() > 0)
                        {
                            foreach (var message in _webSocketRooms[roomName].Positions)
                            {
                                size = ReturnSize(message.Value);
                                await socket.Value.SendAsync(new ArraySegment<byte>(message.Value, 0, size), result.MessageType, result.EndOfMessage, CancellationToken.None);
                            }
                        }

                        if (_webSocketRooms[roomName].Board.Count() > 0)
                        {
                            foreach (var message in _webSocketRooms[roomName].Board)
                            {
                                size = ReturnSize(message);
                                await socket.Value.SendAsync(new ArraySegment<byte>(message, 0, size), result.MessageType, result.EndOfMessage, CancellationToken.None);
                            }
                        }
                        
                    }

                    if (command["type"] == "Join")
                    {
                        command["connection"] = sockedId;
                        string newArray = Convert.ToString(command);
                        buffer = newArray.Select(x => (byte)x).ToArray();
                        size = buffer.Count();
                    }
                    else
                    {
                        size = result.Count;
                    }

                    await socket.Value.SendAsync(new ArraySegment<byte>(buffer, 0, size), result.MessageType, result.EndOfMessage, CancellationToken.None);
                }

                buffer = new byte[1024 * 4];
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        private int ReturnSize(byte[] buffer)
        {
            int index = 0;
            for (int i = 0; i < buffer.Count(); i++)
            {
                if (buffer[i] == 0)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
}
