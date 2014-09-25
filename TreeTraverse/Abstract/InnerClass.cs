using System;
using System.Linq;
using System.Collections.Generic;

class InnerClass
{
	public interface Assumption {
	}

	public class MyState {
		Graph g;

		List<Movement> unpropagatedMovements;
		List<Force> unpropagatedForces;
		List<Movement> movements;
		List<Force> forces;

		private void PropagateMovement(Movement m) {
            var outgoing = g.outgoing(m.target);
            foreach (var e in outgoing)
            {
                AddForce(new Force(e,m));
            }
		}
			
		private void AddMovement(Vertex v, Movement m) {
		}

		private void AddForce(Force f) {
		}

		//assumes v saturated (not sure if this includes incoming active forces, or just passive forces)
		private void PropagateForce(Vertex v) {
			var forces = ForcesOn (v);
			var movement = AggregateForces (forces);
			AddMovement (v, movement);
		}
			
		private Movement AggregateForces(List<Force> forces) {
			return null;
		}

		private List<Force> ForcesOn(Vertex v) {
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
		private Force Force(Edge e) {
			return new Force();
		}

		private int Speed(Vertex v){
			return 0;
		}

		//traces backwards so long as everything before has an equal speed to this (and this has >0 speed)
		private List<Vertex> TraceBackwards(Vertex v, List<Vertex> vs) { 
			List<Vertex> result = new List<Vertex> (){v};          
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
                        Movement(f.target.from).speed==speed)
                    {
                        result.Add(f.target.from);
                        added = true;
                    }
                }
            }
            return result;
		}

		//adds forces from movements
		private void PropagateMovements(){
			foreach (var m in unpropagatedMovements) {
				PropagateMovement (m);
			}
			unpropagatedMovements.Clear ();
		}

		//should these care about active forces? i don't think so?
		private List<Vertex> NewlySaturatedVertices() {
            var result = new List<Vertex>();
            foreach (var v in g.vertices)
            {
                if (movements.Any(m => m.target == v))
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

		private List<Vertex> UnsaturatedVertices() {
			return null;
		}

		//adds movements to saturated vertices
		private void PropagateForces(){
			foreach (var v in NewlySaturatedVertices()) {
				PropagateForce (v);
			}

			unpropagatedForces.Clear ();
		}

		public void Propagate() {
			while (unpropagatedMovements.Any ()) {
				PropagateMovements ();
				PropagateForces ();
			}
		}

		public void Presume(Assumption a) {
		}

		public Edge UnspecifiedPassiveEdge() {
			return new Edge ();
		}

		//are there actually 3 possible assumptions here, or only 2? (not moving/moving in sync)
		//there's a possibility of moving at every slower speed, I guess. O_-
		//if it's moving faster, it may as well be doing nothing at all (other than nuking rolling ability -
		//so options are - move in sync, move at same speed without rolling,move at every speed below that until zero
		//non-moving should be the final option.
		//obviously should only roll if it *can* roll.
		public Assumption[] GenerateAssumptions(Edge E) {
			return new Assumption[0];
		}

		public MyState Clone(){
			return this;
		}

		public bool NoSlip(Edge E){
			return false;
		}

		public void SetState(MyState other){
		}
	}
}

