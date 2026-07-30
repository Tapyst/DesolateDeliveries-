using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPointer : MonoBehaviour
{   
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0f, 0f, Camera.main.transform.position.z) - transform.position;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
}
