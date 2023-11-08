using Microsoft.Xna.Framework;

namespace ParticleLifeSimulation.Simulation.Physics.CollisionDetection;

public static class ParticleCollisions
{
    public static bool Check(Vector2 position1, Vector2 position2, float rMax)
    {
        var distance = (position2 - position1).Length();
        return distance > 0 && distance < rMax;
    }
}