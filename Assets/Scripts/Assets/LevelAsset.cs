using UnityEditor;
using UnityEngine;
using EnemySpawn;
using TurretSpawn;

namespace Assets
{
    [CreateAssetMenu(menuName = "Assets/Level Asset", fileName = "Level Asset")]
    public class LevelAsset : ScriptableObject
    {
        public SceneAsset SceneAsset;
        public SpawnWavesAsset SpawnWavesAsset;
        public TurretMarketAsset TurretMarketAsset;

        public int StartHealth;
        public int StartMoney;
    }
}