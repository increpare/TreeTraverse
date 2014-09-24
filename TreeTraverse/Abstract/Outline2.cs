using System;
using System.Linq;
public class Outline2
{

	public interface MyState {
		void Load(string s);
		MyState Propagate();
		MyState Presume(Assumption a);
		Edge UnspecifiedPassiveEdge();
		Assumption[] GenerateAssumptions(Edge E);
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

	public MyState RunAlgorithm(MyState state) {

		Edge e = state.UnspecifiedPassiveEdge();
		if (e == null) {
			return state;
		} 

		var assumptions = state.GenerateAssumptions (e);
		var consequentialStates = assumptions.Select(a => RunAlgorithm(state.Presume(a))).ToList();
		var result = consequentialStates.FirstOrDefault( s => s.NoSlip(e) );
		if(result==null){
				result=consequentialStates.First();
		}
		return result;
	}

	public MyState DoStuff(MyState state) {
		return RunAlgorithm (state.Propagate());
	}
}

