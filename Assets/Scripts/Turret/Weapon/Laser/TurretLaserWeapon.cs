using System.Collections.Generic;
using Enemy;
using Field;
using JetBrains.Annotations;
using RunTime;
using UnityEngine;

namespace Turret.Weapon.Laser
{
    public class TurretLaserWeapon : ITurretWeapon
    {
        private TurretLaserWeaponAsset m_Asset;
        private LineRenderer m_LineRenderer;
        private TurretView m_View;
        [CanBeNull] 
        private EnemyData m_ClosestEnemyData;
        private float m_MaxDistance;
        private float m_Damage;
        private List<Node> m_AvailableNodes;

        public TurretLaserWeapon(TurretLaserWeaponAsset asset, TurretView view)
        {
            m_View = view;
            m_Asset = asset;
            m_MaxDistance = m_Asset.MaxDistance;
            m_Damage = m_Asset.Damage;
            m_AvailableNodes = Game.Player.Grid.GetNodesInCircle(m_View.transform.position, m_MaxDistance);
            m_LineRenderer = Object.Instantiate(asset.LineRendererPrefab);
            m_LineRenderer.startWidth = 0.05f;
            m_LineRenderer.endWidth = 0.05f;
        }

        public void TickShoot()
        {
            TickTower();
            m_ClosestEnemyData = Game.Player.EnemySearch.GetClosestEnemy(m_View.transform.position, 
                m_MaxDistance, m_AvailableNodes);
            if (m_ClosestEnemyData == null)
            {
                m_LineRenderer.gameObject.SetActive(false);
            }
            else
            {
                m_LineRenderer.SetPosition(0, m_View.ProjectileOrigin.position);
                m_LineRenderer.SetPosition(1, m_ClosestEnemyData.View.Body.transform.position);
                m_LineRenderer.gameObject.SetActive(true);
                m_ClosestEnemyData.GetDamage(m_Damage * Time.deltaTime);
            }
        }
        private void TickTower()
        {
            if (m_ClosestEnemyData != null && !m_ClosestEnemyData.IsDead)
            {
                m_View.TowerLookAt(m_ClosestEnemyData.View.transform.position);
            }
        }
    }
}