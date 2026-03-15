namespace TicTacToe;

using System.Collections.Immutable;
using static MikeNakis.Kit.GlobalStatics;
using Sys = System;

sealed class Board
{
	readonly char[,] chars;

	public Board()
		: this( new string( ' ', 9 ) )
	{
	}

	public Board( string s )
	{
		chars = new char[3, 3];
		int i = 0;
		for( int y = 0; y < 3; y++ )
			for( int x = 0; x < 3; x++ )
			{
				char c = s[i];
				if( c == ' ' )
					c = (char)('1' + i);
				Assert( c is 'X' or 'O' or >= '0' and <= '9' );
				chars[y, x] = c;
				i++;
			}
	}

	public void Print()
	{
		for( int y = 0; y < 3; y++ )
		{
			if( y > 0 )
				Sys.Console.WriteLine( "---+---+---" );
			for( int x = 0; x < 3; x++ )
			{
				if( x > 0 )
					Sys.Console.Write( "|" );
				char c = chars[y, x];
				if( c is 'X' or 'O' )
					Sys.Console.ForegroundColor = Sys.ConsoleColor.Green;
				else
					Sys.Console.ForegroundColor = Sys.ConsoleColor.DarkGray;
				Sys.Console.Write( $" {c} " );
				Sys.Console.ForegroundColor = Sys.ConsoleColor.White;
			}
			Sys.Console.WriteLine();
		}
		Sys.Console.WriteLine();
	}

	sealed class Pathway
	{
		public readonly int StartX;
		public readonly int StartY;
		public readonly int DeltaX;
		public readonly int DeltaY;

		public Pathway( int startX, int startY, int deltaX, int deltaY )
		{
			StartX = startX;
			StartY = startY;
			DeltaX = deltaX;
			DeltaY = deltaY;
		}

		public bool Contains( int x, int y )
		{
			int xi = StartX;
			int yi = StartY;
			for( int i = 0; i < 3; i++ )
			{
				if( xi == x && yi == y )
					return true;
				xi += DeltaX;
				yi += DeltaY;
			}
			return false;
		}
	}

	static readonly ImmutableArray<Pathway> pathways =
	[
		new Pathway( startX: 0, startY: 0, deltaX: 1, deltaY: 0 ),
		new Pathway( startX: 0, startY: 1, deltaX: 1, deltaY: 0 ),
		new Pathway( startX: 0, startY: 2, deltaX: 1, deltaY: 0 ),
		new Pathway( startX: 0, startY: 0, deltaX: 0, deltaY: 1 ),
		new Pathway( startX: 1, startY: 0, deltaX: 0, deltaY: 1 ),
		new Pathway( startX: 2, startY: 0, deltaX: 0, deltaY: 1 ),
		new Pathway( startX: 0, startY: 0, deltaX: 1, deltaY: 1 ),
		new Pathway( startX: 2, startY: 0, deltaX: -1, deltaY: 1 ),
	];

	public bool IsValidMove( int x, int y )
	{
		return chars[y, x] is not 'X' or 'O';
	}

	public bool MakeMoveAndCheckIfComplete( int x, int y, char c )
	{
		Assert( c is 'X' or 'O' );
		Assert( IsValidMove( x, y ) );
		chars[y, x] = c;

		foreach( Pathway pathway in pathways )
			if( pathway.Contains( x, y ) )
				if( isComplete( pathway ) )
					return true;
		return false;

		bool isComplete( Pathway pathway )
		{
			int x = pathway.StartX;
			int y = pathway.StartY;
			char c = chars[y, x];
			if( c is not 'X' or 'O' )
				return false;
			for( int i = 1; i < 3; i++ )
			{
				x += pathway.DeltaX;
				y += pathway.DeltaY;
				if( chars[y, x] != c )
					return false;
			}
			return true;
		}
	}

	public override string ToString()
	{
		return $"{chars[0, 0]}{chars[0, 1]}{chars[0, 2]}\n" +
			   $"{chars[1, 0]}{chars[1, 1]}{chars[1, 2]}\n" +
			   $"{chars[2, 0]}{chars[2, 1]}{chars[2, 2]}";
	}
}
