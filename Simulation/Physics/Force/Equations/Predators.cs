using System;
using ParticleLifeSimulation.Simulation.Interfaces;

namespace ParticleLifeSimulation.Simulation.Physics.Force.Equations;

public class Predators : IForceEquation
{
    public string Name => "Predators";      // For debugging
    public float Compute(float normalizedDistance, float attractionCoefficient)
    {
        // Works best with random matrices
        const float dampener = 0.2f;
        const float spiralFactor = 0.1f;
        
        return ((float)Math.Log(normalizedDistance + spiralFactor) * attractionCoefficient) * dampener;
    }
}