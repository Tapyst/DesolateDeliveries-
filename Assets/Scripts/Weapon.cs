using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Weapon")]
public class Weapon : ScriptableObject
{
    public Sprite sprite;
    public float attackCooldown = 10f;
    
    public string methodName = "name";
    public string description = "description";
    public GameData.Rarity rarity;
    public int price = -1;
    //public List<CakeEventEnums> triggerEvents = new List<CakeEventEnums>();


    public float timeSinceAttack = 1000f;
    public Vector3 currentVelocity = Vector3.zero;
    public Vector3 targetPosition = Vector3.zero;
}
