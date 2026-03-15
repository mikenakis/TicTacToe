namespace TicTacToe;

using MikeNakis.Kit;
using MikeNakis.Kit.Extensions;
using Sys = System;

sealed class TicTacToeMain
{
	public static void Main()
	{
		TicTacToe game = new TicTacToe();
		while( true )
		{
			game.Print();

			Sys.Console.Write( $"Player {game.CurrentPlayer} make a move: " );
			char c = Sys.Console.ReadKey().KeyChar;
			Sys.Console.WriteLine();
			if( !"123456789".Contains2( c ) )
			{
				Sys.Console.WriteLine( "Expected a number!" );
				continue;
			}
			if( !game.IsValidMove( c ) )
			{
				Sys.Console.WriteLine( "Invalid move!" );
				continue;
			}

			game.MakeMove( c );

			BoardStatus boardStatus = game.GetStatus();
			if( boardStatus != BoardStatus.InProgress )
			{
				game.Print();
				Sys.Console.WriteLine( "=============" );
				Sys.Console.WriteLine( getMessage( boardStatus ) );
				Sys.Console.WriteLine( "=============" );
				break;
			}
		}

		Sys.Console.Write( "Press [Enter] to terminate: " );
		Sys.Console.ReadLine();
	}

	static string getMessage( BoardStatus boardStatus )
	{
		return boardStatus switch
		{
			BoardStatus.InProgress => throw new AssertionFailureException(),
			BoardStatus.Draw => "It is a draw!",
			BoardStatus.XWins => "Player X wins!",
			BoardStatus.OWins => "Player O wins!",
			_ => throw new AssertionFailureException()
		};
	}
}
