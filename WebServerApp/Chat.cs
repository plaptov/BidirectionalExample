using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using CommonObjects;

namespace WebServerApp
{
	public class Chat : Hub
	{
		public Task Send(ChatMessage message)
		{
			return Clients.All.SendAsync("send", message);
		}
	}
}
