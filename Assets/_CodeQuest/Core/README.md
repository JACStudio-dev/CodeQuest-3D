# Core

## Objetivo
Es el corazón de la arquitectura. Contiene únicamente componentes reutilizables.

## Contiene
- Interfaces
- Eventos
- Clases base
- Helpers
- Extensions
- State Machines
- Dependency Injection

## NO contiene
- Player
- Enemy
- Inventory
- UI
- Quests

## Ejemplo correcto

Core/
 Interfaces/IHealth.cs
 Helpers/Timer.cs
 Events/EventBus.cs

## Ejemplo incorrecto

Core/
 PlayerController.cs
 Inventory.cs

## Regla principal
Core puede ser utilizado por cualquier módulo.
Core nunca conoce Gameplay.

## Checklist
- ¿Puede reutilizarse?
- ¿Es independiente del juego?
- ¿Podría copiarse a otro proyecto?

Si la respuesta es sí, pertenece a Core.
