using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp1
{
    class Program
    {
		// All interop magic from https://www.codeproject.com/articles/10497/a-tool-to-order-the-window-buttons-in-your-taskbar
		static void Main(string[] args)
        {
            Console.WriteLine("Reading processes");

			var toolBarWindowHandle = GetToolbarWindowHandle();
			if (toolBarWindowHandle == IntPtr.Zero) return;

			UInt32 count = User32.SendMessage(toolBarWindowHandle, TB.BUTTONCOUNT, 0, 0);

			var microphoneInUse = false;

			for (int i = 0; i < count; i++)
			{
				TBBUTTON tbButton = new TBBUTTON();
				string text = String.Empty;
				IntPtr ipWindowHandle = IntPtr.Zero;

				bool b = GetTBButton(toolBarWindowHandle, i, ref tbButton, ref text, ref ipWindowHandle);

				if (b)
				{
					if (text.Contains("is using your microphone"))
					{
						microphoneInUse = true;
						break;
					}
				}
			}

			Console.WriteLine(microphoneInUse ? "Microphone is in use!" : "Microphone not in use.");

			Console.ReadKey();
        }

		private static IntPtr GetToolbarWindowHandle()
		{
			IntPtr hDesktop = User32.GetDesktopWindow();

			IntPtr hTray = User32.FindWindowEx(hDesktop, IntPtr.Zero, "Shell_TrayWnd", null);

			IntPtr hReBar = User32.FindWindowEx(hTray, IntPtr.Zero, "TrayNotifyWnd", null);

			IntPtr hTask = User32.FindWindowEx(hReBar, IntPtr.Zero, "SysPager", null);

			IntPtr hToolbar = User32.FindWindowEx(hTask, IntPtr.Zero, "ToolbarWindow32", null);

			try
			{
				IntPtr htoolbar2 = User32.FindWindowEx(hToolbar, IntPtr.Zero, "NotifyIconOverflowWindow", null);
				IntPtr hToolbar3 = User32.FindWindowEx(htoolbar2, IntPtr.Zero, "ToolbarWindow32", null);

				if (hToolbar3 != IntPtr.Zero) hToolbar = hToolbar3;
				
			}
			catch
			{
				// fail silently, as NotifyIconOverflowWindow did not exist prior to Win7
				
			}
			if (hToolbar == IntPtr.Zero)
				MessageBox.Show(
					"Couldn't find Taskbar",
					"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);

			return hToolbar;
		}


		private static unsafe bool GetTBButton(IntPtr hToolbar, int i, ref TBBUTTON tbButton, ref string text, ref IntPtr ipWindowHandle)
		{
			// One page
			const int BUFFER_SIZE = 0x1000;

			byte[] localBuffer = new byte[BUFFER_SIZE];

			UInt32 processId = 0;
			UInt32 threadId = User32.GetWindowThreadProcessId(hToolbar, out processId);

			IntPtr hProcess = Kernel32.OpenProcess(ProcessRights.ALL_ACCESS, false, processId);
			if (hProcess == IntPtr.Zero) { Debug.Assert(false); return false; }

			IntPtr ipRemoteBuffer = Kernel32.VirtualAllocEx(
				hProcess,
				IntPtr.Zero,
				new UIntPtr(BUFFER_SIZE),
				MemAllocationType.COMMIT,
				MemoryProtection.PAGE_READWRITE);

			if (ipRemoteBuffer == IntPtr.Zero) { Debug.Assert(false); return false; }

			// TBButton
			fixed (TBBUTTON* pTBButton = &tbButton)
			{
				IntPtr ipTBButton = new IntPtr(pTBButton);

				int b = (int)User32.SendMessage(hToolbar, TB.GETBUTTON, (IntPtr)i, ipRemoteBuffer);
				if (b == 0) { Debug.Assert(false); return false; }

				// this is fixed
				Int32 dwBytesRead = 0;
				IntPtr ipBytesRead = new IntPtr(&dwBytesRead);

				bool b2 = Kernel32.ReadProcessMemory(
					hProcess,
					ipRemoteBuffer,
					ipTBButton,
					new UIntPtr((uint)sizeof(TBBUTTON)),
					ipBytesRead);

				if (!b2) { Debug.Assert(false); return false; }
			}

			// button text
			fixed (byte* pLocalBuffer = localBuffer)
			{
				IntPtr ipLocalBuffer = new IntPtr(pLocalBuffer);

				int chars = (int)User32.SendMessage(hToolbar, TB.GETBUTTONTEXTW, (IntPtr)tbButton.idCommand, ipRemoteBuffer);
				if (chars == -1) { Debug.Assert(false); return false; }

				// this is fixed
				Int32 dwBytesRead = 0;
				IntPtr ipBytesRead = new IntPtr(&dwBytesRead);

				bool b4 = Kernel32.ReadProcessMemory(
					hProcess,
					ipRemoteBuffer,
					ipLocalBuffer,
					new UIntPtr(BUFFER_SIZE),
					ipBytesRead);

				if (!b4) { Debug.Assert(false); return false; }

				text = Marshal.PtrToStringUni(ipLocalBuffer, chars);

				if (text == " ") text = String.Empty;
			}

			Kernel32.VirtualFreeEx(
				hProcess,
				ipRemoteBuffer,
				UIntPtr.Zero,
				MemAllocationType.RELEASE);

			Kernel32.CloseHandle(hProcess);

			return true;
		}

	}
}
