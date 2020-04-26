using System;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;

using Microsoft.Win32;

namespace Common
{
	
//-----------------------------------------------------------------------------

	internal enum Hive
	{
		HKCU,
		HKLM,
	}

//-----------------------------------------------------------------------------

	internal class Settings
	{

//-----------------------------------------------------------------------------

		public static string DefaultRegistrySettingsPath = "Software\\" + Application.CompanyName + "\\" + Application.ProductName + "\\Settings";

//-----------------------------------------------------------------------------
// Get

		// bool
		public static bool Get( string sValueName, bool bDefault )
		{
			return Get( null, sValueName, bDefault );
		}

		public static bool Get( string sPath, string sValueName, bool bDefault )
		{
			return Get( null, sPath, sValueName, bDefault );
		}

		public static bool Get( string sRoot, string sPath, string sValueName, bool bDefault )
		{
			return Get( Hive.HKCU, sRoot, sPath, sValueName, bDefault );
		}

		public static bool Get( Hive hive, string sRoot, string sPath, string sValueName, bool bDefault )
		{
			object o = GetObject( hive, sRoot, sPath, sValueName );
			if ( o == null ) return bDefault;

			if ( ! ( o is string ) ) return bDefault;

			return ( string.Compare( (string) o, "False", true, CultureInfo.InvariantCulture ) != 0 );
		}

		// int
		public static int Get( string sValueName, int iDefault )
		{
			return Get( null, sValueName, iDefault );
		}

		public static int Get( string sPath, string sValueName, int iDefault )
		{
			return Get( null, sPath, sValueName, iDefault );
		}

		public static int Get( string sRoot, string sPath, string sValueName, int iDefault )
		{
			return Get( Hive.HKCU, sRoot, sPath, sValueName, iDefault );
		}

		public static int Get( Hive hive, string sRoot, string sPath, string sValueName, int iDefault )
		{
			object o = GetObject( hive, sRoot, sPath, sValueName );
			if ( o == null ) return iDefault;

			if ( ! ( o is int ) ) return iDefault;

			return (int) o;
		}

		// string
		public static string Get( string sValueName, string sDefault )
		{
			return Get( null, sValueName, sDefault );
		}

		public static string Get( string sPath, string sValueName, string sDefault )
		{
			return Get( null, sPath, sValueName, sDefault );
		}

		public static string Get( string sRoot, string sPath, string sValueName, string sDefault )
		{
			return Get( Hive.HKCU, sRoot, sPath, sValueName, sDefault );
		}

		public static string Get( Hive hive, string sRoot, string sPath, string sValueName, string sDefault )
		{
			object o = GetObject( hive, sRoot, sPath, sValueName );
			if ( o == null ) return sDefault;

			if ( ! ( o is string ) ) return sDefault;

			return (string) o;
		}

		// object
		public static object GetObject( string sValueName )
		{
			return GetObject( null, sValueName );
		}

		public static object GetObject( string sPath, string sValueName )
		{
			return GetObject( null, sPath, sValueName );
		}

		public static object GetObject( string sRoot, string sPath, string sValueName )
		{
			string s = DefaultRegistrySettingsPath;
			if ( sRoot != null && sRoot.Length > 0 ) s = sRoot;
			if ( sPath != null && sPath.Length > 0 ) s += "\\" + sPath;

			RegistryKey key = Registry.CurrentUser.OpenSubKey( s );
			if ( key == null ) return null;
			
			object o = key.GetValue( sValueName );

			key.Close();

			return o;
		}

		public static object GetObject( Hive hive, string sRoot, string sPath, string sValueName )
		{
			string s = DefaultRegistrySettingsPath;
			if ( sRoot != null && sRoot.Length > 0 ) s = sRoot;
			if ( sPath != null && sPath.Length > 0 ) s += "\\" + sPath;

			RegistryKey key;

			switch ( hive )
			{
			case Hive.HKCU : key = Registry.CurrentUser .OpenSubKey( s ); break;
			case Hive.HKLM : key = Registry.LocalMachine.OpenSubKey( s ); break;
			default: throw new ArgumentException( "Unknown hive" );
			}

			if ( key == null ) return null;
			
			object o = key.GetValue( sValueName );

			key.Close();

			return o;
		}

//-----------------------------------------------------------------------------
// Set

		// bool
		public static void Set( string sValueName, bool value )
		{
			Set( null, sValueName, value );
		}

		public static void Set( string sPath, string sValueName, bool value )
		{
			Set( null, sPath, sValueName, value );
		}

		public static void Set( string sRoot, string sPath, string sValueName, bool value )
		{
			Set( sRoot, sPath, sValueName, (object) ( value ? "True" : "False" ) );
		}

		// object
		public static void Set( string sValueName, object value )
		{
			Set( null, sValueName, value );
		}

		public static void Set( string sPath, string sValueName, object value )
		{
			Set( null, sPath, sValueName, value );
		}

		public static void Set( string sRoot, string sPath, string sValueName, object value )
		{
			string s = DefaultRegistrySettingsPath;
			if ( sRoot != null && sRoot.Length > 0 ) s = sRoot;
			if ( sPath != null && sPath.Length > 0 ) s += "\\" + sPath;

			RegistryKey key = Registry.CurrentUser.CreateSubKey( s );
			key.Close();

			key = Registry.CurrentUser.OpenSubKey( s, true );

			key.SetValue( sValueName, value );

			key.Close();
		}

//-----------------------------------------------------------------------------

	} // Settings

//-----------------------------------------------------------------------------

	internal class Setting
	{
		private Hive _Hive = Hive.HKCU;
		private string _Root = String.Empty;
		private string _Path = String.Empty;
		private string _Name = String.Empty;
		private object _Default = null;

		public Setting( string name, object defaultValue )
		{
			_Name = name;
			_Default = defaultValue;
		}

		public Setting( Hive hive, string name, object defaultValue )
		{
			_Hive = hive;
			_Name = name;
			_Default = defaultValue;
		}

		public Setting( string path, string name, object defaultValue )
		{
			_Path = path;
			_Name = name;
			_Default = defaultValue;
		}

		public Setting( string root, string path, string name, object defaultValue )
		{
			_Root = root;
			_Path = path;
			_Name = name;
			_Default = defaultValue;
		}

		public bool Bool { get { return GetBool(); } }
		public int Int { get { return GetInt(); } }
		public string String { get { return GetString(); } }

		public bool GetBool()
		{
			Debug.Assert( _Default is bool );
			return Settings.Get( _Hive, _Root, _Path, _Name, ( bool ) _Default );
		}

		public int GetInt()
		{
			Debug.Assert( _Default is int );
			return Settings.Get( _Hive, _Root, _Path, _Name, ( int ) _Default );
		}

		public string GetString()
		{
			Debug.Assert( _Default is string );
			return Settings.Get( _Hive, _Root, _Path, _Name, ( string ) _Default );
		}

		public void Set( bool value )
		{
			Debug.Assert( _Default is bool );
			Settings.Set( _Root, _Path, _Name, value );
		}

		public void Set( int value )
		{
			Debug.Assert( _Default is int );
			Settings.Set( _Root, _Path, _Name, value );
		}

		public void Set( string value )
		{
			Debug.Assert( _Default is string );
			Settings.Set( _Root, _Path, _Name, value );
		}
	}

//-----------------------------------------------------------------------------

}
