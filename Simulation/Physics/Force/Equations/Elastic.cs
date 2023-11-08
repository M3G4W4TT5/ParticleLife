using System;
using ParticleLifeSimulation.Simulation.Interfaces;

namespace ParticleLifeSimulation.Simulation.Physics.Force.Equations;

public class Elastic : IForceEquation
{
    public string Name => "Elastic";      // For debugging
    public float Compute(float normalizedDistance, float attractionCoefficient)
    {
        // Collision stiffness, a large value for a 'hard' collision
        const float collisionStiffness = 1000f;

        return normalizedDistance < 1f ? -collisionStiffness * (1f - normalizedDistance) : 0f;
    }
}