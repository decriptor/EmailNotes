using System;

using Tomboy;

namespace Tomboy.EmailNotes
{
	
	public class EmailNotesPreferenceFactory : AddinPreferenceFactory
	{
		public override Gtk.Widget CreatePreferenceWidget ()
		{
			return new EmailNotesPreferences ();
		}
	}
}
