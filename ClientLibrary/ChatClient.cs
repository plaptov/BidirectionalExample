﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CommonObjects;

namespace ClientLibrary
{
    public class ChatClient
    {
		readonly HubConnection connection;

		public ChatClient()
		{
			connection = new HubConnectionBuilder()
			//.AddJsonProtocol()
			.AddMessagePackProtocol()
			.ConfigureLogging(l => l.AddConsole())
			.WithUrl("http://localhost:45604/chat", Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets)
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
