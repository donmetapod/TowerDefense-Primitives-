using System;
using UnityEngine;

[CreateAssetMenu (fileName = "AudioBox", 
    menuName = "ScriptableObjects/CreateAudioBox")]
public class AudioBox : ScriptableObject
{
    [Serializable]
    public struct AudioParameters
    {
        public string AudioName;
        public AudioClip[] AudioClips;
        public float Volume;
        public float Pitch;
        public bool Loop;
        public float StartDelay;
    }

    public AudioParameters[] Audios;
}
