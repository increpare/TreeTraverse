using System;


public class Outline
{

	public interface MyState {
		void Load(string s);
		void Propagate();
		void Presume(Assumption a);
		Edge UnspecifiedPassiveEdge();
		Assumption[] GenerateAssumptions(Edge E);
		MyState Clone();
		bool Finished();
		void SetState(MyState other);
	}

	public interface Graph {
	}

	public interface Edge {
	}

	public interface Assumption {
	}

	public void RunAlgorithm(MyState state) {
		Edge e = state.UnspecifiedPassiveEdge();
		if (e == null) {
			return;
		} 

		var assumptions = state.GenerateAssumptions (e);

		MyState[] newStates = new MyState[3];
		for (int i = 0; i < 2; i++) {
			var newState = state.Clone ();
			newState.Presume (assumptions [i]);
			newState.Propagate ();		
		}
	}

	public void DoStuff(MyState state) {
		state.Propagate();
		RunAlgorithm (state,null);
	}
}

