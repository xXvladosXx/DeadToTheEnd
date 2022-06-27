using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Enemies;
using UnityEngine;

namespace GameCore.Player
{
    public class EntitySpawner : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MainPlayer mainPlayer))
            {
                Init(mainPlayer);
            }
        }

        public void Init(MainPlayer mainPlayer)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Target = mainPlayer;
                enemy.gameObject.SetActive(true);
            }
        }
    }
}