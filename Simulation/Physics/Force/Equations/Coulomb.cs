using ParticleLifeSimulation.Simulation.Interfaces;

namespace ParticleLifeSimulation.Simulation.Physics.Force.Equations;

public class Coulomb : IForceEquation
{
    public string Name => "Coulomb";      // For debugging
    public float Compute(float normalizedDistance, float attractionCoefficient)
    {
        if (normalizedDistance < 0.01f) normalizedDistance = 0.01f;
        const float dampener = 0.001f;
        
        return (attractionCoefficient / (normalizedDistance * normalizedDistance)) * dampener;
    }
}