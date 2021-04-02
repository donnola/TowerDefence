using Enemy;
using Field;
using RunTime;
using UnityEngine;

namespace Turret.Weapon.Projectile.Rocket
{
    public class RocketProjectile : MonoBehaviour, IProjectile
    {
        private float m_Speed = 10f;
        private float m_DamageRadius = 3f;
        private int m_Damage = 10;
        private bool m_DidHit = false;
        private EnemyData m_HitEnemy = null;
        private EnemyData m_TargetEnemy = null;
        
        public void SetEnemy(EnemyData enemyData)
        {
            m_TargetEnemy = enemyData;
        }
        
        public void TickApproaching()
        {
            transform.LookAt(m_TargetEnemy.View.transform.Find("Body").position);
            transform.Translate(transform.forward * (m_Speed * Time.deltaTime), Space.World);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            m_DidHit = true;
            if (other.CompareTag("Enemy"))
            {
                EnemyView enemyView = other.GetComponent<EnemyView>();
                if (enemyView != null)
                {
                    m_HitEnemy = enemyView.Data;
                }
            }
        }

        public bool DidHit()
        {
            return m_DidHit;
        }

        public void DestroyProjectile()
        {
            if (m_HitEnemy != null)
            {
                foreach (Node node in Game.Player.Grid.GetNodesInCircle(m_HitEnemy.View.transform.position, m_DamageRadius))
                {
                    foreach (EnemyData enemy in node.EnemyDatas)
                    {
                        enemy.GetDamage(m_Damage);
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}