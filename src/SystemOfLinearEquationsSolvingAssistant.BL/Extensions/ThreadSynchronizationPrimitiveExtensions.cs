namespace SystemOfLinearEquationsSolvingAssistant.BL.Extensions;

internal static class ThreadSynchronizationPrimitiveExtensions
{
    public static void ParticipantsRemainingWait(this Barrier barrier, int participantCount)
    {
        while (barrier.ParticipantsRemaining != participantCount) { }
    }
}