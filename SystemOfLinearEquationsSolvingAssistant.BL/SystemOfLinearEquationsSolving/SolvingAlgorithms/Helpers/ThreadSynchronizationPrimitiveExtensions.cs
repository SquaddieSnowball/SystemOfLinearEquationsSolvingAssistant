namespace SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.SolvingAlgorithms.Helpers;

internal static class ThreadSynchronizationPrimitiveExtensions
{
    internal static void ParticipantsRemainingWait(this Barrier barrier, int participantCount)
    {
        while (barrier.ParticipantsRemaining != participantCount) { }
    }
}