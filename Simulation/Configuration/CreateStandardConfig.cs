using Microsoft.Xna.Framework;

namespace ParticleLifeSimulation.Simulation.Configuration;

public static class CreateConfig
{
    public static SimulationConfig Standard(float frameRate)
    {
        var deltaT = 1f / frameRate;
        var frictionHalfLife = 0.025f;
        var frictionFactor = (float)System.Math.Pow(0.5, deltaT / frictionHalfLife);

        return new SimulationConfig(
            FrameRate: frameRate,
            DeltaT: deltaT,
            FrictionHalfLife: frictionHalfLife,
            RMax: 0.1f,
            FrictionFactor: frictionFactor,
            ForceFactor: 10f,
            ParticleCount: 500,        // Functional limit is a 500 with FixedSizedGrid and single thread. 
            ParticleColors: new [] {
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.Green,
                Color.Blue,
                Color.Indigo,
                Color.Purple 
            },
            BoundaryRepulsionDistance: 0.05f,
            BoundaryRepulsionForce: 0.01f,
            WorldBoundaries: new[] {0f, 1f}
        );
    }   
}
