using System;
using System.Linq;
using System.Collections.Generic;


class Process : State
{
	private void PropagateMovement(Movement m) {
            
        var outgoing = g.outgoing(m.target);
        foreach (var e in outgoing)
        {
            AddForce(new Force(e,m));
        }
	}

	//assumes v saturated (not sure if this includes incoming active forces, or just passive forces)
	private void PropagateForce(Vertex v) {
		var forces = ForcesOn (v);
		var movement = Force.Aggregate(forces);
		AddMovement (v, movement);
	}			

	//adds forces from movements
	private void PropagateMovements(){
		foreach (var m in unpropagatedMovements) {
			PropagateMovement (m);
		}
		unpropagatedMovements.Clear ();
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
	
}

