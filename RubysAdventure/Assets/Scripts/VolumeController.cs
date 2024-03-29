using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    Slider slider;
    public AudioMixer mixer;
    public void SetLevel(float sliderValue) {
        mixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    }
}
