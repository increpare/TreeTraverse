using System;
using System.Linq;
using System.Collections.Generic;

public class InnerClass
{
	public interface Assumption {
	}

	public class Graph{
		public Vertex[] vertices;
		public Edge[] edges;

		public Edge[] outgoing(Vertex v) {
			return new Edge[0];
		}
		public Edge[] incoming(Vertex v) {
			return new Edge[0];
		}
		public Vertex[] outgoingV(Vertex v) {
			return new Vertex[0];
		}
		public Vertex[] incomingV(Vertex v) {
			return new Vertex[0];
		}

	}

	public interface Vertex{
	}

	public class Edge {
		public Vertex from;
		public Vertex to;
	}

	public class Force {
		public Edge target;
		public bool passive;
	}

	public class Movement {
		public Vertex target;
		public bool DependsOn (Force f) {
			return false;
		}
	}


	public class MyState {
		Graph g;

		List<Movement> unpropagatedMovements;
		List<Force> unpropagatedForces;
		List<Movement> movements;
		List<Force> forces;

		private void PropagateMovement(Movement m) {
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
			return new Movement ();
		}

		private List<Force> ForcesOn(Vertex v) {
			
		}

		private Movement Movement(Vertex from, Vertex to) {
			return null;
		}
		private Movement Movement(Edge e) {
			return null;
		}

		private int Speed(Vertex v){
			return 0;
		}

		//traces backwards so long as everything before has an equal speed to this (and this has >0 speed)
		private List<Vertex> TraceBackwards(Vertex v, List<Vertex> vs) { 
			List<Vertex> result = new List<Vertex> (){};
			int speed = Speed (v);
			if (speed == 0) {
				return result;
			}
				
			var candidates = g.incomingV (v).Where (w => Speed (w) == speed);

			bool anyadded = false;
			foreach (var cand in candidates) {
				if (cand in vs) {
					continue;
				}
			}
			result = result.Concat (connectedFrom).Distinct().ToList();

			//try to do this recursively
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
			return null;
		}
		private List<Vertex> UnsaturatedVertices() {
			return null;
		}

		//adds movements to saturated vertices
		private void PropagateForces(){
			foreach (var v in NewlySaturatedVertices()) {
				PropagateForces (v);
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

