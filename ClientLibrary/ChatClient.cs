using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using CommonObjects;

namespace ClientLibrary
{
    public class ChatClient
    {
		readonly HubConnection connection;

		public ChatClient()
		{
			connection = new HubConnectionBuilder()
			.WithJsonProtocol()
			.WithConsoleLogger()
			.WithTransport(Microsoft.AspNetCore.Sockets.TransportType.WebSockets)
			.WithUrl("http://localhost:45604/chat")
			.Build();
			connection.On<ChatMessage>("send", msg => OnMessageReceived?.Invoke(this, new ChatMessageEventArgs() { Message = msg }));
		}

		public async Task Start()
		{
			await connection.StartAsync();
		}

		public event EventHandler<ChatMessageEventArgs> OnMessageReceived;

		public async Task SendMessageAsync(ChatMessage msg)
		{
			await connection.SendAsync("send", msg);
		}
    }

	public class ChatMessageEventArgs : EventArgs
	{
		public ChatMessage Message { get; set; }
	}
}
