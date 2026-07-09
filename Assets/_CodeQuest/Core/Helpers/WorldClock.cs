using System;

namespace CodeQuest.Core.Helpers
{
    /// <summary>
    /// Matemática pura del paso del tiempo de juego. No depende de UnityEngine ni de MonoBehaviour,
    /// por lo que es reutilizable y fácil de probar de forma aislada.
    /// </summary>
    public sealed class WorldClock
    {
        private const double SecondsPerDay = 24d * 60d * 60d;

        private double _secondsPerRealSecond;
        private double _totalGameSeconds;

        public int CurrentDay { get; private set; }

        public WorldClock(double dayDurationInRealMinutes, int startingHour, int startingMinute, float timeScaleMultiplier)
        {
            Configure(dayDurationInRealMinutes, timeScaleMultiplier);
            SetTime(0, startingHour, startingMinute);
        }

        /// <summary>Recalcula cuántos segundos de juego avanzan por cada segundo real.</summary>
        public void Configure(double dayDurationInRealMinutes, float timeScaleMultiplier)
        {
            if (dayDurationInRealMinutes <= 0d)
            {
                throw new ArgumentOutOfRangeException(nameof(dayDurationInRealMinutes), "La duración del día debe ser mayor que 0.");
            }

            double realSecondsPerDay = dayDurationInRealMinutes * 60d;
            _secondsPerRealSecond = (SecondsPerDay / realSecondsPerDay) * timeScaleMultiplier;
        }

        /// <summary>Avanza el reloj según el tiempo real transcurrido, en segundos.</summary>
        /// <returns>true si el día cambió durante este avance.</returns>
        public bool Tick(double realDeltaTimeSeconds)
        {
            double previousTotal = _totalGameSeconds;
            _totalGameSeconds += realDeltaTimeSeconds * _secondsPerRealSecond;

            int daysElapsed = (int)Math.Floor(_totalGameSeconds / SecondsPerDay) - (int)Math.Floor(previousTotal / SecondsPerDay);
            if (daysElapsed > 0)
            {
                CurrentDay += daysElapsed;
                return true;
            }

            return false;
        }

        public void SetTime(int day, int hour, int minute)
        {
            hour = Mod(hour, 24);
            minute = Mod(minute, 60);

            CurrentDay = day;
            _totalGameSeconds = (hour * 3600d) + (minute * 60d);
        }

        public int Hour => (int)(SecondsIntoDay() / 3600d);

        public int Minute => (int)((SecondsIntoDay() % 3600d) / 60d);

        public float DayProgress01 => (float)(SecondsIntoDay() / SecondsPerDay);

        private double SecondsIntoDay()
        {
            double seconds = _totalGameSeconds % SecondsPerDay;
            return seconds < 0 ? seconds + SecondsPerDay : seconds;
        }

        private static int Mod(int value, int modulus)
        {
            int result = value % modulus;
            return result < 0 ? result + modulus : result;
        }
    }
}
