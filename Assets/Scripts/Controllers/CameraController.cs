using Cinemachine;
using UnityEngine;

// This controller is only used to create camera shakes
public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineNoise;
    private float _noiseAmplitude;
    private float _noiseFrequency;

    private void Awake()
    {
        _cinemachineNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cinemachineNoise.m_AmplitudeGain = 0;
        _cinemachineNoise.m_FrequencyGain = 0;
    }

    // Sets values to create shake, cameraShakeValues receives an object of type CameraShakeValues
    // passed as parameter to an event on the Inspector
    public void CreateShake(CameraShakeValues cameraShakeValues)
    {
        _noiseAmplitude = cameraShakeValues.NoiseAmplitude;
        _noiseFrequency = cameraShakeValues.NoiseFrequency;
    }

    private void Update()
    {
        if (_noiseAmplitude > 0)
        {
            _cinemachineNoise.m_AmplitudeGain = _noiseAmplitude;
            _noiseAmplitude -= Time.deltaTime;
        }
        else
        {
            _cinemachineNoise.m_AmplitudeGain = 0;
        }

        if (_noiseFrequency > 0)
        {
            _cinemachineNoise.m_FrequencyGain = _noiseFrequency;
            _noiseFrequency -= Time.deltaTime;
        }else
        {
            _cinemachineNoise.m_FrequencyGain = 0;
        }
    }
}
