using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

struct Movement
{
//    public int direction; -- not strictly necessary - direction can be global for a given turn
    public readonly int speed;
    public readonly int rolling;
    public readonly Vertex target;

    public Movement(int speed, int rolling, string target)
    {
        this.speed = speed;
        this.rolling = rolling;
        this.target = Vertex.Find[target];
    }

    public int ContactSpeed()
    {
        return speed + rolling;
    }
}
