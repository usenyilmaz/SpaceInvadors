# 🚀 SpaceInvadors

A console-based Space Invaders clone written in C#. Dodge falling obstacles, survive as long as possible, and rack up points — all inside your Windows terminal.

---

## Table of Contents

- [About](#about)
- [Gameplay](#gameplay)
- [Controls](#controls)
- [Requirements](#requirements)
- [Installation & Running](#installation--running)
- [Project Structure](#project-structure)
- [How It Works](#how-it-works)
- [Releases](#releases)
- [Known Limitations](#known-limitations)

---

## About

SpaceInvadors is a lightweight terminal game built with pure C# and no external libraries. The game renders entirely in the Windows console, using ASCII characters to represent the player, obstacles, and the game boundary. The window is locked to a fixed size (120×30) to ensure consistent rendering.

---

## Gameplay

- Rows of randomly generated obstacles (`#` characters) fall down the screen at regular intervals.
- You control a player character (`@`) at the bottom of the screen and must dodge the falling obstacles.
- Every obstacle row that passes the player without a collision earns you **+1 point**.
- The game ends immediately upon collision between the player and an obstacle tile.
- After a game over, the final score is displayed and the game restarts automatically.

```
|                              |   Points: 7
|   ##  #  ##  #   #  ## ##   |
|                              |
|                              |
|                    @         |
```

---

## Controls

| Key          | Action              |
|--------------|---------------------|
| `←` Left     | Move player left    |
| `→` Right    | Move player right   |
| `Escape`     | Quit to main menu   |
| `P`          | Start game          |
| `E`          | Exit application    |

---

## Requirements

- **OS:** Windows (required — uses `user32.dll` and `kernel32.dll` Win32 APIs to lock the console window size)
- **Runtime:** None — if you download the standalone `v1.1.0` executable, the .NET runtime is bundled inside. Only needed if building from source: [.NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- No third-party NuGet packages are used.

---

## Installation & Running

### Clone the repository

```bash
git clone https://github.com/usenyilmaz/SpaceInvadors.git
cd SpaceInvadors
```

### Run with .NET CLI

```bash
dotnet run
```

### Build a standalone executable

```bash
dotnet build -c Release
```

The output binary will be located at:
```
bin/Release/net10.0/SpaceInvadors.exe
```

### Run the pre-built binary

Download the latest release from the [Releases](https://github.com/usenyilmaz/SpaceInvadors/releases) page and run `SpaceInvadors.exe` directly — no SDK installation needed.

> **Note:** Run the game in a standard Windows terminal (cmd or PowerShell). The application automatically resizes the console window to 120×30 and disables the maximize/resize buttons.

---

## Project Structure

```
SpaceInvadors/
├── Program.cs              # Entry point — main menu, window setup, game loop launcher
├── GamePanel.cs            # Core game loop — input handling, logic updates, rendering
├── Obstacle.cs             # Obstacle entity — position, string representation, fall logic
├── GameOver.cs             # Game over screen — displays final score
├── SpaceInvadors.csproj    # .NET project file (targets net10.0)
├── .gitignore
└── .gitattributes
```

### Class overview

**`Program`** — Sets the console window to 120×30, disables resizing via Win32 API calls, and presents the start menu. On `P`, enters an infinite loop that calls `StartGame()`, allowing the player to restart after each game over.

**`GamePanel`** — The game engine. Runs the main game loop at ~20 FPS (`Thread.Sleep(50)`). Every 1 second, a new randomly generated obstacle row is spawned and all existing rows fall by 2 tiles. Handles left/right player movement and escape key. Checks player-obstacle collisions on every frame.

**`Obstacle`** — Represents a single row of obstacles. Stores a 30-character string where `#` is a solid tile and ` ` (space) is a gap. Exposes `fall(int speed)` to move the row downward and `getY()` / `getObstacleString()` for rendering and collision checks.

**`GameOver`** — Displays the game over screen with the player's final score.

---

## How It Works

1. On startup, the console is resized to 120 columns × 30 rows and resize controls are disabled via `SetWindowLong`.
2. The player starts at the horizontal center of the game boundary, one row above the bottom.
3. Each second, `UpdateLogic()` generates a new obstacle row — each of the 30 tiles has a 25% chance of being a `#` block. The row is enqueued and all queued rows fall by 2 rows.
4. Rows that travel past the bottom of the screen are dequeued and award the player 1 point each.
5. `checkCollisions()` scans every queued obstacle on every frame. If any `#` tile shares the same coordinates as the player, the game loop exits and the game over screen is shown.
6. `Draw()` clears the console and redraws the boundary walls, all obstacle rows, the player character, and the score on every frame.

---

## Releases

| Version | Date       | Notes                        |
|---------|------------|------------------------------|
| v1.1.0  | 2026-05-20 | Self-contained build — no dependencies required, runs on any Windows machine |
| v1.0.0  | 2026-05-17 | Initial release — requires .NET 10.0 SDK |

### v1.1.0 — Standalone Release (Recommended)

Download `SpaceInvadors.exe` from the [v1.1.0 release page](https://github.com/usenyilmaz/SpaceInvadors/releases/tag/v1.1.0) and run it directly. No .NET installation or any other dependency required — the runtime is bundled inside the executable. Works on any 64-bit Windows machine out of the box.

### v1.0.0 — Source / SDK Release

The initial release. Requires [.NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) to be installed. Download from the [v1.0.0 release page](https://github.com/usenyilmaz/SpaceInvadors/releases/tag/v1.0.0).

---

## Known Limitations

- **Windows only.** The game uses P/Invoke calls to `user32.dll` and `kernel32.dll` for console window management, which are not available on macOS or Linux.
- The obstacle fall speed and spawn interval are currently hardcoded (`tileSpeed = 2`, `interval = 1000 ms`) — there is no difficulty scaling over time.
- The console is cleared and fully redrawn every frame (~20 FPS), which may cause visible flicker on some terminals.

---

## Author

**Ural Şenyılmaz** — [@usenyilmaz](https://github.com/usenyilmaz)
