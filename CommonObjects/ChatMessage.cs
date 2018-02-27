using System;
using System.Collections.Generic;
using System.Text;

namespace CommonObjects
{
	[Serializable]
    public class ChatMessage
    {
		public string Author { get; set; }

		public string Message { get; set; }

		public DateTime TimeStamp { get; set; }
    }
}
