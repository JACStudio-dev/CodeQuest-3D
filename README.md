# CodeQuest 3D

CodeQuest 3D es un videojuego desarrollado con Unity. Se trata de un RPG 3D con cámara isométrica (vista superior), enfocado en la exploración, el combate y una progresión lineal con libertad para descubrir el mundo y completar contenido opcional.

## Características

* RPG 3D con perspectiva isométrica.
* Exploración de múltiples zonas conectadas.
* Sistema de combate en tiempo real.
* NPCs, enemigos, misiones y progresión del personaje.
* Arquitectura modular para facilitar el desarrollo colaborativo.

## Tecnologías

* Unity 6
* C#
* Git + GitHub
* Unity Version Control (Plastic SCM)

## Requisitos

* Unity Hub
* Unity 6 (6000.4.5f1)
* Git

## Clonar el proyecto

```bash
git clone https://github.com/JACStudio-dev/CodeQuest-3D
```

Abrir el proyecto desde Unity Hub seleccionando la carpeta raíz del repositorio.

## Estructura del proyecto

```text
Assets/
└── _CodeQuest/
    ├── AI/
    ├── Art/
    ├── Audio/
    ├── Core/
    ├── Data/
    ├── Gameplay/
    ├── Managers/
    ├── Networking/
    ├── Persistence/
    ├── Prefabs/
    ├── Scenes/
    ├── ScriptableObjects/
    ├── Services/
    ├── Settings/
    ├── UI/
    └── VFX/
```

## Flujo de trabajo

* Todo el código fuente y recursos del proyecto se encuentran dentro de `Assets/_CodeQuest`.
* Los directorios `Library`, `Temp`, `Logs` y `UserSettings` no forman parte del repositorio.
* Se utiliza Git para el control de versiones y Unity Version Control como herramienta complementaria.

## Estado del proyecto

Actualmente el proyecto se encuentra en fase inicial de desarrollo y la arquitectura base está siendo construida.

## Licencia

Pendiente de definir.
