# Gameplay

## Objetivo
Contiene toda la lógica específica del videojuego.

## Contiene
- Player
- NPC
- Enemies
- Combat
- Inventory
- Skills
- Quests
- World

## Flujo recomendado

Input
 ↓
PlayerController
 ↓
CharacterMotor
 ↓
Combat
 ↓
UI

## No debe contener
- Guardado
- Audio global
- Configuración
- Analytics

## Ejemplo
Si agregas un sistema de pesca:
Gameplay/World/Fishing

No crear:
Core/Fishing
