using System;
using System.Collections.Generic;
using Data.Combat;
using Entities.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AudioSystem
{
    public class AliveEntityAudioManager : MonoBehaviour
    {
        [field: SerializeField] public List<AudioClip> GetHitAudios { get; private set; }
        [field: SerializeField] public List<AudioClip> MakeAttackAudios { get; private set; }
        [field: SerializeField] public AudioClip IdleAudio { get; private set; }
        [field: SerializeField] public AudioClip WalkAudio { get; private set; }
        [field: SerializeField] public AudioClip RunAudio { get; private set; }

        private AliveEntity _aliveEntity;

        private void Awake()
        {
            _aliveEntity = GetComponent<AliveEntity>();
            _aliveEntity.AttackCalculator.OnDamageTaken += MakeDamageSound;
        }

        private void MakeDamageSound(AttackData obj)
        {
            int randomSound = Random.Range(0, GetHitAudios.Count);
            AudioManager.Instance.PlayEffectSound(GetHitAudios[randomSound]);
        }

        public void PlayAttackSound(int index)
        {
            AudioManager.Instance.PlayEffectSound(MakeAttackAudios[index]);
        }
    }
}