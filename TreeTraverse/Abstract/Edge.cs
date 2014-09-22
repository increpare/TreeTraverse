using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

struct Edge
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
}
