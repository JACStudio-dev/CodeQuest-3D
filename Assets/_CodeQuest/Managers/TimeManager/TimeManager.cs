using System;
using CodeQuest.Core.Helpers;
using CodeQuest.Core.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeQuest.Managers.TimeSystem
{
    /// <summary>
    /// Coordina el paso del tiempo de juego: inicializa el reloj (WorldClock) a partir de
    /// GameClockSettings, lo avanza cada frame y notifica a los sistemas interesados
    /// (UI, iluminación, NPCs, spawners) mediante los eventos de IGameClock.
    /// La lógica de cálculo del tiempo vive en Core/Helpers/WorldClock, no aquí:
    /// este manager solo coordina.
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public class TimeManager : MonoBehaviour, IGameClock
    {
        public static TimeManager Instance { get; private set; }

        [SerializeField] private GameClockSettings settings;

        [Header("Debug (solo para probar en el Editor)")]
        [Tooltip("En Play Mode: 1 = madrugada, 2 = mañana, 3 = tarde, 4 = noche.")]
        [SerializeField] private bool enableDebugHotkeys = true;

        private WorldClock _clock;
        private GameClockSettings _activeSettings;
        private int _lastMinute = -1;
        private TimePeriod _lastPeriod;

        public int CurrentDay => _clock.CurrentDay;
        public int Hour => _clock.Hour;
        public int Minute => _clock.Minute;
        public float DayProgress01 => _clock.DayProgress01;
        public bool IsPaused { get; private set; }

        public TimePeriod CurrentPeriod => ComputePeriod(Hour);

        public bool IsDawn => CurrentPeriod == TimePeriod.Dawn;
        public bool IsMorning => CurrentPeriod == TimePeriod.Morning;
        public bool IsAfternoon => CurrentPeriod == TimePeriod.Afternoon;
        public bool IsNight => CurrentPeriod == TimePeriod.Night;

        public string FormattedTime => $"{Hour:00}:{Minute:00}";

        public event Action<int, int> OnTimeChanged;
        public event Action<TimePeriod> OnPeriodChanged;
        public event Action<int> OnDayChanged;

        // AfterSceneLoad se ejecuta después de Awake/OnEnable de los objetos de la escena
        // (pero antes de Start), por lo que un TimeManager colocado manualmente en la escena
        // ya habrá asignado Instance y esto no crea un duplicado.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void EnsureExistsInScene()
        {
            if (Instance != null)
            {
                return;
            }

            var go = new GameObject(nameof(TimeManager));
            go.AddComponent<TimeManager>();
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _activeSettings = settings != null ? settings : ScriptableObject.CreateInstance<GameClockSettings>();
            _clock = new WorldClock(
                _activeSettings.dayDurationInRealMinutes,
                _activeSettings.startingHour,
                _activeSettings.startingMinute,
                _activeSettings.timeScaleMultiplier);

            IsPaused = _activeSettings.startPaused;
            _lastMinute = Minute;
            _lastPeriod = CurrentPeriod;
        }

        private void Update()
        {
            if (enableDebugHotkeys)
            {
                ReadDebugHotkeys();
            }

            if (IsPaused)
            {
                return;
            }

            bool dayChanged = _clock.Tick(UnityEngine.Time.deltaTime);

            if (Minute != _lastMinute)
            {
                _lastMinute = Minute;
                OnTimeChanged?.Invoke(Hour, Minute);

                TimePeriod period = CurrentPeriod;
                if (period != _lastPeriod)
                {
                    _lastPeriod = period;
                    OnPeriodChanged?.Invoke(period);
                }
            }

            if (dayChanged)
            {
                OnDayChanged?.Invoke(CurrentDay);
            }
        }

        public bool IsPeriod(TimePeriod period) => CurrentPeriod == period;

        public void Pause() => IsPaused = true;

        public void Resume() => IsPaused = false;

        public void SetTime(int hour, int minute)
        {
            _clock.SetTime(CurrentDay, hour, minute);
            _lastMinute = Minute;
            _lastPeriod = CurrentPeriod;
        }

        public void SetTimeScale(float multiplier)
        {
            _activeSettings.timeScaleMultiplier = multiplier;
            _clock.Configure(_activeSettings.dayDurationInRealMinutes, multiplier);
        }

        private void ReadDebugHotkeys()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return;
            }

            if (keyboard.digit1Key.wasPressedThisFrame)
            {
                SetTime(_activeSettings.dawnStartHour, 0);
            }
            else if (keyboard.digit2Key.wasPressedThisFrame)
            {
                SetTime(_activeSettings.morningStartHour, 0);
            }
            else if (keyboard.digit3Key.wasPressedThisFrame)
            {
                SetTime(_activeSettings.afternoonStartHour, 0);
            }
            else if (keyboard.digit4Key.wasPressedThisFrame)
            {
                SetTime(_activeSettings.nightStartHour, 0);
            }
        }

        private TimePeriod ComputePeriod(int hour)
        {
            if (hour >= _activeSettings.nightStartHour || hour < _activeSettings.dawnStartHour)
            {
                return TimePeriod.Night;
            }

            if (hour >= _activeSettings.afternoonStartHour)
            {
                return TimePeriod.Afternoon;
            }

            if (hour >= _activeSettings.morningStartHour)
            {
                return TimePeriod.Morning;
            }

            return TimePeriod.Dawn;
        }
    }
}
