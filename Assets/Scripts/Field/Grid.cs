using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Field
{
    public class Grid
    {
        private Node[,] m_Nodes;

        private int m_Width;
        private int m_Height;
        private Vector3 m_Offset;
        private float m_NodeSize;

        private Vector2Int m_StartCoordinate;
        private Vector2Int m_TargetCoordinate;

        private Node m_SelectedNode = null;
        
        private FlowFieldPathfinding m_Pathfinding;

        public int Width => m_Width;

        public int Height => m_Height;

        public Grid(int width, int height, Vector3 offset, float nodeSize, Vector2Int target, Vector2Int start)
        {
            m_Width = width;
            m_Height = height;
            m_NodeSize = nodeSize;
            m_Offset = offset;

            m_StartCoordinate = start;
            m_TargetCoordinate = target;

            m_Nodes = new Node[m_Width, m_Height];

            for (int i = 0; i < m_Nodes.GetLength(0); i++)
            {
                for (int j = 0; j < m_Nodes.GetLength(1); j++)
                {
                    m_Nodes[i, j] = new Node(offset + new Vector3(i + .5f, 0, j + .5f) * nodeSize, 
                        new Vector2Int(i, j), nodeSize);
                }
            }
            
            m_Pathfinding = new FlowFieldPathfinding(this, target, start);
            
            m_Pathfinding.UpdateField();
        }

        public Node GetStartNode()
        {
            return GetNode(m_StartCoordinate);
        }

        public Node GetTargetNode()
        {
            return GetNode(m_TargetCoordinate);
        }

        public void SelectCoordinate(Vector2Int coordinate)
        {
            m_SelectedNode = GetNode(coordinate);
        }

        public void UnselectNode()
        {
            m_SelectedNode = null;
        }

        public bool HasSelectedNode()
        {
            return m_SelectedNode != null;
        }

        public Node GetSelectedNode()
        {
            return m_SelectedNode;
        }

        public bool CanOccupy(Node node)
        {
            Vector2Int coordinate = node.Coordinate;
            return m_Pathfinding.CanOccupy(coordinate);
        }
        
        public void TryOccupyNode(Vector2Int coordinate, bool occupy)
        {
            Node node = GetNode(coordinate);
            if (!occupy)
            {
                node.IsOccupied = false;
                UpdatePathfinding();
            }
            else if (m_Pathfinding.CanOccupy(coordinate))
            {
                node.IsOccupied = true;
                UpdatePathfinding();
            }
        }

        public Node GetNode(Vector2Int coordinate)
        {
            return GetNode(coordinate.x, coordinate.y);
        }
        
        public Node GetNode(int i, int j)
        {
            if (i < 0 || i >= m_Width)
            {
                return null;
            }

            if (j < 0 || j >= m_Height)
            {
                return null;
            }
            return m_Nodes[i, j];
        }

        public IEnumerable<Node> EnumerateAllNodes()
        {
            for (int i = 0; i < m_Width; i++)
            {
                for (int j = 0; j < m_Height; j++)
                {
                    yield return GetNode(i, j);
                }
            }
        }

        public void UpdatePathfinding()
        {
            m_Pathfinding.UpdateField();
        }

        [CanBeNull]
        public Node GetNodeAtPoint(Vector3 point)
        {
            Vector3 difference = point - m_Offset;

            int x = (int)(difference.x / m_NodeSize);
            int y = (int) (difference.z / m_NodeSize);
            if (x >= m_Nodes.GetLength(0) || y >= m_Nodes.GetLength(1) || x < 0 || y < 0)
            {
                return null;
            }
            return GetNode(x, y);
        }

        public List<Node> GetNodesInCircle(Vector3 point, float radius)
        {
            List<Node> nodesInCircle = new List<Node>();
            float sqrRadius = radius * radius;
            foreach (Node node in m_Nodes)
            {
                float sqrDistance = (node.Position - point).sqrMagnitude;
                if (sqrDistance < sqrRadius)
                {
                    nodesInCircle.Add(node);
                }
            }
            return nodesInCircle;
        }
    }
}