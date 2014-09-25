using System;
using System.Linq;

public class Outline3
{

	public interface MyState {
		void Propagate();
		void Presume(Assumption a);
		Edge UnspecifiedPassiveEdge();

		//are there actually 3 possible assumptions here, or only 2? (not moving/moving in sync)
		//there's a possibility of moving at every slower speed, I guess. O_-
		//if it's moving faster, it may as well be doing nothing at all (other than nuking rolling ability -
		//so options are - move in sync, move at same speed without rolling,move at every speed below that until zero
		//non-moving should be the final option.
		//obviously should only roll if it *can* roll.
        //no, it should be in opposite order - go for lowest energy state
		Assumption[] GenerateAssumptions(Edge E);

		MyState Clone();
		bool NoSlip(Edge E);
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
		state.Propagate ();

		Edge e = state.UnspecifiedPassiveEdge();
		if (e == null) {
			return;
		} 

		var assumptions = state.GenerateAssumptions (e);

		MyState[] newStates = new MyState[assumptions.Length];
		for (int i = 0; i < assumptions.Length; i++) {
			var newState = newStates[i] = state.Clone ();
			newState.Presume (assumptions [i]);
			RunAlgorithm (newState);
			if (newState.NoSlip (e)) {
				state.SetState (newState);
				return;
			}			
		}

		state.SetState (newStates[0]);//first one will always assume non-rolling
	}

	public void DoStuff(MyState state) {
		RunAlgorithm (state);
	}
}

