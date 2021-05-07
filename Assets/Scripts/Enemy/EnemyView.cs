using UnityEngine;
using Grid = Field.Grid;
using UI.InGame.Overtips;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField]
        private EnemyOvertip m_Overtip;
        private EnemyData m_Data;
        private IMovementAgent m_MovementAgent;
        [SerializeField] 
        private Animator m_Animator;

        [SerializeField] 
        private GameObject m_Body;

        private static readonly int Die1 = Animator.StringToHash("Die");

        public GameObject Body => m_Body;

        public EnemyData Data => m_Data;

        public IMovementAgent MovementAgent => m_MovementAgent;

        public void AttachData(EnemyData data)
        {
            m_Data = data;
            m_Overtip.SetData(m_Data);
        }

        public void Die()
        {
            m_Animator.SetTrigger(Die1);
            Destroy(gameObject, 2f);
        }
        
        public void ReachedTarget()
        {
            Destroy(gameObject);
        }

        public void CreateMovementAgent(Grid grid)
        {
            if (m_Data.Asset.IsFlyingEnemy)
            {
                m_MovementAgent = new FlyingMovementAgent(m_Data.Asset.Speed, transform, grid, m_Data);
            }
            else
            {
                m_MovementAgent = new GridMovementAgent(m_Data.Asset.Speed, transform, grid, m_Data);
            }
        }
    }
}