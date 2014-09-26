using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Graph
{
    public Vertex[] vertices;
    public Edge[] edges;

    public Edge[] outgoing(Vertex v)
    {
		return edges.Where (e => e.from == v).ToArray ();
    }
    public Edge[] incoming(Vertex v)
	{
		return edges.Where (e => e.to == v).ToArray ();
    }
    public Vertex[] outgoingV(Vertex v)
	{		
		return edges.Where (e => e.from == v).Select(e=>e.to).ToArray ();
    }
    public Vertex[] incomingV(Vertex v)
	{
		return edges.Where (e => e.to == v).Select(e=>e.from).ToArray ();
    }

}