using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New MusicObject", menuName = "ScriptableObjects/Music", order = 3)]
public class MusicObject : AudioObject
{    
    public AudioClip musicClip;
    
    [SerializeField]
    [Range(0f,2f)]
    private float volume;

    [SerializeField]
    private bool loopMusic; 

    public override void PlayAudio(AudioSource source)
    {        
        source.loop = loopMusic;
        source.clip = musicClip;
        source.volume = volume;
        source.Play();        
    }
}
