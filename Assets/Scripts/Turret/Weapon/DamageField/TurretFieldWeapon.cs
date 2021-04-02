using System.Collections.Generic;
using Enemy;
using Field;
using JetBrains.Annotations;
using RunTime;

namespace Turret.Weapon.DamageField
{
    public class TurretFieldWeapon : ITurretWeapon
    {
        private TurretFieldWeaponAsset m_Asset;
        private TurretView m_View;
        [CanBeNull] 
        private EnemyData m_ClosestEnemyData;
        private float m_MaxDistance;
        private float m_Damage;
        private List<Node> m_AvailableNodes;
        public TurretFieldWeapon(TurretFieldWeaponAsset asset, TurretView view)
        {
            m_View = view;
            m_Asset = asset;
            m_MaxDistance = m_Asset.MaxDistance;
            m_Damage = m_Asset.Damage;
            m_AvailableNodes = Game.Player.Grid.GetNodesInCircle(m_View.transform.position, m_MaxDistance);
        }
        public void TickShoot()
        {
            foreach (Node node in m_AvailableNodes)
            {
                foreach (EnemyData enemy in node.EnemyDatas)
                {
                    enemy.GetDamage(m_Damage);
                }
            }
        }
    }
}