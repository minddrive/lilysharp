using System;
using System.Collections;

namespace lilySharp
{
	/// <summary>
	/// Summary description for LilyDb.
	/// </summary>
	public interface ILilyDb : IEnumerable
	{
        
		ILilyObject this [ string oID ]
		{
			get;
			set;
		}

		ILilyObject this [object obj]
		{
			get;
			set;
		}

		void Clear();
		void Remove(string tag);
		ILilyObject GetByName(string oID);

	}

	public interface ILilyObject
	{
		string Name
		{
			get;
			set;
		}

		string Handle
		{
			get;
		}

		LilyWindow Window
		{
			get;
			set;
		}

	}

	public enum States {Here, Away, Detached, Disconnected};
	public interface IUser : ILilyObject
	{

		string Blurb
		{
			get;
			set;
		}

		string Pronoun
		{
			get;
			set;
		}

		States State
		{
			get;
			set;
		}

		bool Finger
		{
			get;
			set;
		}

		bool Info
		{
			get;
			set;
		}

		bool Memo
		{
			get;
			set;
		}
		
		Ignore IgnoreSettings
		{
			get;
			set;
		}
	}

	public interface IDiscussion : ILilyObject
	{
		bool Private
		{
			get;
			set;
		}

		bool Invulnerable
		{
			get;
			set;
		}

		bool Connect
		{
			get;
		}

		bool Moderated
		{
			get;
			set;
		}

		bool Info
		{
			get;
			set;
		}

		bool Memo
		{
			get;
			set;
		}

		string Title
		{
			get;
			set;
		}

	}

	public class Ignore
	{
		public bool Public,
			        Private;

		public ArrayList Exceptions;

		public Ignore(bool pub, bool priv)
		{
			Public = pub;
			Private = priv;
			Exceptions = new ArrayList();
		}
	}
}
