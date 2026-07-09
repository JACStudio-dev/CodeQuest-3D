using UnityEngine;

namespace CodeQuest.Gameplay.World.DayNightCycle
{
    /// <summary>
    /// Configuración visual del ciclo día/noche, editable desde el Inspector.
    /// Crear con: Assets > Create > CodeQuest > World > Day Night Visual Settings.
    /// Todas las curvas se evalúan con el progreso del día (0 = 00:00, 1 = 24:00),
    /// por lo que la transición es siempre continua y nunca instantánea.
    /// </summary>
    [CreateAssetMenu(menuName = "CodeQuest/World/Day Night Visual Settings", fileName = "DayNightVisualSettings")]
    public class DayNightVisualSettings : ScriptableObject
    {
        [Header("Rotación del sol")]
        [Tooltip("Dirección (eje Y) desde la que sale y se pone el sol.")]
        public float sunYaw = 170f;

        [Header("Color e intensidad del sol")]
        public Gradient sunColorOverDay = new Gradient();
        [Tooltip("Multiplicador de intensidad de la luz direccional a lo largo del día.")]
        public AnimationCurve sunIntensityOverDay = new AnimationCurve();
        [Min(0f)] public float maxSunIntensity = 1.2f;

        [Header("Luz ambiente")]
        public AnimationCurve ambientIntensityOverDay = new AnimationCurve();

        [Header("Skybox")]
        [Tooltip("Si está activo, ajusta la exposición del material de skybox (se instancia en tiempo de ejecución, no modifica el asset original).")]
        public bool adjustSkyboxExposure = true;
        public AnimationCurve skyboxExposureOverDay = new AnimationCurve();

        private void Reset() => ApplyDefaultsIfEmpty();

        private void OnValidate() => ApplyDefaultsIfEmpty();

        /// <summary>Rellena las curvas/gradiente con valores por defecto razonables si están vacíos.</summary>
        public void ApplyDefaultsIfEmpty()
        {
            if (sunColorOverDay == null || sunColorOverDay.colorKeys.Length == 0)
            {
                sunColorOverDay = BuildDefaultSunGradient();
            }

            if (sunIntensityOverDay == null || sunIntensityOverDay.length == 0)
            {
                sunIntensityOverDay = BuildDefaultDayCurve(0f, 1f);
            }

            if (ambientIntensityOverDay == null || ambientIntensityOverDay.length == 0)
            {
                ambientIntensityOverDay = BuildDefaultDayCurve(0.15f, 1f);
            }

            if (skyboxExposureOverDay == null || skyboxExposureOverDay.length == 0)
            {
                skyboxExposureOverDay = BuildDefaultDayCurve(0.15f, 1.3f);
            }
        }

        private static Gradient BuildDefaultSunGradient()
        {
            var gradient = new Gradient();
            var night = new Color(0.05f, 0.06f, 0.15f);
            var sunrise = new Color(1f, 0.6f, 0.3f);
            var day = new Color(1f, 0.95f, 0.85f);
            var sunset = new Color(1f, 0.45f, 0.25f);

            gradient.SetKeys(
                new[]
                {
                    new GradientColorKey(night, 0.00f),
                    new GradientColorKey(sunrise, 0.27f),
                    new GradientColorKey(day, 0.40f),
                    new GradientColorKey(day, 0.60f),
                    new GradientColorKey(sunset, 0.75f),
                    new GradientColorKey(night, 0.85f),
                    new GradientColorKey(night, 1.00f),
                },
                new[]
                {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(1f, 1f),
                });

            return gradient;
        }

        private static AnimationCurve BuildDefaultDayCurve(float nightValue, float dayValue)
        {
            return new AnimationCurve(
                new Keyframe(0.00f, nightValue),
                new Keyframe(0.23f, nightValue),
                new Keyframe(0.32f, dayValue),
                new Keyframe(0.68f, dayValue),
                new Keyframe(0.80f, nightValue),
                new Keyframe(1.00f, nightValue));
        }
    }
}
