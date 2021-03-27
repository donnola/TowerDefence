using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Field
{
    public class Node
    {
        public OccupationAvailability m_OccupationAvailability;
        private Vector3 m_Position;
        private Vector2Int m_Coordinate;
        private float m_NodeSize;
        public List<EnemyData> EnemyDatas;

        public Vector3 Position => m_Position;

        public Vector2Int Coordinate => m_Coordinate;
        
        public float NodeSize => m_NodeSize;

        public Node(Vector3 position, Vector2Int coordinate, float nodeSize)
        {
            m_Position = position;
            m_Coordinate = coordinate;
            m_NodeSize = nodeSize;
            EnemyDatas = new List<EnemyData>();
        }

        public Node NextNode;
        public bool IsOccupied;

        public float PathWeight;

        public void ResetWeight()
        {
            PathWeight = float.MaxValue;
        }
        
    }
}