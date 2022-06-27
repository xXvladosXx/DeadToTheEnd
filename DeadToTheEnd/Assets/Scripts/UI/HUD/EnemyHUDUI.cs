using System.Collections;
using Data.Stats;
using Entities.Core;
using Entities.Enemies;
using GameCore;
using TMPro;
using UI.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class EnemyHUDUI : HUDUI
    {
        public void InitEnemyData(Enemy enemy)
        {
            foreach (var level in Levels)
            {
                level.text = enemy.LevelCalculator.Level.ToString();
            }
            
            Name.text = enemy.name;
            HealthBarUI.InitBarData(enemy);
            
            gameObject.SetActive(true);
        }

       
       
    }
}