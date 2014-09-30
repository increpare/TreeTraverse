using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Steps:
 * - create initial gamestate
 * - do backtracking to passive force + speed decrease
 * - do slip inconsistency stuff 
 */

class Program
{

	static string simpletest = @"
ground player sausage1 sausage2
---
ground player true
ground sausage1 true
player sausage1
sausage1 sausage2 true
---
ground 0 0
player 1 0
";

	static string incompleteforces=	@"
ground player sausage1 sausage2 sausage3
---
ground player true
ground sausage1 true
ground sausage2 true
sausage1 sausage3 true
sausage2 sausage3 true
player sausage1
---
ground 0 0
player 1 0
";

    static void Main(string[] args)
    {
		var s = State.NewState (incompleteforces);
		Outline.RunAlgorithm (s);
		Console.WriteLine (s.s.movements.Count);
		Console.WriteLine ("Hello World");
    }
}

