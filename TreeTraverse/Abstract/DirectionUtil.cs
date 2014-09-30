using System.Collections.Generic;


public static class DirectionUtil
{	
	//a,b at 45 from eachother, or 90
	public static bool LeftOf( this Direction a, Direction b)
	{		
		if (a.Ortho()&&b.Ortho())
		{
			return a.RotClockwise90() == b;
		}
		else
		{
			return DirectionUtil.ContinueRot(a,b) == a.RotClockwise90();
		}
	}
	
	private static int[,] crosses = {
	//	N	S	W	E	NE	SW	NW	SE	0	D	U
	{	8,	8,	10,	9,	0,	0,	0,	0,	8,	0,	0},	//N	 0
	{	8,	8,	9,	10,	0,	0,	0,	0,	8,	0,	0},	//S	 1
	{	9,	10,	8,	8,	0,	0,	0,	0,	8,	0,	0},	//W	 2
	{	10,	9,	8,	8,	0,	0,	0,	0,	8,	0,	0},	//E	 3
	{	0,	0,	0,	0,	8,	0,	0,	0,	8,	0,	0},	//NE 4
	{	0,	0,	0,	0,	0,	8,	0,	0,	8,	0,	0},	//SW 5
	{	0,	0,	0,	0,	0,	0,	8,	0,	8,	0,	0},	//NW 6
	{	0,	0,	0,	0,	0,	0,	0,	8,	8,	0,	0},	//SE 7
	{	8,	8,	8,	8,	8,	8,	8,	8,	8,	8,	8},	//0	 8
	{	0,	0,	0,	0,	0,	0,	0,	0,	8,	8,	8},	//D	 9
	{	0,	0,	0,	0,	0,	0,	0,	0,	8,	8,	8},	//U	 10
	};
	
	public static Direction Cross(this Direction a, Direction b)
	{
		return (Direction)crosses[(int)a,(int)b];
	}
	public static bool Vertical(this Direction a)
	{
		return (int)a>8;
	}
	
	public static bool Horizontal(this Direction a)
	{
		return (int)a<=8;
	}
	
	public static bool Flat(this Direction a)
	{
		return (int)a<8;
	}
	
	//return diagonals
	private static int[,]  rotbetweens= new int[,]{
		{-1,-1,6,4},
		{-1,-1,5,7},
		{6,5,-1,-1},
		{4,7,-1,-1},	
	};
			
	public static Direction RotBetween(Direction a, Direction b)
	{
		return (Direction) rotbetweens[(int)a,(int)b];
	}
	
	private static int[,] continuerot = new int[,]{
		{8,8,8,8,6,8,4,8},
		{8,8,8,8,8,7,8,5},
		{8,8,8,8,8,6,5,8},
		{8,8,8,8,7,8,8,4},
		{3,8,8,0,8,8,8,8},
		{8,2,1,8,8,8,8,8},
		{2,8,0,8,8,8,8,8},
		{8,3,8,1,8,8,8,8},
	};
	
	
	public static bool ParallelTo(this Direction a, Direction b)
	{
		if (!a.Valid() || !b.Valid())
			return false;
		
		return a==b || a.Inverse()==b;
	}
	
	public static bool OrthoTo(this Direction a, Direction b)
	{
		return !a.ParallelTo(b);
	}
	
	public static bool NormalTo(this Direction a, Direction b)
	{
		return !a.ParallelTo(b) && a!=Direction.None && b!=Direction.None;
	}
	
	public static bool Diagonal(this Direction d)
	{
		return (int)d>=4 && (int)d<8;
	}
	
	
	
	public static bool Ortho(this Direction d)
	{
		return (int)d<4;
	}
	
	public static Direction ContinueRot(Direction from, Direction to)
	{
		return (Direction) continuerot[(int)to,(int)from];
	}
	
	public static bool Valid(this Direction a)
	{
		return a != Direction.None;
	}
	
	public static bool Invalid(this Direction a)
	{
		return !Valid(a);
	}
	private static int[] invrot = {
		1,0,3,2, 5,4,7,6, 8, 10,9
	};
		
	public static Direction Inverse(this Direction d)
	{
		return (Direction)invrot[(int)d];
	}
	
	private static int[] rotclockwise = {
		3,2,0,1, 7,6,4,5, 8, 9,10
	};
	
	public static Direction RotClockwise90(this Direction d)
	{
		return (Direction)rotclockwise[(int)d];
	}
	
		
	private static int[] fliph = {
		//0 N
		0,
		//1 S
		1,
		//2 E
		3,
		//3 W
		2, 
		//4 NE
		7,
		//5 SW
		6,
		//6 SE
		5,
		//7 NW
		4, 
		//8
		8, 
		//9
		9,
		//10
		10
	};
	
	private static int[] flipv = {
		//0 N
		1,
		//1 S
		0,
		//2 E
		2,
		//3 W
		3, 
		//4 NE
		6,
		//5 SW
		7,
		//6 SE
		4,
		//7 NW
		5, 
		//8
		8, 
		//9
		9,
		//10
		10
	};
	
	public static Direction FlipH(this Direction d)
	{
		return (Direction)fliph[(int)d];
	}
	
	public static Direction FlipV(this Direction d)
	{
		return (Direction)flipv[(int)d];
	}
	
	private static int[] rotclockwise45 = {
		//0 N
		4,
		//1 S
		5,
		//2 E
		6,
		//3 W
		7, 
		//4 NE
		2,
		//5 SW
		3,
		//6 SE
		1,
		//7 NW
		0, 
		//8
		8, 
		//9
		9,
		//10
		10
	};
	
	public static Direction RotClockwise45(this Direction d)
	{
		return (Direction)rotclockwise45[(int)d];
	}
	
	//clockwise/anticlockwise
	public static bool RotDir(Direction a, Direction b)
	{
		if (a.LeftOf(b))
			return true;
		else
			return false;
	}
	
	public static Direction Rot90(this Direction d, bool clockwise)//1 is clockwise	,-1 anti	
	{
		if (clockwise)
		{
			return d.RotClockwise90();
		}
		else
		{
			return d.RotClockwise90().Inverse();
		}
	}
	
	private static int[] rotcounterclockwise = {
		2,3,1,0, 6,7,5,4, 8, 9,10
	};
	
	public static Direction RotCounterclockwise90(this Direction d)
	{
		return (Direction)rotcounterclockwise[(int)d];
	}
	
		
	private static int[] rotcounterclockwise45 = {
		//0 N
		7,
		//1 S
		6,
		//2 E
		4,
		//3 W
		5, 
		//4 NE
		0,
		//5 SW
		1,
		//6 SE
		2,
		//7 NW
		3, 
		//8
		8, 
		//9
		9,
		//10
		10
	};
	
	public static Direction RotCounterclockwise45(this Direction d)
	{
		return (Direction)rotcounterclockwise45[(int)d];
	}		
	
	private static Coord[] coordrep = {
		new Coord(0,-1,0),	//n
		new Coord(0,1,0),	//s
		new Coord(-1,0,0),	//e	
		new Coord(1,0,0),	//w
		new Coord(1,-1,0),	//ne
		new Coord(-1,1,0),	//sw
		new Coord(-1,-1,0),	//nw
		new Coord(1,1,0),	//se
		new Coord(0,0,0),//NONE #8
		new Coord(0,0,-1),	//d
		new Coord(0,0,1)	//u
	};

	
	public static Coord ToCoord(this Direction d)
	{
		return coordrep[(int)d];
	}

}

