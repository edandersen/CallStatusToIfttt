using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Common
{
	internal enum GW : uint
	{
		HWNDFIRST        = 0,
		HWNDLAST         = 1,
		HWNDNEXT         = 2,
		HWNDPREV         = 3,
		OWNER            = 4,
		CHILD            = 5,
		MAX              = 6
	}

	internal class ICON
	{
		public const UInt32 SMALL          = 0;
		public const UInt32 BIG            = 1;
		public const UInt32 SMALL2         = 2; // XP+
	}

	internal enum MB : uint
	{
		SimpleBeep      = 0xFFFFFFFF,
		IconAsterisk    = 0x00000040,
		IconWarning     = 0x00000030,
		IconError       = 0x00000010,
		IconQuestion    = 0x00000020,
		OK              = 0x00000000
	}

	internal class SW
	{
		public const int HIDE               = 0;
		public const int SHOWNORMAL         = 1;
		public const int NORMAL             = 1;
		public const int SHOWMINIMIZED      = 2;
		public const int SHOWMAXIMIZED      = 3;
		public const int MAXIMIZE           = 3;
		public const int SHOWNOACTIVATE     = 4;
		public const int SHOW               = 5;
		public const int MINIMIZE           = 6;
		public const int SHOWMINNOACTIVE    = 7;
		public const int SHOWNA             = 8;
		public const int RESTORE            = 9;
		public const int SHOWDEFAULT        = 10;
		public const int FORCEMINIMIZE      = 11;
		public const int MAX                = 11;
	}

	internal class TB
	{
		public const uint GETBUTTON       = WM.USER + 23 ;
		public const uint BUTTONCOUNT     = WM.USER + 24 ;
		public const uint CUSTOMIZE       = WM.USER + 27 ;
		public const uint GETBUTTONTEXTA  = WM.USER + 45 ;
		public const uint GETBUTTONTEXTW  = WM.USER + 75 ;
	}

	internal class TBSTATE
	{
		public const uint CHECKED        =  0x01 ;
		public const uint PRESSED        =  0x02 ;
		public const uint ENABLED        =  0x04 ;
		public const uint HIDDEN         =  0x08 ;
		public const uint INDETERMINATE  =  0x10 ;
		public const uint WRAP           =  0x20 ;
		public const uint ELLIPSES       =  0x40 ;
		public const uint MARKED         =  0x80 ;
	}

	internal class WM
	{
		public const uint CLOSE   = 0x0010;
		public const uint GETICON = 0x007F;
		public const uint KEYDOWN = 0x0100;
		public const uint COMMAND = 0x0111;
		public const uint USER    = 0x0400; // 0x0400 - 0x7FFF
		public const uint APP     = 0x8000; // 0x8000 - 0xBFFF
	}

	internal class GCL
	{
		public const int MENUNAME       = - 8;
		public const int HBRBACKGROUND  = -10;
		public const int HCURSOR        = -12;
		public const int HICON          = -14;
		public const int HMODULE        = -16;
		public const int CBWNDEXTRA     = -18;
		public const int CBCLSEXTRA     = -20;
		public const int WNDPROC        = -24;
		public const int STYLE          = -26;
		public const int ATOM           = -32;
		public const int HICONSM        = -34;

		// GetClassLongPtr ( 64-bit )
		private const int GCW_ATOM           = -32;
		private const int GCL_CBCLSEXTRA     = -20;
		private const int GCL_CBWNDEXTRA     = -18;
		private const int GCLP_MENUNAME      = - 8;
		private const int GCLP_HBRBACKGROUND = -10;
		private const int GCLP_HCURSOR       = -12;
		private const int GCLP_HICON         = -14;
		private const int GCLP_HMODULE       = -16;
		private const int GCLP_WNDPROC       = -24;
		private const int GCLP_HICONSM       = -34;
		private const int GCL_STYLE          = -26;

	}

	[ StructLayout( LayoutKind.Sequential ) ]
	internal struct TBBUTTON 
	{
		public Int32 iBitmap;
		public Int32 idCommand;
		public byte fsState;
		public byte fsStyle;
//		[ MarshalAs( UnmanagedType.ByValArray, SizeConst=2 ) ]
//		public byte[] bReserved;
		public byte bReserved1;
		public byte bReserved2;
		public UInt32 dwData;
		public IntPtr iString;
	};

	internal class User32
	{
		private User32() {}

//		public const UInt32 WM_USER = 0x0400;

//		public const UInt32 WM_KEYDOWN = 0x0100;
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(
			IntPtr hWnd,
			UInt32 msg,
			IntPtr wParam,
			IntPtr lParam );

		[DllImport("user32.dll")]
		public static extern UInt32 SendMessage(
			IntPtr hWnd,
			UInt32 msg,
			UInt32 wParam,
			UInt32 lParam );

		[ DllImport( "User32.dll" ) ]
		public static extern bool PostMessage
		(
			IntPtr hWnd,
			UInt32 Msg,
			IntPtr wParam,
			IntPtr lParam
		);

		[ DllImport( "User32.dll" ) ]
		public static extern bool PostMessage
		(
			IntPtr hWnd,
			UInt32 Msg,
			UInt32 wParam,
			UInt32 lParam
		);

		[ DllImport( "User32.dll" ) ]
		public static extern bool MessageBeep
		(
			MB BeepType
		);

		[DllImport("user32.dll")]
		public static extern bool ShowWindow
		(
			IntPtr hWnd,
			int nCmdShow
		);

		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow
		(
			IntPtr hWnd
		);


		[ DllImport( "User32.dll" ) ]
		public static extern IntPtr GetDesktopWindow
		(
		);

		[ DllImport( "user32.dll", CharSet = CharSet.Unicode ) ]
		public static extern IntPtr FindWindowEx(
			IntPtr hwndParent,
			IntPtr hwndChildAfter,
			string lpszClass,
			string lpszWindow);
		
		[ DllImport( "User32.dll" ) ]
		public static extern IntPtr GetWindow
		(
			IntPtr hWnd,
			GW     uCmd
		);

		[ DllImport( "User32.dll" ) ]
		public static extern Int32 GetWindowTextLength
		(
			IntPtr hWnd
		);

		[ DllImport( "User32.dll", SetLastError = true, CharSet = CharSet.Auto ) ]
		public static extern Int32 GetWindowText
		(
			IntPtr hWnd,
			out StringBuilder lpString,
			Int32 nMaxCount
		);

		[ DllImport( "User32.dll", CharSet = CharSet.Auto ) ]
		public static extern Int32 GetClassName
		(
			IntPtr hWnd,
			out StringBuilder lpClassName,
			Int32 nMaxCount
		);

//		[ DllImport( "user32.dll", EntryPoint = "GetClassLongPtrW" ) ]
		[ DllImport( "user32.dll" ) ]
		public static extern UInt32 GetClassLong
		(
			IntPtr hWnd,
			int nIndex
		);

		[DllImport("user32.dll")]
		public static extern uint SetClassLong
		(
			IntPtr hWnd,
			int nIndex,
			uint dwNewLong
		);
		
		[ DllImport( "User32.dll", CharSet=CharSet.Auto ) ]
		public static extern UInt32 GetWindowThreadProcessId
		(
			IntPtr hWnd,
//			[ MarshalAs( UnmanagedType.
			out UInt32 lpdwProcessId
		);






	}
}
