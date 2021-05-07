using System.Collections.Generic;
using Enemy;
using Field;
using JetBrains.Annotations;
using UnityEngine;

namespace Turret.Weapon
{
    public class EnemySearch
    {
        private IReadOnlyList<EnemyData> m_EnemyDatas;

        public EnemySearch(IReadOnlyList<EnemyData> enemyDatas)
        {
            m_EnemyDatas = enemyDatas;
        }

        [CanBeNull]
        public EnemyData GetClosestEnemy(Vector3 center, float maxDistance, List<Node> nodes)
        {
            float minSqrDistance = float.MaxValue;
            EnemyData closestEnemy = null;
            foreach (Node node in nodes)
            {
                foreach (EnemyData enemyData in node.EnemyDatas)
                {
                    float sqrDistance = (enemyData.View.transform.position - center).sqrMagnitude;
                    
                    if (sqrDistance <= maxDistance && sqrDistance < minSqrDistance)
                    {
                        minSqrDistance = sqrDistance;
                        closestEnemy = enemyData;
                    }
                }
            }
            return closestEnemy;
        }
    }
}