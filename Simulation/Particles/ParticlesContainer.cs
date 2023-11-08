using Microsoft.Xna.Framework;

namespace ParticleLifeSimulation.Simulation.Particles;

public class ParticlesContainer
{
    public int[] Colours { get; private set; }
    public Vector2[] Positions { get; private set;}
    public Vector2[] Velocities { get; private set;}

    public ParticlesContainer(int[] colours, Vector2[] positions, Vector2[] velocities)
    {
        Colours = colours;
        Positions = positions;
        Velocities = velocities;
    }
}
