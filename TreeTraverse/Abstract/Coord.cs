using System.Collections.Generic;
using System;

public struct Coord
{		
	public readonly int x;
	public readonly int y;
	public readonly int z;
	
	public static Coord North = new Coord(0,1,0);
	public static Coord South = new Coord(0,-1,0);
	public static Coord East = new Coord(1,0,0);
	public static Coord West = new Coord(-1,0,0);
	public static Coord Up = new Coord(0,0,1);
	public static Coord Down = new Coord(0,0,-1);
	
	public static Coord Zero = new Coord(0,0,0);
	public static Coord One = new Coord(1,1,1);
	
	public static Coord Invalid = new Coord(int.MinValue,int.MinValue,int.MinValue);
	
	public Coord(int _x, int _y, int _z=0)
	{
		x=_x;
		y=_y;
		z=_z;
	}	
	
	public Coord FlipH()
	{
		return new Coord(-x,y,z);
	}
	
	
	public Coord FlipV()
	{
		return new Coord(x,-y,z);
	}
	
	
	public Coord RotateClockwise()
	{
		return new Coord(-y,x,z);
	}
	public static int Dot (Coord a, Coord b)
	{
		return a.x*b.x+a.y*b.y+a.z*b.z;
	}
	
	public override string ToString()
	{
		return "("+x+","+y+","+z+")";
	}
	
	public bool Above(Coord other)
	{
		return x==other.x && y==other.y&z>=other.z;
	}
	
	public override int GetHashCode ()
	{
		return x+y*100+z*10000;
	}
	
	public override bool Equals(System.Object obj) 
	{
		//Check for null and compare run-time types. 
		if (obj == null || this.GetType() != obj.GetType()) 
		{ 
			return false;
		}   
		else 
		{
			Coord p = (Coord) obj;
			return (x == p.x) && (y == p.y) && (z == p.z);
		}
	}

	public int AmountInDirection(Direction dir)
	{
		switch (dir)
		{
		case Direction.North:
			return -y;
		case Direction.South:
			return y;
		case Direction.West:
			return -x;
		case Direction.East:
			return x;
		case Direction.Up:
			return -z;
		case Direction.Down:
			return z;
		default:
			Console.WriteLine("eep " + dir);
			break;
		}
		
		return 0;
	}
	public static Coord operator +(Coord a, Coord b) 
   	{
		return new Coord(a.x+b.x,a.y+b.y,a.z+b.z);
   	}	
	
	public static Coord operator +(Coord a, Direction b) 
   	{
		return a+b.ToCoord();
   	}
	
	public static Coord operator -(Coord a, Direction b) 
   	{
		return a-b.ToCoord();
   	}
	
	
	public static Coord operator -(Coord a, Coord b) 
   	{
		return new Coord(a.x-b.x,a.y-b.y,a.z-b.z);
   	}
	
	public static Coord operator *(int n, Coord b) 
   	{
		return new Coord(n*b.x,n*b.y,n*b.z);
   	}
	
	
	public static Coord operator /(Coord a, int b) 
   	{
		return new Coord(a.x/b,a.y/b,a.z/b);
   	}
	
	public static bool operator ==(Coord a, Coord b) 
   	{
		return a.x==b.x&&a.y==b.y&&a.z==b.z;
   	}
			
	public static bool operator !=(Coord a, Coord b) 
   	{
		return !(a==b);
   	}	
	

	
	public static int DistanceSq(Coord a, Coord b)
	{
		Coord d = b-a;
		return d.MagnitudeSq();
	}
	
	public static float Distance(Coord a, Coord b)
	{
		Coord d = b-a;
		return d.Magnitude();
	}
	
	public int MagnitudeSq()
	{
		return x*x+y*y+z*z;
	}
	
	public float Magnitude()
	{
		return (float)System.Math.Sqrt(x*x+y*y+z*z);
	}

	
}


