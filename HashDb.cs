using System;
using System.Collections;

namespace lilySharp
{
	/// <summary>
	/// An implementation of the client Database as a hashtable
	/// </summary>
	public class HashDb : ILilyDb
	{
		private Hashtable hash;

		public HashDb()
		{
			hash = new Hashtable();
		}

		public void Clear()
		{
			hash.Clear();
		}

		public ILilyObject this [string tag]
		{
			get
			{
				// If the tag is an object ID, we can return the value easily
				if(tag[0] == '#')
				{
					try
					{
						int.Parse(tag.Substring(1));
						return hash[tag] as ILilyObject;
					}
					catch(FormatException)
					{}
				}

				// If the tag is not an object ID, it must me a name.  Find it.
				foreach(DictionaryEntry entry in hash)
				{
					if(entry.Value.ToString() == tag)
						return entry.Value as ILilyObject;
				}
				
				return null;
			}
			set{ hash[tag] = value;}
		}

		public ILilyObject this [ object obj ]
		{
			get
			{
				if(obj.GetType() == typeof(string))
					return this[obj as string];
				else
					throw new ArgumentException("Database Indexer must be a string");
			}
			
			set
			{
				if(obj.GetType() == typeof(string))
					this[obj as string] = value;
				else
					throw new ArgumentException("Database Indexer must be a string");
			}
		}

		public void Remove(string tag)
		{
			// If the tag is an object ID, we can return the value easily
			if(tag[0] == '#')
			{
				try
				{
					int.Parse(tag.Substring(1));
					hash.Remove(tag);
					return;
				}
				catch(FormatException)
				{}
			}

			// If the tag is not an object ID, it must me a name.  Find it.
			foreach(DictionaryEntry entry in hash)
			{
				if(entry.Value.ToString() == tag)
				{
					hash.Remove(entry.Key);
					return;
				}
			}
		}

		public IEnumerator GetEnumerator()
		{
			return hash.GetEnumerator();
		}
	}
}
