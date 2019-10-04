using UnityEngine;

public class AudioObject : ScriptableObject
{
    public string soundName = "New Audio";

    public virtual void PlayAudio(AudioSource source)
    {

    }

    public virtual void StopAudio(AudioSource source)
    {
        source.Stop();
    }
}
