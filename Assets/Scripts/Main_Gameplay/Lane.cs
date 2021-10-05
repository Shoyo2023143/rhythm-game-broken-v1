using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab, longNotePrefab;
    List<Note> notes = new List<Note>();
    public List<Counter> timeStamps = new List<Counter>();

    int spawnIndex = 0;
    int inputIndex = 0;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    public GameObject laneParticle;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                var metricNoteLength = LengthConverter.ConvertTo<MetricTimeSpan>(note.Length, metricTimeSpan, SongManager.midiFile.GetTempoMap());
                var currentTimeStamp = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
                var currentNoteLength = metricNoteLength.Minutes * 60f + metricNoteLength.Seconds + metricNoteLength.Milliseconds / 1000f;
                if (note.LengthAs<MusicalTimeSpan>(SongManager.midiFile.GetTempoMap()) <= MusicalTimeSpan.Sixteenth)
                {
                    timeStamps.Add(new ShortNoteCounter(currentTimeStamp, false, currentNoteLength));
                }
                else if (note.LengthAs<MusicalTimeSpan>(SongManager.midiFile.GetTempoMap()) >= MusicalTimeSpan.Sixteenth)
                {
                    timeStamps.Add(new ShortNoteCounter(currentTimeStamp, true, currentNoteLength));
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex].timeStamp - SongManager.Instance.noteTime)
            {
                if (timeStamps[spawnIndex].isLongNote)
                    LongNoteSpawn();
                else
                    NormalNoteSpawn();
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex].timeStamp;
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            Hits(timeStamp, marginOfError, audioTime);

            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                Instantiate(missEffect, new Vector3(0, -4, 0), missEffect.transform.rotation);
                inputIndex++;
            }
        }
    }

    private void Hit()
    {
        ScoreManager.Hit();
    }
    private void GoodHit()
    {
        ScoreManager.GoodHit();
    }
    private void PerfectHit()
    {
        ScoreManager.PerfectHit();
    }
    private void Miss()
    {
        ScoreManager.Miss();
    }

    private void EarlyStop()
    {
        ScoreManager.Miss();
    }

    private void LateStop()
    {
        ScoreManager.Miss();
    }

    private void ShowEffects(GameObject effectType)
    {
        Instantiate(effectType, new Vector3(0, -4, 0), effectType.transform.rotation);
        Destroy(notes[inputIndex].gameObject);
        Instantiate(laneParticle, new Vector3(transform.position.x, transform.position.y - 4, 0), laneParticle.transform.rotation);
        inputIndex++;
    }

    private void Hits(double stamp, double margin, double time)
    {
        if (Input.GetKeyDown(input))
        {
            if (Math.Abs(time - stamp) <= (margin) && Math.Abs(time - stamp) > (margin - 0.03))
            {
                Hit();
                ShowEffects(hitEffect);
            }
            else if (Math.Abs(time - stamp) <= (margin - 0.03) && Math.Abs(time - stamp) > (margin - 0.07))
            {
                GoodHit();
                ShowEffects(goodEffect);
            }
            else if (Math.Abs(time - stamp) < (margin - 0.07))
            {
                PerfectHit();
                ShowEffects(perfectEffect);
            }
        }
    }

    private void NormalNoteSpawn()
    {
        var note = Instantiate(notePrefab, transform);
        notes.Add(note.GetComponent<Note>());
        note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex].timeStamp;

        spawnIndex++;
    }

    private void LongNoteSpawn()
    {
        var longNote = Instantiate(longNotePrefab, transform);
        notes.Add(longNote.GetComponent<LongNote>());
        var component = longNote.GetComponent<LongNote>();
        component.assignedTime = (float)timeStamps[spawnIndex].timeStamp;
        component.assignedEndTime = component.assignedTime + timeStamps[spawnIndex].noteLength;
        component.heldNoteLength = component.assignedEndTime - component.assignedTime;
        //component.LongNoteRender();
        Debug.Log(component.assignedEndTime.ToString());

        spawnIndex++;
    }
}