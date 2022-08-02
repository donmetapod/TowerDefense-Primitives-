using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu (fileName = "Sfx", 
    menuName = "ScriptableObjects/CreateNewSfxAsset")]
public class AudioSfx : ScriptableObject
{
    [Serializable]
    public struct AudioParametersStruct
    {
        public string AudioName;
        public AudioClip[] AudioClips;
        public float Volume;
        public float Pitch;
        public bool Loop;
        public float StartDelay;
    }

    public AudioParametersStruct AudioParameters;
    private AudioSource _audioSource;
    private GameObject _sourceGO;
    
    public void PlayAudio()
    {
        if (_sourceGO == null)
        {
            _sourceGO = new GameObject($"Audio {AudioParameters.AudioName}");
            _sourceGO.AddComponent<AudioSource>();
            _audioSource = _sourceGO.GetComponent<AudioSource>();
        }
        _audioSource.clip = AudioParameters.AudioClips[Random.Range(0, AudioParameters.AudioClips.Length)];
        _audioSource.volume = AudioParameters.Volume;
        _audioSource.pitch = AudioParameters.Pitch;
        _audioSource.loop = AudioParameters.Loop;
        _audioSource.PlayDelayed(AudioParameters.StartDelay);
    }

    public void StopAudio()
    {
        if(_audioSource.isPlaying)
            _audioSource.Stop();
    }
}
