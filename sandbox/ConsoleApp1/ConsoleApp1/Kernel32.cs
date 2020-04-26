using System;
using System.Runtime.InteropServices;

namespace Common
{

//-----------------------------------------------------------------------------
// Structures

	[StructLayout(LayoutKind.Sequential)]
	internal struct SYSTEM_INFO 
	{
		public _PROCESSOR_INFO_UNION uProcessorInfo;
		public uint dwPageSize;
		public uint lpMinimumApplicationAddress;
		public uint lpMaximumApplicationAddress;
		public uint dwActiveProcessorMask;
		public uint dwNumberOfProcessors;
		public uint dwProcessorType;
		public uint dwAllocationGranularity;
		public uint dwProcessorLevel;
		public uint dwProcessorRevision;
	}

	[StructLayout(LayoutKind.Explicit)]
	internal struct _PROCESSOR_INFO_UNION
	{
		[FieldOffset(0)]
		public uint dwOemId;
		[FieldOffset(0)]
		public ushort wProcessorArchitecture;
		[FieldOffset(2)]
		public ushort wReserved;
	}

	[ StructLayout( LayoutKind.Sequential )]
	internal struct BY_HANDLE_FILE_INFORMATION
	{
		public UInt32 dwFileAttributes;
		public FILETIME ftCreationTime;
		public FILETIME ftLastAccessTime;
		public FILETIME ftLastWriteTime;
		public UInt32 dwVolumeSerialNumber;
		public UInt32 nFileSizeHigh;
		public UInt32 nFileSizeLow;
		public UInt32 nNumberOfLinks;
		public UInt32 nFileIndexHigh;
		public UInt32 nFileIndexLow;
	}

	[ StructLayout( LayoutKind.Sequential )]
	internal class MEMORYSTATUSEX
	{
		public Int32 Length;
		public Int32 MemoryLoad;
		public UInt64 TotalPhysical;
		public UInt64 AvailablePhysical;
		public UInt64 TotalPageFile;
		public UInt64 AvailablePageFile;
		public UInt64 TotalVirtual;
		public UInt64 AvailableVirtual;
		public UInt64 AvailableExtendedVirtual;

		public MEMORYSTATUSEX() { Length = Marshal.SizeOf( this ); }

		private void StopTheCompilerComplaining()
		{
			Length = 0;
			MemoryLoad = 0;
			TotalPhysical = 0;
			AvailablePhysical = 0;
			TotalPageFile = 0;
			AvailablePageFile = 0;
			TotalVirtual = 0;
			AvailableVirtual = 0;
			AvailableExtendedVirtual = 0;
		}
	}

//-----------------------------------------------------------------------------
// Constants

	internal class ProcessRights
	{
		public const UInt32 TERMINATE         = 0x0001  ;
		public const UInt32 CREATE_THREAD     = 0x0002  ;
		public const UInt32 SET_SESSIONID     = 0x0004  ;
		public const UInt32 VM_OPERATION      = 0x0008  ;
		public const UInt32 VM_READ           = 0x0010  ;
		public const UInt32 VM_WRITE          = 0x0020  ;
		public const UInt32 DUP_HANDLE        = 0x0040  ;
		public const UInt32 CREATE_PROCESS    = 0x0080  ;
		public const UInt32 SET_QUOTA         = 0x0100  ;
		public const UInt32 SET_INFORMATION   = 0x0200  ;
		public const UInt32 QUERY_INFORMATION = 0x0400  ;
		public const UInt32 SUSPEND_RESUME    = 0x0800  ;

		private const UInt32 STANDARD_RIGHTS_REQUIRED = 0x000F0000;
		private const UInt32 SYNCHRONIZE              = 0x00100000;

		public const UInt32 ALL_ACCESS        = STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0xFFF;
	}

	internal class MemoryProtection
	{
		public const UInt32 PAGE_NOACCESS          =  0x01     ;
		public const UInt32 PAGE_READONLY          =  0x02     ;
		public const UInt32 PAGE_READWRITE         =  0x04     ;
		public const UInt32 PAGE_WRITECOPY         =  0x08     ;
		public const UInt32 PAGE_EXECUTE           =  0x10     ;
		public const UInt32 PAGE_EXECUTE_READ      =  0x20     ;
		public const UInt32 PAGE_EXECUTE_READWRITE =  0x40     ;
		public const UInt32 PAGE_EXECUTE_WRITECOPY =  0x80     ;
		public const UInt32 PAGE_GUARD             = 0x100     ;
		public const UInt32 PAGE_NOCACHE           = 0x200     ;
		public const UInt32 PAGE_WRITECOMBINE      = 0x400     ;
	}

	internal class MemAllocationType
	{
		public const UInt32 COMMIT       =     0x1000     ;
		public const UInt32 RESERVE      =     0x2000     ;
		public const UInt32 DECOMMIT     =     0x4000     ;
		public const UInt32 RELEASE      =     0x8000     ;
		public const UInt32 FREE         =    0x10000     ;
		public const UInt32 PRIVATE      =    0x20000     ;
		public const UInt32 MAPPED       =    0x40000     ;
		public const UInt32 RESET        =    0x80000     ;
		public const UInt32 TOP_DOWN     =   0x100000     ;
		public const UInt32 WRITE_WATCH  =   0x200000     ;
		public const UInt32 PHYSICAL     =   0x400000     ;
		public const UInt32 LARGE_PAGES  = 0x20000000     ;
		public const UInt32 FOURMB_PAGES = 0x80000000     ;
	}

	[Flags]
	public enum EFileAccess : uint
	{
		GenericRead = 0x80000000,
		GenericWrite = 0x40000000,
		GenericExecute = 0x20000000,
		GenericAll = 0x10000000,
	}

	[Flags]
	public enum EFileShare : uint
	{
		None = 0x00000000,
		Read = 0x00000001,
		Write = 0x00000002,
		Delete = 0x00000004,
	}

	public enum ECreationDisposition : uint
	{
		New = 1,
		CreateAlways = 2,
		OpenExisting = 3,
		OpenAlways = 4,
		TruncateExisting = 5,
	}

	[Flags]
	public enum EFileAttributes : uint
	{
		Readonly = 0x00000001,
		Hidden = 0x00000002,
		System = 0x00000004,
		Directory = 0x00000010,
		Archive = 0x00000020,
		Device = 0x00000040,
		Normal = 0x00000080,
		Temporary = 0x00000100,
		SparseFile = 0x00000200,
		ReparsePoint = 0x00000400,
		Compressed = 0x00000800,
		Offline= 0x00001000,
		NotContentIndexed = 0x00002000,
		Encrypted = 0x00004000,
		Write_Through = 0x80000000,
		Overlapped = 0x40000000,
		NoBuffering = 0x20000000,
		RandomAccess = 0x10000000,
		SequentialScan = 0x08000000,
		DeleteOnClose = 0x04000000,
		BackupSemantics = 0x02000000,
		PosixSemantics = 0x01000000,
		OpenReparsePoint = 0x00200000,
		OpenNoRecall = 0x00100000,
		FirstPipeInstance = 0x00080000
	}




//-----------------------------------------------------------------------------
// Functions

	internal class Kernel32
	{
		[DllImport("kernel32.dll")]
		public static extern void GetSystemInfo(
			out SYSTEM_INFO lpSystemInfo );


		[ DllImport( "Kernel32.dll" ) ]
		public static extern bool GetFileInformationByHandle
		(
			IntPtr hFile,
			out BY_HANDLE_FILE_INFORMATION lpFileInformation
		);

		[ DllImport( "kernel32.dll", SetLastError = true ) ]
		public static extern IntPtr CreateFile(
			string lpFileName,
			EFileAccess dwDesiredAccess,
			EFileShare dwShareMode,
			IntPtr lpSecurityAttributes,
			ECreationDisposition dwCreationDisposition,
			EFileAttributes dwFlagsAndAttributes,
			IntPtr hTemplateFile );


		[ DllImport( "Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode ) ]
		public static extern bool CreateHardLink
		(
			string FileName,
			string ExistingFileName,
			IntPtr lpSecurityAttributes
		);

		[ DllImport( "Kernel32.dll" ) ]
		public static extern bool Beep
		(
			UInt32 frequency,
			UInt32 duration
		);

		[ DllImport( "Kernel32.dll", SetLastError = true ) ]
		public static extern IntPtr OpenProcess(
			uint dwDesiredAccess,
			bool bInheritHandle,
			uint dwProcessId );

		[DllImport( "kernel32.dll", SetLastError = true ) ]
		public static extern IntPtr VirtualAllocEx(
			IntPtr hProcess,
			IntPtr lpAddress,
			UIntPtr dwSize,
			uint flAllocationType,
			uint flProtect);

		[DllImport("kernel32.dll")]
		public static extern bool ReadProcessMemory(
			IntPtr hProcess,
			IntPtr lpBaseAddress,
			IntPtr lpBuffer,
			UIntPtr nSize,
			IntPtr lpNumberOfBytesRead );

		[DllImport("kernel32.dll")]
		public static extern bool VirtualFreeEx(
			IntPtr hProcess,
			IntPtr lpAddress,
			UIntPtr dwSize,
			UInt32 dwFreeType );

		[DllImport("kernel32.dll")]
		public static extern bool GlobalMemoryStatusEx(
			MEMORYSTATUSEX buffer );


		[ DllImport( "kernel32.dll", SetLastError = true ) ]
		public static extern bool CloseHandle(
			IntPtr hObject );

	}

//-----------------------------------------------------------------------------

}
