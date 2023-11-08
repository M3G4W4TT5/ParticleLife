using Microsoft.Xna.Framework;

namespace ParticleLifeSimulation.Simulation.Configuration;

public record SimulationConfig(
    float FrameRate,
    float DeltaT,
    float FrictionHalfLife,
    float RMax,
    float FrictionFactor,
    float ForceFactor,
    int ParticleCount,
    Color[] ParticleColors,
    float BoundaryRepulsionDistance,
    float BoundaryRepulsionForce,
    float[] WorldBoundaries
);
