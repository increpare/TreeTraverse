using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeTraverse.Abstract
{
    class ProcessState
    {
        Graph g;

        List<Movement> unpropagatedMovements;
        List<Force> unpropagatedForces;
        List<Movement> movements;
        List<Force> forces;

        private void AddMovement(Vertex v, Movement m)
        {
        }

        private void AddForce(Force f)
        {
        }

        private List<Force> ForcesOn(Vertex v)
        {
            return null;
        }

        private Movement Movement(Vertex v)
        {
            return null;
        }

        private Force Force(Vertex from, Vertex to)
        {
            return new Force();
        }

        private Force Force(Edge e)
        {
            return new Force();
        }

        private int Speed(Vertex v)
        {
            return 0;
        }

        //traces backwards so long as everything before has an equal speed to this (and this has >0 speed)
        private List<Vertex> TraceBackwards(Vertex v, List<Vertex> vs)
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
                        Movement(f.target.from).speed == speed)
                    {
                        result.Add(f.target.from);
                        added = true;
                    }
                }
            }
            return result;
        }

        //should these care about active forces? i don't think so?
        private List<Vertex> NewlySaturatedVertices()
        {
            var result = new List<Vertex>();
            foreach (var v in g.vertices)
            {
                if (Movement(v)!=null)
                {
                    continue;
                }
                if (ForcesOn(v).Count == g.outgoing(v).Length)
                {
                    result.Add(v);
                }
            }
            return result;
        }

        private List<Vertex> UnsaturatedVertices()
        {
            return null;
        }

        //adds movements to saturated vertices
        private void PropagateForces()
        {
            foreach (var v in NewlySaturatedVertices())
            {
                PropagateForce(v);
            }

            unpropagatedForces.Clear();
        }

        public void Propagate()
        {
            while (unpropagatedMovements.Any())
            {
                PropagateMovements();
                PropagateForces();
            }
        }

        public void Presume(Assumption a)
        {
        }

        public Edge UnspecifiedPassiveEdge()
        {
            return new Edge();
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

        public MyState Clone()
        {
            return this;
        }

        public bool NoSlip(Edge E)
        {
            return false;
        }

        public void SetState(MyState other)
        {
        }
    }
}
