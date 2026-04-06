namespace TicTacToe;

using MikeNakis.Kit;
using Sys = System;

sealed class TicTacToeMain
{
	public static void Main()
	{
		TicTacToe game = new TicTacToe();
		while( checkForNextMove( game ) )
		{
			game.Print();
			Sys.Console.Write( $"Player {game.CurrentPlayer} make a move: " );
			char c = readCharacter();
			Sys.Console.WriteLine();

			if( !game.IsValidMove( c ) )
			{
				Sys.Console.WriteLine( "Expected a number!" );
				continue;
			}
			if( !game.IsLegalMove( c ) )
			{
				Sys.Console.WriteLine( "Not a legal move!" );
				continue;
			}

			game.MakeMove( c );
		}

		Sys.Console.Write( "Press [Enter] to terminate: " );
		Sys.Console.ReadLine();
		return;

		static bool checkForNextMove( TicTacToe game )
		{
			BoardStatus boardStatus = game.GetStatus();
			if( boardStatus == BoardStatus.InProgress )
				return true;
			game.Print();
			Sys.Console.WriteLine( "=============" );
			Sys.Console.WriteLine( getMessage( boardStatus ) );
			Sys.Console.WriteLine( "=============" );
			return false;
		}

		static char readCharacter()
		{
			char c = Sys.Console.ReadKey( intercept: true ).KeyChar;
			Sys.Console.Write( $"'{c}'" );
			return c;
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
}

