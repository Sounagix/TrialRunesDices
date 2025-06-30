using UnityEngine;

public class Equipment : MonoBehaviour
{
    private Weapon _equipedWeapon;



    public void EquipWeapon(Weapon weapon)
    {
        if (_equipedWeapon)
            Inventory.Instance.AddItem(weapon);
        _equipedWeapon = weapon;
    }


    public float GetDamage()
    {
        return _equipedWeapon ? _equipedWeapon._damage : 0;
    }
}
