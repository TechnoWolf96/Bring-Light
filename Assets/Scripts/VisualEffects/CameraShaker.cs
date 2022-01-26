using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShaker : MonoBehaviour
{
    public float gainShake;
    CinemachineBasicMultiChannelPerlin shake;

    void Start()
    {
        shake = GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>().
            GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public IEnumerator StartShake(float time)
    {
        shake.m_AmplitudeGain += gainShake;
        yield return new WaitForSeconds(time);
        shake.m_AmplitudeGain -= gainShake;
    }

}
