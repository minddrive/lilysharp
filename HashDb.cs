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
		private IUser myObj;

		public HashDb()
		{
			hash = new Hashtable();
		}

		public void Clear()
		{
			hash.Clear();
		}

		public ILilyObject this [string id]
		{
			get	{ return hash[id] as ILilyObject;}
			set{ hash[id] = value;}
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

		public void Remove(string id)
		{
			hash.Remove(id);
		}

		public IUser Me
		{
			get { return myObj;}
			set { myObj = value;}
		}

		public ILilyObject GetByName(string name)
		{
			foreach(DictionaryEntry entry in hash)
			{
				if( ((ILilyObject)entry.Value).Name.ToUpper() == name.ToUpper())
					return entry.Value as ILilyObject;
			}
			return null;
		}

		public IEnumerator GetEnumerator()
		{
			return hash.GetEnumerator();
		}

	}
}
