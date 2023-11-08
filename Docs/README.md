# Particle Life (WIP)

![Particle Life](https://github.com/4lexanderWatts/ParticleLife/blob/3df33f3932d1462595f80888dd8e5f0bdf13e8c2/Docs/ParticleLifeDemo.gif)

### _Simulating emergent behaviour from simple rules_

#### About:
This particle simulation is based on *Jeffrey Ventrella's 'Clusters'* and inspired by Tom Mohr's version. The goal is to create a light-weight and modifiable particle life simulation with the aim of accurately simulating physical phenomena.

The project is still in it's early stages and as such it is not optimised, restricting the amount of simulation particles.
#### Controls:

- **Left / Right Arrow Keys:** Cycle simulation modes
- **Enter:** Reset current simulation
- **Escape:** Exit programme
#### Built versions:
- Windows [32](https://github.com/4lexanderWatts/ParticleLife/blob/fff96e7430f2dbb49a7c29595d832ebff21e8559/Build/ParticleLife_v0.1_win-x86.zip)/[64](https://github.com/4lexanderWatts/ParticleLife/blob/c60747c3a258db457b5ac9c014ac711f59555416/Build/ParticleLife_v0.1_win-x64.zip)-bit
- [Portable](https://github.com/4lexanderWatts/ParticleLife/blob/fff96e7430f2dbb49a7c29595d832ebff21e8559/Build/ParticleLife_v0.1_portable.zip)
#### Dependencies:
- [MonoGame](https://github.com/MonoGame/MonoGame)
    - [MonoGame.Content.Builder.Task](https://www.nuget.org/packages/MonoGame.Content.Builder.Task)
        - `dotnet add package MonoGame.Content.Builder.Task --version 3.8.1.303`
    - [MonoGame.Framework.DesktopGL](https://www.nuget.org/packages/MonoGame.Framework.DesktopGL)
        -  `dotnet add package MonoGame.Framework.DesktopGL --version 3.8.1.303`
