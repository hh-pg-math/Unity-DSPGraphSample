using System;
using System.Collections;

public interface IPushNoteNotify
{
    void AddPushNoteCallback(Action<int> callback);
    void RemovePushNoteCallaback(Action<int> callback);

    void AddReleaseNoteCallback(Action<int> callback);
    void RemoveReleaseNoteCallback(Action<int> callback);

    void Update();
}
