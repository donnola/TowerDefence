using UnityEngine;

namespace Field
{
    public struct Connection
    {
        public Vector2Int coordinate;
        public float weight;

        public Connection(Vector2Int coordinate, float weight)
        {
            this.coordinate = coordinate;
            this.weight = weight;
        }
    }
}