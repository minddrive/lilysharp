using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lilySharp
{
	/// <summary>
	/// Summary description for Util.
	/// </summary>
	public class Util
	{
		public static ILilyDb Database;

		public static DateTime ConvertFromUnixTime(string unixTime)
		{
			DateTime time = new DateTime(1970, 1, 1);
			time += new TimeSpan( long.Parse(unixTime) * 10000000 );
			time += TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);  // Convert from GMT to client time
			return time;
		}


		public static Hashtable ParseIgnore(string ignoreStr)
		{
			Hashtable ignoreTable = new Hashtable();
			Match ignoreMatch = Regex.Match(ignoreStr, @"\(you are currently ignoring (.*) and being ignored by (.*)\)");
			
			
			if(!ignoreMatch.Success || ( ignoreMatch.Result("$1") == "no one" && ignoreMatch.Result("$2") == "no one"))
				return ignoreTable;


			foreach(string ignoreString in ignoreMatch.Result("$1").Split(",".ToCharArray()))
			{
				// Get the user we are ignoring
				Match match = Regex.Match(ignoreString, @"([^{]*)( {([^}]*)})?$");
				string user = match.Result("$1").Trim();
				Ignore settings = new Ignore();

				// Get the ignore settings
				Match whereIgnored = Regex.Match(match.Result("$3"), @"(privately)?( and )?(publicly)?(( except )|( and )|( ) )?(in (.*))?");
				if(whereIgnored.Success)
				{
					settings.Private = whereIgnored.Result("$1") != String.Empty;
					settings.Public  = whereIgnored.Result("$3") != String.Empty;
			
					if(whereIgnored.Result("$9") != String.Empty)
						foreach(string discName in whereIgnored.Result("$9").Split(",".ToCharArray()))
							settings.Exceptions.Add(Util.Database.GetByName(discName.Trim()));
				}

				// Add the user to the list
				ignoreTable[user] = settings;
			}

			return ignoreTable;
		}

		public static Ignore ParseIgnore(string ignoreStr,IUser ignoredUser)
		{
			Ignore iSettings = new Ignore(false, false);
			Match ignoreMatch = Regex.Match(ignoreStr, @"\(you are currently ignoring (.*) and being ignored by (.*)\)");

			// If noone is ignoring me, and I'm ignoring noone, don't need to do anything more
			if(!ignoreMatch.Success || ( ignoreMatch.Result("$1") == "no one" && ignoreMatch.Result("$2") == "no one"))
				return iSettings;
	
			// Retrieve the ignore settings for the passed-in user
			foreach(string ignoreString in ignoreMatch.Result("$1").Split(",".ToCharArray()))
			{
				// Get the user we are ignoring
				Match match = Regex.Match(ignoreString, @"([^{]*)( {([^}]*)})?$");
				IUser user = Util.Database.GetByName(match.Result("$1").Trim()) as IUser;
				
				// Make sure the user exists
				if(user == null)
				{
					// TODO: User is probably not connected (/bye)
					continue;
					//MessageBox.Show("Bad user name for ignore: " + match.Result("$1").Trim());
					//return iSettings;
				}

				// If it's the one we want, get the ignore settings and return
				if(user == ignoredUser)
				{
					Match whereIgnored = Regex.Match(match.Result("$3"), @"(privately)?( and )?(publicly)?(( except )|( and )|( ) )?(in (.*))?");
					if(whereIgnored.Success)
					{
						iSettings.Private = whereIgnored.Result("$1") != String.Empty;
						iSettings.Public  = whereIgnored.Result("$3") != String.Empty;
				
						if(whereIgnored.Result("$9") != String.Empty)
							foreach(string discName in whereIgnored.Result("$9").Split(",".ToCharArray()))
								iSettings.Exceptions.Add(Util.Database.GetByName(discName.Trim()));
					}

					return iSettings;
				}
			}

			// This returns if we are not ignoring this user
			return iSettings;
			
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
	
		public static Hashtable Parse(string str)
		{
			Hashtable table = new Hashtable();
			
			str += " ";  //Pad the end of the line for the last token
			int delimIndex;
			int nextDelimIndex;
			string token;

			/*
			 * Itterate through all the tokens, and populate the hash
			 */
			while(str.Length > 0)
			{
				delimIndex = str.IndexOfAny(new char[]{' ','='});
				token = str.Substring(0, delimIndex);

				//The token is valueless
				if(str[delimIndex] == ' ')
				{
					table[token] = string.Empty;
					str = str.Substring(delimIndex).TrimStart(new char[]{' '});
				}

					//There is a value
				else
				{
					nextDelimIndex = str.IndexOfAny(new char[]{' ','='}, delimIndex + 1);
					if(nextDelimIndex == -1)
						return table;

					//The value does not contain spaces
					if(str[nextDelimIndex] == ' ')
					{
						table[token] = str.Substring(delimIndex + 1, nextDelimIndex - delimIndex - 1);
						str = str.Substring(nextDelimIndex).TrimStart(new char[]{' '});
					}

						//The value may contain a space
					else
					{
						int valLength;

						// Get the lenght of the value
						try
						{
							valLength = int.Parse(str.Substring(delimIndex + 1, nextDelimIndex - delimIndex - 1));
						}
						catch(FormatException e)
						{
							MessageBox.Show(e + " is not a a valid length");
							str = str.Substring(str.IndexOf(' '));
							continue;
						}
					
						table[token] = str.Substring(nextDelimIndex + 1, valLength);
						str = str.Substring(nextDelimIndex + valLength  + 1).TrimEnd(new char[]{' '});
						
					} // end if(str[nextDelimIndex] == ' ')
				} // end if(str[DelimIndex] == ' ')
			} // end While
			return table;
		} // end Parse
	}
}
