using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    
    [SerializeField] public List<Weapon> weaponPrefabs = new List<Weapon>();
    public List<GameObject> weaponGameObjects = new List<GameObject>();
    public List<WeaponHandler> weaponHandlers = new List<WeaponHandler>();
    public int numberOfWeapons = 6;
    private float weaponDistance = 1.5f;
    public GameObject weaponPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfWeapons; i++)
        {
            AddWeapon(RandomWeapon());
        }
        for (int i = 0; i < GameData.weapons.Count; i++)
        {
            Weapon weapon = GameData.weapons[i];
            weaponGameObjects.Add(Instantiate(weaponPrefab, GetWeaponOrigin(i), Quaternion.identity, transform));
            weaponHandlers.Add(weaponGameObjects[i].GetComponent<WeaponHandler>());
            weaponGameObjects[i].GetComponent<SpriteRenderer>().sprite = weapon.sprite;
            weaponHandlers[i].currentVelocity = Vector3.zero;
        }
    }
    void Update()
    {
        for (int i = 0; i < GameData.weapons.Count; i++)
        {
            Weapon weapon = GameData.weapons[i];
            weaponHandlers[i].timeSinceAttack += Time.deltaTime;
        }
    }
    private void AddWeapon(Weapon weapon)
    {
        GameData.weapons.Add(weapon);
    }
    private void ResetWeaponLocations()
    {
        
    }
    public Vector3 GetWeaponOrigin(int index)
    {
        float angle = 360f / (float)GameData.weapons.Count * (float)index;
        return new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * weaponDistance, Mathf.Sin(Mathf.Deg2Rad * angle) * weaponDistance, 0);
    }
    private Weapon RandomWeapon()
    {
        int randomIndex = Random.Range(0, weaponPrefabs.Count);
        return weaponPrefabs[randomIndex];
    }
    public List<GameObject> GetWeaponObjects()
    {
        return weaponGameObjects;
    }
}
