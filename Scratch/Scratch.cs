#pragma warning disable IDE0211 // Convert to 'Program.Main' style program

int i = 5;
int j = 10;

string s = "hello!";

int k = i + j;
char c = 'a';

System.Console.WriteLine( $"1. i = {k}" );
System.Console.WriteLine( $"2. {s[0]}{s[2]}{s[5]} {s.Length} {s.Substring( 2, 2 )} {s.ToUpperInvariant()}" );
System.Console.WriteLine( $"3. {s}, {c}" );

for( i = 33; i < 65536; i++ )
	System.Console.Write( $"{(char)i} " );
System.Console.WriteLine( "" );

System.Console.ReadLine();
