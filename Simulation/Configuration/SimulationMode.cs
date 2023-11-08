using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLifeSimulation.Simulation.Interfaces;

namespace ParticleLifeSimulation.Simulation.Configuration;

public class SimulationMode
{
    public SimulationConfig Config { get; }
    public Texture2D ParticleTexture { get; }
    public Color[] ParticleColors { get; }
    public IForceEquation ForceEquation { get; }
    public ISpacePartitioner SpacePartitioner { get; }
    public float[,] AttractionMatrix { get; private set; }
    
    public event Action OnModeReloaded;
    public void Reload()
    {
        OnModeReloaded?.Invoke();
    }
    
    // For debugging
    public override string ToString()
    {
        return $"Force Equation: {ForceEquation.Name}, Space Partitioner: {SpacePartitioner.Type}, {GetMatrixString(AttractionMatrix)}";
    }
    
    public SimulationMode(
        SimulationConfig config,
        Texture2D particleTexture,
        IForceEquation forceEquation, 
        ISpacePartitioner spacePartitioner,
        float[,] attractionMatrix)
    {
        Config = config;
        ParticleTexture = particleTexture;
        ParticleColors = config.ParticleColors;
        ForceEquation = forceEquation;
        SpacePartitioner = spacePartitioner;
        AttractionMatrix = attractionMatrix;
    }
    
    // For debugging
    private string GetMatrixString(float[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);
        var matrixString = new StringBuilder();

        matrixString.AppendLine("Attraction Matrix:");
        for (int i = 0; i < rows; i++)
        {
            matrixString.Append("| ");
            for (int j = 0; j < columns; j++)
            {
                matrixString.AppendFormat("{0,6:F2} ", matrix[i, j]);
            }
            matrixString.AppendLine("|");
        }

        return matrixString.ToString();
    }
}
