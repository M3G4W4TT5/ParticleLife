# Particle Life (WIP)

![Particle Life](https://github.com/4lexanderWatts/ParticleLife/blob/3df33f3932d1462595f80888dd8e5f0bdf13e8c2/Docs/ParticleLifeDemo.gif)

### _Simulating emergent behaviour from simple rules_

#### About:
This particle simulation is based on *Jeffrey Ventrella's 'Clusters'* and inspired by Tom Mohr's version. The goal is to create a light-weight and modifiable particle life simulation with the aim of accurately simulating physical phenomena.

The simulation consists of particles of varying colours which are mapped to an attraction matrix that defines the attractive or repulsive relationship between each particle colour pair. Based on this various equations for calculating forces are applied to update the velocities and positions of particles. These simple calculations cause particles to form varying patterns, clusters, break apart, and interact in ways that echo the behaviour of living entities.

The project is still in it's early stages and as such it is not optimised, restricting the amount of simulation particles.
#### Controls:

- **Left / Right Arrow Keys:** Cycle simulation modes
- **Enter:** Reset current simulation
- **Escape:** Exit programme

#### Description of the code base:
The project is designed with the MonoGame engine, mainly to use it's rendering capabilities. The main simulation loop is found within `SimulationEngine`.

`SimulationManager` contains all necessary elements to run the simulation. It manages the current state, orchestrates the transitions between different simulation modes, and handles user interactions.

`ModeManager` within SimulationManager keeps track of the available simulation modes and enables switching between them.

`SimulationModeFactory` manages the creation of the various modes. These are created in `SimulationManager` and injected into `ModeManager` constructor, which in turn is injected into `SimulationManager`.

`PhysicsEngine` handles the updates for particle velocities and positions using `ParticlePhysics` which contains the actual updating logic, such as spatial partitioning (using `ISPacePartitioner FixedSizexGrid`), collision detection and force calculations.

`ForceCalculator` handles the actual calculations for inter-particle and particle-boundary forces. It uses a concrete implementation of the `IForceEquation` interface to perform the appropriate calculations for a given `SimulationMode`.

`IForceCalculation` implementations are the basis for the different behaviours seen in the various simulations modes. The attraction matrices, which are simply multi-dimensional float arrays contain the particles colours attraction coefficients, floats between -1 and 1, where negative values indicate repulsion from another particle colour. These are instantiated within the `SimulationModeFactory` by their respective factories.

#### Known Issues:
- **Space Partitioning:** Currently, fixed size grid partitioning is used to optimise collision detection. This is far from optimal when particles cluster in non-uniform distributions. Therefore, the simulation is limited to 500 particles.
- **No Collisions Boxes:** Depending on the given IForceEquation implementation particles may form very dense clusters, in which only the top particle is visible. This further reduces the efficacy of the partition-based collision detection.
- **Single-threaded Architecture:** No matter the type of collision detection and partitioning strategy, the simulation's performance is severely limited by running on a single thread.  

#### Built versions:
- Windows [32](https://github.com/4lexanderWatts/ParticleLife/blob/fff96e7430f2dbb49a7c29595d832ebff21e8559/Build/ParticleLife_v0.1_win-x86.zip)/[64](https://github.com/4lexanderWatts/ParticleLife/blob/c60747c3a258db457b5ac9c014ac711f59555416/Build/ParticleLife_v0.1_win-x64.zip)-bit
- [Portable](https://github.com/4lexanderWatts/ParticleLife/blob/fff96e7430f2dbb49a7c29595d832ebff21e8559/Build/ParticleLife_v0.1_portable.zip)
#### Dependencies:
- [MonoGame](https://github.com/MonoGame/MonoGame)
    - [MonoGame.Content.Builder.Task](https://www.nuget.org/packages/MonoGame.Content.Builder.Task)
        - `dotnet add package MonoGame.Content.Builder.Task --version 3.8.1.303`
    - [MonoGame.Framework.DesktopGL](https://www.nuget.org/packages/MonoGame.Framework.DesktopGL)
        -  `dotnet add package MonoGame.Framework.DesktopGL --version 3.8.1.303`
