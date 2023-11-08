using Microsoft.Xna.Framework.Graphics;
using ParticleLifeSimulation.Simulation.Configuration;
using ParticleLifeSimulation.Simulation.Interfaces;

namespace ParticleLifeSimulation.Simulation.Factories;

public class SimulationModeFactory : ISimulationModeFactory
{
    private readonly ForceEquationFactory _forceEquationFactory;
    private readonly AttractionMatrixFactory _attractionMatrixFactory;
    private readonly ISpacePartitionerFactory _spacePartitionerFactory;
    private readonly Texture2D _particleTexture;
    
    public SimulationModeFactory(
        Texture2D particleTexture,
        ForceEquationFactory forceEquationFactory,
        AttractionMatrixFactory attractionMatrixFactory,
        ISpacePartitionerFactory spacePartitionerFactory)
    {
        _particleTexture = particleTexture;
        _forceEquationFactory = forceEquationFactory;
        _attractionMatrixFactory = attractionMatrixFactory;
        _spacePartitionerFactory = spacePartitionerFactory;
    }

    public SimulationMode Create(
        float frameRate, 
        string forceEquationType, 
        string matrixType, 
        string spacePartitionerType)
    {
        var config = CreateConfig.Standard(frameRate);
        
        var forceEquation = _forceEquationFactory.Create(forceEquationType);
        var attractionMatrix = _attractionMatrixFactory.Create(matrixType, config.ParticleColors.Length);
        var spacePartitioner = _spacePartitionerFactory.Create(spacePartitionerType, config.RMax, config.ParticleCount);

        return new SimulationMode(
            config,
            _particleTexture,
            forceEquation,
            spacePartitioner,
            attractionMatrix);
    }
}
