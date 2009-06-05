using System;
using System.Collections.Generic;
using System.Net.Mail;
using Mono.Unix;
using Gtk;

using Tomboy;

namespace Tomboy.EmailNotes{
	public class EmailNotesNoteAddin : NoteAddin{
		
		Gtk.ImageMenuItem item;

		public override void Initialize ()
		{
			
		}

		public override void Shutdown ()
		{
			if (item != null)
				item.Activated -= OnImageMenuItemActivated;
		}

		public override void OnNoteOpened ()
		{
			item = new Gtk.ImageMenuItem (Catalog.GetString("Email"));
			item.Activated += OnImageMenuItemActivated;
			item.AddAccelerator ("activate", Window.AccelGroup,
				(uint) Gdk.Key.e, Gdk.ModifierType.ControlMask,
				Gtk.AccelFlags.Visible);
			item.Show ();
			AddPluginMenuItem (item);
		}
		
		void OnImageMenuItemActivated (object sender, EventArgs args)
		{
			Logger.Info ("Activated 'Email Note' menu item");
			Gtk.MessageDialog md = new Gtk.MessageDialog(null,
			                                             DialogFlags.DestroyWithParent,
			                                             MessageType.Question,
			                                             ButtonsType.OkCancel,
			                                             "Send email?");
			md.Title = "Confirm: Email Note?";
			
			ResponseType result = (ResponseType)md.Run();
			
			if( result == ResponseType.Ok)
			{
				string body = string.Empty;
				body = Note.TextContent.ToString();

				MailMessage mail = new MailMessage();
				//mail.IsBodyHtml = true;
				mail.To.Add(Preferences.Get("/apps/tomboy/email_notes/email_to").ToString());
				mail.From = new MailAddress(Preferences.Get("/apps/tomboy/email_notes/email_from").ToString());
				mail.Subject = Preferences.Get("/apps/tomboy/email_notes/email_subject_prefix") + Note.Title.ToString();
				mail.Body = body;
				
				
				SmtpClient smtp = new SmtpClient(Preferences.Get("/apps/tomboy/email_notes/smtp_server_address").ToString(), 
				                                 Convert.ToInt32(Preferences.Get("/apps/tomboy/email_notes/smtp_server_port")));
				smtp.Credentials = new System.Net.NetworkCredential(
					           Preferences.Get("/apps/tomboy/email_notes/smtp_auth_user").ToString(),
					           Preferences.Get("/apps/tomboy/email_notes/smtp_auth_pass").ToString());
				
				smtp.EnableSsl = true;
				smtp.Timeout = 5000;
				smtp.Send( mail );
				Logger.Info("Email sent from {0} to {1}", Preferences.Get("/apps/tomboy/email_notes/email_to").ToString(),
			            Preferences.Get("/apps/tomboy/email_notes/email_from").ToString());
		
			}
			md.Destroy();
		}
		
	}
}