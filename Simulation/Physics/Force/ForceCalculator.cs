using System;
using Microsoft.Xna.Framework;
using ParticleLifeSimulation.Simulation.Interfaces;
using ParticleLifeSimulation.Simulation.Particles;
using ParticleLifeSimulation.Simulation.Physics.CollisionDetection;

namespace ParticleLifeSimulation.Simulation.Physics.Force;

public class ForceCalculator
{
    private IForceEquation _forceEquation;

    public ForceCalculator(IForceEquation forceEquation)
    {
        _forceEquation = forceEquation;
    }

    public Vector2 CalculateBoundaryForce(BoundaryCollisionResult collisionResult, 
                                          Vector2 position, 
                                          float[] worldBoundaries,
                                          float boundaryRepulsionDistance,
                                          float boundaryRepulsionForce)
    {
        var totalRepulsionForce = Vector2.Zero;
        if (collisionResult.Left)
        {
            var repulsionModifier = worldBoundaries[0] + boundaryRepulsionDistance - position.X;
            totalRepulsionForce.X += repulsionModifier / boundaryRepulsionDistance * boundaryRepulsionForce;
        }
        if (collisionResult.Right)
        {
            var repulsionModifier = position.X - (worldBoundaries[1] - boundaryRepulsionDistance);
            totalRepulsionForce.X -= repulsionModifier / boundaryRepulsionDistance * boundaryRepulsionForce;
        }
        if (collisionResult.Top)
        {
            var repulsionModifier = worldBoundaries[0] + boundaryRepulsionDistance - position.Y;
            totalRepulsionForce.Y += repulsionModifier / boundaryRepulsionDistance * boundaryRepulsionForce;
        }
        if (collisionResult.Bottom)
        {
            var repulsionModifier = position.Y - (worldBoundaries[1] - boundaryRepulsionDistance);
            totalRepulsionForce.Y -= repulsionModifier / boundaryRepulsionDistance * boundaryRepulsionForce;
        }
        
        return totalRepulsionForce;
    }
    
    public Vector2 CalculateParticleForce(float[,] attractionMatrix, 
                                          ParticlesContainer pContainer, 
                                          int i, 
                                          int j, 
                                          float rMax,
                                          float forceFactor)
    {
        // TODO: Change procedure such that forceEquation takes a Vector2 and directly modifies it.
        /* This will allow for more complex behaviours. Necessary for implementing 'planets', 'rotator' etc.*/
        var positionDelta = pContainer.Positions[j] - pContainer.Positions[i];
        var distance = positionDelta.Length();
        var normalizedDistance = distance / rMax;
        var pForce = _forceEquation.Compute(normalizedDistance, 
                                            attractionMatrix[pContainer.Colours[i], pContainer.Colours[j]]);
        var limitedPForce = Math.Clamp(pForce, -1f, 1f);        // Avoid particles on speed
        var forceMagnitude = rMax * forceFactor * limitedPForce;
        var force = positionDelta / distance * forceMagnitude;

        return force;
    }
}