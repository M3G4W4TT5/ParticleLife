using System;
using ParticleLifeSimulation.Simulation.Interfaces;

namespace ParticleLifeSimulation.Simulation.Physics.Force.Equations;

public class Magnetic : IForceEquation
{
    public string Name => "Magnetic";      // For debugging
    public float Compute(float normalizedDistance, float attractionCoefficient)
    {
        // Works best with random matrices
        if (normalizedDistance < 0.01f) normalizedDistance = 0.01f;
        const float dampener = 0.1f;
        
        return (-attractionCoefficient / normalizedDistance)  * dampener;
    }
}