using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Game/Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private string animationTrigger;
    [SerializeField] private int damage = 30;
    [SerializeField] private float duration = 1f;
    public Sprite Icon
    {
        get { return icon; }  // the same as => icon;
    }
    public GameObject WeaponPrefab => weaponPrefab;

    public string AnimationTrigger => animationTrigger;

    public int Damage => damage;

    public float Duration => duration;
}
