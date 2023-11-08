using Microsoft.Xna.Framework;
using ParticleLifeSimulation.Simulation.Interfaces;

namespace ParticleLifeSimulation.Simulation.Maths.SpacePartitioning;

public class FixedSizeGrid : ISpacePartitioner
{
    private readonly double _spacing;
    private readonly int _tableSize; 
    private readonly int[] _cellStart; 
    private readonly int[] _cellEntries;
    public string Type => "FixedSizeGrid";      // For debugging

    public int[] QueryIds { get; }
    public int QuerySize { get; private set; }

    public FixedSizeGrid(double spacing, int numberOfPoints)
    {
        _spacing = spacing;
        _tableSize = 2 * numberOfPoints;
        _cellStart = new int[_tableSize + 1];
        _cellEntries = new int[numberOfPoints];
        QueryIds = new int[numberOfPoints];
        QuerySize = 0;
    }

    public void PerformPartition(Vector2[] positions) 
    {
        var numObjects = _cellEntries.Length;
        
        System.Array.Clear(_cellStart, 0, _cellStart.Length);
        System.Array.Clear(_cellEntries, 0, _cellEntries.Length);

        for (var i = 0; i < numObjects; i++) 
        {
            var h = HashWorldPosition(positions[i]);
            _cellStart[h]++;
        }
        
        var start = 0;
        for (var i = 0; i < _tableSize; i++) 
        {
            start += _cellStart[i];
            _cellStart[i] = start;
        }
        _cellStart[_tableSize] = start; // guard
        
        for (var i = 0; i < numObjects; ++i)
        {
            var h = HashWorldPosition(positions[i]);
            _cellStart[h]--;
            _cellEntries[_cellStart[h]] = i;
        }
    }

    public void Query(Vector2 position, float queryRange)
    {
        // Lower-left corner of the query rectangle
        Vector2 queryMin = position - new Vector2(queryRange, queryRange);
        // Upper-right corner of the query rectangle
        Vector2 queryMax = position + new Vector2(queryRange, queryRange);
        
        // Convert to grid coordinates
        Vector2 gridMin = new Vector2(WorldCoordToGridCoord(queryMin.X), WorldCoordToGridCoord(queryMin.Y));
        Vector2 gridMax = new Vector2(WorldCoordToGridCoord(queryMax.X), WorldCoordToGridCoord(queryMax.Y));

        QuerySize = 0;

        // Loop through the grid cells covered by the query rectangle
        for (var xi = (int)gridMin.X; xi <= gridMax.X; xi++)
        {
            for (var yi = (int)gridMin.Y; yi <= gridMax.Y; yi++)
            {
                var h = HashGridPosition(xi, yi);
                var start = _cellStart[h];
                var end = _cellStart[h + 1];

                for (var i = start; i < end; i++)
                {
                    QueryIds[QuerySize] = _cellEntries[i];
                    QuerySize++;
                }
            }
        }
    }
    
    private int HashWorldPosition(Vector2 position)
    {
        var gridX = WorldCoordToGridCoord(position.X);
        var gridY = WorldCoordToGridCoord(position.Y);
        return HashGridPosition(gridX, gridY);
    }

    private int WorldCoordToGridCoord(float coord)
    {
        return (int)System.Math.Floor(coord / _spacing);
    }
    
    private int HashGridPosition(int x, int y)
    {
        var h = (x * 92837111) ^ (y * 689287499);
        return System.Math.Abs(h) % _tableSize;
    }
}
