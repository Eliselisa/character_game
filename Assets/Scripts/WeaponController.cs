using UnityEngine;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponData[] weaponsData;
    [SerializeField] private Transform weaponHolder;
    public UnityEvent<WeaponData> onWeaponChanged;

    private GameObject currentWeapon;
    private int currentWeaponIndex = 0;

    public WeaponData SelectedWeapon => weaponsData[currentWeaponIndex];

    private void Start()
    {
        SelectWeapon(0);
    }

    public void SelectWeapon(int weaponIndex)
    {
        var data = weaponsData[weaponIndex];
        var weaponPrefab = data.WeaponPrefab;

        if (currentWeapon != null)
        {
            Destroy(currentWeapon); // Destroy the current weapon if it exists
        }

        currentWeapon = Instantiate(weaponPrefab, weaponHolder);
        onWeaponChanged.Invoke(SelectedWeapon);
    }
    public void NextWeapon()
    {
        currentWeaponIndex++;

        // currentWeaponIndex %= weaponPrefabs.Length; // Ensure the index wraps around if it exceeds the array length
        // o % indica resto da divisão, ou seja, se currentWeaponIndex for maior que o tamanho do array, ele volta para 0

        if (currentWeaponIndex >= weaponsData.Length)
        {
          currentWeaponIndex = 0; // Loop back to the first weapon
        }

        SelectWeapon(currentWeaponIndex);
    }

    public void PreviousWeapon()
    {
        currentWeaponIndex--;

        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weaponsData.Length - 1; // Loop back to the last weapon
        }

        SelectWeapon(currentWeaponIndex);
    }




}
