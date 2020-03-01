using System.Collections.Generic;
using Unity.Audio;
using UnityEngine;

public class Demo6 : MonoBehaviour
{
    private AudioOutputHandle _outputHandle;

    private DSPNode[] noteNodes = new DSPNode[128];

    private IPushNoteNotify pushNoteNotify = default;

    // Start is called before the first frame update
    void Start()
    {
        var graph = DSPGraph.Create(SoundFormat.Stereo, 2, 1024, 48000);

        var driver = new DefaultDSPGraphDriver { Graph = graph };
        _outputHandle = driver.AttachToDefaultOutput();

        pushNoteNotify = new KeyCodePushNoteNotify(GetNoteNumberCollection());

        pushNoteNotify.AddPushNoteCallback(noteNumber =>
        {
            var block = graph.CreateCommandBlock();

            noteNodes[noteNumber] = block.CreateDSPNode<SineOscillatorNodeJob.Params, NoProvs, SineOscillatorNodeJob>();
            block.AddOutletPort(noteNodes[noteNumber], 2, SoundFormat.Stereo);
            block.SetFloat<SineOscillatorNodeJob.Params, NoProvs, SineOscillatorNodeJob>(
                noteNodes[noteNumber],
                SineOscillatorNodeJob.Params.Frequency, Utility.NoteNumberToFrequency(noteNumber)
            );

            block.Connect(noteNodes[noteNumber], 0, graph.RootDSP, 0);

            block.Complete();
        });

        pushNoteNotify.AddReleaseNoteCallback(noteNumber =>
        {
            var block = graph.CreateCommandBlock();

            block.ReleaseDSPNode(noteNodes[noteNumber]);

            block.Complete();
        });
    }

    // Update is called once per frame
    void Update()
    {
        pushNoteNotify.Update();
    }

    private void OnDestroy()
    {
        _outputHandle.Dispose();
    }

    private IReadOnlyList<(KeyCode key, int noteNumber)> GetNoteNumberCollection()
    {
        return KeyCodePushNoteNotify.GetDefaultMapping();
    }
}
