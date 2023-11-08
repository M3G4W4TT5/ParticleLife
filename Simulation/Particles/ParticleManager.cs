using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleLifeSimulation.Simulation.Particles;

public class ParticleManager
{
    private readonly Texture2D _particleTexture;
    private int _particleCount;
    private Color[] _colorList;
    public ParticlesContainer Particles { get; private set; }

    private readonly System.Random _random = new();
    
    public ParticleManager(Texture2D particleTexture, Color[] colorList, int particleCount)
    {
        _particleTexture = particleTexture;
        _colorList = colorList;
        _particleCount = particleCount;
        InitialiseParticles();
    }

    private void InitialiseParticles()
    {
        var colours = new int[_particleCount];
        var positions = new Vector2[_particleCount];
        var velocities = new Vector2[_particleCount];
        
        for (var i = 0; i < _particleCount; i++)
        {
            colours[i] = (int)System.Math.Floor(_random.NextSingle() * _colorList.Length);
            positions[i] = new Vector2(_random.NextSingle(), _random.NextSingle());
            velocities[i] = new Vector2(0f);
        }
        
        Particles = new ParticlesContainer(colours, positions, velocities);
    }

    public void Reload()
    {
        InitialiseParticles();
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 actualScreenSize)
    {
        for (var i = 0; i < _particleCount; i++)
        {
            spriteBatch.Draw(
                texture: _particleTexture,
                position: Particles.Positions[i] * actualScreenSize,
                sourceRectangle: null,
                color: _colorList[Particles.Colours[i]],
                rotation: 0f,
                origin: Vector2.Zero,
                scale: Vector2.One,
                effects: SpriteEffects.None,
                layerDepth: 1f
            );
        }
    }
}
