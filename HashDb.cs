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
			get	{ return hash[tag] as ILilyObject;}
			set{ hash[tag] = value;}
		}

		public ILilyObject this [ object obj ]
		{
			get
			{
				if(obj is string)
					return this[obj as string];
				else
					throw new ArgumentException("Database Indexer must be a string");
			}
			
			set
			{
				if(obj is string)
					this[obj as string] = value;
				else
					throw new ArgumentException("Database Indexer must be a string");
			}
		}

		public void Remove(string tag)
		{
			hash.Remove(tag);
		}

		public IEnumerator GetEnumerator()
		{
			return hash.GetEnumerator();
		}

	}
}
