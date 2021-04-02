﻿using System.Collections.Generic;
using Enemy;
using Field;
using JetBrains.Annotations;
using UnityEngine;

namespace Turret.Weapon
{
    public static class EnemySearch
    {

        [CanBeNull]
        public static EnemyData GetClosestEnemy(Vector3 center, List<Node> nodes)
        {
            float minSqrDistance = float.MaxValue;
            EnemyData closestEnemy = null;
            foreach (Node node in nodes)
            {
                foreach (EnemyData enemyData in node.EnemyDatas)
                {
                    float sqrDistance = (enemyData.View.transform.position - center).sqrMagnitude;

                    if (sqrDistance < minSqrDistance)
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