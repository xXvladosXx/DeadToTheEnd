using UnityEngine;

namespace AudioSystem
{
    public class PlaySoundMusicOnStart : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        private void Start()
        {
            AudioManager.Instance.PlayMusicSound(_clip);
        }
    }
}