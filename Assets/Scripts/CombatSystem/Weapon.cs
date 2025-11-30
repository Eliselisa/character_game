using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;

    public abstract void Attack();

    public void Damage(Damageable target)
    {
        target.TakeDamage(weaponData.Damage);
    }

}
