using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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

		public static void PopulateIgnoreSettings(string ignoreString, ILilyDb database)
		{
			//MessageBox.Show("Ignore: " + ignoreString);
			Match match = Regex.Match(ignoreString, @"([^{]*)( {([^}]*)})?$");

			//Get the user object we are ignoring
			IUser ignoredUser = database.GetByName(match.Result("$1").Trim()) as IUser;
			if(ignoredUser == null)
			{
				MessageBox.Show("Bad user name: |" + match.Result("$1").Trim() + "|");
				return;
			}

			// Clear current ignore settings
			ignoredUser.IgnoreSettings = new Ignore();

			// Get where we are ignoring that user, and populate that user's ignore settings
			Match whereIgnored = Regex.Match(match.Result("$3"), @"(privately)?( and )?(publicly)?(( except )|( and )|( ) )?(in (.*))?");
			if(whereIgnored.Success)
			{
				ignoredUser.IgnoreSettings.Private = whereIgnored.Result("$1") != String.Empty;
				ignoredUser.IgnoreSettings.Public  = whereIgnored.Result("$3") != String.Empty;
				
				if(whereIgnored.Result("$9") != String.Empty)
					foreach(string discName in whereIgnored.Result("$9").Split(",".ToCharArray()))
						ignoredUser.IgnoreSettings.Exceptions.Add(database.GetByName(discName.Trim()));
			}
		}
	}
}
