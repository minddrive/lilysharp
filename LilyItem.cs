using System;
using System.Collections;
using System.Windows.Forms;

namespace lilySharp
{

	/// <summary>
	/// The base class for IUser/IDiscussion classes
	/// </summary>
	public abstract class LilyObject : IComparable, ILilyObject
	{
		protected LilyWindow window;
		protected string name, handle;

		/// <summary>
		/// Allows access to the IUser/IDiscussion name
		/// </summary>
		/// <value>Allows access to the IUser/IDiscussion name</value>
		public string Name
		{
			get	{ return name;}
			set { name = value;}
		}

		/// <summary>
		/// Allows access to the IUser/IDiscussion object ID
		/// </summary>
		/// <value>Allows access to the IUser/IDiscussion object ID</value>
		public string Handle
		{
			get { return handle; }
		}

		/// <summary>
		/// Allows access to the IUser/IDiscussion window
		/// </summary>
		/// <value>Allows access to the IUser/IDiscussion window</value>
        public LilyWindow Window
		{
			get { return window;}
			set { window = value;}
		}

		/// <summary>
		/// Returns the name of the IUser/IDiscussion
		/// </summary>
		/// <returns>The IUser/IDiscussion's name</returns>
		public override string ToString()
		{
			return name;
		}

		/// <summary>
		/// Compairs two IUser/IDiscussion's object IDs to determine if they are equal
		/// </summary>
		/// <param name="lhs">The left hand operand</param>
		/// <param name="rhs">The right hand operand</param>
		/// <returns>True if they are equal, false otherwise</returns>
		/// <remarks>
		/// Need to do some crazy tricks to compensate for null values =(
		/// </remarks>
		public static bool operator==(LilyObject lhs, LilyObject rhs)
		{
			try
			{
				return lhs.Equals(rhs);
			}
			catch(System.NullReferenceException)      // happens if lhs is null
			{
				try
				{
					return rhs.Equals(lhs);
				}
				catch(System.NullReferenceException)  // happens if both references are null
				{
					return true;
				}
			}
		}

		/// <summary>
		/// Compairs two IUser/IDiscussion's object IDs to determine if they are not equal
		/// </summary>
		/// <param name="lhs">Left hand operand</param>
		/// <param name="rhs">Right hand operand</param>
		/// <returns>True of the oIDs are not equal, false otherwise</returns>
		public static bool operator!=(LilyObject lhs, LilyObject rhs)
		{
			return !(lhs == rhs);
		}


		/// <summary>
		/// Compairs this with another object to see if they are equal
		/// </summary>
		/// <param name="o">Object to compair this to</param>
		/// <returns>True if the object is a ILilyObject and it's oID matches this one, false otherwise</returns>
		public override bool Equals(object o)
		{
			if(o == null || o.GetType() != GetType())
				return false;

			return ((ILilyObject)o).Handle == this.handle;
		}
		
		/// <summary>
		/// Returns this IUser/IDiscussion's object ID in numeric form as this item's hashcode
		/// </summary>
		/// <returns>The numerical representation of this IUser/IDiscussion's oID</returns>
		public override int GetHashCode()
		{
			string str = handle.Substring(1,handle.Length - 1);
			return int.Parse(str);
		}

		public int CompareTo(object obj)
		{
			if(obj == null) return -1;

			if(!(obj.GetType() == this.GetType()))
				throw new ArgumentException("Agument is not of type ILilyObject");

			return string.Compare(this.ToString(), obj.ToString());
		}
	}

	
	/// <summary>
	/// Holds the data for a discussion
	/// </summary>
	public class Discussion : LilyObject, IDiscussion
	{
		private string title, game;
		private bool priv,
			connect,
			inv,
			moderated,
			info,
			memo;
		private DateTime created,
			             lastInput;

		public Discussion(Hashtable attributes)
		{
			handle = attributes["HANDLE"] as string;
			name = attributes["NAME"] as string;
			title = attributes["TITLE"] as string;
            if(attributes["GAME"] != null) game = attributes["GAME"] as string;		
            created = Util.ConvertFromUnixTime(attributes["CREATION"] as string);
			lastInput = Util.ConvertFromUnixTime(attributes["INPUT"] as string);

			priv      = ((string)attributes["ATTRIB"]).IndexOf("private") == -1 ? false : true;
			connect   = ((string)attributes["ATTRIB"]).IndexOf("connect") == -1 ? false : true;
			inv       = ((string)attributes["ATTRIB"]).IndexOf("inv") == -1 ? false : true;
			moderated = ((string)attributes["ATTRIB"]).IndexOf("moderated") == -1 ? false : true;
			info      = ((string)attributes["ATTRIB"]).IndexOf("info") == -1 ? false : true;
			memo      = ((string)attributes["ATTRIB"]).IndexOf("memo") == -1 ? false : true;
		}

		#region Properties

		/// <summary>
		/// Allows access to this IDiscussion's private flag
		/// </summary>
		/// <value>Allows access to this IDiscussion's private flag</value>
		public bool Private
		{
			get { return priv; }
			set { priv = value;}
		}

		/// <summary>
		/// Allows access to this IDiscussion's connect flag
		/// </summary>
		/// <value>Allows access to this IDiscussion's connect flag</value>
		public bool Connect
		{
			get	{ return connect; }
		}

		/// <summary>
		/// Allows access to this IDiscussion's invulnerable flag
		/// </summary>
		/// <value>Allows access to this IDiscussion's invulnerable flag</value>
		public bool Invulnerable
		{
			get	{ return inv; }
			set { inv = value;}
		}

		/// <summary>
		/// Allows access to this IDiscussion's info flag
		/// </summary>
		/// <value>Allows access to this IDiscussion's info flag</value>
		public bool Info
		{
			get { return info; }
			set { info = value;}
		}

		/// <summary>
		/// Allows access to this IDiscussion's memo flag
		/// </summary>
		/// <value>Allows access to this IDiscussion's memo flag</value>
		public bool Memo
		{
			get	{ return memo;}
			set { memo = value;}
		}

		public bool Moderated
		{
			get { return moderated;}
			set { moderated = value;}
		}
		/// <summary>
		/// Allows access to this IDiscussion's title
		/// </summary>
		/// <value>Allows access to this IDiscussion's title</value>
		public string Title
		{
			get { return title; }
			set { title = value;}
		}
		#endregion
	
	}


	/// <summary>
	/// Holds the data for a user
	/// </summary>
	public class User : LilyObject, IUser
	{
		private string blurb,
			           pronoun;

		private bool finger,
					 info,
				     memo;

		private DateTime login,
			             lastInput;
		private States state;

		private Ignore ignore;


		public User(Hashtable attributes)
		{
			handle    = attributes["HANDLE"] as string;
			name      = attributes["NAME"] as string;
			blurb     = attributes["BLURB"]   == null ? "" : attributes["BLURB"] as string;
			pronoun   = attributes["PRONOUN"] == null ? "" : attributes["PRONOUN"] as string;
			login     = attributes["LOGIN"]   == null ? DateTime.Now : Util.ConvertFromUnixTime(attributes["LOGIN"] as string);
			lastInput = attributes["INPUT"]   == null ? DateTime.Now : Util.ConvertFromUnixTime(attributes["INPUT"] as string);

			ignore = new Ignore(false,false);

			// Determine state
			switch(attributes["STATE"] as string)
			{
				case "here":
					state = States.Here;
					break;
				case "away":
					state = States.Away;
					break;
				case "detach":
					state = States.Detached;
					break;
				default:
					state = States.Disconnected;
					break;
			}
			
			// Determine attributes
			if(attributes["ATTRIB"] != null)
			{
				foreach(string attrib in ((string)attributes["ATTRIB"]).Split(new char[]{','}))
				{
					switch(attrib)
					{
						case "finger":
							finger = true;
							break;
						case "info":
							info = true;
							break;
						case "memo":
							memo = true;
							break;
					} // end case
				} // end foreach
			} // end if

		}

		#region Properties

		/// <summary>
		/// Allows access to the IUser's blurb
		/// </summary>
		/// <value>Allows access to the IUser's blurb</value>
		public string Blurb
		{
			get { return blurb;}
			set { blurb = value;}
		}

		/// <summary>
		/// Allows access to the IUser's pronoun
		/// </summary>
		/// <value>Allows access to the IUser's pronoun</value>
		public string Pronoun
		{
			get { return pronoun; }
			set { pronoun = value;}
		}

		/// <summary>
		/// Allows access to the IUser's state
		/// </summary>
		/// <value>Allows access to the IUser's state</value>
		public States State
		{
			get { return state; }
			set	{ state = value;}
		}

		/// <summary>
		/// Allows access to the IUser's finger flag
		/// </summary>
		/// <value>Allows access to the IUser's finger flag</value>
		public bool Finger
		{
			get { return finger; }
			set { finger = value; }
		}

		/// <summary>
		/// Allows access to the IUser's info flag
		/// </summary>
		/// <value>Allows access to the IUser's info flag</value>
		public bool Info
		{
			get { return info;}
			set { info = value;}
		}

		/// <summary>
		/// Allows access to the IUser's memo flag
		/// </summary>
		/// <value>Allows access to the IUser's memo flag</value>
		public bool Memo
		{
			get { return memo;}
			set { memo = value;}
		}

		public Ignore IgnoreSettings
		{
			get { return ignore;}
			set { ignore = value;}
		}
		#endregion


	}
}