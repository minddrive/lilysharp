using System;
using System.Collections;

namespace lilySharp
{
	/// <summary>
	/// Parses %notify events into easily accessable fields
	/// </summary>
	public class NotifyEvent
	{
		private String command,
				eventStr,
				valueStr;
		
		private ILilyObject source;

		private ILilyObject[] recipients;

		private DateTime time;

		private bool notify,
			         bell,
			         stamp,
			         empty;

		/// <summary>
		/// Allows access to the command ID of the event
		/// TODO: Make this an integer
		/// </summary>
		/// <value>Allows access to the command ID of the event</value>
		public String Command
		{
			get { return command; }
		}

		/// <summary>
		/// Allows access to the source of the event
		/// </summary>
		/// <value>Allows access to the source of the event</value>
		public ILilyObject Source
		{
			get { return source; }
		}

		/// <summary>
		/// Allows access to the type of the event
		/// </summary>
		/// <value>Allows access to the type of the event</value>
		public String Event
		{
			get { return eventStr;}
		}

		/// <summary>
		/// Allows access to the value of the event
		/// </summary>
		/// <value>Allows access to the value of the event</value>
		public String Value
		{
			get { return valueStr;}
		}

		/// <summary>
		/// Allows access to the recipient list
		/// </summary>
		/// <value>Allows access to the recipient list</value>
		public ILilyObject[] Recipients
		{
			get { return recipients; }
		}

		/// <summary>
		/// Allows access to the timestamp of the event
		/// </summary>
		/// <value>Allows access to the recipient list</value>
		public DateTime Time
		{
			get { return time; }
		}

		public bool Notify
		{
			get{ return notify;}
		}

		public bool Stamp
		{
			get{ return stamp;}
		}

		public bool Bell
		{
			get { return bell;}
		}

		public bool Empty
		{
			get{ return empty;}
		}

		public NotifyEvent(Hashtable attributes, ILilyDb database)
		{
			source = database[ attributes["SOURCE"] ];
			eventStr = attributes["EVENT"] as string;

			command = attributes["COMMAND"] == null ? "" : attributes["COMMAND"] as string;
			notify  = attributes["NOTIFY"]  == null ? false : true;
			bell    = attributes["BELL"]    == null ? false : true;
			stamp   = attributes["STAMP"]   == null ? false : true;
			empty   = attributes["EMPTY"]   == null ? false : true;
			
			valueStr = empty ? "" : attributes["VALUE"] as string;

			if(attributes["RECIPS"] != null)
			{
				string[] recipHandles = ((string)attributes["RECIPS"]).Split(new char[] {','});
				recipients = new ILilyObject[ recipHandles.Length];

				for(int i = 0; i < recipients.Length; i++)
				{
					recipients[i] = ((ILilyObject)database[ recipHandles[i] ]);
				}
			}
			
			time = Util.ConvertFromUnixTime(attributes["TIME"] as string);
		}
	}
}
