using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New SoundDictionary", menuName = "ScriptableObjects/Sound Dictionary", order = 7)]
public class SoundDictionary : ScriptableObject
{
    public List<AudioObject> audioObjects = new List<AudioObject>();

    private Dictionary<string, AudioObject> audio;

    public void InitializeDictionary()
    {
        audio = new Dictionary<string, AudioObject>();
        foreach (var s in audioObjects)
        {
            audio.Add(s.soundName, s);
        }
    }

    public void PlaySound(string soundName)
    {
        if (audio.ContainsKey(soundName))
        {
            audio[soundName].PlayAudio(AudioManager.Instance.GetAvailableAudioSource());
        }
        else
        {
            Debug.LogError("No sounds with name: " + soundName);
        }
    }

    public AudioObject GetSoundObject(string soundName)
    {
        if (audio.ContainsKey(soundName))
        {
            return audio[soundName];            
        }
        else
        {
            Debug.LogError("No sounds with name: " + soundName);
            return null;
        }
    }

}
