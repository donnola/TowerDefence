using Assets;
using Enemy;

namespace EnemySpawn
{
    [System.Serializable]
    public class SpawnWave
    {
        public EnemyAsset EnemyAsset;
        public int Count;
        public float TimeBetweenSpawn;

        public float TimeBeforeStartWave;
    }
}