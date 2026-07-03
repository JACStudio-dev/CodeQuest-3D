# Managers

## Objetivo
Coordinar sistemas globales.

## Managers recomendados
- GameManager
- SceneManager
- AudioManager
- SaveManager
- PoolManager

## Responsabilidad
Coordinar, no implementar toda la lógica.

## Error común

GameManager con 3000 líneas de código.

## Correcto

GameManager
 ├─ Inicializa servicios
 ├─ Cambia estados
 └─ Coordina módulos

La lógica permanece dentro de cada módulo.
