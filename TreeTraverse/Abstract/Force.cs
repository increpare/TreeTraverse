﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


struct Force
{
    public readonly Edge target;
    public readonly Movement movement;

    public Force(Edge target, Movement movement)
    {
        this.target = target;
        this.movement = movement;
    }

    public Vertex from()
    {
        return target.from; 
    }

    public Vertex to()
    {
        return target.to;
    }


	public static Movement Aggregate(List<Force> forces, Vertex target)
    {
		var m = new Movement ();
		m.speed = forces.Max (f => f.movement.speed);
		m.rolling = 0;
		m.target=target;
		return m;
    }

}
