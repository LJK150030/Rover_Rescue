using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLiners : MonoBehaviour {

    public AudioClip[] clips;
    AudioSource source;

	// Use this for initialization
	void Start ()
    {
        
        int rand = Random.Range(0, clips.Length);

        source = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        source.clip = clips[rand] as AudioClip;
        source.Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Return))
        {
            int rand = Random.Range(0, clips.Length);

            source = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
            source.clip = clips[rand] as AudioClip;
            source.Play();
        }

    }
}
