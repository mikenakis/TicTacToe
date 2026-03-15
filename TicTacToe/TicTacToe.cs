namespace TicTacToe;

using MikeNakis.Kit.Extensions;
using static MikeNakis.Kit.GlobalStatics;

public sealed class TicTacToe
{
	readonly Board board = new Board();
	public char CurrentPlayer { get; private set; } = 'X';
	public char? Winner { get; private set; }

	public void Print()
	{
		board.Print();
	}

	public bool MakeMove( char c )
	{
		Assert( "123456789".Contains2( c ) );
		int n = c - '1';
		Assert( n is >= 0 and < 9 );
		(int x, int y) = getXAndYFromN( n );
		if( !board.IsValidMove( x, y ) )
			return false;
		if( board.MakeMoveAndCheckIfComplete( x, y, CurrentPlayer ) )
		{
			Winner = CurrentPlayer;
			return true;
		}
		CurrentPlayer = togglePlayer( CurrentPlayer );
		return true;
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

	public override string ToString()
	{
		return board.ToString();
	}
}
