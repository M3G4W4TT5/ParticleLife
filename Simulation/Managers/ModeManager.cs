using System;
using System.Collections.Generic;
using ParticleLifeSimulation.Simulation.Configuration;

namespace ParticleLifeSimulation.Simulation.Managers;

public class ModeManager
{
    private readonly List<SimulationMode> _modes;
    private int _currentModeIndex;

    public ModeManager(List<SimulationMode> modes)
    {
        _modes = modes ?? throw new ArgumentNullException(nameof(modes));
        _currentModeIndex = 0;
    }

    public SimulationMode CurrentMode => _modes[_currentModeIndex];

    private void ChangeMode(int newModeIndex)
    {
        _currentModeIndex = newModeIndex;
        var currentMode = CurrentMode;
        CurrentMode.Reload();
        LogCurrentMode(currentMode);
    }
    
    // For debugging
    private void LogCurrentMode(SimulationMode mode)
    {
        Console.WriteLine(mode);
    }
    
    public void SwitchToNextMode()
    {
        ChangeMode((_currentModeIndex + 1) % _modes.Count);
    }

    public void SwitchToPreviousMode()
    {
        ChangeMode((_currentModeIndex - 1 + _modes.Count) % _modes.Count);
    }
}