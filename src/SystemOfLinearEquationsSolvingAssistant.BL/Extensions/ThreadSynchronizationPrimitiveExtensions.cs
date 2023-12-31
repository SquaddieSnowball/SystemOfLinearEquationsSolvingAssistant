namespace SystemOfLinearEquationsSolvingAssistant.BL.Extensions;

/// <summary>
/// Provides extension methods for thread synchronization primitives.
/// </summary>
internal static class ThreadSynchronizationPrimitiveExtensions
{
    /// <summary>
    /// Waits until the number of remaining participants reaches the specified value.
    /// </summary>
    /// <param name="barrier"><see cref="Barrier"/> instance to wait for.</param>
    /// <param name="participantCount">Number of participants to wait.</param>
    public static void ParticipantsRemainingWait(this Barrier barrier, int participantCount)
    {
        while (barrier.ParticipantsRemaining != participantCount) { }
    }
}