using System;
using ParticleLifeSimulation.Simulation.Interfaces;

namespace ParticleLifeSimulation.Simulation.Physics.Force.Equations;

public class SineWave : IForceEquation
{
    public string Name => "SineWave";      // For debugging
    public float Compute(float normalizedDistance, float attractionCoefficient)
    {
        // Works best with chains, creating circles.
        const float frequency = -2f;

        return attractionCoefficient * (float)Math.Sin(normalizedDistance * Math.PI * frequency);
    }
}