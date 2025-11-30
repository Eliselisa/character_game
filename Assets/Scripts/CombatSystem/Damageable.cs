using UnityEngine;

public class Damageable : MonoBehaviour
{

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public int CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}/{maxHealth}");
        if (currentHealth <= 0)
        {
            OnDestroyed();
        }
    }

    private void OnDestroyed()
    {
        Debug.Log($"{gameObject.name} has been destroyed.");
        Destroy(gameObject);
    }



}
