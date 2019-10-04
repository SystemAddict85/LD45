using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New SoundFXObject", menuName = "ScriptableObjects/SoundFX", order = 3)]
public class SoundFXObject : AudioObject
{
    public AudioClip[] soundEffects;   // sound made when used; may use multiple for variance

    [MinMaxRange(0f,2f)]
    public RangedFloat pitch;

    [MinMaxRange(0f, 2f)]
    public RangedFloat volume;

    public override void PlayAudio(AudioSource source)
    {        
        var randVol = Random.Range(volume.minValue, volume.maxValue);
        var randPitch = Random.Range(pitch.minValue, pitch.maxValue);
        var randClip = Random.Range(0, soundEffects.Length);
        source.loop = false;

        source.volume = randVol;
        source.pitch = randPitch;
        source.clip = soundEffects[randClip];
        source.Play();
        
    }

}
