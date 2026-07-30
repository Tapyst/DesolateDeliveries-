using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static int daysCompleted = 0;
    //public static double mostDamage = -1;
    public static int money = 4; //2147483647
    public static bool isDev = true;
    public static List<Weapon> weapons = new List<Weapon>();
    public enum Rarity
    {
        common,
        uncommon,
        rare,
        legendary,
        mythic
    }
}
