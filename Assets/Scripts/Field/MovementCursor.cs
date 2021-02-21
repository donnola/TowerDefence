using System;
using UnityEngine;

namespace Field
{
    public class MovementCursor : MonoBehaviour
    {
        [SerializeField]
        private int m_GridWidth;
        [SerializeField]
        private int m_GridHeight;

        [SerializeField]
        private float m_NodeSize;

        private Camera m_Camera;

        private Vector3 m_Offset;
        
        [SerializeField]
        private MovementAgent m_MovementAgent;
        [SerializeField]
        private GameObject m_Cursor;


        private void OnValidate()
        {
            m_Camera = Camera.main;

            float width = m_GridWidth * m_NodeSize;
            float height = m_GridHeight * m_NodeSize;

            // Default plane size is 10 by 10
            transform.localScale = new Vector3(
                width * 0.1f, 
                1f,
                height * 0.1f);

            m_Offset = transform.position -
                       (new Vector3(width, 0f, height) * 0.5f);
        }
        

        private void Update()
        {
            if (m_Camera == null)
            {
                return;
            }
            
            Vector3 mousePosition = Input.mousePosition;

            Ray ray = m_Camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform != transform)
                {
                    m_Cursor.SetActive(false);
                    return;
                }

                m_Cursor.SetActive(true);
                Vector3 hitPosition = hit.point;
                Vector3 difference = hitPosition - m_Offset;

                int x = (int)(difference.x / m_NodeSize);
                int y = (int) (difference.z / m_NodeSize);

                Vector3 newPos = m_Offset + new Vector3(x + 0.5f, 0, y + 0.5f) * m_NodeSize;
                m_Cursor.transform.position = newPos;
                if (Input.GetMouseButtonDown(0))
                {
                    m_MovementAgent.SetTarget(newPos);
                }
            }
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            for (int i = 1; i < m_GridWidth; ++i)
            {
                Gizmos.DrawLine(
                    new Vector3(m_Offset.x + m_NodeSize * i, 0, m_Offset.z), 
                    new Vector3(m_Offset.x + m_NodeSize * i, 0, -m_Offset.z));
            }
            for (int i = 1; i < m_GridHeight; ++i)
            {
                Gizmos.DrawLine(
                    new Vector3(m_Offset.x, 0, m_Offset.z + m_NodeSize * i), 
                    new Vector3(-m_Offset.x, 0, m_Offset.z + m_NodeSize * i));
            }
        }
    }
}