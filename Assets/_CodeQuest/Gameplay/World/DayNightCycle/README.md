# Day Night Cycle

## Objetivo
Sistema de paso del tiempo del mundo (1 minuto real = 1 hora de juego, día completo = 24 minutos reales)
y la iluminación que reacciona a él.

## Piezas y responsabilidades

- `Core/Interfaces/IGameClock.cs` — contrato del reloj. Cualquier módulo (UI, NPC, Quests) programa
  contra esta interfaz, nunca contra `TimeManager` directamente.
- `Core/Interfaces/TimePeriod.cs` — enum Dawn/Morning/Afternoon/Night (madrugada/mañana/tarde/noche).
- `Core/Helpers/WorldClock.cs` — matemática pura del tiempo (sin UnityEngine). Testeable en aislamiento.
- `Managers/TimeManager/TimeManager.cs` — MonoBehaviour singleton que coordina el `WorldClock`, lo avanza
  cada frame y dispara los eventos de `IGameClock`. Se auto-instancia al arrancar el juego
  (`RuntimeInitializeOnLoadMethod`), no requiere configuración manual en la escena.
- `Managers/TimeManager/GameClockSettings.cs` — ScriptableObject con toda la configuración
  (duración del día, hora inicial, velocidad, franjas horarias).
- `Gameplay/World/DayNightCycle/DayNightLightingController.cs` — traduce `DayProgress01` en rotación,
  color e intensidad de la luz direccional, luz ambiente y exposición del skybox. Se auto-instancia
  igual que `TimeManager` y busca la luz direccional de la escena si no se le asigna una.
- `Gameplay/World/DayNightCycle/DayNightVisualSettings.cs` — ScriptableObject con las curvas/gradiente
  que definen el aspecto visual del ciclo.

## Cómo consultarlo desde otro script

```csharp
IGameClock clock = TimeManager.Instance;
if (clock.IsNight) { /* encender antorchas */ }
string hora = clock.FormattedTime; // "23:59"
clock.OnPeriodChanged += period => Debug.Log($"Ahora es {period}");
```

## Configuración recomendada en el Editor (opcional)

El sistema funciona out-of-the-box con valores por defecto razonables. Para ajustarlo desde el
Inspector de forma persistente:

1. `Assets > Create > CodeQuest > Time > Game Clock Settings` y asignarlo al campo `settings` de un
   GameObject con `TimeManager` en la escena.
2. `Assets > Create > CodeQuest > World > Day Night Visual Settings` y asignarlo al campo
   `visualSettings` de un GameObject con `DayNightLightingController`.

## Extensiones futuras
Ver `ESTADO_DESARROLLO.md`.
