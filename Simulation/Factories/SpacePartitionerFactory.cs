using System;
using ParticleLifeSimulation.Simulation.Interfaces;
using ParticleLifeSimulation.Simulation.Maths.SpacePartitioning;

namespace ParticleLifeSimulation.Simulation.Factories;

public class SpacePartitionerFactory : ISpacePartitionerFactory
{
    public ISpacePartitioner Create(string type, float rMax, int particleCount)
    {
        return type switch
        {
            "FixedSizeGrid" => new FixedSizeGrid(rMax, particleCount),
            // TODO: Add more partitioning methods: Octree for densely clustered particle systems. 
            _ => throw new ArgumentException("Invalid space partitioner type", nameof(type))
        };
    }
}
