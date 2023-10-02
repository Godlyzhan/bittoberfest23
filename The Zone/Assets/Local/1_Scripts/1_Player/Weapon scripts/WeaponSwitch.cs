using System;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Transform[] weaponPositions;
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();

    private int currentActiveWeapon = 0;
    private int maxWeaponCount = 4;
    private int weaponCount = 0;
    private void OnEnable()=> inputHandler.WeaponSwitchEvent += SwitchWeapon;
    private void OnDisable() => inputHandler.WeaponSwitchEvent -= SwitchWeapon;
    
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

    private void SwitchWeapon()
    {
        if (currentActiveWeapon < maxWeaponCount)
        {
            currentActiveWeapon++;
        }
        else
        {
            currentActiveWeapon = 0;
        }
        Debug.Log(currentActiveWeapon.Cyan());
       
        var newRotation = transform.eulerAngles.y + 72;
        transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
    }

}

