using System;
using UnityEngine;

[System.Serializable]
public class SurfaceAudioClips
{
    public SurfaceTypeEnum surfaceType;
    public AudioClip[] footstepClips;

    public static implicit operator AudioClip(SurfaceAudioClips v)
    {
        throw new NotImplementedException();
    }
}
