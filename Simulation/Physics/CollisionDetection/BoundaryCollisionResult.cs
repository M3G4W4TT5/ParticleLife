namespace ParticleLifeSimulation.Simulation.Physics.CollisionDetection;

public struct BoundaryCollisionResult
{
    public bool Left { get; }
    public bool Right { get; }
    public bool Top { get; }
    public bool Bottom { get; }

    public BoundaryCollisionResult(bool left, bool right, bool top, bool bottom)
    {
        Left = left;
        Right = right;
        Top = top;
        Bottom = bottom;
    }
}
