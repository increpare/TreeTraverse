using System;
using System.Linq;
using System.Collections.Generic;


class State
{
	public GameGraph s;

	public State Clone() {
		var s = new State ();
		s.s = s.s.Clone ();
		return s;
	}

	private void PropagateMovement(Movement m) {
            
        var outgoing = s.g.outgoing(m.target);
        foreach (var e in outgoing)
        {
			 s.AddForce(new Force(e,m));
        }
	}

	//assumes v saturated (not sure if this includes incoming active forces, or just passive forces)
	private void PropagateForce(Vertex v) {
		var forces = s.ForcesOn (v);
		var movement = Force.Aggregate(forces);
		s.AddMovement (v, movement);
	}			

	//adds forces from movements
	private void PropagateMovements(){
		foreach (var m in s.unpropagatedMovements) {
			PropagateMovement (m);
		}
		s.unpropagatedMovements.Clear ();
	}

	//adds movements to saturated vertices
	private void PropagateForces(){
		foreach (var v in s.NewlySaturatedVertices()) {
			PropagateForce (v);
		}
		s.unpropagatedForces.Clear ();
	}

	public void Propagate() {
		while (s.unpropagatedMovements.Any ()) {
			PropagateMovements ();
			PropagateForces ();
		}
	}
	
}

