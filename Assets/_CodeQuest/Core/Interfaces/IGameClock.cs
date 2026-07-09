using System;

namespace CodeQuest.Core.Interfaces
{
    /// <summary>
    /// Contrato del reloj de juego. Cualquier módulo (UI, NPC, Quests, Spawners)
    /// depende de esta abstracción en lugar de conocer al TimeManager concreto.
    /// </summary>
    public interface IGameClock
    {
        int CurrentDay { get; }
        int Hour { get; }
        int Minute { get; }

        /// <summary>Progreso del día actual, de 0 (00:00) a 1 (justo antes de 00:00 del día siguiente).</summary>
        float DayProgress01 { get; }

        TimePeriod CurrentPeriod { get; }
        bool IsPaused { get; }

        bool IsDawn { get; }
        bool IsMorning { get; }
        bool IsAfternoon { get; }
        bool IsNight { get; }

        /// <summary>Devuelve la hora formateada como "HH:mm", ej. "00:00", "23:59".</summary>
        string FormattedTime { get; }

        bool IsPeriod(TimePeriod period);

        void Pause();
        void Resume();
        void SetTime(int hour, int minute);
        void SetTimeScale(float multiplier);

        /// <summary>Se dispara cada vez que cambia el minuto de juego.</summary>
        event Action<int, int> OnTimeChanged;

        /// <summary>Se dispara únicamente cuando cambia la franja horaria (madrugada/mañana/tarde/noche).</summary>
        event Action<TimePeriod> OnPeriodChanged;

        /// <summary>Se dispara al completarse un día y comenzar el siguiente.</summary>
        event Action<int> OnDayChanged;
    }
}
