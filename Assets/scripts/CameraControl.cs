using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       Vector3 newPosistion = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z);
        transform.position = newPosistion;
    }
}
