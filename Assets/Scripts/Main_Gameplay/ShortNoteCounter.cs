using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortNoteCounter : Counter
{
    public ShortNoteCounter(double theTimeStamp, bool longNote, float theNoteLength)
    {
        timeStamp = theTimeStamp;
        isLongNote = longNote;
        noteLength = theNoteLength;
    }

}
