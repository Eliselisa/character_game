using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] Animator animator;
    private WeaponController weaponController;

    public WeaponController WeaponController => weaponController;


    private void Start()
    {
        weaponController = GetComponent<WeaponController>();
    }

    public AttackCommand CreateAttackCommand()
    {
        return new AttackCommand(this);
    }

    public void Attack()
    {
            animator.SetTrigger("Attack");
            weaponController.CurrentWeapon?.GetComponent<Weapon>()?.Attack();
    }
}
