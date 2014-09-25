using System;
using System.Linq;

class Outline
{

	public void RunAlgorithm(Process state) {
		state.Propagate ();

		Edge e = state.UnspecifiedPassiveEdge();
		if (e == null) {
			return;
		} 

		var assumptions = state.GenerateAssumptions (e);

		Process[] newStates = new Process[assumptions.Length];
		for (int i = 0; i < assumptions.Length; i++) {
			var newState = newStates[i] = (Process)state.Clone ();
			newState.Presume (assumptions [i]);
			RunAlgorithm (newState);
			if (newState.NoSlip (e)) {
				state.SetState (newState);
				return;
			}			
		}

		state.SetState (newStates[0]);//first one will always assume non-rolling
	}

	public void DoStuff(Process state) {
		RunAlgorithm (state);
	}
}

