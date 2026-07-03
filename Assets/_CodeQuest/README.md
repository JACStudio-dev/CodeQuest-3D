# Arquitectura del Proyecto Unity

# Objetivo
Este proyecto utiliza una arquitectura modular para facilitar el trabajo colaborativo.

## Principios
- Una responsabilidad por módulo.
- Bajo acoplamiento.
- Alta cohesión.
- Código reutilizable.
- Escalable.
- Fácil de mantener.

## Flujo de dependencias

Gameplay --> Core
Managers --> Core
Services --> Core
UI --> Gameplay/Core

Core nunca depende de Gameplay.

## ¿Dónde creo un script?

- Lógica del jugador -> Gameplay
- Interfaces -> Core
- Audio global -> Managers
- Guardado -> Persistence
- Localización -> Services

## Antes de crear un script
1. ¿Ya existe?
2. ¿Qué responsabilidad tendrá?
3. ¿En qué módulo vive?
4. ¿Depende de otro módulo?

## Errores comunes
- Crear Managers para todo.
- Duplicar utilidades.
- Referencias cruzadas entre módulos.
- Mezclar UI con lógica del juego.
