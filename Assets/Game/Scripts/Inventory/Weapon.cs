using UnityEngine;

public enum WEAPON_TYPE
{
    MELEE,
    RANGE
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]

public class Weapon : Item
{
    public float _damage;

    public WEAPON_TYPE WEAPON_TYPE;
}
