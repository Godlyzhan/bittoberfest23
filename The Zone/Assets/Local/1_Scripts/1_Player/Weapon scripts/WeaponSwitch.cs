using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Transform[] weaponPositions;
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();

    private int currentActiveWeapon = 0;
    private int weaponCount = 0;
    private int maxWeaponCount = 4;
        
    private void OnEnable()
    {
        inputHandler.NextWeaponSwitchEvent += NextWeapon;
        inputHandler.PreviousWeaponSwitchEvent += PreviousWeapon;
    }

    private void OnDisable()
    {
        inputHandler.NextWeaponSwitchEvent -= NextWeapon;
        inputHandler.PreviousWeaponSwitchEvent -= PreviousWeapon;
    }
    
    public void SetGameObjectPosition(GameObject pickObject)
    {
        if (weapons.Count < 5)
        {
            weapons.Add(pickObject);
            weaponCount++;
            pickObject.transform.parent = weaponPositions[weaponCount];
            pickObject.transform.position = weaponPositions[weaponCount].position;
            pickObject.transform.eulerAngles = weaponPositions[weaponCount].eulerAngles;
        }
    }

    private void NextWeapon()
    {
        var newRotation = transform.eulerAngles.y + 72;
        currentActiveWeapon++;
        transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
    }

    private void PreviousWeapon()
    {
        var newRotation = transform.eulerAngles.y - 72;
        currentActiveWeapon--;
        transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
    }

}

