using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : Note
{
    public float heldNoteLength;
    public float assignedEndTime;

    public GameObject objBegin;
    public GameObject objTrail;
    public GameObject objEnd;

    public Color32 processedColorEdges;
    public Color32 processedColorTrail;
    public static bool isProcessed;

    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject();

    }

    public void MoveObject()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));
        if (t > 0.6)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
