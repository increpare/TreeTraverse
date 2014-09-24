﻿using System;


public class Outline
{

	public interface MyState {
		void Load(string s);
		void Propagate();
		void Presume(Assumption a);
		Edge UnspecifiedPassiveEdge();
		Assumption[] GenerateAssumptions(Edge E);
		MyState Clone();
		bool NoSlip(Edge E);
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

		MyState[] newStates = new MyState[3];
		for (int i = 0; i < 2; i++) {
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

