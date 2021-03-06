using UnityEngine;

namespace Field
{
    public class Node
    {
        public OccupationAvailability m_OccupationAvailability;
        public Vector3 Position;

        public Node(Vector3 position)
        {
            Position = position;
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