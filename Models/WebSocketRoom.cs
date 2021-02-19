using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace Models
{
    public class WebSocketRoom
    {
        public Dictionary<string,WebSocket> Connections;
        public Dictionary<string, byte[]> Positions;
        public List< byte[]> Board;
        public WebSocketRoom()
        {
            Connections = new Dictionary<string, WebSocket>();
            Positions = new Dictionary<string,byte[]>();
            Board = new List<byte[]>();
        }
    }
}
