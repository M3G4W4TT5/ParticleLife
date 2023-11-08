using ParticleLifeSimulation.Simulation.Configuration;

namespace ParticleLifeSimulation.Simulation.Interfaces;

public interface ISimulationModeFactory
{
    SimulationMode Create(
        float frameRate, 
        string forceEquationType, 
        string matrixType,
        string spacePartitionerType);
}