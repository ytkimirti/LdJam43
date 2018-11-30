using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[System.Serializable]
public class Sound {
	public string name;

	[Space]

	public AudioClip[] clips = new AudioClip[1];

    [Space]

    public bool playIfItsNotPlaying = false;

    [Space]

    public AudioMixerGroup group;

	[Space]

	public bool randomPitch = false;
	public Vector2 pitchValues = new Vector2(0.3f,1.5f);

	[Space]

	[HideInInspector]
	public bool multipleClips = false;
	
	[Range(0,1)]
	public float volume = 1f;
	public float pitch = 1f;

	[HideInInspector]
	public AudioSource source;
}

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	//Çok basit bir singleton
	public static AudioManager main;

	public void Awake(){
		main = this;

		foreach (Sound s in sounds)//Bütün audio source'ları eklemesi
		{
			s.source = gameObject.AddComponent<AudioSource>();

            s.source.outputAudioMixerGroup = s.group;

			s.source.playOnAwake = false;

			if(s.clips.Length == 1)
				s.source.clip = s.clips[0];
			else if(s.clips.Length > 1){
				s.multipleClips = true;
			}else{
				Debug.LogError("There is no clip!!!");
			}

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
		}
	}

	//Çalması
	public void Play(string name){
		Sound s = Array.Find(sounds,sound => sound.name == name);

		if(s == null)
			return;

        if (s.playIfItsNotPlaying && s.source.isPlaying)
            return;

		if(s.multipleClips)
			s.source.clip = s.clips[Random.Range(0,s.clips.Length)];

		if(s.randomPitch)
			s.source.pitch = Random.Range(s.pitchValues.x,s.pitchValues.y);

		s.source.Play();
	}

	//Durdurması
	public void Stop(string name){
		Sound s = Array.Find(sounds,sound => sound.name == name);

		if(s == null)
			return;

		s.source.Stop();
	}
}
