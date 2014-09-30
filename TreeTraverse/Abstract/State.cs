using System;
using System.Linq;
using System.Collections.Generic;

class State
{
	public GameGraph s;


	public static void ProcessString(string dat, State s){

		dat = dat.Trim();
		var lines = dat.Split(new char[]{'\n'},StringSplitOptions.RemoveEmptyEntries).Select(str=>str.Trim()).ToList();
		var lineTokens = lines.Select(l=>l.Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries)).ToList();
		s.s.g.vertices = lineTokens[0].Select( tokenName => new Vertex(tokenName) ).ToArray();
		List<Edge> eList = new List<Edge>();
		List<Movement> mList = new List<Movement>();

		var section = 1;
		for (int i=2;i<lines.Count;i++){
			if (lines[i].Trim()=="---"){
				section++;
				continue;
			}
			var lt = lineTokens[i];
			if (section==1){
				var e = new Edge(lt[0],lt[1],lt.Length>2?bool.Parse(lt[2]):false);
				eList.Add (e);
			} else if (section==2){
				var m = new Movement (lt[0],int.Parse (lt [1]), int.Parse (lt [2]));
				mList.Add (m);
			}
		}
		s.s.g.edges = eList.ToArray ();
		s.s.movements = mList;
		s.s.unpropagatedMovements = new List<Movement> (s.s.movements);
	}

	public static State NewState(string dat){
		var s = new State ();

		ProcessString (dat, s);
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

