using System;
using System.Diagnostics;

using Common;

namespace TaskbarSorter
{

//-----------------------------------------------------------------------------
// DataType

	public enum DataType
	{
		Process,
		Window,
	}

//-----------------------------------------------------------------------------
// DataBase

	internal abstract class DataBase
	{
		private TBBUTTON _TBButton;
		private string _ButtonText = String.Empty;
		private IntPtr _WindowHandle = IntPtr.Zero;
		private int _ImageIndex = -1;
		private INode _Node = null;

		public TBBUTTON TBButton { get { return _TBButton; } set { _TBButton = value; } }
		public string ButtonText { get { return _ButtonText; } set { _ButtonText = value; } }
		public IntPtr WindowHandle { get { return _WindowHandle; } set { _WindowHandle = value; } }
		public int ImageIndex { get { return _ImageIndex; } set { _ImageIndex = value; } }
		public INode Node { get { return _Node; } set { _Node = value; } }

		public DataBase( TBBUTTON tbButton, string buttonText, IntPtr windowHandle, int imageIndex )
		{
			_TBButton = tbButton;
			_ButtonText = buttonText;
			_WindowHandle = windowHandle;
			_ImageIndex = imageIndex;
		}

		public override string ToString()
		{
			return _ButtonText;
		}


		public abstract DataType DataType { get; }
	}

//-----------------------------------------------------------------------------
// DataProcess

	internal class DataProcess : DataBase
	{
		public DataProcess( TBBUTTON tbButton, string buttonText, IntPtr windowHandle )
			: base( tbButton, buttonText, windowHandle, 0 )
		{
			Debug.Assert( windowHandle == IntPtr.Zero );
		}

		public override DataType DataType { get { return DataType.Process; } }
	}

//-----------------------------------------------------------------------------
// DataWindow

	internal class DataWindow : DataBase
	{
		private UInt32 _ProcessId = 0;

		public UInt32 ProcessId { get { return _ProcessId; } set { _ProcessId = value; } }
		
		public DataWindow( TBBUTTON tbButton, string buttonText, IntPtr windowHandle )
			: base( tbButton, buttonText, windowHandle, 1 )
		{
			UInt32 threadId = User32.GetWindowThreadProcessId( windowHandle, out _ProcessId );

//			Process p = Process.GetProcessById( ( int ) processId );
//			string s2 = p.ProcessName;
//			int z = 0;
		}

		public override DataType DataType { get { return DataType.Window; } }
	}

//-----------------------------------------------------------------------------

}
