namespace TicTacToe_Test;

using MikeNakis.Kit.Extensions;
using Sys = System;
using VSTesting = Microsoft.VisualStudio.TestTools.UnitTesting;
using static MikeNakis.Kit.GlobalStatics;

[VSTesting.TestClass]
public sealed class T100_TicTacToeTests
{
	[VSTesting.TestMethod]
	public void T101_Happy_Path_Works()
	{
		var game = new TicTacToe.TicTacToe();
		Assert( game.ToString() == "123\n456\n789" );
		Assert( game.Winner == null );
		Assert( game.CurrentPlayer == 'X' );

		Assert( game.MakeMove( '5' ) );
		Assert( game.ToString() == "123\n4X6\n789" );
		Assert( game.Winner == null );
		Assert( game.CurrentPlayer == 'O' );

		Assert( game.MakeMove( '1' ) );
		Assert( game.ToString() == "O23\n4X6\n789" );
		Assert( game.Winner == null );
		Assert( game.CurrentPlayer == 'X' );

		Assert( game.MakeMove( '7' ) );
		Assert( game.ToString() == "O23\n4X6\nX89" );
		Assert( game.Winner == null );
		Assert( game.CurrentPlayer == 'O' );

		Assert( game.MakeMove( '3' ) );
		Assert( game.ToString() == "O2O\n4X6\nX89" );
		Assert( game.Winner == null );
		Assert( game.CurrentPlayer == 'X' );

		Assert( game.MakeMove( '2' ) );
		Assert( game.ToString() == "OXO\n4X6\nX89" );
		Assert( game.Winner == null );
		Assert( game.CurrentPlayer == 'O' );

		Assert( game.MakeMove( '9' ) );
		Assert( game.ToString() == "OXO\n4X6\nX8O" );
		Assert( game.Winner == null );
		Assert( game.CurrentPlayer == 'X' );

		Assert( game.MakeMove( '8' ) );
		Assert( game.ToString() == "OXO\n4X6\nXXO" );
		Assert( game.Winner == 'X' );
		Assert( game.CurrentPlayer == 'X' );
	}
}
