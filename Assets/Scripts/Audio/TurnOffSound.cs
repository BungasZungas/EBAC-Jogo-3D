using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TurnOffSound : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string exposedParameter = "MasterVolume";

    [SerializeField] private bool isMuted = false;
    private float normalVolume = 0f;

    private void Start()
    {
        audioMixer.GetFloat(exposedParameter, out normalVolume);
    }

    public void ChangeMute()
    {
        if (isMuted == false)
        {
            audioMixer.SetFloat(exposedParameter, -80f);
            isMuted = true;
        }
        else if (isMuted == true)
        {
            audioMixer.SetFloat(exposedParameter, normalVolume);
            isMuted = false;
        }
    }


}