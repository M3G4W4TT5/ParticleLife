using System;
using ParticleLifeSimulation.Simulation.Interfaces;
using ParticleLifeSimulation.Simulation.Physics.Force.Equations;

namespace ParticleLifeSimulation.Simulation.Factories;

public class ForceEquationFactory
{
    public IForceEquation Create(string type)
    {
        return type switch
        {
            "ParticleLife" => new ParticleLife(),
            "Magnetic" => new Magnetic(),
            "Predators" => new Predators(),
            "VanDerWaals" => new VanDerWaals(),
            "Coulomb" => new Coulomb(),
            "SineWave" => new SineWave(),
            "Elastic" => new Elastic(),
            "LennardJonesPotential" => new LennardJonesPotential(),
            _ => throw new ArgumentException("Invalid force equation type", nameof(type))
        };
    }
}
