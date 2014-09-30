using System;
using System.Linq;

static class Outline
{

	public static void RunAlgorithm(State state) {
		state.Propagate ();

		Edge e = state.s.UnspecifiedPassiveEdge();
		if (e == null) {
			return;
		} 

		var assumptions = state.s.GenerateAssumptions (e);

		State[] newStates = new State[assumptions.Length];
		for (int i = 0; i < assumptions.Length; i++) {
			var newState = newStates[i] = (State)state.Clone ();
			newState.s.Presume (assumptions [i]);
			RunAlgorithm (newState);
			if (newState.s.NoSlip (e)) {
				state.s.SetState (newState.s);
				return;
			}			
		}

		state.s.SetState (newStates[0].s);//first one will always assume non-rolling
	}

	public static void DoStuff(State state) {
		RunAlgorithm (state);
	}
}

