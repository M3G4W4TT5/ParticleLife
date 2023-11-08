using Microsoft.Xna.Framework;

namespace ParticleLifeSimulation.Simulation.Configuration;

public static class CreateConfig
{
    public static SimulationConfig Standard(float frameRate)
    {
        var deltaT = 1f / frameRate;
        var frictionFactor = (float)System.Math.Pow(0.5, deltaT / 0.025f);

        return new SimulationConfig(
            FrameRate: frameRate,
            DeltaT: deltaT,
            FrictionHalfLife: 0.025f,
            RMax: 0.1f,
            FrictionFactor: frictionFactor,
            ForceFactor: 10f,
            ParticleCount: 1000,
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
            BoundaryRepulsionForce: 0.0f,
            WorldBoundaries: new[] {0f, 1f}
        );
    }   
}
