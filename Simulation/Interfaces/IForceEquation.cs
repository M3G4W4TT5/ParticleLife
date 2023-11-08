namespace ParticleLifeSimulation.Simulation.Interfaces;

public interface IForceEquation
{ 
    string Name { get; }
    float Compute(float normalizedDistance, float attractionCoefficient);
}
