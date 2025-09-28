using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Game/Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject weaponPrefab;

    public Sprite Icon
    {
        get { return icon; }  // the same as => icon;
    }
    public GameObject WeaponPrefab => weaponPrefab;
}
