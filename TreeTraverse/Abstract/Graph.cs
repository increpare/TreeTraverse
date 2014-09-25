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
        return new Edge[0];
    }
    public Edge[] incoming(Vertex v)
    {
        return new Edge[0];
    }
    public Vertex[] outgoingV(Vertex v)
    {
        return new Vertex[0];
    }
    public Vertex[] incomingV(Vertex v)
    {
        return new Vertex[0];
    }

}