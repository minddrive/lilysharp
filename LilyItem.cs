namespace lilySharp
{

	/// <summary>
	/// The base class for User/Discussion classes
	/// </summary>
	public abstract class LilyItem
	{
		protected LilyWindow window;
		protected string name, handle;

		/// <summary>
		/// Allows access to the User/Discussion name
		/// </summary>
		/// <value>Allows access to the User/Discussion name</value>
		public string Name
		{
			get	{ return name;}
			set { name = value;}
		}

		/// <summary>
		/// Allows access to the User/Discussion object ID
		/// </summary>
		/// <value>Allows access to the User/Discussion object ID</value>
		public string Handle
		{
			get { return handle; }
		}

		/// <summary>
		/// Allows access to the User/Discussion window
		/// </summary>
		/// <value>Allows access to the User/Discussion window</value>
        public LilyWindow Window
		{
			get { return window;}
			set { window = value;}
		}

		/// <summary>
		/// Returns the name of the User/Discussion
		/// </summary>
		/// <returns>The User/Discussion's name</returns>
		public override string ToString()
		{
			return name;
		}

		/// <summary>
		/// Compairs two User/Discussion's object IDs to determine if they are equal
		/// </summary>
		/// <param name="lhs">The left hand operand</param>
		/// <param name="rhs">The right hand operand</param>
		/// <returns>True if they are equal, false otherwise</returns>
		/// <remarks>
		/// Need to do some crazy tricks to compensate for null values =(
		/// </remarks>
		public static bool operator==(LilyItem lhs, LilyItem rhs)
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
		/// Compairs two User/Discussion's object IDs to determine if they are not equal
		/// </summary>
		/// <param name="lhs">Left hand operand</param>
		/// <param name="rhs">Right hand operand</param>
		/// <returns>True of the oIDs are not equal, false otherwise</returns>
		public static bool operator!=(LilyItem lhs, LilyItem rhs)
		{
			return !(lhs == rhs);
		}


		/// <summary>
		/// Compairs this with another object to see if they are equal
		/// </summary>
		/// <param name="o">Object to compair this to</param>
		/// <returns>True if the object is a LilyItem and it's oID matches this one, false otherwise</returns>
		public override bool Equals(object o)
		{
			if(o == null || o.GetType() != GetType())
				return false;

			return ((LilyItem)o).handle == this.handle;
		}
		
		/// <summary>
		/// Returns this User/Discussion's object ID in numeric form as this item's hashcode
		/// </summary>
		/// <returns>The numerical representation of this User/Discussion's oID</returns>
		public override int GetHashCode()
		{
			string str = handle.Substring(1,handle.Length - 1);
			return int.Parse(str);
		}

	}

	
	/// <summary>
	/// Holds the data for a discussion
	/// </summary>
	public class Discussion : LilyItem
	{
		private string title;
		private bool priv,
			connect,
			inv,
			moderated,
			info,
			memo;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="desc">The %disc line from the server</param>
		public Discussion(string desc)
		{
			this.handle = LilyParent.Parse(desc, "HANDLE");

			/*
			 * Parse the description
			 */
			// Name
			name = LilyParent.Parse(desc, "NAME");

			// TITLE
			title = LilyParent.Parse(desc, "TITLE");

			// Attributes
			int index = desc.ToUpper().IndexOf("ATTRIB");
			priv      = (desc.IndexOf("private", index)   == -1) ? false : true;
			connect   = (desc.IndexOf("connect", index)   == -1) ? false : true;
			inv       = (desc.IndexOf("inv", index)       == -1) ? false : true;
			moderated = (desc.IndexOf("moderated", index) == -1) ? false : true;
			info      = (desc.IndexOf("info", index)      == -1) ? false : true;
			memo      = (desc.IndexOf("info", index)      == -1) ? false : true;
		}
		#region Properties

		/// <summary>
		/// Allows access to this Discussion's private flag
		/// </summary>
		/// <value>Allows access to this Discussion's private flag</value>
		public bool Private
		{
			get { return priv; }
		}

		/// <summary>
		/// Allows access to this Discussion's connect flag
		/// </summary>
		/// <value>Allows access to this Discussion's connect flag</value>
		public bool Connect
		{
			get	{ return connect; }
		}

		/// <summary>
		/// Allows access to this Discussion's invulnerable flag
		/// </summary>
		/// <value>Allows access to this Discussion's invulnerable flag</value>
		public bool Invulnerable
		{
			get	{ return inv; }
		}

		/// <summary>
		/// Allows access to this Discussion's info flag
		/// </summary>
		/// <value>Allows access to this Discussion's info flag</value>
		public bool Info
		{
			get { return info; }
		}

		/// <summary>
		/// Allows access to this Discussion's memo flag
		/// </summary>
		/// <value>Allows access to this Discussion's memo flag</value>
		public bool Memo
		{
			get	{ return memo;}
		}

		public bool Moderated
		{
			get { return moderated;}
		}
		/// <summary>
		/// Allows access to this Discussion's title
		/// </summary>
		/// <value>Allows access to this Discussion's title</value>
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
	public class User : LilyItem
	{
		private string blurb,
			           pronoun;
		private bool finger,
					 info,
				     memo;

		private States state;

		public enum States {Here, Away, Detached, None}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="desc">The %user line from the server</param>
		public User(string desc)
		{
			this.handle  = LilyParent.Parse(desc, "HANDLE");
			name    = LilyParent.Parse(desc, "NAME");
			blurb   = LilyParent.Parse(desc, "BLURB");
			pronoun = LilyParent.Parse(desc, "PRONOUN");
			setState(desc);

			int index = desc.ToUpper().IndexOf("ATTRIB");
			if(index == -1) index = desc.Length;
			finger = (desc.IndexOf("finger", index) == -1) ? false : true;
			info   = (desc.IndexOf("info", index)   == -1) ? false : true;
			memo   = (desc.IndexOf("memo", index)   == -1) ? false : true;

		}

		/// <summary>
		/// Sets the initial state of the user
		/// </summary>
		/// <param name="desc">The %user line from the server</param>
		private void setState(string desc)
		{
			string attribs = LilyParent.Parse(desc, "STATE");
			if(attribs.IndexOf("here") != -1)
					state = States.Here;
			else if(attribs.IndexOf("away") != -1)
					state = States.Away;
			else if(attribs.IndexOf("detach") != -1)
				state = States.Detached;
			else
				state = States.None;

		}

		#region Properties

		/// <summary>
		/// Allows access to the User's blurb
		/// </summary>
		/// <value>Allows access to the User's blurb</value>
		public string Blurb
		{
			get { return blurb;}
			set { blurb = value;}
		}

		/// <summary>
		/// Allows access to the User's pronoun
		/// </summary>
		/// <value>Allows access to the User's pronoun</value>
		public string Pronoun
		{
			get { return pronoun; }
			set { pronoun = value;}
		}

		/// <summary>
		/// Allows access to the User's state
		/// </summary>
		/// <value>Allows access to the User's state</value>
		public States State
		{
			get { return state; }
			set	{ state = value;}
		}

		/// <summary>
		/// Allows access to the User's finger flag
		/// </summary>
		/// <value>Allows access to the User's finger flag</value>
		public bool Finger
		{
			get { return finger; }
			set { finger = value; }
		}

		/// <summary>
		/// Allows access to the User's info flag
		/// </summary>
		/// <value>Allows access to the User's info flag</value>
		public bool Info
		{
			get { return info;}
			set { info = value;}
		}

		/// <summary>
		/// Allows access to the User's memo flag
		/// </summary>
		/// <value>Allows access to the User's memo flag</value>
		public bool Memo
		{
			get { return memo;}
			set { memo = value;}
		}
		#endregion

	}
}