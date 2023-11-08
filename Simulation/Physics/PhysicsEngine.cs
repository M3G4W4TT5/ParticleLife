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
        _particlePhysics.UpdateParticle(pContainer);
    }
}
