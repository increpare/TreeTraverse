using System;
using System.Linq;
using System.Collections.Generic;


class State
{
	public GameGraph s;


	public static State NewState(){
		var s = new State ();

		s.s.g.vertices = new Vertex[]
		{
			new Vertex("player"),
			new Vertex("sausage1",true),
			new Vertex("sausage2"),
			//new Vertex("island"),
			new Vertex("ground")
		};

		s.s.g.edges = new Edge[] 
		{
			new Edge("ground","player",true),
			new Edge("ground","sausage1",true),
			new Edge("player","sausage1"),
			new Edge("sausage1","sausage2",true),
		//	new Edge("sausage2","island"),
		//	new Edge("island","sausage2"),
		};

		s.s.movements = new List<Movement>
		{
			new Movement(1,0,"player"),
			new Movement(0,0,"ground"),
		};
		s.s.unpropagatedMovements = new List<Movement>(s.s.movements);
		return s;
	}
	public State(){
		s = new GameGraph ();
	}
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
		var movement = Force.Aggregate(forces,v);
		movement.target = v;
		s.AddMovement (movement);
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
		var nsv = s.NewlySaturatedVertices ();
		foreach (var v in nsv) {
			PropagateForce (v);
		}
	}

	public void Propagate() {
		while (s.unpropagatedMovements.Any ()) {
			PropagateMovements ();
			PropagateForces ();
		}
	}	
}

