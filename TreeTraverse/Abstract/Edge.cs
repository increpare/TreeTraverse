using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Edge
{
    public readonly Vertex from;
    public readonly Vertex to;
    public readonly bool passive;

    public Edge(string from, string to, bool passive=false)
    {
        this.from = Vertex.Find[from];
        this.to = Vertex.Find[to];
        this.passive = passive;
    }
	public override int GetHashCode()
	{
		return from.GetHashCode()*to.GetHashCode()+passive.GetHashCode();
	}

	public static bool operator ==(Edge v1, Edge v2)
	{
		return 
			v1.from == v2.from &&
			v1.to == v2.to &&
			v1.passive == v2.passive;
	}


	public static bool operator !=(Edge v1, Edge v2)
	{
		return 
			v1.from != v2.from ||
			v1.to != v2.to ||
			v1.passive != v2.passive;
	}

	public override bool Equals(Object obj)
	{
		if (obj == null || !(obj is Vertex))
			return false;
		else {
			Edge o = (Edge)obj;
			return from == o.from && to==o.to&&passive==o.passive;
		}
	}      

}
