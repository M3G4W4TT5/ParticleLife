using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParticleLifeSimulation.Simulation.Configuration;
using ParticleLifeSimulation.Simulation.Input;
using ParticleLifeSimulation.Simulation.Particles;
using ParticleLifeSimulation.Simulation.Physics;
using ParticleLifeSimulation.Simulation.Physics.Force;

namespace ParticleLifeSimulation.Simulation.Managers;

public class SimulationManager
{
    private readonly Game _context;
    private readonly ModeManager _modeManager;
    private ParticleManager _particleManager;
    private PhysicsEngine _physicsEngine;

    public SimulationManager(Game context, ModeManager modeManager)
    {
        _context = context;
        _modeManager = modeManager;
        var forceCalculator = new ForceCalculator(_modeManager.CurrentMode.ForceEquation);

        var particlePhysics = new ParticlePhysics( 
            _modeManager.CurrentMode.SpacePartitioner, 
            forceCalculator, 
            _modeManager.CurrentMode.AttractionMatrix, 
            _modeManager.CurrentMode.Config.WorldBoundaries, 
            _modeManager.CurrentMode.Config.ForceFactor, 
            _modeManager.CurrentMode.Config.RMax, 
            _modeManager.CurrentMode.Config.BoundaryRepulsionDistance, 
            _modeManager.CurrentMode.Config.BoundaryRepulsionForce, 
            _modeManager.CurrentMode.Config.DeltaT, 
            _modeManager.CurrentMode.Config.FrictionFactor);

        _physicsEngine = new PhysicsEngine(particlePhysics);

        _particleManager = new ParticleManager(
            _modeManager.CurrentMode.ParticleTexture,
            _modeManager.CurrentMode.ParticleColors,
            _modeManager.CurrentMode.Config.ParticleCount);
        
        _modeManager.CurrentMode.OnModeReloaded += ReloadCurrentMode;
        
        // For debugging
        LogCurrentMode(_modeManager.CurrentMode);       // Log the initial mode
    }
    
    // For debugging
    private void LogCurrentMode(SimulationMode mode)
    {
        Console.WriteLine(mode);
    }

    
    private void ReloadCurrentMode()
    {
        var forceCalculator = new ForceCalculator(_modeManager.CurrentMode.ForceEquation);

        var particlePhysics = new ParticlePhysics( 
            _modeManager.CurrentMode.SpacePartitioner, 
            forceCalculator, 
            _modeManager.CurrentMode.AttractionMatrix, 
            _modeManager.CurrentMode.Config.WorldBoundaries, 
            _modeManager.CurrentMode.Config.ForceFactor, 
            _modeManager.CurrentMode.Config.RMax, 
            _modeManager.CurrentMode.Config.BoundaryRepulsionDistance, 
            _modeManager.CurrentMode.Config.BoundaryRepulsionForce, 
            _modeManager.CurrentMode.Config.DeltaT, 
            _modeManager.CurrentMode.Config.FrictionFactor);


        _physicsEngine = new PhysicsEngine(particlePhysics);
        
        // None of these currently change but probably will at a later stage so leave the logic as is.
        _particleManager = new ParticleManager(
            _modeManager.CurrentMode.ParticleTexture,
            _modeManager.CurrentMode.ParticleColors,
            _modeManager.CurrentMode.Config.ParticleCount);
    }

    public void Update()
    {
        _physicsEngine.Update(_particleManager.Particles);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 screenSize)
    {
        _particleManager.Draw(spriteBatch, screenSize);
    }
    
    public void HandleInput()
    {
        KeyBoard.GetState();

        if (KeyBoard.KeyHasBeenPressed(Keys.Escape))
        {
            _context.Exit();
        }

        if (KeyBoard.KeyHasBeenPressed(Keys.Enter))
        {
            _particleManager.Reload();
        }

        if (KeyBoard.KeyHasBeenPressed(Keys.Left))
        {
            _modeManager.SwitchToPreviousMode();
        }

        if (KeyBoard.KeyHasBeenPressed(Keys.Right))
        {
            _modeManager.SwitchToNextMode();
        }
    }
}
