using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    private float range = 4f;
    private bool isAttacking = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AttackStart();
        }
        moveWeapons();
    }
    public void AttackStart()
    {
        // use mouse position to teleport weapons to mouse direction within range
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0f, 0f, Camera.main.transform.position.z);
        Vector3 direction = (mousePosition - transform.position).normalized;
        for (int i = 0; i < weaponManager.weaponPrefabs.Count; i++)
        {
            if (GameData.weapons[i].timeSinceAttack > GameData.weapons[i].attackCooldown)
            {
                GameData.weapons[i].timeSinceAttack = 0f;
                GameObject weaponObject = weaponManager.GetWeaponObjects()[i];
                GameData.weapons[i].targetPosition = transform.position + direction * range;
                weaponObject.transform.position = Vector3.SmoothDamp(
                        weaponManager.GetWeaponObjects()[i].transform.position, 
                        transform.position + direction * range, 
                        ref GameData.weapons[i].currentVelocity, 
                        GameData.weapons[i].attackCooldown / 4
                        );


                //get weaponpointer script from weaponObject and set isPointing to false
                WeaponPointer weaponPointer = weaponObject.GetComponent<WeaponPointer>();
                if (weaponPointer != null)
                {
                    weaponPointer.isPointing = false;
                }

            }
        }
    }
    private void moveWeapons()
    {
        for (int i = 0; i < weaponManager.weaponPrefabs.Count; i++)
        {
            GameObject weaponObject = weaponManager.GetWeaponObjects()[i];
            if (GameData.weapons[i].currentVelocity.x == null || GameData.weapons[i].currentVelocity.y == null)
            {
                GameData.weapons[i].currentVelocity = Vector3.zero;
            }
            if (weaponObject.transform.position != GameData.weapons[i].targetPosition && GameData.weapons[i].timeSinceAttack < GameData.weapons[i].attackCooldown / 4)
            {
                
                weaponObject.transform.position = Vector3.SmoothDamp(
                        weaponObject.transform.position, 
                        GameData.weapons[i].targetPosition, 
                        ref GameData.weapons[i].currentVelocity, 
                        GameData.weapons[i].attackCooldown / 10
                        );
            }
            if (GameData.weapons[i].timeSinceAttack >= GameData.weapons[i].attackCooldown / 4 && GameData.weapons[i].timeSinceAttack < GameData.weapons[i].attackCooldown / 2)
            {
                weaponObject.transform.position = GameData.weapons[i].targetPosition;
            }
            if (weaponObject.transform.position != new Vector3(Mathf.Cos(Mathf.Deg2Rad * 360/i) * 2, Mathf.Sin(Mathf.Deg2Rad * 360/i) * 2, 0) && GameData.weapons[i].timeSinceAttack >= GameData.weapons[i].attackCooldown / 2 && GameData.weapons[i].timeSinceAttack < GameData.weapons[i].attackCooldown)
            {
                
                Debug.Log("weaponObject.transform.position: " + weaponObject.transform.position + " targetPosition: " + GameData.weapons[i].targetPosition + " currentVelocity: " + GameData.weapons[i].currentVelocity + " attackCooldown: " + GameData.weapons[i].attackCooldown);
                weaponObject.transform.position = Vector3.SmoothDamp(
                        weaponObject.transform.position,
                        new Vector3(Mathf.Cos(Mathf.Deg2Rad * 360/i) * 2, Mathf.Sin(Mathf.Deg2Rad * 360/i) * 2, 0),
                        ref GameData.weapons[i].currentVelocity, 
                        GameData.weapons[i].attackCooldown / 4
                        );
            }
            if (GameData.weapons[i].timeSinceAttack >= GameData.weapons[i].attackCooldown)
            {
                //GameData.weapons[i].targetPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * 360/i) * 2, Mathf.Sin(Mathf.Deg2Rad * 360/i) * 2, 0);
                WeaponPointer weaponPointer = weaponObject.GetComponent<WeaponPointer>();
                if (weaponPointer != null)
                {
                    weaponPointer.isPointing = true;
                }
            }
            
        }
    }
}
