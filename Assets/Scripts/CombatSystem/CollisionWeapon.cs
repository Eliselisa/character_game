using System.Collections;
using UnityEngine;

public class CollisionWeapon : Weapon
{
    [SerializeField] float enableColliderTime = 0.43f;
    [SerializeField] float disableColliderTime = 0.63f;

    Collider weaponCollider;

    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false;
    }

    public override void Attack()
    {
        Debug.Log("CollisionWeapon Attack");
        StartCoroutine(ColliderAttackCoroutine());
    }

    IEnumerator ColliderAttackCoroutine()
    {
        yield return new WaitForSeconds(enableColliderTime);
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(disableColliderTime - enableColliderTime);
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("CollisionWeapon OnTriggerEnter with " + other.gameObject.name);
        var damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            Damage(damageable);
        }
    }
}
