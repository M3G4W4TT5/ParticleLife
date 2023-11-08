using System;
using ParticleLifeSimulation.Simulation.Interfaces;

namespace ParticleLifeSimulation.Simulation.Physics.Force.Equations;

public class ParticleLife : IForceEquation
{
    public string Name => "ParticleLife";      // For debugging
    public float Compute(float normalizedDistance, float attractionCoefficient)
    {
        const float beta = 0.3f;
        return normalizedDistance switch
        {
            < beta => normalizedDistance / beta - 1,
            > beta and < 1 => attractionCoefficient * (1 - Math.Abs(2 * normalizedDistance - 1 - beta) / (1 - beta)),
            _ => 0
        };
    }
}