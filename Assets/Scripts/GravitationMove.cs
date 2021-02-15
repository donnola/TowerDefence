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
    private Vector3 _a;
    private bool _goToCenter = false;

    private void Awake()
    {
        transform.position = m_rStart;
    }

    void FixedUpdate()
    {
        float distance = (m_R - m_rStart).magnitude;
        _a = m_G * m_M * (m_R - m_rStart).normalized / (distance * distance);
        if (_a.magnitude > 5)
        {
            _a = _a.normalized * 5;
        }
        float time = Time.fixedDeltaTime;
        m_rStart += m_VStart * time + _a * time * time / 2;
        m_VStart += _a * time;
        transform.position = m_rStart;
    }
}
