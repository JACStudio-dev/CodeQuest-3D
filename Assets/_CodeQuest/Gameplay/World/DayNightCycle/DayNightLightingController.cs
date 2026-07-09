using CodeQuest.Core.Interfaces;
using CodeQuest.Managers.TimeSystem;
using UnityEngine;

namespace CodeQuest.Gameplay.World.DayNightCycle
{
    /// <summary>
    /// Traduce el progreso del día (IGameClock.DayProgress01) en iluminación: rotación,
    /// color e intensidad de la luz direccional, luz ambiente y exposición del skybox.
    /// Al leer un valor continuo cada frame, la transición es siempre suave, nunca instantánea.
    /// No conoce ni modifica la lógica del tiempo: solo consume IGameClock.
    /// </summary>
    public class DayNightLightingController : MonoBehaviour
    {
        [SerializeField] private Light sunLight;
        [SerializeField] private DayNightVisualSettings visualSettings;

        private IGameClock _clock;
        private Material _runtimeSkyboxInstance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void EnsureExistsInScene()
        {
            if (FindAnyObjectByType<DayNightLightingController>() != null)
            {
                return;
            }

            var go = new GameObject(nameof(DayNightLightingController));
            go.AddComponent<DayNightLightingController>();
        }

        private void Awake()
        {
            if (visualSettings == null)
            {
                visualSettings = ScriptableObject.CreateInstance<DayNightVisualSettings>();
            }

            visualSettings.ApplyDefaultsIfEmpty();

            if (sunLight == null)
            {
                sunLight = ResolveSunLight();
            }

            if (visualSettings.adjustSkyboxExposure && RenderSettings.skybox != null)
            {
                _runtimeSkyboxInstance = new Material(RenderSettings.skybox);
                RenderSettings.skybox = _runtimeSkyboxInstance;
            }
        }

        private void Start()
        {
            _clock = TimeManager.Instance;

            if (sunLight != null && RenderSettings.sun == null)
            {
                RenderSettings.sun = sunLight;
            }
        }

        private void Update()
        {
            if (_clock == null || sunLight == null)
            {
                return;
            }

            ApplyLighting(_clock.DayProgress01);
        }

        private void ApplyLighting(float dayProgress01)
        {
            float sunAngle = (dayProgress01 * 360f) - 90f;
            sunLight.transform.rotation = Quaternion.Euler(sunAngle, visualSettings.sunYaw, 0f);

            sunLight.color = visualSettings.sunColorOverDay.Evaluate(dayProgress01);
            sunLight.intensity = visualSettings.sunIntensityOverDay.Evaluate(dayProgress01) * visualSettings.maxSunIntensity;

            RenderSettings.ambientIntensity = visualSettings.ambientIntensityOverDay.Evaluate(dayProgress01);

            if (visualSettings.adjustSkyboxExposure && _runtimeSkyboxInstance != null && _runtimeSkyboxInstance.HasProperty("_Exposure"))
            {
                _runtimeSkyboxInstance.SetFloat("_Exposure", visualSettings.skyboxExposureOverDay.Evaluate(dayProgress01));
            }
        }

        private static Light ResolveSunLight()
        {
            if (RenderSettings.sun != null)
            {
                return RenderSettings.sun;
            }

            foreach (var light in FindObjectsByType<Light>(FindObjectsSortMode.None))
            {
                if (light.type == LightType.Directional)
                {
                    return light;
                }
            }

            return null;
        }

        private void OnDestroy()
        {
            if (_runtimeSkyboxInstance != null)
            {
                Destroy(_runtimeSkyboxInstance);
            }
        }
    }
}
