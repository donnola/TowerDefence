using Field;
using RunTime;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class GridMovementAgent : IMovementAgent
    {
        private float m_Speed;
        private Transform m_Transform;
        private EnemyData m_Data;
        private Grid m_Grid;

        public GridMovementAgent(float speed, Transform transform, Grid grid, EnemyData data)
        {
            m_Speed = speed;
            m_Transform = transform;
            m_Data = data;
            m_Grid = grid;
            m_LastNode = Game.Player.Grid.GetNodeAtPoint(transform.position);
            if (m_LastNode != null)
            {
                m_LastNode.EnemyDatas.Add(m_Data);
            }


            SetTargetNode(grid.GetStartNode());
        }

        private const float TOLERANCE = 0.1f;

        private Node m_LastNode;
        private Node m_TargetNode;

        public void TickMovement()
        {
            if (m_TargetNode == null)
            {
                return;
            }

            Vector3 target = m_TargetNode.Position;
            target.y = m_Transform.position.y;
            Node lastNode = null;

            float distance = (target - m_Transform.position).magnitude;
            if (distance < TOLERANCE)
            {
                m_TargetNode = m_TargetNode.NextNode;
                return;
            }

            Vector3 dir = (target - m_Transform.position).normalized;
            Vector3 delta = dir * (m_Speed * Time.deltaTime);
            m_Transform.Translate(delta);
            Node curNode = m_Grid.GetNodeAtPoint(m_Transform.position);
            if (m_LastNode != curNode && curNode != null)
            {
                if (m_LastNode != null)
                {
                    m_LastNode.EnemyDatas.Remove(m_Data);
                }
                curNode.EnemyDatas.Add(m_Data);
                m_LastNode = curNode;
            }
        }

        public void Die()
        {
            Node curNode = m_Grid.GetNodeAtPoint(m_Transform.position);
            if (curNode != null)
            {
                curNode.EnemyDatas.Remove(m_Data);
            }
        }

        public Node GetCurrentNode()
        {
            return m_Grid.GetNodeAtPoint(m_Transform.position);
        }

        private void SetTargetNode(Node node)
        {
            m_TargetNode = node;
            m_TargetNode.EnemyDatas.Add(m_Data);
        }
    }
}