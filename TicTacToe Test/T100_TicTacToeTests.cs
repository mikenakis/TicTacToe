namespace TicTacToe_Test;

using VSTesting = Microsoft.VisualStudio.TestTools.UnitTesting;
using static MikeNakis.Kit.GlobalStatics;
using System.Linq;
using Math = System.Math;
using System.Collections.Generic;
using TicTacToe;
using System.Collections.Immutable;

[VSTesting.TestClass]
public sealed class T100_TicTacToeTests
{
	[VSTesting.TestMethod]
	public void T101_One_Happy_Path_Works()
	{
		TicTacToe game = new TicTacToe();
		Assert( game.ToString() == "123\n456\n789" );
		Assert( game.CurrentPlayer == 'X' );

		BoardStatus boardStatus;
		game.MakeMove( '5' );
		boardStatus = game.GetStatus();
		Assert( boardStatus == BoardStatus.InProgress );
		Assert( game.ToString() == "123\n4X6\n789" );
		Assert( game.CurrentPlayer == 'O' );

		game.MakeMove( '1' );
		boardStatus = game.GetStatus();
		Assert( boardStatus == BoardStatus.InProgress );
		Assert( game.ToString() == "O23\n4X6\n789" );
		Assert( game.CurrentPlayer == 'X' );

		game.MakeMove( '7' );
		boardStatus = game.GetStatus();
		Assert( boardStatus == BoardStatus.InProgress );
		Assert( game.ToString() == "O23\n4X6\nX89" );
		Assert( game.CurrentPlayer == 'O' );

		game.MakeMove( '3' );
		boardStatus = game.GetStatus();
		Assert( boardStatus == BoardStatus.InProgress );
		Assert( game.ToString() == "O2O\n4X6\nX89" );
		Assert( game.CurrentPlayer == 'X' );

		game.MakeMove( '2' );
		boardStatus = game.GetStatus();
		Assert( boardStatus == BoardStatus.InProgress );
		Assert( game.ToString() == "OXO\n4X6\nX89" );
		Assert( game.CurrentPlayer == 'O' );

		game.MakeMove( '9' );
		boardStatus = game.GetStatus();
		Assert( boardStatus == BoardStatus.InProgress );
		Assert( game.ToString() == "OXO\n4X6\nX8O" );
		Assert( game.CurrentPlayer == 'X' );

		game.MakeMove( '8' );
		boardStatus = game.GetStatus();
		Assert( boardStatus == BoardStatus.XWins );
		Assert( game.ToString() == "OXO\n4X6\nXXO" );
		Assert( game.CurrentPlayer == 'X' );
	}

	[VSTesting.TestMethod]
	public void T110_Regression_1()
	{
		// going from "O2OXXOXOX" to "OXOXXOXOX" should give a draw
		var game = new TicTacToe();
		game.SetBoardState( "O2OXXOXOX" );
		Assert( game.GetStatus() == BoardStatus.InProgress );
		Assert( game.CurrentPlayer == 'X' );
		game.MakeMove( '2' );
		BoardStatus boardStatus = game.GetStatus();
		Assert( boardStatus == BoardStatus.Draw );
	}

	[VSTesting.TestMethod]
	public void T102_All_Happy_Paths_Work()
	{
		var game = new TicTacToe();
		ImmutableArray<char[]> legalBoards = generateLegalBoards().ToImmutableArray();
		for( int i = 0; i < legalBoards.Length; i++ )
		{
			char[] board = legalBoards[i];
			string s = new string( board );
			//System.Diagnostics.Debug.WriteLine( s );
			game.SetBoardState( s );
			BoardStatus boardStatus1 = game.GetStatus();
			BoardStatus boardStatus2 = calculateBoardStatus( board );
			Assert( boardStatus1 == boardStatus2 );
		}
	}

	static IEnumerable<char[]> generateLegalBoards()
	{
		char[] symbols = { ' ', 'X', 'O' };
		int total = (int)Math.Pow( 3, 9 );

		for( int i = 0; i < total; i++ )
		{
			int n = i;
			char[] board = new char[9];

			for( int pos = 8; pos >= 0; pos-- )
			{
				board[pos] = symbols[n % 3];
				n /= 3;
			}

			if( isLegal( board ) )
				yield return board;
		}

		static bool isLegal( char[] board )
		{
			int xCount = board.Count( c => c == 'X' );
			int oCount = board.Count( c => c == 'O' );

			// turn order
			if( !(xCount == oCount || xCount == oCount + 1) )
				return false;

			bool xWin = isWinner( board, 'X' );
			bool oWin = isWinner( board, 'O' );

			// both players can't win
			if( xWin && oWin )
				return false;

			// winner must match move counts
			if( xWin && xCount != oCount + 1 )
				return false;

			if( oWin && xCount != oCount )
				return false;

			return true;
		}
	}

	static readonly int[][] winningBoards =
	{
		new[]{0,1,2}, new[]{3,4,5}, new[]{6,7,8}, // rows
        new[]{0,3,6}, new[]{1,4,7}, new[]{2,5,8}, // cols
        new[]{0,4,8}, new[]{2,4,6}                // diagonals
    };

	static BoardStatus calculateBoardStatus( char[] board )
	{
		if( isWinner( board, 'X' ) )
			return BoardStatus.XWins;
		if( isWinner( board, 'O' ) )
			return BoardStatus.OWins;
		if( isComplete( board ) )
			return BoardStatus.Draw;
		return BoardStatus.InProgress;

		static bool isComplete( char[] board )
		{
			for( int i = 0; i < board.Length; i++ )
				if( board[i] is not 'X' and not 'O' )
					return false;
			return true;
		}
	}

	static bool isWinner( char[] board, char player )
	{
		foreach( int[] line in winningBoards )
		{
			if( board[line[0]] == player &&
				board[line[1]] == player &&
				board[line[2]] == player )
				return true;
		}

		return false;
	}
}
