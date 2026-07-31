using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    
    [SerializeField] public List<Weapon> weaponPrefabs = new List<Weapon>();
    private List<GameObject> weaponGameObjects = new List<GameObject>();
    public GameObject weaponPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            AddWeapon(RandomWeapon());
        }
        for (int i = 0; i < GameData.weapons.Count; i++)
        {
            Weapon weapon = GameData.weapons[i];
            float angle = (360 / GameData.weapons.Count) * i;
            weaponGameObjects.Add(Instantiate(weaponPrefab, new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * 2, Mathf.Sin(Mathf.Deg2Rad * angle) * 2, 0), Quaternion.identity, transform));
            weaponGameObjects[i].GetComponent<SpriteRenderer>().sprite = weapon.sprite;
        }
    }
    private void AddWeapon(Weapon weapon)
    {
        GameData.weapons.Add(weapon);
    }
    private Weapon RandomWeapon()
    {
        int randomIndex = Random.Range(0, weaponPrefabs.Count);
        return weaponPrefabs[randomIndex];
    }
}
