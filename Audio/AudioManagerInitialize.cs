using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerInitialize : MonoBehaviour {


	void Awake()
	{
		// make sure we only have one of this game object
		// in the game
		if (!AudioManager.Initialized)
		{
            print("initilized");
            // initialize audio manager and persist audio source across scenes
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			
			AudioManager.Initialize(audioSource);
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			// duplicate game object, so destroy
			Destroy(gameObject);
		}
	}


}