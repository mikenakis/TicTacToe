namespace TicTacToe;

using MikeNakis.Kit.Extensions;
using static MikeNakis.Kit.GlobalStatics;

public sealed class TicTacToe
{
	readonly Board board = new Board();
	public char CurrentPlayer { get; private set; } = 'X';

	public void Print()
	{
		board.Print();
	}

	public void SetBoardState( string s )
	{
		Assert( s.Length == 9 );
		board.SetBoardState( s );
	}

	public BoardStatus GetStatus()
	{
		return board.GetStatus();
	}

	public bool IsValidMove( char c )
	{
		Assert( "123456789".Contains2( c ) );
		int n = c - '1';
		Assert( n is >= 0 and < 9 );
		(int x, int y) = getXAndYFromN( n );
		return board.IsValidMove( x, y );
	}

	public void MakeMove( char c )
	{
		Assert( "123456789".Contains2( c ) );
		int n = c - '1';
		Assert( n is >= 0 and < 9 );
		(int x, int y) = getXAndYFromN( n );
		Assert( board.IsValidMove( x, y ) );
		board.MakeMove( x, y, CurrentPlayer );
		BoardStatus boardStatus = board.GetStatus();
		if( boardStatus == BoardStatus.InProgress )
			CurrentPlayer = togglePlayer( CurrentPlayer );
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
