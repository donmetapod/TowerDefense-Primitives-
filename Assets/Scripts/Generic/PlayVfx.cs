using UnityEngine;

// Starts playing a particle system
public class PlayVfx : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void PlayParticles()
    {
        _particleSystem.Play();
    }
}
