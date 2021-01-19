using System.Collections.Generic;

//Represents a vertical lane that zombies can travel up
public class Lane
{
    public readonly List<GruntController> Zombies = new List<GruntController>();
    public Barrier Barrier;
    //TODO: Add survivors assigned to this lane


}
