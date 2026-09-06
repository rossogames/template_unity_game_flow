# Rosso Games

<table>
  <tr>
    <td><img src="https://raw.githubusercontent.com/rossogames/rossogames/main/logo.png" alt="Rossoforge" width="64"/></td>
    <td><h2>Rossoforge - GameFlow</h2></td>
  </tr>
</table>

**Rossoforge-GameFlow** is an architectural template project designed for Unity that demonstrates the integration and production-ready usage of the entire Rossoforge SDK suite. It provides a structured, scalable, and decoupled game loop skeleton featuring an intermediate service layer, a complete UI popup lifecycle with async await operations, and memory-optimized gameplay systems utilizing native pooling mechanisms.

#
**Version:** Unity 6.3 or higher

#
**Dependencies:**
* [Rossoforge-Audio](https://github.com/rossogames/Rossoforge-Audio.git)
* [Rossoforge-Controls](https://github.com/rossogames/Rossoforge-Controls.git)
* [Rossoforge-Core](https://github.com/rossogames/Rossoforge-Core.git)
* [Rossoforge-Events](https://github.com/rossogames/Rossoforge-Events.git)
* [Rossoforge-Extensions](https://github.com/rossogames/Rossoforge-Extensions.git)
* [Rossoforge-Persistence](https://github.com/rossogames/Rossoforge-Persistence.git)
* [Rossoforge-Pool](https://github.com/rossogames/Rossoforge-Pool.git)
* [Rossoforge-Popups](https://github.com/rossogames/Rossoforge-Popups.git)
* [Rossoforge-Scenes](https://github.com/rossogames/Rossoforge-Scenes.git)
* [Rossoforge-Service](https://github.com/rossogames/Rossoforge-Services.git)
* [Rossoforge-TimeFlow](https://github.com/rossogames/Rossoforge-TimeFlow.git)
* [Rossoforge-Toolbar](https://github.com/rossogames/RossoForge-Toolbar.git)
* [Rossoforge-Screens](https://github.com/rossogames/Rossoforge-Screens.git)
* [Rossoforge-Utils](https://github.com/rossogames/Rossoforge-Utils.git)

---
### Project Purpose

This repository contains a comprehensive template that serves as a starting point and a practical demonstration of the framework. Its primary goal is to showcase how all the packages in the suite interact in an integrated, orderly, and scalable real-world game flow.

> **Important Note regarding Visuals**
> The aesthetic design of this template is extremely simple, featuring flat UI popups and a basic gameplay loop that resembles a beginner tutorial. This is completely intentional to avoid distractions with art assets and keep the focus purely on analyzing data flow, execution performance, and the underlying architectural solidity.


## Key Features

* **SDK Integration Showcase** Demonstrates how to properly implement, configure, and utilize all packages from the Rossoforge SDK suite within a unified production environment.

## Intermediate Service Architecture

The project introduces an intermediate service layer to avoid tight coupling between game components and the original framework APIs:

* **SceneFlowService** Encapsulates all available scenes in the project and exposes their loading mechanics through individual, independent methods, which prevents the rest of the scripts from using hardcoded string paths or calling scenes directly.
* **PopupFlowService** Operates identically to the SceneFlowService, centralizing configuration parameters, visual variables, and access to all game popups in a controlled manner.
* **InputsService** Captures keyboard or peripheral inputs from the user and dispatches strongly typed events across systems using the global event bus.
* **SettingsService** Manages runtime option states by handling data serialization and retrieval directly through `PlayerPrefs`.
* **ProgressionService** Coordinates player progress persistence using the suite saving tools, serving as an example of how to define a serializable class to store runtime information.
* **GameplayService** The core component in charge of coordinating, initializing, and finalizing mechanics and scoring inside the gameplay scene.
 
## Game Flow Structure

The template execution cycle is split into three main stages:

### 1 Boot Scene
The entry point of the executable application. Essential services are instantiated here and, once the environment configuration is set, an additive transition screen is triggered to give way to the main menu.

### 2 Main Menu Scene
Presents the initial user interface featuring three key action buttons:
* **Exit** Calls a generic parameterizable message box to confirm exiting the application.
* **Settings** Opens an options panel showing how to read and write configurations to the platform registry (`PlayerPrefs`) via a local settings service. Both dialog windows utilize completely distinct animation behaviors driven by their respective *Animator Controllers*.
* **Start** Triggers the additive scene transition process to load the playable demonstration.

### 3 Gameplay Scene
An empty scene where the core gameplay mechanics are implemented. The heads-up display (*HUD*) updates player currency by reading directly from the event bus, keeping data logic entirely separate from UI view components.

Pressing the **Escape** key alters the time scale to freeze the game and deploys a pause menu. If the player chooses to return to the main menu, the system restores the time scale, and automatically forces all active gameplay elements back into their pools.

---
This package is part of the **Rossoforge** suite, designed to streamline and enhance Unity development workflows.

Developed by Agustin Rosso
https://www.linkedin.com/in/rossoagustin/
