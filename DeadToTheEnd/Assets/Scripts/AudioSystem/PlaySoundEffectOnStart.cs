using System;
using UnityEngine;

namespace AudioSystem
{
    public class PlaySoundEffectOnStart : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        private void Start()
        {
            AudioManager.Instance.PlayEffectSound(_clip);
        }
    }
}