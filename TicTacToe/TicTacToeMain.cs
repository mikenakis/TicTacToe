namespace TicTacToe;

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

			if( !game.MakeMove( c ) )
			{
				Sys.Console.WriteLine( "Invalid move!" );
				continue;
			}

			if( game.Winner != null )
			{
				game.Print();
				Sys.Console.WriteLine( "=============" );
				Sys.Console.WriteLine( $"Player {game.Winner} has won!" );
				Sys.Console.WriteLine( "=============" );
				break;
			}
		}

		Sys.Console.Write( "Press [Enter] to terminate: " );
		Sys.Console.ReadLine();
	}
}
