using Microsoft.Xna.Framework;

namespace ParticleLifeSimulation.Simulation.Interfaces;

public interface ISpacePartitioner
{
    string Type { get; }
    public int[] QueryIds { get; }
    public int QuerySize { get; }
    
    public void PerformPartition(Vector2[] positions);
    public void Query(Vector2 position, float queryRange);
}