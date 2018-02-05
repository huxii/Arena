using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsControl : MonoBehaviour
{
    public AudioSource[] audios;

    // Use this for initialization
    void Start ()
    {
        audios = GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Play(int idx)
    {
        audios[idx].Play();
    }
}
