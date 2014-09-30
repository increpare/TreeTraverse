using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Movement
{
    public int speed;
    public int rolling;
    public Vertex target;
    public bool DependsOn(Force f)
    {
        return false;
    }    
    public Movement(int speed, int rolling, string target)
    {
        this.speed = speed;
        this.rolling = rolling;
        this.target = Vertex.Find[target];
    }
	public Movement(){
	}

    public int ContactSpeed()
    {
        return speed + rolling;
    }
}
