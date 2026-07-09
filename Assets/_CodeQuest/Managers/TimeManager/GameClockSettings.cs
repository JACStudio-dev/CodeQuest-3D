using UnityEngine;

namespace CodeQuest.Managers.TimeSystem
{
    /// <summary>
    /// Configuración del ciclo de tiempo, editable desde el Inspector.
    /// Crear con: Assets > Create > CodeQuest > Time > Game Clock Settings.
    /// </summary>
    [CreateAssetMenu(menuName = "CodeQuest/Time/Game Clock Settings", fileName = "GameClockSettings")]
    public class GameClockSettings : ScriptableObject
    {
        [Header("Duración del día")]
        [Tooltip("Minutos reales que dura un día completo de juego. 24 = 1 minuto real por hora de juego.")]
        [Min(0.01f)]
        public float dayDurationInRealMinutes = 24f;

        [Header("Hora inicial")]
        [Range(0, 23)] public int startingHour = 6;
        [Range(0, 59)] public int startingMinute = 0;

        [Header("Velocidad")]
        [Tooltip("Multiplicador adicional sobre la duración configurada del día. 1 = velocidad normal.")]
        [Min(0f)]
        public float timeScaleMultiplier = 1f;

        public bool startPaused = false;

        [Header("Franjas horarias (hora de inicio de cada una)")]
        [Tooltip("Hora en la que comienza la madrugada.")]
        [Range(0, 23)] public int dawnStartHour = 5;
        [Tooltip("Hora en la que comienza la mañana.")]
        [Range(0, 23)] public int morningStartHour = 8;
        [Tooltip("Hora en la que comienza la tarde.")]
        [Range(0, 23)] public int afternoonStartHour = 13;
        [Tooltip("Hora en la que comienza la noche.")]
        [Range(0, 23)] public int nightStartHour = 19;
    }
}
