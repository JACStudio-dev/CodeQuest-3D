namespace CodeQuest.Core.Interfaces
{
    /// <summary>
    /// Franja horaria del día de juego. Los límites de cada franja son configurables
    /// (ver GameClockSettings) para no atarse a valores mágicos.
    /// </summary>
    public enum TimePeriod
    {
        Dawn,       // Madrugada
        Morning,    // Mañana
        Afternoon,  // Tarde
        Night       // Noche
    }
}
