using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class GameGraph
{
    public Graph g;

    public List<Movement> unpropagatedMovements;
    public List<Movement> movements;
    public List<Force> forces;

	public GameGraph(){
		g = new Graph ();
		unpropagatedMovements = new List<Movement> ();
		movements = new List<Movement> ();
		forces = new List<Force> ();
	}

    public void AddMovement(Movement m)
    {
		movements.Add (m);
		unpropagatedMovements.Add (m);
    }

    public void AddForce(Force f)
    {
		forces.Add (f);
    }

    public List<Force> ForcesOn(Vertex v)
    {
		return forces.Where (f => f.target.to == v).ToList ();
    }

    public Movement getMovement(Vertex v)
    {
		return movements.FirstOrDefault (m => m.target == v);
    }

    public Force getForce(Vertex from, Vertex to)
    {
        return new Force();
    }

    public Force getForce(Edge e)
    {
        return new Force();
    }

    public int Speed(Vertex v)
    {
        return 0;
    }

    //traces backwards so long as everything before has an equal speed to this (and this has >0 speed)
    public List<Vertex> TraceBackwards(Vertex v, List<Vertex> vs)
    {
        List<Vertex> result = new List<Vertex>() { v };
        int speed = Speed(v);
        if (speed == 0)
        {
            return result;
        }

        bool added = true;
        while (added)
        {
            added = false;
            foreach (var f in forces)
            {
                if (!f.target.passive &&
                    result.Contains(f.target.to) &&
                    !result.Contains(f.target.from) &&
                    getMovement(f.target.from).speed == speed)
                {
                    result.Add(f.target.from);
                    added = true;
                }
            }
        }
        return result;
    }

    //should these care about active forces? i don't think so?
    public List<Vertex> NewlySaturatedVertices()
    {
        var result = new List<Vertex>();
        foreach (var v in g.vertices)
        {
			if (getMovement(v)!=null)
            {
                continue;
            }
			var forcesOn = ForcesOn (v);
			var inComing = g.incoming (v);
			if ((inComing.Length>0)&&(inComing.Length==forcesOn.Count))
            {
                result.Add(v);
            }
        }
        return result;
    }

    public void Presume(Assumption a)
    {
    }

    public Edge UnspecifiedPassiveEdge()
    {
        return null;
    }

    //are there actually 3 possible assumptions here, or only 2? (not moving/moving in sync)
    //there's a possibility of moving at every slower speed, I guess. O_-
    //if it's moving faster, it may as well be doing nothing at all (other than nuking rolling ability -
    //so options are - move in sync, move at same speed without rolling,move at every speed below that until zero
    //non-moving should be the final option.
    //obviously should only roll if it *can* roll.
    public Assumption[] GenerateAssumptions(Edge E)
    {
        return new Assumption[0];
    }

    public GameGraph Clone()
    {
        return this;
    }

    public bool NoSlip(Edge E)
    {
        return false;
    }

    public void SetState(GameGraph other)
    {
    }
}
