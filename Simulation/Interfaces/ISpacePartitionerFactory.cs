namespace ParticleLifeSimulation.Simulation.Interfaces;

public interface ISpacePartitionerFactory
{
    ISpacePartitioner Create(string type, float rMax, int particleCount);
}