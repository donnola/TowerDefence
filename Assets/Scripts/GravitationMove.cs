using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GravitationMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_rStart;
    [SerializeField]
    private Vector3 m_VStart;
    [SerializeField]
    private float m_M;
    [SerializeField]
    private float m_G;
    [SerializeField]
    private Vector3 m_R;

    private float _m = 1.0f;
    private Vector3 _a;
    private Vector3 _initPos;
    private bool _goToCenter = false;

    private void Start()
    {
        transform.position = m_rStart;
        _initPos = m_rStart;
    }
    
    void FixedUpdate()
    {
        if ((m_R - _initPos).magnitude < (m_R - m_rStart).magnitude & _goToCenter)
        {
            return;
        }
        float square_distance = Mathf.Pow((m_R - m_rStart).magnitude, 2);
        _a = m_G * m_M * (m_R - m_rStart).normalized / square_distance;
        m_rStart += m_VStart * Time.fixedDeltaTime + _a * (float)Math.Pow(Time.fixedDeltaTime, 2) / 2;
        m_VStart += _a * Time.fixedDeltaTime;
        transform.position = m_rStart;
        if ((m_R - _initPos).magnitude > (m_R - m_rStart).magnitude)
        {
            _goToCenter = true;
        }
    }
}
