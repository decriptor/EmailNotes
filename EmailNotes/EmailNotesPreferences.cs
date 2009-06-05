using System;
using Mono.Unix;

using Tomboy;

namespace Tomboy.EmailNotes
{
	public class EmailNotesPreferences : Gtk.VBox
	{
		private const string emailFromUrlPrefPath =
			"/apps/tomboy/email_notes/email_from";
		private const string emailToUrlPrefPath =
			"/apps/tomboy/email_notes/email_to";
		private const string emailMessageFormatUrlPrefPath =
			"/apps/tomboy/email_notes/email_message_format";
		private const string emailSubjectPrefixUrlPrefPath =
			"/apps/tomboy/email_notes/email_subject_prefix";
		private const string smtpServerAddressUrlPrefPath =
			"/apps/tomboy/email_notes/smtp_server_address";
		private const string smtpServerPortUrlPrefPath =
			"/apps/tomboy/email_notes/smtp_server_port";
		private const string smtpUseSSLPrefPath =
			"/apps/tomboy/email_notes/ssl";
		private const string smtpAuthUserUrlPrefPath =
			"/apps/tomboy/email_notes/smtp_auth_user";
		private const string smtpAuthPassUrlPrefPath =
			"/apps/tomboy/email_notes/smtp_auth_pass";
		

		private Gtk.Entry emailFromEntry;
		private Gtk.Entry emailToEntry;
		private Gtk.Entry emailSubjectEntry;
		private Gtk.CheckButton email_format;
		private Gtk.Entry smtpServerEntry;
		private Gtk.Entry smtpPortEntry;
		private Gtk.CheckButton use_ssl;
		private Gtk.Entry smtpUserEntry;
		private Gtk.Entry smtpPassEntry;
		
		public EmailNotesPreferences()
		{
			
			Gtk.Label label = new Gtk.Label (Catalog.GetString(
			        "These are some basic mail settings"));
			label.Wrap = true;
			label.Xalign = 0;
			PackStart (label);

			//The default from address
			Gtk.HBox from = new Gtk.HBox (false, 12);
			PackStart (from);
			Gtk.Label from_label = new Gtk.Label (Catalog.GetString("From:"));
			from.PackStart (from_label);
			emailFromEntry = new Gtk.Entry ();
			from.PackStart (emailFromEntry);
			
			IPropertyEditor fromEntryEditor = Services.Factory.CreatePropertyEditorEntry (
			        emailFromUrlPrefPath , emailFromEntry);
			fromEntryEditor.Setup();
			
			//The default to address
			Gtk.HBox to = new Gtk.HBox (false, 12);
			PackStart (to);
			Gtk.Label to_label = new Gtk.Label (Catalog.GetString("To:"));
			to.PackStart (to_label);
			emailToEntry = new Gtk.Entry ();
			to.PackStart (emailToEntry);

			IPropertyEditor toEntryEditor = Services.Factory.CreatePropertyEditorEntry (
			        emailToUrlPrefPath , emailToEntry);
			toEntryEditor.Setup();
			
			//The default subject
			Gtk.HBox subject = new Gtk.HBox (false, 12);
			PackStart (subject);
			Gtk.Label subject_label = new Gtk.Label (Catalog.GetString("Subject:"));
			subject.PackStart (subject_label);
			emailSubjectEntry = new Gtk.Entry ();
			subject.PackStart (emailSubjectEntry);
			
			IPropertyEditor subjectEntryEditor = Services.Factory.CreatePropertyEditorEntry (
			        emailSubjectPrefixUrlPrefPath , emailSubjectEntry);
			subjectEntryEditor.Setup();
			
			//The smtp server
			Gtk.HBox smtp_server = new Gtk.HBox (false, 12);
			PackStart (smtp_server);
			Gtk.Label smtp_server_label = new Gtk.Label (Catalog.GetString("smtp server:"));
			smtp_server.PackStart (smtp_server_label);
			smtpServerEntry = new Gtk.Entry ();
			smtp_server.PackStart (smtpServerEntry);

			IPropertyEditor smtpServerEntryEditor = Services.Factory.CreatePropertyEditorEntry (
			        smtpServerAddressUrlPrefPath, smtpServerEntry);
			smtpServerEntryEditor.Setup();
			
			//The smtp port
			Gtk.HBox smtp_port = new Gtk.HBox (false, 12);
			PackStart (smtp_port);
			Gtk.Label smtp_port_label = new Gtk.Label (Catalog.GetString("smtp port:"));
			smtp_port.PackStart (smtp_port_label);
			smtpPortEntry = new Gtk.Entry ();
			smtp_port.PackStart (smtpPortEntry);

			IPropertyEditor smtpPortEntryEditor = Services.Factory.CreatePropertyEditorEntry (
			        smtpServerPortUrlPrefPath, smtpPortEntry);
			smtpPortEntryEditor.Setup();
			
			use_ssl = new Gtk.CheckButton (Catalog.GetString("Use SSL?"));
			PackStart (use_ssl);
			// Activate/deactivate widgets
			if(Preferences.Get(smtpUseSSLPrefPath) == true)
				email_format.Active =  true;
			
			// Register checked state for email format
			use_ssl.Toggled += OnUseSSLToggled;
			
			//The smtp user
			Gtk.HBox smtp_user = new Gtk.HBox (false, 12);
			PackStart (smtp_user);
			Gtk.Label smtp_user_label = new Gtk.Label (Catalog.GetString("smtp user:"));
			smtp_user.PackStart (smtp_user_label);
			smtpUserEntry = new Gtk.Entry ();
			smtp_user.PackStart (smtpUserEntry);

			IPropertyEditor smtpUserEntryEditor = Services.Factory.CreatePropertyEditorEntry (
			        smtpAuthUserUrlPrefPath, smtpUserEntry);
			smtpUserEntryEditor.Setup();

			//The smtp pass
			Gtk.HBox smtp_pass = new Gtk.HBox (false, 12);
			PackStart (smtp_pass);
			Gtk.Label smtp_pass_label = new Gtk.Label (Catalog.GetString("smtp password:"));
			smtp_pass.PackStart (smtp_pass_label);
			smtpPassEntry = new Gtk.Entry ();
			smtpPassEntry.Visibility = false;
			smtp_pass.PackStart (smtpPassEntry);

			IPropertyEditor smtpPassEntryEditor = Services.Factory.CreatePropertyEditorEntry (
			        smtpAuthPassUrlPrefPath, smtpPassEntry);
			smtpPassEntryEditor.Setup();

			
			email_format = new Gtk.CheckButton (Catalog.GetString("Use html format"));
			PackStart (email_format);
			// Activate/deactivate widgets
			//if(Preferences.Get("/apps/tomboy/email_notes/email_format") as bool)
				email_format.Active =  true;
			
			// Register checked state for email format
			email_format.Toggled += OnEmailFormatToggled;

			ShowAll ();
		}
		
		/// <summary>
		/// Called when toggling the checked state
		/// for email format
		/// </summary>
		void OnEmailFormatToggled (object sender, EventArgs args)
		{
			if (email_format.Active) {
			}
			
		}
		
		/// <summary>
		/// Called when toggling the checked state
		/// for use ssl
		/// </summary>
		void OnUseSSLToggled (object sender, EventArgs args)
		{
			if (use_ssl.Active) {
				Preferences.Set(smtpUseSSLPrefPath, true);
			}
			else
			{
				Preferences.Set(smtpUseSSLPrefPath, false);
			}
			
		}
	}
}
