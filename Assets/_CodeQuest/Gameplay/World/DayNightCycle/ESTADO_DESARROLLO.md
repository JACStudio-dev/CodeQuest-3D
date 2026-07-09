# Estado del desarrollo — Sistema de tiempo y ciclo día/noche

> Este archivo se mantiene actualizado durante todo el desarrollo de la feature
> `features/SistemaDiaNoche` para poder retomar el trabajo si se pierde el contexto de la conversación.

## Rama
`features/SistemaDiaNoche` (creada desde `main`, commit `8fb6759`).

## 0. Cambio pendiente de commitear (posterior al commit `793f9ee`)
Se agregaron teclas de acceso rápido de **debug** en `TimeManager.cs` para que se pueda probar el
ciclo sin escribir código: en Play Mode, teclas `1`/`2`/`3`/`4` saltan a madrugada/mañana/tarde/noche
(usa el nuevo Input System vía `UnityEngine.InputSystem.Keyboard`, ya que el proyecto tiene
`activeInputHandler: 2` — solo Input System, el `Input` clásico lanzaría excepción). Controlado por
el campo `enableDebugHotkeys` (bool, default `true`) en el Inspector. Este cambio todavía no tiene
commit nuevo en git ni se subió a Plastic — queda pendiente junto con el checkin descrito en
`ESTADO_SESION.md` (raíz del repo).

## 1. Archivos creados

| Archivo | Rol |
|---|---|
| `Assets/_CodeQuest/Core/Interfaces/TimePeriod.cs` | Enum Dawn/Morning/Afternoon/Night |
| `Assets/_CodeQuest/Core/Interfaces/IGameClock.cs` | Contrato del reloj de juego |
| `Assets/_CodeQuest/Core/Helpers/WorldClock.cs` | Matemática pura del tiempo (sin UnityEngine) |
| `Assets/_CodeQuest/Managers/TimeManager/GameClockSettings.cs` | ScriptableObject de configuración del tiempo |
| `Assets/_CodeQuest/Managers/TimeManager/TimeManager.cs` | MonoBehaviour singleton que coordina el reloj |
| `Assets/_CodeQuest/Gameplay/World/DayNightCycle/DayNightVisualSettings.cs` | ScriptableObject de configuración visual |
| `Assets/_CodeQuest/Gameplay/World/DayNightCycle/DayNightLightingController.cs` | Aplica iluminación según el tiempo |
| `Assets/_CodeQuest/Gameplay/World/DayNightCycle/README.md` | Documentación del módulo |
| `Assets/_CodeQuest/Gameplay/World/DayNightCycle/ESTADO_DESARROLLO.md` | Este archivo |

## 2. Archivos modificados
Ninguno. Todo el trabajo es aditivo — no se tocó ninguna escena, asset ni script existente
(incluyendo los cambios ya presentes sin commitear en `main` antes de empezar: escena `mundo_real.unity`,
`DefaultVolumeProfile.asset`, `UniversalRenderPipelineGlobalSettings.asset`, borrado de
`playes_controller.cs`/`handler_input.cs`). Esos cambios se dejaron intactos tal cual estaban.

## 3. Scripts nuevos
Ver tabla de "Archivos creados". Total: 7 scripts/assets de código + 2 documentos.

## 4. Decisiones de arquitectura

- **Núcleo desacoplado de Unity**: `WorldClock` es una clase C# pura (sin `MonoBehaviour` ni
  `UnityEngine`), vive en `Core/Helpers` porque es reutilizable y testeable de forma aislada,
  siguiendo el propio `Core/README.md` del proyecto ("¿Podría copiarse a otro proyecto? -> pertenece a Core").
- **Contrato público en `Core/Interfaces`**: `IGameClock` es lo único que el resto del proyecto
  (UI, NPCs, Quests) debería conocer. Así, mañana se puede cambiar la implementación (por ejemplo,
  sincronizar el tiempo por red) sin tocar a los consumidores.
- **`TimeManager` en `Managers/`**: coordina (inicializa `WorldClock`, lo avanza, dispara eventos),
  pero no implementa la lógica del tiempo — cumple la regla del propio `Managers/README.md`
  ("GameManager con 3000 líneas" es el error a evitar).
- **`DayNightLightingController` en `Gameplay/World/DayNightCycle/`**: es lógica de mundo específica
  del juego (igual que el ejemplo de "Fishing" del `Gameplay/README.md`), no una responsabilidad de
  infraestructura, por eso no vive en `Managers/`.
- **Auto-bootstrap (`RuntimeInitializeOnLoadMethod`)**: tanto `TimeManager` como
  `DayNightLightingController` se crean solos al iniciar el juego si no existen ya en la escena.
  Se eligió este patrón para no tener que editar a mano el archivo `.unity` de la escena
  (evita el riesgo de corromper la escena sin poder validarla en el Editor) y para que el sistema
  funcione "out of the box" sin pasos manuales. Sigue siendo 100% configurable desde el Inspector
  si el equipo decide colocar los componentes manualmente en la escena con sus ScriptableObjects.
- **ScriptableObjects para configuración** (`GameClockSettings`, `DayNightVisualSettings`): evita
  valores mágicos hardcodeados y permite tener distintos perfiles (ej. un día muy corto para testing).
- **Transición suave por construcción**: la iluminación se recalcula cada frame a partir de un valor
  continuo (`DayProgress01`), no por saltos discretos entre "de día" y "de noche", así que nunca hay
  un cambio instantáneo.

## 5. Qué falta por hacer
- Nada bloqueante para el objetivo pedido. El sistema es funcional de punta a punta.
- Pendiente de verificación manual en el Editor de Unity (ver sección 6): no se pudo abrir Unity
  desde este entorno para probar en Play Mode.
- Opcional / futuro: ver README.md del módulo, sección "Extensiones futuras".

## 6. Problemas encontrados
- No hay acceso a un Editor de Unity en este entorno para compilar/ejecutar y verificar visualmente.
  Por eso se evitó cualquier edición manual de archivos `.unity`/`.asset` binarios-YAML (alto riesgo
  de corrupción sin poder validar), y se optó por el patrón auto-bootstrap para que el sistema
  funcione sin tocar la escena.
- El proyecto no tenía aún ningún script real (`Core`, `Managers`, `Gameplay` estaban vacíos, solo
  con `README.md`), así que no había convenciones de namespace ni patrón de singleton previos que
  reutilizar. Se definieron desde cero siguiendo los `README.md` de cada carpeta.
- Nombrar el namespace `CodeQuest.Managers.Time` colisionaba con `UnityEngine.Time`. Se renombró a
  `CodeQuest.Managers.TimeSystem`.
- El auto-bootstrap de `TimeManager` usaba `RuntimeInitializeLoadType.BeforeSceneLoad`, momento en el
  que los objetos de la escena todavía no existen: si alguien colocaba un `TimeManager` manualmente en
  la escena, el auto-creado (con settings por defecto) se instanciaba igual y el manual terminaba
  autodestruyéndose por ser el "duplicado". Se corrigió usando `AfterSceneLoad`, que se ejecuta
  después del `Awake` de los objetos de la escena.
- **Limitación conocida (no resuelta, fuera de alcance actual)**: `RuntimeInitializeOnLoadMethod` solo
  se dispara una vez por ejecución del juego, no en cada carga de escena. Con un único escenario
  (`mundo_real.unity`) esto no es un problema. Si en el futuro el proyecto añade más escenas y cada una
  necesita su propio ciclo día/noche ligado a su propia luz direccional, habrá que enganchar
  `DayNightLightingController` a `SceneManager.sceneLoaded` para re-resolver la luz del sol en cada
  cambio de escena. No se implementó porque no hay hoy ninguna escena adicional que lo requiera.

## 7. Próximo paso recomendado
1. Abrir el proyecto en Unity (se generarán los `.meta` de los scripts nuevos automáticamente).
2. Entrar en Play Mode en `mundo_real.unity` y confirmar que el `Directional Light` gira y cambia
   de color/intensidad de forma continua a lo largo de un ciclo de 24 minutos.
3. (Opcional) Crear los assets `GameClockSettings` y `DayNightVisualSettings` desde el menú
   `Assets > Create > CodeQuest > ...` y asignarlos para ajustar horas/curvas desde el Inspector.
4. Hacer commit de los archivos nuevos en esta rama y abrir el PR contra `main`.
