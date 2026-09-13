<div align="center">
  <h1>Super Fantasy Kingdom Project Patcher</h1>

  <img src="images/banner.jpg" />

  <p>
    A game wrapper that generates a Unity project from Super Fantasy Kingdom's build that can be playable in-editor
  </p>
</div>
<br />

# Table of Contents

- [Current State](#current-state)
- [About the Project](#about-the-project)
- [Getting Started](#getting-started)
- [Installation](#installation)
- [Usage](#usage)
- [FAQ](#faq)

## Current State

This project is currently in a early stage of development. It is able to extract the necessary game assets, move the required DLLs, and apply a number of source code patches to generate a Unity project that can be opened normally in the Unity editor without entering Safe Mode.

However, the generated project might **not yet be fully functional**. 

## About the Project

This tool is a game wrapper on top of the [Unity Project Patcher](https://github.com/nomnomab/unity-project-patcher) and was build by looking into other Wrappers like https://github.com/Kesomannen/unity-repo-project-patcher. (As you might notice from the very similar Readme)

The Tool takes a build of Super Fantasy Kingdom, extracts its assets/scripts/etc, and then generates a project for usage in the Unity editor.

> [!IMPORTANT]  
> This tool does not distribute game files. It simply works off of your copy of the game!
>
> Also, this tool is for **personal** use only. Do not re-distrubute game files to others.

## Getting Started

Make sure you have the following before using the tool in any way:

- [Git](https://git-scm.com/download/win)
  - To download packages in Package Manager through git URL
- [.NET 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
  - To run Asset Ripper

> [!IMPORTANT]  
> The Project requires up to ~17GB of free Space.


## Installation

### Unity Project

- Requires [Unity 2021.3.45f2](https://unity.com/de/releases/editor/whats-new/2021.3.45f2)
- Unity (3D) URP render pipeline

Create a new Unity project with the above requirements before getting started.

You will need to install two packages in sequence here:

- Unity Project Patcher: `https://github.com/nomnomab/unity-project-patcher.git`
  - [Can be disabled](#disabling-bepinex-usage)
- This project

### Installing the Unity Project Patcher core

1. Open the Package Manager from `Window > Package Manager`
2. Click the '+' button in the top-left of the window
3. Click 'Add package from git URL'
4. Provide the URL of the this git repository: `https://github.com/nomnomab/unity-project-patcher.git`
   - If you are using a specific version, you can append it to the end of the git URL, such as `#v1.2.3`
5. Click the 'add' button

```json
"com.nomnom.unity-project-patcher": "https://github.com/nomnomab/unity-project-patcher.git"
```

- If you are using a specific version, you can append it to the end of the git URL, such as `#v1.2.3`

### Installing this Game Wrapper

The same steps as previously, just with `https://github.com/Skydorm1/unity-sfk-project-patcher.git`

## Usage

The tool window can be opened via `Tools > Unity Project Patcher > Open Window`

1. Open the **Unity Project Patcher** window and press **Run**.
2. If Unity asks you to restart for the **Input System**, select **Yes**.
3. If Unity asks whether to enter **Safe Mode**, select **Ignore** and allow the project to open normally. This will happen 2 times throughout the whole process. Make sure to press ignore, otherwise it will not continue till you do so.

> [!IMPORTANT]  
> Keep your mods in a seperate folder, so if a mendatory update is pushed for the game that breaks mods, you can move important folders with you.
> You probably need to patch the Project from the start if that happens.

Estimated patch durations:

- Fresh patch: ~0:25h

These can vary wildly depending on system speed and project size.

For this project, we use a **custom AssetRipper build** that makes use of the newest version of AssetRipper 2.0, while the one in the REPO wrapper was custom and 1.3 i think.

### Known Issues

Playmode is not running. Some Object inside the scene stops it from starting. Let the Hunt begin

## FAQ

**Q: Question for later**

Answer for later