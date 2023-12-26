using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;

    private AudioSource musicSrc;
    private AudioSource soundSrc;
    // Start is called before the first frame update
    void Start()
    {
        
        musicSrc = transform.GetChild(0).GetComponent<AudioSource>();
        soundSrc = transform.GetChild(1).GetComponent<AudioSource>();
        musicSrc.volume = PlayerPrefs.GetFloat("music");
        musicSlider.value = PlayerPrefs.GetFloat("music");
        soundSrc.volume = PlayerPrefs.GetFloat("sound");
        soundSlider.value = PlayerPrefs.GetFloat("sound");
    }

    // Update is called once per frame


    public void MusicSlider()
    {
     
        var volume = musicSlider.value;
        musicSrc.volume = volume;
        PlayerPrefs.SetFloat("music", volume);   
    }
    
    public void SoundSlider()
    {
        var volume = soundSlider.value;
        soundSrc.volume = volume;
        PlayerPrefs.SetFloat("sound", volume);
    }
}
