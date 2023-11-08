using Microsoft.Xna.Framework;
using ParticleLifeSimulation.Simulation.Interfaces;
using ParticleLifeSimulation.Simulation.Particles;
using ParticleLifeSimulation.Simulation.Physics.CollisionDetection;
using ParticleLifeSimulation.Simulation.Physics.Force;

namespace ParticleLifeSimulation.Simulation.Physics;

public class ParticlePhysics
{
    private readonly ISpacePartitioner _spacePartitioner;
    private readonly ForceCalculator _forceCalculator;
    private readonly float[,] _attractionMatrix;
    private readonly float[] _worldBoundaries;
    private readonly float _deltaT;
    private readonly float _frictionFactor;
    private readonly float _forceFactor;
    private readonly float _rMax;
    private readonly float _boundaryRepulsionDistance;
    private readonly float _boundaryRepulsionForce;
    
    public ParticlePhysics(
        ISpacePartitioner spacePartitioner,
        ForceCalculator forceCalculator,
        float[,] attractionMatrix,
        float[] worldBoundaries,
        float forceFactor,
        float rMax,
        float boundaryRepulsionDistance,
        float boundaryRepulsionForce, float deltaT, float frictionFactor)
    {
        _spacePartitioner = spacePartitioner;
        _forceCalculator = forceCalculator;
        _attractionMatrix = attractionMatrix;
        _worldBoundaries = worldBoundaries;
        _deltaT = deltaT;
        _frictionFactor = frictionFactor;
        _forceFactor = forceFactor;
        _rMax = rMax;
        _boundaryRepulsionDistance = boundaryRepulsionDistance;
        _boundaryRepulsionForce = boundaryRepulsionForce;
        
    }
    
    public void PerformSpacePartitioning(ParticlesContainer pContainer)
    {
        _spacePartitioner.PerformPartition(pContainer.Positions);
    }
    
    public void UpdateParticle(ParticlesContainer pContainer, int i)
    {
        var boundaryCollisionResult = BoundaryCollisions.Check(pContainer.Positions[i], _worldBoundaries, _boundaryRepulsionDistance);
        var boundaryCollisionForce = _forceCalculator.CalculateBoundaryForce(
            boundaryCollisionResult, 
            pContainer.Positions[i], 
            _worldBoundaries,
            _boundaryRepulsionDistance,
            _boundaryRepulsionForce
        );
        
        var totalParticleForce = CalculateTotalForce(pContainer, i);
        
        ApplyPhysics(pContainer, i, boundaryCollisionForce, totalParticleForce);
    }
    

    private Vector2 CalculateTotalForce(ParticlesContainer pContainer, int i)
    {
        _spacePartitioner.Query(pContainer.Positions[i], _rMax);

        var totalForce = new Vector2();

        for (var index = 0; index < _spacePartitioner.QuerySize; index++)
        {
            var j = _spacePartitioner.QueryIds[index];

            if (j == i) continue;

            if (ParticleCollisions.Check(pContainer.Positions[i], pContainer.Positions[j], _rMax))
            {
                totalForce += _forceCalculator.CalculateParticleForce(
                    _attractionMatrix, 
                    pContainer, 
                    i, 
                    j, 
                    _rMax,
                    _forceFactor 
                );
            }
        }

        return totalForce;
    }
    
    private void ApplyPhysics(ParticlesContainer pContainer, int pIndex, Vector2 boundaryForce, Vector2 totalParticleForce)
    {   
        var velocity = pContainer.Velocities[pIndex] * _frictionFactor + boundaryForce + totalParticleForce * _deltaT;
        var position = pContainer.Positions[pIndex] + velocity * _deltaT;
        
        pContainer.Velocities[pIndex] = velocity;
        pContainer.Positions[pIndex] = position;
    }
}
