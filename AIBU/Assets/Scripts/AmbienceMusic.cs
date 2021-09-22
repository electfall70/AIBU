using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbienceMusic : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource[] sources; //AudioSources to play audioclips from 
    public AudioClip music; 
    public AudioClip water; 
    public AudioClip wind; 

    void Start()
    {
        sources = GetComponents<AudioSource>();
        sources[0].clip = music;
        sources[0].loop = true;
        sources[0].Play();


        
        sources[1].clip = water;
        sources[1].loop = true;
        sources[1].Play();

        sources[2].clip = wind;
        sources[2].loop = true;
        sources[2].Play();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
