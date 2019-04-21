using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Load audio files from the resource folder to a variable
/// </summary>
public static class AudioManager {
	
	static bool initialized = false;
    static AudioSource _audioSource;
	static Dictionary<AudioName,AudioClip> _audioClips = new Dictionary<AudioName, AudioClip> ();

	public static bool Initialized
	{
		get { return initialized; }
	}

	public static void Initialize(AudioSource ASource )
	{
        _audioSource = ASource;
        initialized = true;
		_audioClips.Add(AudioName.BackgroundMusic , Resources.Load<AudioClip>(@"Audio\Music"));  
        _audioClips.Add(AudioName.EnemyDeath, Resources.Load<AudioClip>(@"Audio\EnemyDeath"));
        _audioClips.Add(AudioName.PlayerDeath, Resources.Load<AudioClip>(@"Audio\PlayerDeath"));
        _audioClips.Add(AudioName.Attack, Resources.Load<AudioClip>(@"Audio\Attack"));
        _audioClips.Add(AudioName.Knockdown, Resources.Load<AudioClip>(@"Audio\PlayerKnockDown"));
        _audioClips.Add(AudioName.HitGround, Resources.Load<AudioClip>(@"Audio\GroundPound"));
        _audioClips.Add(AudioName.Hover, Resources.Load<AudioClip>(@"Audio\Hover"));
        _audioClips.Add(AudioName.Hover2, Resources.Load<AudioClip>(@"Audio\Hover2"));
        _audioClips.Add(AudioName.MenuSelect, Resources.Load<AudioClip>(@"Audio\Select"));
        _audioClips.Add(AudioName.Back, Resources.Load<AudioClip>(@"Audio\Back")); 
        _audioClips.Add(AudioName.Powerup, Resources.Load<AudioClip>(@"Audio\Powerup"));
        _audioClips.Add(AudioName.EnemyFall, Resources.Load<AudioClip>(@"Audio\EnemyFall"));



    }

    public static void Play(AudioName name)
	{
		_audioSource.PlayOneShot(_audioClips[name]);
	}

}
