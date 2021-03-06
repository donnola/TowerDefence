using System;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class FlowFieldPathfinding
    {
        private Grid m_Grid;
        private Vector2Int m_Target;
        private Vector2Int m_Start;
        

        public FlowFieldPathfinding(Grid mGrid, Vector2Int mTarget, Vector2Int mStart)
        {
            m_Grid = mGrid;
            m_Target = mTarget;
            m_Start = mStart;
        }

        public bool CanOccupy(Vector2Int coordinate)
        {
            if (m_Grid.GetNode(coordinate).m_OccupationAvailability == OccupationAvailability.CanNotOccupy)
            {
                return false;
            }
            if (m_Grid.GetNode(coordinate).m_OccupationAvailability == OccupationAvailability.CanOccupy)
            {
                return true;
            }
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            queue.Enqueue(m_Start);
            
            bool[,] nodes = new bool[m_Grid.Width, m_Grid.Height];
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    nodes[i, j] = false;
                }
            }

            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                nodes[current.x, current.y] = true;
                foreach (Connection neighbour in GetNeighbours(current))
                {
                    if (!nodes[neighbour.coordinate.x, neighbour.coordinate.y] && neighbour.coordinate != coordinate)
                    {
                        if (neighbour.coordinate == m_Target)
                        {
                            m_Grid.GetNode(coordinate).m_OccupationAvailability = OccupationAvailability.CanOccupy;
                            return true;
                        }
                        nodes[neighbour.coordinate.x, neighbour.coordinate.y] = true;
                        queue.Enqueue(neighbour.coordinate);
                    }
                }
            }
            m_Grid.GetNode(coordinate).m_OccupationAvailability = OccupationAvailability.CanNotOccupy;
            return false;
        }

        public void UpdateField()
        {
            foreach (Node node in m_Grid.EnumerateAllNodes())
            {
                node.ResetWeight();
                node.m_OccupationAvailability = OccupationAvailability.CanOccupy;
            }

            m_Grid.GetNode(m_Start).m_OccupationAvailability = OccupationAvailability.CanNotOccupy;
            m_Grid.GetNode(m_Target).m_OccupationAvailability = OccupationAvailability.CanNotOccupy;
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            
            queue.Enqueue(m_Target);
            m_Grid.GetNode(m_Target).PathWeight = 0;

            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                Node currentNode = m_Grid.GetNode(current);

                foreach (Connection neighbour in GetNeighbours(current))
                {
                    float weightToTarget = m_Grid.GetNode(current).PathWeight + neighbour.weight;
                    Node neighbourNode = m_Grid.GetNode(neighbour.coordinate);
                    if (weightToTarget < neighbourNode.PathWeight)
                    {
                        neighbourNode.NextNode = currentNode;
                        neighbourNode.PathWeight = weightToTarget;
                        queue.Enqueue(neighbour.coordinate);
                    }
                }
            }

            Node next = m_Grid.GetNode(m_Start).NextNode;
            while (next != m_Grid.GetNode(m_Target))
            {
                next.m_OccupationAvailability = OccupationAvailability.Undefined;
                next = next.NextNode;
            }
        }

        private IEnumerable<Connection> GetNeighbours(Vector2Int coordinate)
        {
            Vector2Int rightCoordinate = coordinate + Vector2Int.right;
            Vector2Int leftCoordinate = coordinate + Vector2Int.left;
            Vector2Int upCoordinate = coordinate + Vector2Int.up;
            Vector2Int downCoordinate = coordinate + Vector2Int.down;
            
            Vector2Int rightUpCoordinate = coordinate + Vector2Int.right + Vector2Int.up;
            Vector2Int rightDownCoordinate = coordinate + Vector2Int.right + Vector2Int.down;
            Vector2Int leftUpCoordinate = coordinate + Vector2Int.left + Vector2Int.up;
            Vector2Int leftDownCoordinate = coordinate + Vector2Int.left + Vector2Int.down;
            
            bool hasRightNode = rightCoordinate.x < m_Grid.Width && !m_Grid.GetNode(rightCoordinate).IsOccupied;
            bool hasLeftNode = leftCoordinate.x >= 0 && !m_Grid.GetNode(leftCoordinate).IsOccupied;
            bool hasUpNode = upCoordinate.y < m_Grid.Height && !m_Grid.GetNode(upCoordinate).IsOccupied;
            bool hasDownNode = downCoordinate.y >= 0 && !m_Grid.GetNode(downCoordinate).IsOccupied;
            
            bool hasRightUpNode = rightUpCoordinate.x < m_Grid.Width && 
                                  rightUpCoordinate.y < m_Grid.Height && 
                                  !m_Grid.GetNode(rightUpCoordinate).IsOccupied && 
                                  !m_Grid.GetNode(rightCoordinate).IsOccupied && 
                                  !m_Grid.GetNode(upCoordinate).IsOccupied;
            bool hasRightDownNode = rightDownCoordinate.x < m_Grid.Width && 
                                    rightDownCoordinate.y >= 0 && 
                                    !m_Grid.GetNode(rightDownCoordinate).IsOccupied && 
                                    !m_Grid.GetNode(rightCoordinate).IsOccupied && 
                                    !m_Grid.GetNode(downCoordinate).IsOccupied;
            bool hasLeftUpNode = leftUpCoordinate.x >= 0 && 
                                 leftUpCoordinate.y < m_Grid.Width && 
                                 !m_Grid.GetNode(leftUpCoordinate).IsOccupied && 
                                 !m_Grid.GetNode(leftCoordinate).IsOccupied && 
                                 !m_Grid.GetNode(upCoordinate).IsOccupied;
            bool hasLeftDownNode = leftDownCoordinate.x >= 0 && 
                                   leftDownCoordinate.y >= 0 && 
                                   !m_Grid.GetNode(leftDownCoordinate).IsOccupied && 
                                   !m_Grid.GetNode(leftCoordinate).IsOccupied && 
                                   !m_Grid.GetNode(downCoordinate).IsOccupied;

            float diagWay = (float) Math.Pow(2, 0.5);
            if (hasRightUpNode)
            {
                yield return new Connection(rightUpCoordinate, diagWay);
            }
            if (hasRightDownNode)
            {
                yield return new Connection(rightDownCoordinate, diagWay);
            }
            if (hasLeftUpNode)
            {
                yield return new Connection(leftUpCoordinate, diagWay);
            }
            if (hasLeftDownNode)
            {
                yield return new Connection(leftDownCoordinate, diagWay);
            }
            
            if (hasRightNode)
            {
                yield return new Connection(rightCoordinate, 1f);
            }
            if (hasLeftNode)
            {
                yield return new Connection(leftCoordinate, 1f);
            }
            if (hasUpNode)
            {
                yield return new Connection(upCoordinate, 1f);
            }
            if (hasDownNode)
            {
                yield return new Connection(downCoordinate, 1f);
            }

        }
    }
}