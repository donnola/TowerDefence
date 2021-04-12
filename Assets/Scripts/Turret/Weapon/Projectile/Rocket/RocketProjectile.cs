using Enemy;
using Field;
using RunTime;
using UnityEngine;

namespace Turret.Weapon.Projectile.Rocket
{
    public class RocketProjectile : MonoBehaviour, IProjectile
    {
        private float m_Speed;
        private float m_DamageRadius;
        private float m_Damage;
        private bool m_DidHit = false;
        private EnemyData m_HitEnemy = null;
        private EnemyData m_TargetEnemy = null;

        public void SetAsset(RocketProjectileAsset rocketProjectileAsset)
        {
            m_Damage = rocketProjectileAsset.Damage;
            m_Speed = rocketProjectileAsset.Speed;
            m_DamageRadius = rocketProjectileAsset.DamageRadius;
        }
        
        public void SetEnemy(EnemyData enemyData)
        {
            m_TargetEnemy = enemyData;
        }
        
        public void TickApproaching()
        {
            transform.LookAt(m_TargetEnemy.View.Body.transform.position);
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
            Vector3 pointHit = m_HitEnemy.View.transform.position;
            float sqrDamageRadius = m_DamageRadius * m_DamageRadius;
            if (m_HitEnemy != null)
            {
                foreach (Node node in Game.Player.Grid.GetNodesInCircle(m_HitEnemy.View.transform.position, m_DamageRadius))
                {
                    foreach (EnemyData enemy in node.EnemyDatas)
                    {
                        float sqrDistance = (enemy.View.transform.position - pointHit).sqrMagnitude;
                        if (sqrDistance <= sqrDamageRadius)
                        {
                            enemy.GetDamage(m_Damage);
                        }
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}