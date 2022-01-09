using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMovementDriver : MonoBehaviour
{
    [SerializeField] private float bridgeSpeed;
    [SerializeField] private Transform targetTransform;

    void Update()
    {
        if (transform.position == targetTransform.position) return;

        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, bridgeSpeed * Time.deltaTime);
    }
}
