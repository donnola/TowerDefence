using Field;

namespace Enemy
{
    public class EnemyView : MovementAgent
    {
        private EnemyData m_Data;
        private IMovementAgent m_MovementAgent;

        public EnemyData Data => m_Data;

        public IMovementAgent MovementAgent => m_MovementAgent;

        public void AttachData(EnemyData data)
        {
            m_Data = data;
        }

        public void CreateMovementAgent(Grid grid)
        {
            if (m_Data.Asset.IsFlyingEnemy)
            {
                m_MovementAgent = new FlyingMovementAgent(5f, transform, grid);
            }
            else
            {
                m_MovementAgent = new GridMovementAgent(5f, transform, grid);
            }
        }
    }
}