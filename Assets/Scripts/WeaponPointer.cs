using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WeaponPointer : MonoBehaviour
{   
    private GameObject player;
    // Update is called once per frame
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) {
            Debug.LogError("CameraFollow: No object with tag 'Player' found!");
            return;
        }
    }
    void Update()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0f, 0f, Camera.main.transform.position.z) - transform.position;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
}
