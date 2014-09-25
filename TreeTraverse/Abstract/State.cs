using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class State
{
    public Graph graph = new Graph();
    public List<Movement> movements = new List<Movement>();
    public List<Force> forces = new List<Force>();
    public List<Movement> unpropagatedmovements = new List<Movement>();

    public State Clone()
    {
        var s = new State();
        s.graph = graph;
        s.movements = new List<Movement>(movements);
        s.forces = new List<Force>(forces);
        s.unpropagatedmovements = new List<Movement>(unpropagatedmovements);
        return s;
    }

    public static State NewState(){
        var s = new State();
        /*
        graph.vertices = new List<Vertex>()
        {
            new Vertex("player"),
            new Vertex("sausage1"),
            new Vertex("sausage2"),
            new Vertex("sausage3"),
            new Vertex("sausage4")
        };

        graph.edges = new List<Edge>() {
            new Edge("player","sausage1"),
            new Edge("sausage1","sausage2"),
            new Edge("sausage1","sausage2"),
            new Edge("sausage2","sausage3",true),
            new Edge("sausage2","sausage4",true)
        };
        */

        s.graph.vertices = new Vertex[] 
        {
            new Vertex("player"),
            new Vertex("sausage1",true),
            new Vertex("sausage2"),
            new Vertex("island"),
            new Vertex("ground")
        };

        s.graph.edges = new Edge[] {
            new Edge("player","ground",true),
            new Edge("sausage1","ground",true),
            new Edge("player","sausage1"),
            new Edge("sausage1","sausage2",true),
            new Edge("sausage2","island"),
            new Edge("island","sausage2"),
        };

        s.movements = new List<Movement>() {
            new Movement(1,0,"player"),
        };
        
        s.unpropagatedmovements = new List<Movement>(s.movements);

        return s;
    }

/*
    private void AddUnderForces(Vertex v, List<Force> incoming)
    {
        var under = graph.Under(v);
        foreach (var g in under)
        {
            if (incoming.Any(F => F.target.from == g))
            {
                continue;
            }

            var m = new Movement(0, 0, g.name);
            var e = new Edge(g.name, v.name, true);
            var f = new Force(e, m);
            if (!movements.Contains(m))
            {
                movements.Add(m);
            }
            incoming.Add(f);
            //don't need to add to unpropagated movements
            forces.Add(f);
        }
    }*/

    private Movement ConsolidateForces(Vertex v, List<Force> incoming)
    {
        int speed = 0;
        int rolling = 0;

        List<Force> activeforces = incoming.Where(f => !f.target.passive ).ToList();
        List<Force> passiveforces = incoming.Where(f => f.target.passive).ToList();
        
        int activespeed = activeforces.Max(f => f.movement.speed);

        bool passiveParallel = passiveforces.All(f => f.movement.target.canRoll);
        var contactSpeeds = passiveforces.Select(f => f.movement.ContactSpeed()).Distinct();
        bool uniformContactSurface = contactSpeeds.Count()==1;
        int contactSpeed = contactSpeeds.Min();
        var rollings = passiveforces.Select(f => f.movement.rolling).Distinct();
        bool uniformPassiveRollings = rollings.Count() == 1;

        bool shouldRoll = v.canRoll && uniformContactSurface && uniformPassiveRollings;

        if (shouldRoll)
        {
            var underRoll = rollings.First();
            rolling = -underRoll;
            speed = contactSpeed + rolling;
            if (speed < activespeed)
            {
                rolling = 0;
                speed = activespeed;
            }
        }
        else
        {
            speed = Math.Max(contactSpeed,activespeed);
        }        

        Movement m = new Movement(speed,rolling,v.name);
        return m;
    }

    private void CalculateMovement(Vertex v) 
    {
        var incoming = forces.Where(f => f.target.to == v).ToList();
        var m = ConsolidateForces(v, incoming);
        movements.Add(m);
        unpropagatedmovements.Add(m);
    }

    public void PropagateMovements()
    {
        int unpropagatedCount = unpropagatedmovements.Count;

        foreach (var m in unpropagatedmovements)
        {
            var outs = graph.outgoing(m.target);            
            foreach (var o in outs){
                forces.Add(new Force(o,m));
            }
        }

        var forceTargets = graph.vertices.Where(v => forces.Any(f => f.target.to == v)).ToList();

        foreach (var forceTarget in forceTargets)
        {
            CalculateMovement(forceTarget);
        }

        unpropagatedmovements.RemoveRange(0, unpropagatedCount);
    }

}
