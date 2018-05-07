using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsControl : MonoBehaviour
{
    public enum SoundsRef
    {
        FISHY_CREATE = 0,
        FISHY_DESTROY = 1,
        SHARKY_CREATE = 2,
        SHARKY_DESTROY = 3,
        CRIKY_CREATE = 4,
        CRIKY_DESTROY = 5,
    };

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
