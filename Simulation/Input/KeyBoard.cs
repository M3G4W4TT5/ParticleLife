using Microsoft.Xna.Framework.Input;

namespace ParticleLifeSimulation.Simulation.Input;

public class KeyBoard
{
    private static KeyboardState _currentKeyState;
    private static KeyboardState _previousKeyState;

    public static KeyboardState GetState()
    {
        _previousKeyState = _currentKeyState;
        _currentKeyState = Keyboard.GetState();
        return _currentKeyState;
    }

    public static bool KeyHasBeenPressed(Keys key)
    {
        return _currentKeyState.IsKeyDown(key) && !_previousKeyState.IsKeyDown(key);
    }
}
