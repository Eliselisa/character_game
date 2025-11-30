using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponSelectionUI : MonoBehaviour
{

    [SerializeField] private WeaponController weaponController;
    [SerializeField] private Transform weaponButtonContainer;
    [SerializeField] private Button weaponButtonPrefab; 
    [SerializeField] private CanvasGroup canvasGroup;

    public bool IsOpen => canvasGroup.interactable;

    public UnityEvent<bool> UIStateChanged;

    void Start()
    {
        foreach(var weapondata in weaponController.WeaponsData)
        {
            var button = Instantiate<Button>(weaponButtonPrefab, weaponButtonContainer);
            var image = button.GetComponentInChildren<Image>();
            image.sprite = weapondata.Icon;
            button.onClick.AddListener(() => SelectWeapon(weapondata)); // Capture the current weapondata in the lambda

        }
        Close();
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        UIStateChanged.Invoke(true);
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        UIStateChanged.Invoke(false);
    }

    void SelectWeapon(WeaponData weaponData)
    {
        var index = System.Array.IndexOf(weaponController.WeaponsData, weaponData);
        weaponController.SelectWeapon(index);

        Close();
    }
}
