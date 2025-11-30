using UnityEngine;

public class AttackCommand : ICommand
{
    Attacker attacker;
    bool completed = false;
    public bool Completed => completed;
    float startAttackTime;
    float attackDuration;

    public AttackCommand(Attacker attacker)
    {
        this.attacker = attacker;
    }

    public void Execute()
    {
        attackDuration = attacker.WeaponController.SelectedWeapon.Duration;

        startAttackTime = Time.time;
        attacker.Attack();
    }

    public void Stop()
    {
        
    }

    public void Update()
    {
       if (Time.time >= startAttackTime + attackDuration)
        {
            completed = true;
        }
    }
}
