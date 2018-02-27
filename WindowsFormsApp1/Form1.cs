using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientLibrary;
using CommonObjects;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		ChatClient chat;

		public Form1()
		{
			InitializeComponent();
			chat = new ChatClient();
			chat.OnMessageReceived += Chat_OnMessageReceived;
		}

		private void Chat_OnMessageReceived(object sender, ChatMessageEventArgs e)
		{
			var s = $"{e.Message.TimeStamp}\t{e.Message.Author}: {e.Message.Message}";
			BeginInvoke(new Action(() => listBox1.Items.Add(s)));
		}

		private async void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter)
				return;
			string s = textBox1.Text;
			if (s == String.Empty)
				return;
			textBox1.Text = String.Empty;
			var m = new ChatMessage() { Author = edtUserName.Text, Message = s, TimeStamp = DateTime.Now };
			await chat.SendMessageAsync(m);
		}

		private async void Form1_Load(object sender, EventArgs e)
		{
			await chat.Start();
		}
	}
}
