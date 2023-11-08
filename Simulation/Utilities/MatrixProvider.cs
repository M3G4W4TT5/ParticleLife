using System;

namespace ParticleLifeSimulation.Simulation.Utilities;

public static class MatrixProvider
{
    private static readonly Random Random = new();

    public static float[,] FullyRandom(int size)
    {
        var randomMatrix = new float[size, size];
        for (var i = 0; i <= randomMatrix.GetUpperBound(0); i++)
        {
            for (var j = 0; j <= randomMatrix.GetUpperBound(1); j++)
            {
                randomMatrix[i, j] = Random.NextSingle() * 2 - 1;
            }
        }

        return randomMatrix;
    }

    public static float[,] Chains(int size)
    {
        var chainsMatrix = new float[size, size];
        for (var i = 0; i <= chainsMatrix.GetUpperBound(0); i++)
        {
            for (var j = 0; j <= chainsMatrix.GetUpperBound(1); j++)
            {
                if (j == i || j == (i + 1) % size || j == (i + size - 1) % size)
                {
                    chainsMatrix[i, j] = 1f;
                }
                else
                {
                    chainsMatrix[i, j] = -1f;
                }

            }
        }

        return chainsMatrix;
    }
    
    public static float[,] Snakes(int size)
    {
        var snakesMatrix = new float[size, size];
        for (var i = 0; i <= snakesMatrix.GetUpperBound(0); i++)
        {
            snakesMatrix[i, i] = 1f;
            snakesMatrix[i, (i + 1) % size] = 0.2f;
        }

        return snakesMatrix;
    }
}
