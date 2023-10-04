using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Transform[] weaponPositions;
    [SerializeField] private List<WeaponStats> weaponStatsList = new List<WeaponStats>(); //Ensure there is a default state
    [SerializeField] private Transform ShootingPoint;

    private WeaponStats activeWeaponStats;

    private int currentActiveWeapon = 0;
    private int weaponCount = 0;
    private int maxWeaponCount = 4;
        
    private void OnEnable()
    {
        inputHandler.NextWeaponSwitchEvent += NextWeapon;
        inputHandler.PreviousWeaponSwitchEvent += PreviousWeapon;
        inputHandler.ShootEvent += Shoot;

        activeWeaponStats = weaponStatsList[0];
    }

    private void OnDisable()
    {
        inputHandler.NextWeaponSwitchEvent -= NextWeapon;
        inputHandler.PreviousWeaponSwitchEvent -= PreviousWeapon;
        inputHandler.ShootEvent -= Shoot;
    }
    
    public void SetGameObjectPosition(GameObject pickObject, WeaponStats weaponStats)
    {
        if (weaponCount < 4)
        {
            weaponCount++;
            weaponStatsList[weaponCount] = weaponStats;
            pickObject.transform.parent = weaponPositions[weaponCount];
            pickObject.transform.position = weaponPositions[weaponCount].position;
            pickObject.transform.eulerAngles = weaponPositions[weaponCount].eulerAngles;
        }
    }

    private void NextWeapon()
    {
        if (currentActiveWeapon > 0)
        {
            currentActiveWeapon--;
        }
        else
        {
            currentActiveWeapon = 4;
        }
        
        activeWeaponStats = weaponStatsList[currentActiveWeapon];

        var newRotation = transform.eulerAngles.y + 72;
        transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
    }

    private void PreviousWeapon()
    {
        if (currentActiveWeapon < 4)
        {
            currentActiveWeapon++;
        }
        else
        {
            currentActiveWeapon = 0;
        }

        activeWeaponStats = weaponStatsList[currentActiveWeapon];
        
        var newRotation = transform.eulerAngles.y - 72;
        transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
    }

    private void Shoot()
    {
        Debug.Log(currentActiveWeapon.Cyan());
        if (activeWeaponStats.prefab != null)
        {
            GameObject bullet = Instantiate(activeWeaponStats.prefab, ShootingPoint.position, Quaternion.Euler(transform.forward));
            var rigidBody = bullet.GetComponent<Rigidbody>();
            rigidBody.velocity = ShootingPoint.forward * activeWeaponStats.BulletSpeed;
        }
    }
}

