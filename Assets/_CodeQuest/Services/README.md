# Services

## Objetivo
Proveer funcionalidades reutilizables.

## Ejemplos
- Localization
- Cloud Save
- Addressables
- Analytics
- Leaderboards

## Cuándo crear un Service

Si una funcionalidad puede ser utilizada por distintos módulos.

## Ejemplo

Player
      ↘
Inventory ----> LocalizationService
      ↗
UI

Todos consumen el mismo servicio.

## No usar Services para lógica exclusiva del gameplay.
