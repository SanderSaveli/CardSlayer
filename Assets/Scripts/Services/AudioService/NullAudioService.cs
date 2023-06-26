using UnityEngine;
namespace Services
{
    public class NullAudioService : IAudioService
    {
        public void ChangeSoundtrack()
        { }

        public void ChangeVolume(float volume)
        { }

        public void Initialize()
        { }

        public void PlaySound(AudioClip sound)
        { }
    }
}
