using MidiJack;
using System;

public class MidiPushNoteNotify : IPushNoteNotify
{
    private Action<int> onPushNote = null;
    private Action<int> onReleaseNote = null;

    public MidiPushNoteNotify()
    {
        MidiMaster.noteOnDelegate += (_, noteNumber, __) =>
        {
            onPushNote?.Invoke(noteNumber);
        };

        MidiMaster.noteOffDelegate += (_, noteNumber) =>
        {
            onReleaseNote?.Invoke(noteNumber);
        };
    }

    public void AddPushNoteCallback(Action<int> callback)
    {
        onPushNote += callback;
    }

    public void AddReleaseNoteCallback(Action<int> callback)
    {
        onReleaseNote += callback;
    }

    public void RemovePushNoteCallaback(Action<int> callback)
    {
        onPushNote -= callback;
    }

    public void RemoveReleaseNoteCallback(Action<int> callback)
    {
        onReleaseNote -= callback;
    }

    public void Update()
    {
    }
}