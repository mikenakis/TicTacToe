namespace TicTacToe;

using MikeNakis.Kit.Extensions;
using static MikeNakis.Kit.GlobalStatics;
using Sys = System;

sealed class TicTacToeMain
{
	public static void Main()
	{
		Board board = new Board();

		char player = 'X';
		while( true )
		{
			board.Print();

			Sys.Console.Write( $"Player {player} make a move: " );
			char c = Sys.Console.ReadKey().KeyChar;
			Sys.Console.WriteLine();
			if( !"123456789".Contains2( c ) )
			{
				Sys.Console.WriteLine( "Expected a number!" );
				continue;
			}
			int n = c - '1';
			Assert( n is >= 0 and < 9 );
			(int x, int y) = getXAndYFromN( n );
			if( !board.IsValidMove( x, y ) )
			{
				Sys.Console.WriteLine( "Invalid move!" );
				continue;
			}

			if( board.MakeMoveAndCheckIfComplete( x, y, player ) )
			{
				board.Print();
				Sys.Console.WriteLine( "=============" );
				Sys.Console.WriteLine( $"Player {player} has won!" );
				Sys.Console.WriteLine( "=============" );
				break;
			}

			player = togglePlayer( player );
		}

		Sys.Console.Write( "Press [Enter] to terminate: " );
		Sys.Console.ReadLine();
	}

	static char togglePlayer( char player )
	{
		Assert( player is 'X' or 'O' );
		return player == 'X' ? 'O' : 'X';
	}

	static (int x, int y) getXAndYFromN( int n )
	{
		Assert( n is >= 0 and < 9 );
		int y = n / 3;
		int x = n % 3;
		return (x, y);
	}
}
