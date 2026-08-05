using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;

    public float range = 4f;
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
        
        for (int i = 0; i < weaponManager.weaponGameObjects.Count; i++)
        {
            Vector3 direction = (mousePosition - weaponManager.weaponGameObjects[i].transform.position).normalized;
            if (weaponManager.weaponHandlers[i].timeSinceAttack > GameData.weapons[i].attackCooldown)
            {
                weaponManager.weaponHandlers[i].timeSinceAttack = 0f;
                GameObject weaponObject = weaponManager.GetWeaponObjects()[i];
                weaponManager.weaponHandlers[i].targetPosition = (weaponObject.transform.position + direction * range - transform.position).normalized * range + transform.position;
                weaponObject.transform.position = Vector3.SmoothDamp(
                    weaponManager.GetWeaponObjects()[i].transform.position, 
                    weaponManager.weaponHandlers[i].targetPosition, 
                    ref weaponManager.weaponHandlers[i].currentVelocity, 
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
        for (int i = 0; i < weaponManager.weaponGameObjects.Count; i++)
        {
            GameObject weaponObject = weaponManager.GetWeaponObjects()[i];
            if (float.IsNaN(weaponManager.weaponHandlers[i].currentVelocity.x) || float.IsNaN(weaponManager.weaponHandlers[i].currentVelocity.y))
            {
                Debug.Log("WE ARE ALL GONNA DIE");
                weaponManager.weaponHandlers[i].currentVelocity = Vector3.zero;
            }
            if (weaponObject.transform.position != weaponManager.weaponHandlers[i].targetPosition && weaponManager.weaponHandlers[i].timeSinceAttack < GameData.weapons[i].attackCooldown / 4)
            {
                
                weaponObject.transform.position = Vector3.SmoothDamp(
                    weaponObject.transform.position, 
                    weaponManager.weaponHandlers[i].targetPosition, 
                    ref weaponManager.weaponHandlers[i].currentVelocity, 
                    GameData.weapons[i].attackCooldown / 10
                    );
            }
            if (weaponManager.weaponHandlers[i].timeSinceAttack >= GameData.weapons[i].attackCooldown / 4 && weaponManager.weaponHandlers[i].timeSinceAttack < GameData.weapons[i].attackCooldown / 2)
            {
                weaponObject.transform.position = weaponManager.weaponHandlers[i].targetPosition;
            }
            Vector3 originalPosition = weaponManager.GetWeaponOrigin(i) + transform.position;
            if (weaponObject.transform.position != originalPosition && weaponManager.weaponHandlers[i].timeSinceAttack >= GameData.weapons[i].attackCooldown / 2 && weaponManager.weaponHandlers[i].timeSinceAttack < GameData.weapons[i].attackCooldown)
            {
                
                //Debug.Log("weaponObject.transform.position: " + weaponObject.transform.position + " targetPosition: " + originalPosition + " currentVelocity: " + weaponManager.weaponHandlers[i].currentVelocity + " attackCooldown: " + GameData.weapons[i].attackCooldown);
                weaponObject.transform.position = Vector3.SmoothDamp(
                    weaponObject.transform.position,
                    originalPosition,
                    ref weaponManager.weaponHandlers[i].currentVelocity, 
                    GameData.weapons[i].attackCooldown / 8
                    );
            }
            if (weaponManager.weaponHandlers[i].timeSinceAttack >= GameData.weapons[i].attackCooldown)
            {
                weaponManager.weaponHandlers[i].targetPosition = originalPosition;
                WeaponPointer weaponPointer = weaponObject.GetComponent<WeaponPointer>();
                if (weaponPointer != null)
                {
                    weaponPointer.isPointing = true;
                }
            }
            
        }
    }
}
