using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Cinemachine;
using UnityEngine;

public class FollowingTargetSelector : MonoBehaviour
{
    CinemachineVirtualCamera vCamera;
    Transform follow;

    private void Awake()
    {
        vCamera = GetComponent<CinemachineVirtualCamera>();
        follow = new GameObject("FollowingTarget").transform;
        vCamera.Follow = follow;
    }

    public void Update()
    {
        var wonjuns = GameObject.FindGameObjectsWithTag("wonjun")
                        .Where((wonjun) => wonjun.GetComponent<RollAndFall>().isFallTime)
                        .Where((wonjun) => wonjun.GetComponent<Rigidbody2D>().velocityY >= -0.1)
                        .ToArray();
        var y = -10f;
        if (wonjuns.Length > 0)
        {
            y = wonjuns.Max((wonjun) => wonjun.transform.position.y);
        }
        follow.position = new Vector3(0, y + 5f);
    }
}
