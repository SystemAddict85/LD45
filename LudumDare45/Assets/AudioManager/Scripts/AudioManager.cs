using System.Linq;
using UnityEngine;

public class AudioManager : SimpleSingleton<AudioManager>
{
    private AudioSource musicSource;
    private AudioSource[] sfxSources;

    [SerializeField]
    private SoundDictionary globalSounds;

    private void Awake()
    {
        var sources = GetComponentsInChildren<AudioSource>();

        musicSource = sources[0];
        sfxSources = sources.Skip(1).ToArray();

        if(globalSounds)
            globalSounds.InitializeDictionary();
    }

    private void Start()
    {
        ChangeMusic((MusicObject)globalSounds.GetSoundObject("mainTheme"));
    }

    public static void ChangeMusic(MusicObject music, bool isLooping = true)
    {
        music.PlayAudio(Instance.musicSource);
        Instance.musicSource.loop = isLooping;
    }
    
    public static void PlaySound(string soundName)
    {
        Instance.globalSounds.PlaySound(soundName);
    }

    public static void PlaySound(SoundFXObject soundEffect)
    {
        var source = Instance.GetAvailableAudioSource();
        soundEffect.PlayAudio(source);
    }

    public AudioSource GetAvailableAudioSource()
    {
        foreach (var s in sfxSources)
        {
            if (!s.isPlaying)
            {
                return s;
            }
        }
        sfxSources[0].Stop();
        return sfxSources[0];
    }


}