using System;
using ParticleLifeSimulation.Simulation.Interfaces;

namespace ParticleLifeSimulation.Simulation.Physics.Force.Equations;

public class LennardJonesPotential : IForceEquation
{
    public string Name => "LennardJonesPotential";      // For debugging
    public float Compute(float normalizedDistance, float attractionCoefficient)
    {
        // Simulates intermolecular forces
        const float epsilon = 1f; 
        const float sigma = 0.1f; 

        var sigmaOverR = sigma / normalizedDistance;
        // Pow is painfully slow so we do it the old-fashioned way
        var sigmaOverR6 = sigmaOverR * sigmaOverR * sigmaOverR * sigmaOverR * sigmaOverR * sigmaOverR;
        var sigmaOverR12 = sigmaOverR6 * sigmaOverR6;
        
        return 4 * epsilon * attractionCoefficient * (sigmaOverR12 - sigmaOverR6);
    }
}