﻿using System;
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

    static void Main(string[] args)
    {
		var s = State.NewState ();
		s.Propagate ();
		Console.WriteLine (s.s.movements.Count);
		Console.WriteLine ("Hello World");
    }
}

