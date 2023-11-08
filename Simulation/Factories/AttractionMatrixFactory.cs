using System;
using ParticleLifeSimulation.Simulation.Utilities;

namespace ParticleLifeSimulation.Simulation.Factories;

public class AttractionMatrixFactory
{
    public float[,] Create(string type, int size)
    {
        return type switch
        {
            "Random" => MatrixProvider.FullyRandom(size),
            "Chains" => MatrixProvider.Chains(size),
            "Snake" => MatrixProvider.Snakes(size),
            _ => throw new ArgumentException("Invalid matrix type", nameof(type))
        };
    }
}
