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
		
		private LilyItem source;

		private LilyItem[] recipients;

		private DateTime       time;

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
		public LilyItem Source
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
		public LilyItem[] Recipients
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



		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="eventString">The notify event from the server</param>
		/// <param name="handles">The Client's database</param>
		public NotifyEvent(string eventString, Hashtable handles)
		{
			source = ((LilyItem)handles[ LilyParent.Parse(eventString, "SOURCE" )] );
			command = LilyParent.Parse(eventString, "COMMAND");
			eventStr = LilyParent.Parse(eventString, "EVENT");
			valueStr = LilyParent.Parse(eventString, "VALUE");
			
			string recipList = LilyParent.Parse(eventString, "RECIPS");
			if(recipList != "")
			{
				string[] recipHandles = recipList.Split(new char[] {','});
				recipients = new LilyItem[ recipHandles.Length];

				for(int i = 0; i < recipients.Length; i++)
				{
					recipients[i] = ((LilyItem)handles[ recipHandles[i] ]);
				}
			}
			
			/*
			 * The server privides UNIX time, so we need to convert this into .NET time
			 *   We multiply by 10^7 because unix time is in seconds, and .NET time is in 100 nanoseconds
			 */
            time = new DateTime(1970, 1, 1);
			time += new TimeSpan( long.Parse(LilyParent.Parse(eventString, "TIME")) * 10000000 );
			time += TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);  // Convert from GMT to client time
		}
	}
}
