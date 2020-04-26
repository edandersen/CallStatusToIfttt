using System;
using System.Windows.Forms;

namespace Common
{
	internal class CCursor : IDisposable
	{
		private Cursor saved = null;

		public CCursor( Cursor newCursor )
		{
			saved = Cursor.Current;

			Cursor.Current = newCursor;
		}

		/// <summary>Releases all resources.</summary>
		/// <remarks>Call this method when you are finished with the instance.</remarks>
		public virtual void Dispose()
		{
			Cursor.Current = saved;
		}
	}

	internal class CWaitCursor : CCursor
	{
		public CWaitCursor() : base( Cursors.WaitCursor ) {}
	}

	internal class StatusBarText : CWaitCursor, IDisposable
	{
		private StatusBar sb;

		public StatusBarText( StatusBar sb, string s )
		{
			this.sb = sb;
			sb.Text = s;
		}

		/// <summary>Releases all resources.</summary>
		/// <remarks>Call this method when you are finished with the instance.</remarks>
		public override void Dispose()
		{
			base.Dispose();

			sb.Text = "Ready";
		}
	}


}
