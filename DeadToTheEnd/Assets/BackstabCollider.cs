using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Core;
using UnityEngine;

public class BackstabCollider : MonoBehaviour
{
    private AliveEntity _user;
    private void Awake()
    {
        _user = GetComponentInParent<AliveEntity>();
    }

    private void Update()
    {
        
    }

    protected float GetViewAngle(Transform user, Transform target)
    {
        Vector3 targetDirection = target.position - user.position;

        float viewableAngle = Vector3.SignedAngle(targetDirection, user.forward, Vector3.up);
        return viewableAngle;
    }
}
