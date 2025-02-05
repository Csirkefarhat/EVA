# Asteroid Avoidance Game

This repository contains the implementation of an asteroid avoidance game built using three different UI technologies in C#:

- **Windows Forms** using the Model-View (MV) design pattern
- **WPF** using the Model-View-ViewModel (MVVM) design pattern
- **Avalonia** using the Model-View-ViewModel (MVVM) design pattern

The game is built to simulate a spaceship navigating through an asteroid field, with the goal of avoiding collisions for as long as possible. The player can move and must dodge incoming asteroids that spawn at random intervals at the top of the screen. The challenge increases over time as more asteroids appear, and the player must survive as long as possible.

## Features

- **Spaceship Navigation**: Move the spaceship left, right, up, or down.
- **Asteroid Generation**: Asteroids appear at random positions at the top of the screen and move towards the bottom at a constant speed.
- **Increasing Difficulty**: As time progresses, more asteroids appear, making it harder to avoid collisions.
- **Game Pause**: The game can be paused, freezing all action and allowing the player to take a break.
- **Save and Load Game**: While paused, the game can be saved and later resumed, maintaining the state of the game.
- **Game Over**: The game ends when the spaceship collides with an asteroid, and the player's game time is displayed.

## Controls

- **WASD**: Move the spaceship left, right, up, or down.
- **Esc**: Pause the game.

## Saving & Loading

- When the game is paused, there is an option to save the current state.
- Saved games can be loaded and resumed from the point they were saved.
