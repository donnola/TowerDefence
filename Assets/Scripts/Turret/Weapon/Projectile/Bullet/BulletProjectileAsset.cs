using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile.Bullet
{
    [CreateAssetMenu(menuName = "Assets/Bullet Projectile Asset", fileName = "Bullet Projectile Asset")]
    public class BulletProjectileAsset : ProjectileAssetBase
    {
        [SerializeField] 
        private BulletProjectile m_BulletPrefab;
        public override IProjectile CreateProjectile(Vector3 origin, Vector3 originForward, EnemyData enemyData)
        {
            BulletProjectile bullet = Instantiate(m_BulletPrefab, origin, Quaternion.LookRotation(originForward, Vector3.up));
            bullet.transform.LookAt(enemyData.View.Body.transform.position);
            return bullet;
        }
    }
}