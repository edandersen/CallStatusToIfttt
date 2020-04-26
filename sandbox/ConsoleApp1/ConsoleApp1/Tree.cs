
//#define FxCop
//#define SupportXmlSerialization

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Common
{

//-----------------------------------------------------------------------------

#if FxCop || SupportXmlSerialization
	public
#else
	internal
#endif
	interface IDeepCopy
	{
		object CreateDeepCopy();
	}

//-----------------------------------------------------------------------------

#if FxCop || SupportXmlSerialization
	public
#else
	internal
#endif
	class NodeTreeDataEventArgs : EventArgs
	{
		private object _Data = null;

		public object Data { get { return _Data; } }

		public NodeTreeDataEventArgs( object data )
		{
			_Data = data;
		}
	}

#if FxCop || SupportXmlSerialization
	public
#else
	internal
#endif
	class NodeTreeNodeEventArgs : EventArgs
	{
		private INode _Node = null;

		public INode Node { get { return _Node; } }

		public NodeTreeNodeEventArgs( INode node )
		{
			_Node = node;
		}
	}

#if FxCop || SupportXmlSerialization
	public
#else
	internal
#endif
	enum NodeTreeInsertOperation
	{
		Previous,
		Next,
		Child,
		Tree
	}

#if FxCop || SupportXmlSerialization
	public
#else
	internal
#endif
	class NodeTreeInsertEventArgs : EventArgs
	{
		private NodeTreeInsertOperation _Operation;
		private object _Node = null;

		public NodeTreeInsertOperation Operation { get { return _Operation; } }
		public object Node { get { return _Node; } }

		public NodeTreeInsertEventArgs( NodeTreeInsertOperation operation, object node )
		{
			_Operation = operation;
			_Node      = node;
		}
	}

#if FxCop || SupportXmlSerialization
	public
#else
	internal
#endif
	delegate void NodeTreeDataEventHandler   ( object sender, NodeTreeDataEventArgs   e );

#if FxCop || SupportXmlSerialization
	public
#else
	internal
#endif
	delegate void NodeTreeNodeEventHandler   ( object sender, NodeTreeNodeEventArgs   e );

#if FxCop || SupportXmlSerialization
	public
#else
	internal
#endif
	delegate void NodeTreeInsertEventHandler ( object sender, NodeTreeInsertEventArgs e );

//-----------------------------------------------------------------------------

#if FxCop || SupportXmlSerialization
	public
#else
	internal
#endif
	interface INode : IValuesCollection
	{
		object Data { get; set; }

		string ToStringRecursive();

		int Depth { get; }

		int IndexBranch { get; }

		int CountBranch { get; }

		INode Parent   { get; }
		INode Previous { get; }
		INode Next     { get; }
		INode Child    { get; }

		ITree Tree     { get; }

		INode Root     { get; }
		INode Top      { get; }
		INode First    { get; }
		INode Last     { get; }

		INode LastChild { get; }

		bool IsTree      { get; }
		bool IsRoot      { get; }
		bool IsTop       { get; }
		bool HasParent   { get; }
		bool HasPrevious { get; }
		bool HasNext     { get; }
		bool HasChild    { get; }
		bool IsFirst     { get; }
		bool IsLast      { get; }

		INode this[ object o ] { get; }

		bool Contains( object o );

		INode InsertPrevious( object o );
		INode InsertNext    ( object o );
		INode InsertChild   ( object o );
		INode Add           ( object o );
		INode AddChild      ( object o );

		void InsertPrevious( INode node );
		void InsertNext    ( INode node );
		void InsertChild   ( INode node );
		void Add           ( INode node );
		void AddChild      ( INode node );

		void InsertPrevious( ITree tree );
		void InsertNext    ( ITree tree );
		void InsertChild   ( ITree tree );
		void Add           ( ITree tree );
		void AddChild      ( ITree tree );

		ITree Cut           ( object o );
		ITree Copy          ( object o );
		ITree DeepCopy      ( object o );
		void  Remove        ( object o );

		ITree Cut           ();
		ITree Copy          ();
		ITree DeepCopy      ();
		void  Remove        ();

		bool CanMoveToParent   { get; }
		bool CanMoveToPrevious { get; }
		bool CanMoveToNext     { get; }
		bool CanMoveToChild    { get; }
		bool CanMoveToFirst    { get; }
		bool CanMoveToLast     { get; }

		void MoveToParent   (); 
		void MoveToPrevious (); 
		void MoveToNext     (); 
		void MoveToChild    (); 
		void MoveToFirst    ();
		void MoveToLast     ();

//		IEnumerable AllChildren             { get; }
//		IEnumerable DirectChildren          { get; }
//		IEnumerable DirectChildrenInReverse { get; }
//		ICollection Nodes                   { get; }
//		IEnumerable NodesData               { get; }

		IValuesCollection AllChildren             { get; }
		IValuesCollection DirectChildren          { get; }
		IValuesCollection DirectChildrenInReverse { get; }
		IValuesCollection Nodes                   { get; }
//		IEnumerable NodesData               { get; }

		int DirectChildCount { get; }

		event NodeTreeDataEventHandler     Validate;
		event NodeTreeDataEventHandler     Setting;
		event NodeTreeDataEventHandler     SetDone;
		event NodeTreeInsertEventHandler   Inserting;
		event NodeTreeInsertEventHandler   Inserted;
		event EventHandler                 Cutting;
		event EventHandler                 CutDone;
		event NodeTreeNodeEventHandler     Copying;
		event NodeTreeNodeEventHandler     Copied;
		event NodeTreeNodeEventHandler     DeepCopying;
		event NodeTreeNodeEventHandler     DeepCopied;
	}

//-----------------------------------------------------------------------------

#if FxCop || SupportXmlSerialization
	public
#else
	internal
#endif
	interface ITree : IValuesCollection
	{
		Type DataType { get; }

		void Clear();

		INode Root { get; }

		INode this[ object o ] { get; }

		string ToStringRecursive();

		bool Contains( object o );

		INode InsertChild   ( object o );
		INode AddChild      ( object o );

		void InsertChild   ( INode node );
		void AddChild      ( INode node );

		void InsertChild   ( ITree tree );
		void AddChild      ( ITree tree );

		ITree Cut           ( object o );
		ITree Copy          ( object o );
		ITree DeepCopy      ( object o );
		void  Remove        ( object o );

		ITree Copy          ();
		ITree DeepCopy      ();

//		IEnumerable AllChildren             { get; }
//		IEnumerable DirectChildren          { get; }
//		IEnumerable DirectChildrenInReverse { get; }
//		ICollection Nodes                   { get; }
//		IEnumerable NodesData               { get; }

		IValuesCollection AllChildren             { get; }
		IValuesCollection DirectChildren          { get; }
		IValuesCollection DirectChildrenInReverse { get; }
		IValuesCollection Nodes                   { get; }
//		IEnumerable NodesData               { get; }

		int DirectChildCount { get; }

		event NodeTreeDataEventHandler     Validate;
		event EventHandler                 Clearing;
		event EventHandler                 Cleared;
		event NodeTreeDataEventHandler     Setting;
		event NodeTreeDataEventHandler     SetDone;
		event NodeTreeInsertEventHandler   Inserting;
		event NodeTreeInsertEventHandler   Inserted;
		event EventHandler                 Cutting;
		event EventHandler                 CutDone;
		event NodeTreeNodeEventHandler     Copying;
		event NodeTreeNodeEventHandler     Copied;
		event NodeTreeNodeEventHandler     DeepCopying;
		event NodeTreeNodeEventHandler     DeepCopied;
	}

//-----------------------------------------------------------------------------

#if FxCop || SupportXmlSerialization
	public
#else
	internal
#endif
	interface IValuesCollection : ICollection
	{
		ICollection Values { get; }
	}

//-----------------------------------------------------------------------------

	[Serializable]
#if FxCop || SupportXmlSerialization
	public
#else
	internal
//	public
#endif
		class NodeTree : INode, ITree, ISerializable
	{
		private object _Data = null;

		private NodeTree _Parent   = null;
		private NodeTree _Previous = null;
		private NodeTree _Next     = null;
		private NodeTree _Child    = null;

		protected NodeTree() {}

		/// <summary>Obtains the <see cref="String"/> representation of this instance.</summary>
		/// <returns>The <see cref="String"/> representation of this instance.</returns>
		/// <remarks>
		/// <p>
		/// This method returns a <see cref="String"/> that represents this instance.
		/// </p>
		/// </remarks>
		public override string ToString()
		{
			return Data.ToString();
		}

		public virtual string ToStringRecursive()
		{
			string s = String.Empty;

			if ( ! IsRoot ) s += this + "\n";

			foreach ( NodeTree node in AllChildren )
				s += new String( '\t', node.Depth ) + node + "\n";

			return s;
		}

		public virtual int Depth
		{
			get
			{
				int i = -1;

				for ( INode node = this ; ! node.IsRoot ; node = node.Parent ) i++;

				return i;
			}
		}

		public virtual int IndexBranch
		{
			get
			{
				int i = -1;

				for ( INode node = this ; node != null ; node = node.Previous ) i++;

				return i;
			}
		}

		public virtual int CountBranch
		{
			get
			{
				int i = 0;

				for ( INode node = First ; node != null ; node = node.Next ) i++;

				return i;
			}
		}

//-----------------------------------------------------------------------------

		[ ReflectionPermission( SecurityAction.Demand, Unrestricted = true ) ]
		protected virtual object DeepCopyData( object data )
		{
//			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			if ( data is IDeepCopy )
			{
				IDeepCopy o = (IDeepCopy) data;
				return o.CreateDeepCopy();
			}

			try
			{
				return Activator.CreateInstance( data.GetType(), new object[] { data } );
			}
			catch ( MissingMethodException ) {}

			return data;
		}

//-----------------------------------------------------------------------------

		[ Serializable ]
		internal protected class RootObject : ISerializable
		{
			private Type _DataType = typeof( Object );
//			private bool _Changed = false;
			private int _Version = 0;

			public Type DataType { get { return _DataType; } }
//			public bool Changed { get { return _Changed; } set { _Changed = value; } }
			public int Version { get { return _Version; } set { _Version = value; } }

			public RootObject() {}

			public RootObject( Type dataType )
			{
				_DataType = dataType;
			}

			/// <summary>Obtains the <see cref="String"/> representation of this instance.</summary>
			/// <returns>The <see cref="String"/> representation of this instance.</returns>
			/// <remarks>
			/// <p>
			/// This method returns a <see cref="String"/> that represents this instance.
			/// </p>
			/// </remarks>
			public override string ToString() { return "ROOT: " + DataType.Name; }

			// Save
			/// <summary>Populates a SerializationInfo with the data needed to serialize the target object.</summary>
			/// <param name="info">The SerializationInfo to populate with data.</param>
			/// <param name="context">The destination for this serialization.</param>
			/// <remarks>
			/// <p>This method is called during serialization.</p>
			/// <p>Do not call this method directly.</p>
			/// </remarks>
			[ SecurityPermission( SecurityAction.Demand, SerializationFormatter = true ) ]
			public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue( "RootObjectVersion" , 1         );
				info.AddValue( "DataType"          , _DataType );
			}

			// Load
			/// <summary>Initializes a new instance of the class during deserialization.</summary>
			/// <param name="info">The SerializationInfo populated with data.</param>
			/// <param name="context">The source for this serialization.</param>
			/// <remarks>
			/// <p>This method is called during deserialization.</p>
			/// <p>Do not call this method directly.</p>
			/// </remarks>
			protected RootObject( SerializationInfo info, StreamingContext context )
			{
				int iVersion = info.GetInt32( "RootObjectVersion" );
				_DataType    = info.GetValue( "DataType"          , typeof( Type ) ) as Type;
			}
		}

//-----------------------------------------------------------------------------

		public static ITree NewTree()
		{
			NodeTree n = new NodeTree();
			n._Data = new RootObject();
			return n;
		}

		public static ITree NewTree( Type dataType )
		{
			NodeTree n = new NodeTree();
			n._Data = new RootObject( dataType );
			return n;
		}

		protected virtual RootObject CreateRootObject()
		{
			if ( ! IsRoot ) throw new InvalidOperationException( "This is not a Root" );

			return new RootObject();
		}

		protected virtual RootObject CreateRootObject( Type dataType )
		{
			if ( ! IsRoot ) throw new InvalidOperationException( "This is not a Root" );

			return new RootObject( dataType );
		}

		protected virtual NodeTree CreateNode()
		{
			return new NodeTree();
		}

		protected virtual NodeTree CreateTree( Type dataType )
		{
			NodeTree n = CreateNode();
			n._Data = n.CreateRootObject( dataType );
			return n;
		}

//-----------------------------------------------------------------------------

		//		protected bool Changed
		//		{
		//			get
		//			{
		//				if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a Tree" );
		//
		//				RootObject o = (RootObject) Root.Data;
		//				return o.Changed;
		//			}
		//
		//			set
		//			{
		//				if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a Tree" );
		//
		//				RootObject o = (RootObject) Root.Data;
		//				o.Changed = value;
		//			}
		//		}

		protected int Version
		{
			get
			{
				if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a Tree" );

				RootObject o = (RootObject) Root.Data;

				return o.Version;
			}

			set
			{
				if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a Tree" );

				RootObject o = (RootObject) Root.Data;

				o.Version = value;
			}
		}

		protected bool HasChanged( int version ) { return ( Version != version ); }

		protected void IncrementVersion()
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a Tree" );

			RootObject o = (RootObject) Root.Data;

			o.Version++;
		}

//-----------------------------------------------------------------------------
// ISerializable

		// Save
		/// <summary>Populates a SerializationInfo with the data needed to serialize the target object.</summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		/// <remarks>
		/// <p>This method is called during serialization.</p>
		/// <p>Do not call this method directly.</p>
		/// </remarks>
		[ SecurityPermission( SecurityAction.Demand, SerializationFormatter = true ) ]
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			info.AddValue( "NodeTreeVersion"     , 1         );
			info.AddValue( "Data"                , _Data     );
			info.AddValue( "Parent"              , _Parent   );
			info.AddValue( "Previous"            , _Previous );
			info.AddValue( "Next"                , _Next     );
			info.AddValue( "Child"               , _Child    );
		}

		// Load
		/// <summary>Initializes a new instance of the class during deserialization.</summary>
		/// <param name="info">The SerializationInfo populated with data.</param>
		/// <param name="context">The source for this serialization.</param>
		/// <remarks>
		/// <p>This method is called during deserialization.</p>
		/// <p>Do not call this method directly.</p>
		/// </remarks>
		protected NodeTree( SerializationInfo info, StreamingContext context )
		{
			int iVersion =            info.GetInt32( "NodeTreeVersion" );
			_Data        =            info.GetValue( "Data"        , typeof( object   ) );
			_Parent      = (NodeTree) info.GetValue( "Parent"      , typeof( NodeTree ) );
			_Previous    = (NodeTree) info.GetValue( "Previous"    , typeof( NodeTree ) );
			_Next        = (NodeTree) info.GetValue( "Next"        , typeof( NodeTree ) );
			_Child       = (NodeTree) info.GetValue( "Child"       , typeof( NodeTree ) );
		}

//-----------------------------------------------------------------------------
// INode

		public object Data
		{
			get
			{
				return _Data;
			}
			
			set
			{
				if ( IsTree ) throw new InvalidOperationException( "This is a Root" );

				OnSetting( this, value );

				_Data = value;

				OnSetDone( this, value );
			}
		}

		public INode Parent   { get { return _Parent   ; } }
		public INode Previous { get { return _Previous ; } }
		public INode Next     { get { return _Next     ; } }
		public INode Child    { get { return _Child    ; } }

//-----------------------------------------------------------------------------

		public ITree Tree
		{
			get
			{
				return (ITree) Root;
			}
		}

		public INode Root
		{
			get
			{
				INode node = this;
				while ( node.Parent != null )
					node = node.Parent;

				return node;
			}
		}

		public INode Top
		{
			get
			{
				if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );
				if (   this.IsRoot ) throw new InvalidOperationException( "This is a Root" );

				INode node = this;
				while ( node.Parent.Parent != null )
					node = node.Parent;

				return node;
			}			
		}

		public INode First
		{
			get
			{
				INode node = this;
				while ( node.Previous != null )
					node = node.Previous;

				return node;
			}
		}

		public INode Last
		{
			get
			{
				INode node = this;
				while ( node.Next != null )
					node = node.Next;

				return node;
			}
		}

		public INode LastChild
		{
			get
			{
				if ( Child == null ) return null;
				return Child.Last;
			}
		}

//-----------------------------------------------------------------------------

		public bool HasPrevious { get { return Previous != null; } }
		public bool HasNext     { get { return Next     != null; } }
		public bool HasChild    { get { return Child    != null; } }
		public bool IsFirst     { get { return Previous == null; } }
		public bool IsLast      { get { return Next     == null; } }

		public bool IsTree
		{
			get
			{
				if ( ! IsRoot ) return false;
				return Data is RootObject;
			}
		}

		public bool IsRoot
		{
			get
			{
				bool b = ( Parent == null );

				if ( b )
				{
					Debug.Assert( Previous == null );
					Debug.Assert( Next     == null );
				}

				return b;
			}
		}

		public bool HasParent
		{
			get
			{
				if ( IsRoot ) throw new InvalidOperationException( "This is a Root" );
				return Parent.Parent   != null;
			}
		}

		public bool IsTop
		{
			get
			{
				if ( IsRoot ) throw new InvalidOperationException( "This is a Root" );
				return Parent.Parent == null;
			}
		}

//-----------------------------------------------------------------------------

		public INode this[ object o ]
		{
			get
			{
				if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

				foreach ( INode n in this )
					if ( n.Data == o )
						return n;

				return null;
			}
		}

		public bool Contains( object o )
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			return this[ o ] != null;
		}

//-----------------------------------------------------------------------------

		public INode InsertPrevious( object o )
		{
			if (   this.IsRoot ) throw new InvalidOperationException( "This is a Root" );
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			NodeTree newNode = CreateNode();
			newNode._Data = o;

			this.InsertPreviousCore( newNode );

			return newNode;
		}

		public INode InsertNext( object o )
		{
			if (   this.IsRoot ) throw new InvalidOperationException( "This is a Root" );
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			NodeTree newNode = CreateNode();
			newNode._Data = o;

			this.InsertNextCore( newNode );

			return newNode;
		}

		public INode InsertChild( object o )
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			NodeTree newNode = CreateNode();
			newNode._Data = o;

			this.InsertChildCore( newNode );

			return newNode;
		}

		public INode Add( object o )
		{
			if (   this.IsRoot ) throw new InvalidOperationException( "This is a Root" );
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			return this.Last.InsertNext( o );
		}

		public INode AddChild( object o )
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			if ( Child == null )
				return InsertChild( o );
			else
				return Child.Add( o );
		}

//-----------------------------------------------------------------------------

		public void InsertPrevious( INode node ) { throw new ArgumentException( "Object is a node" ); }
		public void InsertNext    ( INode node ) { throw new ArgumentException( "Object is a node" ); }
		public void InsertChild   ( INode node ) { throw new ArgumentException( "Object is a node" ); }
		public void Add           ( INode node ) { throw new ArgumentException( "Object is a node" ); }
		public void AddChild      ( INode node ) { throw new ArgumentException( "Object is a node" ); }

//-----------------------------------------------------------------------------

		public void InsertPrevious( ITree newITree )
		{
			if (   this.IsRoot ) throw new InvalidOperationException( "This is a Root" );
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			NodeTree newTree = GetNodeTree( newITree );

			if ( ! newTree.IsRoot ) throw new ArgumentException( "Tree is not a Root" );
			if ( ! newTree.IsTree ) throw new ArgumentException( "Tree is not a tree" );

			for ( INode n = newTree.Child ; n != null ; n = n.Next )
			{
				NodeTree node = GetNodeTree( n );
				NodeTree copy = node.CopyCore();
				InsertPreviousCore( copy );
			}
		}

		public void InsertNext( ITree newITree )
		{
			if (   this.IsRoot ) throw new InvalidOperationException( "This is a Root" );
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			NodeTree newTree = GetNodeTree( newITree );

			if ( ! newTree.IsRoot ) throw new ArgumentException( "Tree is not a Root" );
			if ( ! newTree.IsTree ) throw new ArgumentException( "Tree is not a tree" );

			for ( INode n = newTree.LastChild ; n != null ; n = n.Previous )
			{
				NodeTree node = GetNodeTree( n );
				NodeTree copy = node.CopyCore();
				InsertNextCore( copy );
			}
		}

		public void InsertChild( ITree newITree )
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			NodeTree newTree = GetNodeTree( newITree );

			if ( ! newTree.IsRoot ) throw new ArgumentException( "Tree is not a Root" );
			if ( ! newTree.IsTree ) throw new ArgumentException( "Tree is not a tree" );

			for ( INode n = newTree.LastChild ; n != null ; n = n.Previous )
			{
				NodeTree node = GetNodeTree( n );
				NodeTree copy = node.CopyCore();
				InsertChildCore( copy );
			}
		}

		public void Add( ITree newITree )
		{
			if (   this.IsRoot ) throw new InvalidOperationException( "This is a Root" );
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			this.Last.InsertNext( newITree );
		}

		public void AddChild( ITree newITree )
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			if ( this.Child == null )
				this.InsertChild( newITree );
			else
				this.Child.Add( newITree );
		}

//-----------------------------------------------------------------------------

		protected virtual void InsertPreviousCore( INode newINode )
		{
			if (   this    .IsRoot ) throw new InvalidOperationException( "This is a Root" );
			if ( ! newINode.IsRoot ) throw new ArgumentException( "Node is not a Root" );
			if (   newINode.IsTree ) throw new ArgumentException( "Node is a tree" );

			IncrementVersion();

			OnInserting( this, NodeTreeInsertOperation.Previous, newINode );

			NodeTree newNode = GetNodeTree( newINode );

			newNode._Parent   = this._Parent;
			newNode._Previous = this._Previous;
			newNode._Next     = this;
			this._Previous    = newNode;

			if ( newNode.Previous != null )
			{
				NodeTree Previous = GetNodeTree( newNode.Previous );
				Previous._Next = newNode;
			}
			else // this is a first node
			{
				NodeTree Parent = GetNodeTree( newNode.Parent );
				Parent._Child = newNode;
			}

			OnInserted( this, NodeTreeInsertOperation.Previous, newINode );
		}

		protected virtual void InsertNextCore( INode newINode )
		{
			if (   this    .IsRoot ) throw new InvalidOperationException( "This is a Root" );
			if ( ! newINode.IsRoot ) throw new ArgumentException( "Node is not a Root" );
			if (   newINode.IsTree ) throw new ArgumentException( "Node is a tree" );

			IncrementVersion();

			OnInserting( this, NodeTreeInsertOperation.Next, newINode );

			NodeTree newNode = GetNodeTree( newINode );

			newNode._Parent = this._Parent;
			newNode._Previous = this;
			newNode._Next = this._Next;
			this._Next = newNode;

			if ( newNode.Next != null )
			{
				NodeTree Next = GetNodeTree( newNode.Next );
				Next._Previous = newNode;
			}

			OnInserted( this, NodeTreeInsertOperation.Next, newINode );
		}

		protected virtual void InsertChildCore( INode newINode )
		{
			if ( ! newINode.IsRoot ) throw new ArgumentException( "Node is not a Root" );
			if (   newINode.IsTree ) throw new ArgumentException( "Node is a tree" );
			
			IncrementVersion();

			OnInserting( this, NodeTreeInsertOperation.Child, newINode );

			NodeTree newNode = GetNodeTree( newINode );

			newNode._Parent = this;
			newNode._Next = this._Child;
			this._Child = newNode;

			if ( newNode.Next != null )
			{
				NodeTree Next = GetNodeTree( newNode.Next );
				Next._Previous = newNode;
			}

			OnInserted( this, NodeTreeInsertOperation.Child, newINode );
		}

		protected virtual void AddCore( INode newINode )
		{
			if ( this.IsRoot ) throw new InvalidOperationException( "This is a Root" );

			NodeTree lastNode = GetNodeTree( Last );

			lastNode.InsertNextCore( newINode );
		}

		protected virtual void AddChildCore( INode newINode )
		{
			if ( this.Child == null )
				this.InsertChildCore( newINode );
			else
			{
				NodeTree childNode = GetNodeTree( Child );

				childNode.AddCore( newINode );
			}
		}

//-----------------------------------------------------------------------------
	
		public ITree Cut( object o )
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			INode n = this[ o ];
			if ( n == null ) return null;
			return n.Cut();
		}

		public ITree Copy( object o )
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			INode n = this[ o ];
			if ( n == null ) return null;
			return n.Copy();
		}

		public ITree DeepCopy( object o )
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			INode n = this[ o ];
			if ( n == null ) return null;
			return n.DeepCopy();
		}

		public void Remove( object o )
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			INode n = this[ o ];
			if ( n == null ) return;
			n.Remove();
		}

//-----------------------------------------------------------------------------

		private NodeTree BoxInTree( Type dataType, NodeTree node )
		{
			if ( ! node.IsRoot ) throw new ArgumentException( "Node is not a Root" );
			if (   node.IsTree ) throw new ArgumentException( "Node is a tree"     );
			
			NodeTree tree = CreateTree( dataType );

			tree.AddChildCore( node );

			return tree;
		}

		public ITree Cut()
		{
			if (   this.IsRoot ) throw new InvalidOperationException( "This is a Root" );
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			Type DataType = this.DataType;

			NodeTree node = CutCore();

			return BoxInTree( DataType, node );
		}

		public ITree Copy()
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			if ( IsTree )
			{
				NodeTree NewTree = CopyCore();

				return NewTree;
			}
			else
			{
				NodeTree NewNode = CopyCore();

				return BoxInTree( DataType, NewNode );
			}
		}

		public ITree DeepCopy()
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			if ( IsTree )
			{
				NodeTree NewTree = DeepCopyCore();

				return NewTree;
			}
			else
			{
				NodeTree NewNode = DeepCopyCore();

				return BoxInTree( DataType, NewNode );
			}
		}

		public void Remove()
		{
			if (   this.IsRoot ) throw new InvalidOperationException( "This is a Root" );
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			RemoveCore();
		}

//-----------------------------------------------------------------------------
		
		protected virtual NodeTree CutCore()
		{
			if ( this.IsRoot ) throw new InvalidOperationException( "This is a Root" );

			IncrementVersion();

			OnCutting( this );

			INode OldRoot = Root;

			if ( this._Next != null )
				this._Next._Previous = this._Previous;

			if ( this.Previous != null )
				this._Previous._Next = this._Next;
			else // this is a first node
			{
				Debug.Assert( Parent.Child == this );
				this._Parent._Child = this._Next;
				Debug.Assert( this.Next == null || this.Next.Previous == null );
			}

			this._Parent   = null;
			this._Previous = null;
			this._Next     = null;

			OnCutDone( OldRoot, this );

			return this;
		}

		protected virtual NodeTree CopyCore()
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );
			if ( IsRoot && ! IsTree ) throw new InvalidOperationException( "This is a Root" );

			if ( IsTree )
			{
				NodeTree NewTree = CreateTree( DataType );

				OnCopying( this, NewTree );

				CopyChildNodes( this, NewTree, false );

				OnCopied( this, NewTree );

				return NewTree;
			}
			else
			{
				NodeTree NewNode = CreateNode();

				NewNode._Data = Data;

				OnCopying( this, NewNode );

				CopyChildNodes( this, NewNode, false );

				OnCopied( this, NewNode );

				return NewNode;
			}
		}

		protected virtual NodeTree DeepCopyCore()
		{
			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );
			if ( IsRoot && ! IsTree ) throw new InvalidOperationException( "This is a Root" );

			if ( IsTree )
			{
				NodeTree NewTree = CreateTree( DataType );

				OnCopying( this, NewTree );

				CopyChildNodes( this, NewTree, true );

				OnCopied( this, NewTree );

				return NewTree;
			}
			else
			{
				NodeTree NewNode = CreateNode();

				NewNode._Data = DeepCopyData( Data );

				OnDeepCopying( this, NewNode );

				CopyChildNodes( this, NewNode, true );

				OnDeepCopied( this, NewNode );

				return NewNode;
			}
		}

		private void CopyChildNodes( INode oldNode, NodeTree newNode, bool bDeepCopy )
		{
			NodeTree previousNewChildNode = null;

			for ( INode oldChildNode = oldNode.Child ; oldChildNode != null ; oldChildNode = oldChildNode.Next )
			{
				NodeTree newChildNode = CreateNode();

				if ( ! bDeepCopy )
					newChildNode._Data = oldChildNode.Data;
				else
					newChildNode._Data = DeepCopyData( oldChildNode.Data );

//				if ( ! bDeepCopy )
//					OnCopying( oldChildNode, newChildNode );
//				else
//					OnDeepCopying( oldChildNode, newChildNode );

				if ( oldChildNode.Previous == null ) newNode._Child = newChildNode;
					
				newChildNode._Parent = newNode;
				newChildNode._Previous = previousNewChildNode;
				if ( previousNewChildNode != null ) previousNewChildNode._Next = newChildNode;
					
//				if ( ! bDeepCopy )
//					OnCopied( oldChildNode, newChildNode );
//				else
//					OnDeepCopied( oldChildNode, newChildNode );

				CopyChildNodes( oldChildNode, newChildNode, bDeepCopy );

				previousNewChildNode = newChildNode;
			}
		}


		protected virtual void RemoveCore()
		{
			if ( this.IsRoot ) throw new InvalidOperationException( "This is a Root" );

			CutCore();
		}

//-----------------------------------------------------------------------------

		public bool CanMoveToParent
		{
			get
			{
				if ( this.IsRoot  ) return false;
				if ( this.IsTop   ) return false;

				return true;
			}
		}

		public bool CanMoveToPrevious
		{
			get
			{
				if ( this.IsRoot  ) return false;
				if ( this.IsFirst ) return false;

				return true;
			}
		}

		public bool CanMoveToNext
		{
			get
			{
				if ( this.IsRoot  ) return false;
				if ( this.IsLast  ) return false;

				return true;
			}
		}

		public bool CanMoveToChild
		{
			get
			{
				if ( this.IsRoot  ) return false;
				if ( this.IsFirst ) return false;

				return true;
			}
		}

		public bool CanMoveToFirst
		{
			get
			{
				if ( this.IsRoot  ) return false;
				if ( this.IsFirst ) return false;

				return true;
			}
		}

		public bool CanMoveToLast
		{
			get
			{
				if ( this.IsRoot  ) return false;
				if ( this.IsLast  ) return false;

				return true;
			}
		}

//-----------------------------------------------------------------------------

		public void MoveToParent()
		{
			if ( ! CanMoveToParent ) throw new InvalidOperationException( "Cannot move to Parent" );


			NodeTree parentNode = GetNodeTree( this.Parent );

			NodeTree thisNode = this.CutCore();

			parentNode.InsertNextCore( thisNode );
		}

		public void MoveToPrevious()
		{
			if ( ! CanMoveToPrevious ) throw new InvalidOperationException( "Cannot move to Previous" );

			NodeTree previousNode = GetNodeTree( this.Previous );

			NodeTree thisNode = this.CutCore();

			previousNode.InsertPreviousCore( thisNode );
		}

		public void MoveToNext()
		{
			if ( ! CanMoveToNext ) throw new InvalidOperationException( "Cannot move to Next" );

			NodeTree nextNode = GetNodeTree( this.Next );

			NodeTree thisNode = this.CutCore();

			nextNode.InsertNextCore( thisNode );
		}

		public void MoveToChild()
		{
			if ( ! CanMoveToChild ) throw new InvalidOperationException( "Cannot move to Child" );

			NodeTree previousNode = GetNodeTree( this.Previous );

			NodeTree thisNode = this.CutCore();

			previousNode.AddChildCore( thisNode );
		}

		public void MoveToFirst()
		{
			if ( ! CanMoveToFirst ) throw new InvalidOperationException( "Cannot move to first" );

			NodeTree firstNode = GetNodeTree( this.First );

			NodeTree thisNode = this.CutCore();

			firstNode.InsertPreviousCore( thisNode );
		}


		public void MoveToLast()
		{
			if ( ! CanMoveToLast ) throw new InvalidOperationException( "Cannot move to last" );

			NodeTree lastNode = GetNodeTree( this.Last );

			NodeTree thisNode = this.CutCore();

			lastNode.InsertNextCore( thisNode );
		}


//-----------------------------------------------------------------------------
// Enumerators

//		private class EnumeratorBase : IEnumerable, IEnumerator
		private abstract class EnumeratorBase : IValuesCollection, IEnumerator
		{
			private int _Version = 0;
			private bool _Values = false;
			private object _SyncRoot = new object();

			protected INode _Root = null;
			protected INode _Current = null;

			protected bool _BeforeFirst = true;
			protected bool _AfterLast = false;

			public EnumeratorBase( INode root )
			{
				_Root    = root;
				_Current = root;

				_Version = ( ( NodeTree ) _Root ).Version;
			}

			protected abstract EnumeratorBase CreateCopy();

			protected bool HasChanged { get { return ( ( NodeTree ) _Root ).HasChanged( _Version ); } }

			// IEnumerable
			public virtual IEnumerator GetEnumerator()
			{
				return this;
			}

			// ICollection
			public virtual void CopyTo( Array array, int index )// { throw new NotImplementedException( "CopyTo" ); }
			{
				if ( array == null ) throw new ArgumentNullException( "array" );
				if ( array.Rank > 1 ) throw new ArgumentException( "array is multidimensional", "array" );
				if ( index < 0 ) throw new ArgumentOutOfRangeException( "index" );

				int count = Count;

				if ( count > 0 )
					if ( index >= array.Length ) throw new ArgumentException( "index is out of bounds", "index" );
			
				if ( index + count > array.Length ) throw new ArgumentException( "Not enough space in array", "array" );

				EnumeratorBase e = CreateCopy();

				foreach ( INode n in e )
					array.SetValue( n.Data, index++ );
			}

			public virtual int Count
			{
				get
				{
					EnumeratorBase e = CreateCopy();

					int i = 0;
					foreach ( object o in e ) { object p = o; i++; }
					return i;
				}
			}

			public virtual bool IsSynchronized { get { return false; } }

			public virtual object SyncRoot { get { return _SyncRoot; } }

			// IValuesCollection
			public ICollection Values
			{
				get
				{
					_Values = true;
					return this;
				} 
			}

			// IEnumerator
			public virtual void Reset()
			{
				if ( HasChanged ) throw new InvalidOperationException( "Tree has been modified." );

				_Current = _Root;

				_BeforeFirst = true;
				_AfterLast = false;
			}

			public virtual object Current
			{
				get
				{
					if ( _BeforeFirst ) throw new InvalidOperationException( "Enumeration has not started." );
					if ( _AfterLast   ) throw new InvalidOperationException( "Enumeration has finished." );

					if ( ! _Values ) return _Current;

					if ( _Current == null ) { Debug.Assert( false ); return null; }

					return _Current.Data;
				}
			}

			public virtual bool MoveNext()
			{
				if ( HasChanged ) throw new InvalidOperationException( "Tree has been modified." );

				_BeforeFirst = false;
				
				return true;
			}
		}

		public virtual IEnumerator GetEnumerator()
		{
			return new DefaultEnumerator( this );
		}

		public virtual ICollection Values
		{
			get
			{
				return new DefaultEnumerator( this ).Values;
			} 
		}

		private class DefaultEnumerator : EnumeratorBase
		{
			protected bool mFirst = true;

			public DefaultEnumerator( INode root ) : base( root ) {}

			public DefaultEnumerator( DefaultEnumerator o ) : base ( o._Root ) {}

			protected override EnumeratorBase CreateCopy() { return new DefaultEnumerator( this ); }

			public override void Reset()
			{
				base.Reset();

				mFirst = true;
			}

			public override bool MoveNext()
			{
				if ( ! base.MoveNext() ) goto hell;

				if ( _Current == null ) throw new InvalidOperationException( "Current is null" );

				if ( _Current.IsRoot ) { _Current = _Current.Child; }
				if ( _Current == null ) goto hell;

				if ( mFirst ) { mFirst = false; return true; }

				if ( _Current.Child != null ) { _Current = _Current.Child ; return true; }

				for ( ; _Current.Parent != null ; _Current = _Current.Parent )
				{
					if ( _Current == _Root ) goto hell;
					if ( _Current.Next != null ) { _Current = _Current.Next ; return true; }
				}

			hell:

				_AfterLast = true;
				return false;
			}
		}

		public IValuesCollection AllChildren { get { return new AllChildrenEnumerator( this ); } }

		private class AllChildrenEnumerator : EnumeratorBase
		{
			public AllChildrenEnumerator( INode root ) : base( root ) {}

			public AllChildrenEnumerator( AllChildrenEnumerator o ) : base ( o._Root ) {}

			protected override EnumeratorBase CreateCopy() { return new AllChildrenEnumerator( this ); }

			public override bool MoveNext()
			{
				if ( ! base.MoveNext() ) { _AfterLast = true; return false; }

				if ( _Current == null ) throw new InvalidOperationException( "Current is null" );

				if ( _Current.Child != null ) { _Current = _Current.Child ; return true; }

				for ( ; _Current.Parent != null ; _Current = _Current.Parent )
				{
					if ( _Current == _Root ) { _AfterLast = true; return false; }
					if ( _Current.Next != null ) { _Current = _Current.Next ; return true; }
				}

				_AfterLast = true;
				return false;
			}
		}

		public IValuesCollection DirectChildren { get { return new DirectChildrenEnumerator( this ); } }
		public IValuesCollection Nodes          { get { return new DirectChildrenEnumerator( this ); } }

		private class DirectChildrenEnumerator : EnumeratorBase
		{
			public DirectChildrenEnumerator( INode root ) : base( root ) {}

			public DirectChildrenEnumerator( DirectChildrenEnumerator o ) : base ( o._Root ) {}

			protected override EnumeratorBase CreateCopy() { return new DirectChildrenEnumerator( this ); }

			public override bool MoveNext()
			{
				if ( ! base.MoveNext() ) { _AfterLast = true; return false; }

				if ( _Current == null ) throw new InvalidOperationException( "Current is null" );

				if ( _Current == _Root )
					_Current = _Root.Child;
				else
					_Current = _Current.Next;
				
				if ( _Current != null ) return true;

				_AfterLast = true;
				return false;
			}

//			public virtual int Count
//			{
//				get
//				{
//					int i = 0;
//					for ( INode n = _Root.Child ; n != null ; n = n.Next ) i++;
//					return i;
//				}
//			}
		}

		public IValuesCollection DirectChildrenInReverse { get { return new DirectChildrenEnumeratorInReverse( this ); } }

		private class DirectChildrenEnumeratorInReverse : EnumeratorBase
		{
			public DirectChildrenEnumeratorInReverse( INode root ) : base( root ) {}

			public DirectChildrenEnumeratorInReverse( DirectChildrenEnumeratorInReverse o ) : base ( o._Root ) {}

			protected override EnumeratorBase CreateCopy() { return new DirectChildrenEnumeratorInReverse( this ); }

			public override bool MoveNext()
			{
				if ( ! base.MoveNext() ) { _AfterLast = true; return false; }

				if ( _Current == null ) throw new InvalidOperationException( "Current is null" );

				if ( _Current == _Root )
					_Current = _Root.LastChild;
				else
					_Current = _Current.Previous;
				
				if ( _Current != null ) return true;

				_AfterLast = true;
				return false;
			}
		}

		public int DirectChildCount
		{
			get
			{
				int i = 0;

				for ( INode n = this.Child ; n != null ; n = n.Next )
					i++;

				return i;
			}
		}

//-----------------------------------------------------------------------------
// ITree

		public virtual Type DataType
		{
			get
			{
//				if ( ! this.IsRoot ) throw new InvalidOperationException( "This is not a Root" );
//				if ( ! this.IsTree ) throw new InvalidOperationException( "This is not a tree" );
				if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );
				
//				RootObject o = (RootObject) Data;
				RootObject o = (RootObject) Root.Data;
				
				return o.DataType;
			}
		}

		public void Clear()
		{
			if ( ! this.IsRoot ) throw new InvalidOperationException( "This is not a Root" );
			if ( ! this.IsTree ) throw new InvalidOperationException( "This is not a tree" );

			OnClearing( this );

			_Child = null;

			OnCleared( this );
		}

//-----------------------------------------------------------------------------

		protected static NodeTree GetNodeTree( ITree tree )
		{
			if ( tree == null ) throw new InvalidCastException( "Tree is null" );

			return ( NodeTree ) tree; // can throw an InvalidCastException.
		}

		protected static NodeTree GetNodeTree( INode node )
		{
			if ( node == null ) throw new InvalidCastException( "Node is null" );

			return ( NodeTree ) node; // can throw an InvalidCastException.
		}

//-----------------------------------------------------------------------------
// ICollection

		public virtual bool IsSynchronized { get { return false; } } // Not thread safe
		
		public virtual object SyncRoot { get { return this; } } // Not sure about this!

		public virtual void CopyTo( Array array, int index )
		{
			if ( array == null ) throw new ArgumentNullException( "array" );
			if ( array.Rank > 1 ) throw new ArgumentException( "array is multidimensional", "array" );
			if ( index < 0 ) throw new ArgumentOutOfRangeException( "index" );

			if ( Count > 0 )
			if ( index >= array.Length ) throw new ArgumentException( "index is out of bounds", "index" );
			
			if ( index + Count > array.Length ) throw new ArgumentException( "Not enough space in array", "array" );

			foreach ( INode n in this )
				array.SetValue( n.Data, index++ );
		}

		public virtual int Count
		{
			get
			{
				int i = IsRoot ? 0 : 1;

				for ( INode n = this.Child ; n != null ; n = n.Next )
					i += n.Count;

				return i;
			}
		}

//-----------------------------------------------------------------------------

		private EventHandlerList _EventHandlerList = new EventHandlerList();

		private static readonly object ValidateEventKey    = new object();
		private static readonly object ClearingEventKey    = new object();
		private static readonly object ClearedEventKey     = new object();
		private static readonly object SettingEventKey     = new object();
		private static readonly object SetDoneEventKey     = new object();
		private static readonly object InsertingEventKey   = new object();
		private static readonly object InsertedEventKey    = new object();
		private static readonly object CuttingEventKey     = new object();
		private static readonly object CutDoneEventKey     = new object();
		private static readonly object CopyingEventKey     = new object();
		private static readonly object CopiedEventKey      = new object();
		private static readonly object DeepCopyingEventKey = new object();
		private static readonly object DeepCopiedEventKey  = new object();

		public event NodeTreeDataEventHandler Validate
		{
			add    { _EventHandlerList.AddHandler    ( ValidateEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( ValidateEventKey, value ); }
		}

		public event EventHandler Clearing
		{
			add    { _EventHandlerList.AddHandler    ( ClearingEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( ClearingEventKey, value ); }
		}

		public event EventHandler Cleared
		{
			add    { _EventHandlerList.AddHandler    ( ClearedEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( ClearedEventKey, value ); }
		}

		public event NodeTreeDataEventHandler Setting
		{
			add    { _EventHandlerList.AddHandler    ( SettingEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( SettingEventKey, value ); }
		}

		public event NodeTreeDataEventHandler SetDone
		{
			add    { _EventHandlerList.AddHandler    ( SetDoneEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( SetDoneEventKey, value ); }
		}

		public event NodeTreeInsertEventHandler Inserting
		{
			add    { _EventHandlerList.AddHandler    ( InsertingEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( InsertingEventKey, value ); }
		}

		public event NodeTreeInsertEventHandler Inserted
		{
			add    { _EventHandlerList.AddHandler    ( InsertedEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( InsertedEventKey, value ); }
		}

		public event EventHandler Cutting
		{
			add    { _EventHandlerList.AddHandler    ( CuttingEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( CuttingEventKey, value ); }
		}

		public event EventHandler CutDone
		{
			add    { _EventHandlerList.AddHandler    ( CutDoneEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( CutDoneEventKey, value ); }
		}

		public event NodeTreeNodeEventHandler Copying
		{
			add    { _EventHandlerList.AddHandler    ( CopyingEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( CopyingEventKey, value ); }
		}

		public event NodeTreeNodeEventHandler Copied
		{
			add    { _EventHandlerList.AddHandler    ( CopiedEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( CopiedEventKey, value ); }
		}

		public event NodeTreeNodeEventHandler DeepCopying
		{
			add    { _EventHandlerList.AddHandler    ( DeepCopyingEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( DeepCopyingEventKey, value ); }
		}

		public event NodeTreeNodeEventHandler DeepCopied
		{
			add    { _EventHandlerList.AddHandler    ( DeepCopiedEventKey, value ); }
			remove { _EventHandlerList.RemoveHandler ( DeepCopiedEventKey, value ); }
		}


//-----------------------------------------------------------------------------
// Validate

		protected virtual void OnValidate( INode node, object data )
		{
//			if ( data is RootObject ) return;

			if ( ! Root.IsTree ) throw new InvalidOperationException( "This is not a tree" );
			if ( data is INode ) throw new ArgumentException( "Object is a node" );

//			Delegate Validate = _EventHandlerList[ ValidateEventKey ];
//			if ( Validate != null ) Validate.DynamicInvoke( new object[]
//				{ node, new NodeTreeDataEventArgs( data ) } );

			NodeTreeDataEventHandler e = (NodeTreeDataEventHandler) _EventHandlerList[ ValidateEventKey ];
			if ( e != null ) e( node, new NodeTreeDataEventArgs( data ) );

			if ( ! IsRoot ) GetNodeTree( Root ).OnValidate( node, data );

			if ( ! DataType.IsInstanceOfType( data ) )
				throw new ArgumentException( "Object is not a " + DataType.Name );
		}

//-----------------------------------------------------------------------------
// Clear

		protected virtual void OnClearing( ITree tree )
		{
			EventHandler e = (EventHandler) _EventHandlerList[ ClearingEventKey ];
			if ( e != null ) e( tree, EventArgs.Empty );
		}

		protected virtual void OnCleared( ITree tree )
		{
			EventHandler e = (EventHandler) _EventHandlerList[ ClearedEventKey ];
			if ( e != null ) e( tree, EventArgs.Empty );
		}

//-----------------------------------------------------------------------------
// Set

		protected virtual void OnSetting( INode node, object data )
		{
			OnSettingCore( node, data, true );
		}

		protected virtual void OnSettingCore( INode node, object data, bool raiseValidate )
		{
			NodeTreeDataEventHandler e = (NodeTreeDataEventHandler) _EventHandlerList[ SettingEventKey ];
			if ( e != null ) e( node, new NodeTreeDataEventArgs( data ) );

			if ( ! IsRoot ) GetNodeTree( Root ).OnSettingCore( node, data, false );

			if ( raiseValidate ) OnValidate( node, data );
		}

		protected virtual void OnSetDone( INode node, object data )
		{
			OnSetDoneCore( node, data, true );
		}

		protected virtual void OnSetDoneCore( INode node, object data, bool raiseValidate )
		{
			NodeTreeDataEventHandler e = (NodeTreeDataEventHandler) _EventHandlerList[ SetDoneEventKey ];
			if ( e != null ) e( node, new NodeTreeDataEventArgs( data ) );

			if ( ! IsRoot ) GetNodeTree( Root ).OnSetDoneCore( node, data, false );

//			if ( raiseValidate ) OnValidate( Node, Data );
		}

//-----------------------------------------------------------------------------
// Insert

		protected virtual void OnInserting( INode oldNode, NodeTreeInsertOperation operation, INode newNode )
		{
			OnInsertingCore( oldNode, operation, newNode, true );
		}

		protected virtual void OnInsertingCore( INode oldNode, NodeTreeInsertOperation operation, INode newNode, bool raiseValidate )
		{
			NodeTreeInsertEventHandler e = (NodeTreeInsertEventHandler) _EventHandlerList[ InsertingEventKey ];
			if ( e != null ) e( oldNode, new NodeTreeInsertEventArgs( operation, newNode ) );

			if ( ! IsRoot ) GetNodeTree( Root ).OnInsertingCore( oldNode, operation, newNode, false );

			if ( raiseValidate ) OnValidate( oldNode, newNode.Data );

			if ( raiseValidate ) OnInsertingTree( newNode );
		}

		protected virtual void OnInsertingTree( INode newNode )
		{
			for ( INode child = newNode.Child ; child != null ; child = child.Next )
			{
				OnInsertingTree( newNode, child );

				OnInsertingTree( child );
			}
		}

		protected virtual void OnInsertingTree( INode newNode, INode child )
		{
			OnInsertingTreeCore( newNode, child, true );
		}

		protected virtual void OnInsertingTreeCore( INode newNode, INode child, bool raiseValidate )
		{
			NodeTreeInsertEventHandler e = (NodeTreeInsertEventHandler) _EventHandlerList[ InsertingEventKey ];
			if ( e != null ) e( newNode, new NodeTreeInsertEventArgs( NodeTreeInsertOperation.Tree, child ) );

			if ( ! IsRoot ) GetNodeTree( Root ).OnInsertingTreeCore( newNode, child, false );

			if ( raiseValidate ) OnValidate( newNode, child.Data );
		}

		protected virtual void OnInserted( INode oldNode, NodeTreeInsertOperation operation, INode newNode )
		{
			OnInsertedCore( oldNode, operation, newNode, true );
		}

		protected virtual void OnInsertedCore( INode oldNode, NodeTreeInsertOperation operation, INode newNode, bool raiseValidate )
		{
			NodeTreeInsertEventHandler e = (NodeTreeInsertEventHandler) _EventHandlerList[ InsertedEventKey ];
			if ( e != null ) e( oldNode, new NodeTreeInsertEventArgs( operation, newNode ) );

			if ( ! IsRoot ) GetNodeTree( Root ).OnInsertedCore( oldNode, operation, newNode, false );

//			if ( raiseValidate ) OnValidate( OldNode, NewNode.Data );

			if ( raiseValidate ) OnInsertedTree( newNode );
		}

		protected virtual void OnInsertedTree( INode newNode )
		{
			for ( INode child = newNode.Child ; child != null ; child = child.Next )
			{
				OnInsertedTree( newNode, child );

				OnInsertedTree( child );
			}
		}

		protected virtual void OnInsertedTree( INode newNode, INode child )
		{
			OnInsertedTreeCore( newNode, child, true );
		}

		protected virtual void OnInsertedTreeCore( INode newNode, INode child, bool raiseValidate )
		{
			NodeTreeInsertEventHandler e = (NodeTreeInsertEventHandler) _EventHandlerList[ InsertedEventKey ];
			if ( e != null ) e( newNode, new NodeTreeInsertEventArgs( NodeTreeInsertOperation.Tree, child ) );

			if ( ! IsRoot ) GetNodeTree( Root ).OnInsertedTreeCore( newNode, child, false );

//			if ( raiseValidate ) OnValidate( newNode, child.Data );
		}

//-----------------------------------------------------------------------------
// Cut

		protected virtual void OnCutting( INode oldNode )
		{
			EventHandler e = (EventHandler) _EventHandlerList[ CuttingEventKey ];
			if ( e != null ) e( oldNode, EventArgs.Empty );

			if ( ! IsRoot ) GetNodeTree( Root ).OnCutting( oldNode );
		}

		protected virtual void OnCutDone( INode oldRoot, INode oldNode )
		{
			EventHandler e = (EventHandler) _EventHandlerList[ CutDoneEventKey ];
			if ( e != null ) e( oldNode, EventArgs.Empty );

			if ( ! IsTree ) GetNodeTree( oldRoot ).OnCutDone( oldRoot, oldNode );
		}

//-----------------------------------------------------------------------------
// Copy

		protected virtual void OnCopying( INode oldNode, INode newNode )
		{
			OnCopyingCore( oldNode, newNode, true );
		}

		protected virtual void OnCopyingCore( INode oldNode, INode newNode, bool raiseValidate )
		{
			NodeTreeNodeEventHandler e = (NodeTreeNodeEventHandler) _EventHandlerList[ CopyingEventKey ];
			if ( e != null ) e( oldNode, new NodeTreeNodeEventArgs( newNode ) );

			if ( ! IsRoot ) GetNodeTree( Root ).OnCopyingCore( oldNode, newNode, false );

//			if ( raiseValidate ) OnValidate( oldNode, newNode.Data );
		}

		protected virtual void OnCopied( INode oldNode, INode newNode )
		{
			OnCopiedCore( oldNode, newNode, true );
		}

		protected virtual void OnCopiedCore( INode oldNode, INode newNode, bool raiseValidate )
		{
			NodeTreeNodeEventHandler e = (NodeTreeNodeEventHandler) _EventHandlerList[ CopiedEventKey ];
			if ( e != null ) e( oldNode, new NodeTreeNodeEventArgs( newNode ) );

			if ( ! IsRoot ) GetNodeTree( Root ).OnCopiedCore( oldNode, newNode, false );

//			if ( raiseValidate ) OnValidate( oldNode, newNode.Data );
		}

//-----------------------------------------------------------------------------
// DeepCopy

		protected virtual void OnDeepCopying( INode oldNode, INode newNode )
		{
			OnDeepCopyingCore( oldNode, newNode, true );
		}

		protected virtual void OnDeepCopyingCore( INode oldNode, INode newNode, bool raiseValidate )
		{
			NodeTreeNodeEventHandler e = (NodeTreeNodeEventHandler) _EventHandlerList[ DeepCopyingEventKey ];
			if ( e != null ) e( oldNode, new NodeTreeNodeEventArgs( newNode ) );

			if ( ! IsRoot ) GetNodeTree( Root ).OnDeepCopyingCore( oldNode, newNode, false );

//			if ( raiseValidate ) OnValidate( oldNode, newNode.Data );
		}

		protected virtual void OnDeepCopied( INode oldNode, INode newNode )
		{
			OnDeepCopiedCore( oldNode, newNode, true );
		}

		protected virtual void OnDeepCopiedCore( INode oldNode, INode newNode, bool raiseValidate )
		{
			NodeTreeNodeEventHandler e = (NodeTreeNodeEventHandler) _EventHandlerList[ DeepCopiedEventKey ];
			if ( e != null ) e( oldNode, new NodeTreeNodeEventArgs( newNode ) );

			if ( ! IsRoot ) GetNodeTree( Root ).OnDeepCopiedCore( oldNode, newNode, false );

//			if ( raiseValidate ) OnValidate( oldNode, newNode.Data );
		}

//-----------------------------------------------------------------------------

	} // class NodeTree

//-----------------------------------------------------------------------------

}


