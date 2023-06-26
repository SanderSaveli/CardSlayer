using UnityEngine;

namespace Services
{
    public interface IAudioService : IService
    {
        public void ChangeVolume(float volume);
        public void PlaySound(AudioClip sound);
        public void ChangeSoundtrack();
    }
}
