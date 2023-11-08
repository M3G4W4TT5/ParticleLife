using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLifeSimulation.Simulation.Configuration;
using ParticleLifeSimulation.Simulation.Factories;
using ParticleLifeSimulation.Simulation.Managers;

namespace ParticleLifeSimulation.Simulation;

public class SimulationEngine : Game
{
    private const float FrameRate = 144f;
    
    private readonly GraphicsDeviceManager _graphics;
    private Vector2 _screenSize;
    private RenderTarget2D _renderTarget;
    private SpriteBatch _spriteBatch;
    
    private ModeManager _modeManager;
    private SimulationManager _simulationManager;
    
    public SimulationEngine()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // For debugging
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        AllocConsole();
        System.Console.WriteLine("DEBUG CONSOLE WINDOW CREATED");
        
        
        TargetElapsedTime = System.TimeSpan.FromSeconds(1.0 / FrameRate);
        IsFixedTimeStep = true;

        base.Initialize();

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
    }

    protected override void LoadContent()
    {
        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        var underScan = new Vector2(8f, 8f);
        _screenSize = new Vector2(screenWidth, screenHeight) - underScan;
        _renderTarget = new RenderTarget2D(GraphicsDevice, screenWidth, screenHeight);
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var particleTexture = Content.Load<Texture2D>("particle");
        var forceEquationFactory = new ForceEquationFactory();
        var attractionMatrixFactory = new AttractionMatrixFactory();
        var spacePartitionerFactory = new SpacePartitionerFactory();
        var modeFactory = new SimulationModeFactory(
            particleTexture, 
            forceEquationFactory, 
            attractionMatrixFactory, 
            spacePartitionerFactory);
        var modes = new List<SimulationMode>
        {
            // TODO: Add particle spawn pattern variations (spiral, circle, colour sorted, etc.)
            modeFactory.Create(FrameRate, "ParticleLife", "Random", "FixedSizeGrid"),
            modeFactory.Create(FrameRate, "ParticleLife", "Chains", "FixedSizeGrid"),
            modeFactory.Create(FrameRate, "ParticleLife", "Snake", "FixedSizeGrid"),
            modeFactory.Create(FrameRate, "Magnetic", "Random", "FixedSizeGrid"), // fSGrid is optimal
            modeFactory.Create(FrameRate, "Predators", "Random", "FixedSizeGrid"),
            modeFactory.Create(FrameRate, "Coulomb", "Random", "FixedSizeGrid"),  // fSGrid is optimal
            modeFactory.Create(FrameRate, "SineWave", "Snake", "FixedSizeGrid"),
            modeFactory.Create(FrameRate, "Elastic", "Random", "FixedSizeGrid"),  // fSGrid is optimal
            modeFactory.Create(FrameRate, "VanDerWaals", "Random", "FixedSizeGrid"),  // fSGrid is optimal
            modeFactory.Create(FrameRate, "LennardJonesPotential", "Random", "FixedSizeGrid")  // fSGrid is optimal
        };

        _modeManager = new ModeManager(modes);
        _simulationManager = new SimulationManager(this, _modeManager);
    }

    protected override void Update(GameTime gameTime)
    {
        _simulationManager.HandleInput();
        _simulationManager.Update();
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        float scale = 1f / (1080f / _graphics.GraphicsDevice.Viewport.Height);

        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.Black);
            
        _spriteBatch.Begin();
        _simulationManager.Draw(_spriteBatch, _screenSize);
        _spriteBatch.End();
            
        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_renderTarget, 
            Vector2.Zero, 
            null, 
            Color.White, 
            0f, 
            Vector2.Zero, 
            scale, 
            SpriteEffects.None, 
            0f);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}