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

    private void Awake()
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
        float square_distance = (m_R - m_rStart).magnitude * (m_R - m_rStart).magnitude;
        _a = m_G * _m * m_M * (m_R - m_rStart).normalized / square_distance;
        float time = Time.fixedDeltaTime;
        m_rStart += m_VStart * time + _a * time * time / 2;
        m_VStart += _a * time;
        transform.position = m_rStart;
        if ((m_R - _initPos).magnitude > (m_R - m_rStart).magnitude)
        {
            _goToCenter = true;
        }
    }
}
