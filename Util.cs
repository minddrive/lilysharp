using System;

namespace lilySharp
{
	/// <summary>
	/// Summary description for Util.
	/// </summary>
	public class Util
	{
		public static DateTime ConvertFromUnixTime(string unixTime)
		{
			DateTime time = new DateTime(1970, 1, 1);
			time += new TimeSpan( long.Parse(unixTime) * 10000000 );
			time += TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);  // Convert from GMT to client time
			return time;
		}
	}
}
