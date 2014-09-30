using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

struct Vertex
{
    public readonly string name;
    public readonly bool canRoll;

    public Vertex(string name, bool canRoll=false)
    {
        this.name = name;
        this.canRoll = canRoll;

        Find[name] = this;
    }

    public override int GetHashCode()
    {
        return name.GetHashCode();
    }

    public static bool operator ==(Vertex v1, Vertex v2)
    {
        return v1.name == v2.name;
    }


    public static bool operator !=(Vertex v1, Vertex v2)
    {
        return v1.name != v2.name;
    }

    public override bool Equals(Object obj)
    {
        if (obj == null || !(obj is Vertex))
            return false;
        else
            return name == ((Vertex)obj).name;
    }      



    public static Dictionary<string, Vertex> Find = new Dictionary<string, Vertex>();


	public override string ToString ()
	{
		return name.ToString ()+(canRoll?" * ":"");
	}
}
