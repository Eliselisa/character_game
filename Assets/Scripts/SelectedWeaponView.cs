using UnityEngine;
using UnityEngine.UI;

public class SelectedWeaponView : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private Image weaponIcon;

    private void Start()
    {
        UpdateWeaponIcon(weaponController.SelectedWeapon);
        weaponController.onWeaponChanged.AddListener(UpdateWeaponIcon);
    }

    private void OnDestroy()
    {
        weaponController.onWeaponChanged.RemoveListener(UpdateWeaponIcon);
    }
    private void UpdateWeaponIcon(WeaponData selectedWeapon)
    {
        weaponIcon.sprite = selectedWeapon.Icon;
    }
}
