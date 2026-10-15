# Third-person Shooter

A third-person shooter prototype in Unity 2021.3. Its focus is the player controller: a hierarchical state machine drives movement and shooting, and Animation Rigging makes the body, head and weapon follow the aim.

## Features

- Walk, run, sprint and jump, with aiming available while moving.
- Hitscan gun with a raycast, muzzle flash and a line-rendered bullet trail.
- Upper body, head and weapon aim through Animation Rigging `MultiAimConstraint`s; the left hand holds the gun with a `TwoBoneIKConstraint`.
- Enemies notice the player in a trigger zone and shoot back. Health and damage go through `IDamageable` / `IDieable`.
- Menu and play scenes, a health bar and an invert-Y-axis setting for the camera.
- Input System actions for keyboard and mouse.

## Hierarchical state machine

`Assets/Scripts/Logic/State Machine` is a small HFSM: a `StateMachine` is itself a `State`, so states nest. Transitions are either event-driven switches (`SwitchTransition`, enabled by input actions) or polled conditions (`ConditionTransition`).

The player's locomotion states (`Running`, `Walking`, `Sprinting`, `Jumping`) are wrapped in behaviour states that decide what the upper body does: `FireableState` aims and shoots, `HeadFolowingState` only turns the head. `PlayerStatesFactory` wires the graph, so behaviour is changed in one place:

```
Running ──Aim──▶ Walking (aiming, slower)
Running ──Sprint──▶ Sprinting ──release──▶ Running
Running / Sprinting ──Jump──▶ Jumping ──grounded──▶ Running
```

The game flow (`MenuState`, `PlayState`) uses the same machine.

## Code layout

```
Assets/Scripts/Logic        state machine, game states, camera, update loop, entry point
Assets/Scripts/Player       player, physics and animation views, gun handling with IK, states
Assets/Scripts/Guns         gun, muzzle flash, shells
Assets/Scripts/UI           main menu, HUD, settings
```

## Running

Open the project in Unity 2021.3 and start `Assets/Scenes/MenuScene`.
