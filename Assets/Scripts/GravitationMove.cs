using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

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
    private Vector3 _lastPos;
    private bool _goToCenter = false;
    private const float TOLERANCE = 0.5f;


    private void Start()
    {
        transform.position = m_rStart;
        _lastPos = m_rStart;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Mathf.Pow((m_R - m_rStart).magnitude, 2);
        if (distance < TOLERANCE)
        {
            return;
        }
        _a = m_G * m_M * (m_R - m_rStart).normalized / distance;
        Vector3 newPos = m_rStart + m_VStart + _a * Time.deltaTime / 2;
        if ((m_R - _lastPos).magnitude < (m_R - newPos).magnitude && _goToCenter)
        {
            m_VStart = m_VStart.normalized;
        }
        else
        {
            m_rStart = newPos;
        }

        if ((m_R - _lastPos).magnitude > (m_R - m_rStart).magnitude)
        {
            _goToCenter = true;
        }
        _lastPos = m_rStart;
        m_VStart += _a;
        transform.position = m_rStart;
    }
}
