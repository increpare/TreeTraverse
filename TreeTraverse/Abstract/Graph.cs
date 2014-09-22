using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Graph
{
    public List<Edge> edges;
    public List<Vertex> vertices;

    public List<Edge> outgoing(Vertex v) {
        return edges.Where(e => e.from == v).ToList();
    }

    public List<Edge> incoming(Vertex v) {
        return edges.Where(e => e.to == v).ToList();
    }


    public List<Vertex> Under(Vertex v1) {
        return vertices.Where(v2 => edges.Any(e => e.passive && e.from == v2 && e.to == v1)).ToList();
    }
}
