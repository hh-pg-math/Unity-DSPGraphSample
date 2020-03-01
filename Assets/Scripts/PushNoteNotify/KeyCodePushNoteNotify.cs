using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyCodePushNoteNotify : IPushNoteNotify
{
    public static List<(KeyCode key, int noteNumber)> GetDefaultMapping()
    {
        var collection = new List<(KeyCode key, int noteNumber)>();
        collection.Add((KeyCode.A, 60));
        collection.Add((KeyCode.W, 61));
        collection.Add((KeyCode.S, 62));
        collection.Add((KeyCode.E, 63));
        collection.Add((KeyCode.D, 64));
        collection.Add((KeyCode.F, 65));
        collection.Add((KeyCode.T, 66));
        collection.Add((KeyCode.G, 67));
        collection.Add((KeyCode.Y, 68));
        collection.Add((KeyCode.H, 69)); // 440 Hz
        collection.Add((KeyCode.U, 70));
        collection.Add((KeyCode.J, 71));
        collection.Add((KeyCode.K, 72));
        return collection;
    }

    private Action<int> onPushNote = null;
    private Action<int> onReleaseNote = null;

    private Dictionary<KeyCode, int> noteNumberTable = new Dictionary<KeyCode, int>();

    public KeyCodePushNoteNotify(IReadOnlyList<(KeyCode key, int noteNumber)> noteNumberCollection)
    {
        foreach (var pair in noteNumberCollection)
        {
            this.noteNumberTable[pair.key] = pair.noteNumber;
        }
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
        foreach (var key in noteNumberTable.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                onPushNote?.Invoke(noteNumberTable[key]);
            }
            if (Input.GetKeyUp(key))
            {
                onReleaseNote?.Invoke(noteNumberTable[key]);
            }
        }
    }
}
