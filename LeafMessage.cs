using System;

namespace lilySharp
{
	/// <summary>
	/// Interface for the Leaf-Cmd message system
	/// </summary>
	public interface ILeafCmd
	{
		/// <summary>
		/// Processes the responce to the messages
		/// </summary>
		/// <param name="msg"></param>
		void ProcessResponse(LeafMessage msg);
	}


	/// <summary>
	/// Holds the data for messages used in the ILeafCmd interface
	/// </summary>
	public class LeafMessage
	{
		private String command, response = "", tag = "";
		private ILeafCmd source;
		private int commandID;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="command">The command to send to the server</param>
		/// <param name="src">A reference to this object so I know who sent the message</param>
		public LeafMessage(String command, ILeafCmd src)
		{
			this.command = command;
			this.source  = src;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="command">The command to send to the server</param>
		/// <param name="tag">An additional lable to help classify the message</param>
		/// <param name="src">A reference to this object so I know who sent the message</param>
		public LeafMessage(string command, string tag, ILeafCmd src)
		{
			this.command = command;
			this.source  = src;
			this.tag     = tag;
		}

		/// <summary>
		/// Allows access to the command to be sent
		/// </summary>
		/// <value>Allows access to the command to be sent</value>
		public String Command
		{
			get{ return command;}
		}

		/// <summary>
		/// Allows access to the response to the command
		/// </summary>
		/// <value>Allows access to the response to the command</value>
		public String Response
		{
			get{ return response;}
			set{ response += value + "\n";}
		}

		/// <summary>
		/// Allows access to the tag string
		/// </summary>
		/// <value>Allows access to the tag string</value>
		public string Tag
		{
			get { return tag;}
			set { tag = value;}
		}

		/// <summary>
		/// Allows access to the ID of the command
		/// </summary>
		/// <value>Allows access to the ID of the command</value>
		public int ID
		{
			get{ return commandID;}
			set{ commandID = value;}
		}

		/// <summary>
		/// Indicates the message is complete, and calls the source to process the response
		/// </summary>
		public void End()
		{
			source.ProcessResponse(this);
		}
	}
}
