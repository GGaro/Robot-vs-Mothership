using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Attack : MonoBehaviour
{
    [SerializeField] GameObject bullet_Prefab;
    [SerializeField] Transform fire_Point;
    [SerializeField] float bullet_Power;
    [SerializeField] Transform lookAt;

    void FixedUpdate()
    {
        gameObject.transform.LookAt(lookAt, Vector3.up);
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.TransformDirection(Vector3.forward * 100));
        if (Input.GetMouseButtonDown(0))
        {
            Fire_Bullet();
        }
    }

    void Fire_Bullet()
    {
        GameObject bullet = Instantiate(bullet_Prefab, fire_Point.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = fire_Point.TransformDirection(Vector3.forward * bullet_Power);
    }
}
