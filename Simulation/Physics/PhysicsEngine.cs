using ParticleLifeSimulation.Simulation.Particles;

namespace ParticleLifeSimulation.Simulation.Physics;

public class PhysicsEngine
{
    private readonly ParticlePhysics _particlePhysics;

    public PhysicsEngine(ParticlePhysics particlePhysics)
    {
        _particlePhysics = particlePhysics;
    }
    
    public void Update(ParticlesContainer pContainer)
    {    
        // TODO: PerformSpacePartition and Loop should really be placed within ParticlePhysics.UpdateParticle
        _particlePhysics.PerformSpacePartitioning(pContainer);
        
        for (var i = 0; i < pContainer.Positions.Length; ++i)
        {
            _particlePhysics.UpdateParticle(pContainer, i);
        }
    }
}
