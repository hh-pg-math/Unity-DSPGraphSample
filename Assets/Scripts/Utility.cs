using Unity.Mathematics;

public static class Utility
{
    // 69 = 440 Hz
    public static float NoteNumberToFrequency(int noteNumber)
    {
        return math.pow(2, (noteNumber - 69) / 12.0f) * 440.0f;
    }
}
