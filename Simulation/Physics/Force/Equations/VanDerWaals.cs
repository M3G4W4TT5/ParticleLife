using System;
using ParticleLifeSimulation.Simulation.Interfaces;

namespace ParticleLifeSimulation.Simulation.Physics.Force.Equations;

public class VanDerWaals : IForceEquation
{
    public string Name => "VanDerWaals";      // For debugging
    public float Compute(float normalizedDistance, float attractionCoefficient)
    {
        const float beta = 0.3f;
        if (normalizedDistance > beta) return 0;
        
        const float epsilon = 1f; 
        const float sigma = 0.1f;

        if (normalizedDistance < 0.01f) normalizedDistance = 0.01f;
        
        var invDistanceRatio = sigma / normalizedDistance;
        var invDistanceRatioSquared = invDistanceRatio * invDistanceRatio;

        var lennardJonesPotential = 4 * epsilon *
                                    (float)(Math.Pow(invDistanceRatioSquared, 3) -
                                            Math.Pow(invDistanceRatioSquared, 1.5)) * attractionCoefficient;

        return lennardJonesPotential;
    }
}