using Microsoft.Xna.Framework;

namespace ParticleLifeSimulation.Simulation.Physics.CollisionDetection;

public static class BoundaryCollisions
{
    public static BoundaryCollisionResult Check(Vector2 position, float[] boundaries, float repulsionDistance)
    {
        return new BoundaryCollisionResult(
            left: position.X < boundaries[0] + repulsionDistance,
            right: position.X > boundaries[1] - repulsionDistance,
            top: position.Y < boundaries[0] + repulsionDistance,
            bottom: position.Y > boundaries[1] - repulsionDistance
        );
    }
}