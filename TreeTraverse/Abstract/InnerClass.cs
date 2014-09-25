using System;
using System.Linq;
using System.Collections.Generic;


class InnerClass : ProcessState
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
                    getMovement(f.target.from).speed==speed)
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

