using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Transform[] weaponPositions;
    [SerializeField] private List<WeaponStats> weaponStatsList = new List<WeaponStats>(); //Ensure there is a default state
    [SerializeField] private Transform ShootingPoint;

    private WeaponStats activeWeaponStats;
    private WeaponTypes.WeaponsTypes previousWeaponType;

    private int currentActiveWeapon = 0;
    private int weaponCount = 0;
    private int maxWeaponCount = 4;
    private int minWeaponCount = 4;

    private float blasterPowerUp;
    private float blasterMaxPower = 10.0f;

    private bool weaponSwapped;

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

    private void Update()
    {
            
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
        currentActiveWeapon = currentActiveWeapon > minWeaponCount ? currentActiveWeapon-- : maxWeaponCount;
        activeWeaponStats = weaponStatsList[currentActiveWeapon];

        var newRotation = transform.eulerAngles.y + 72;
        transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
    }

    private void PreviousWeapon()
    {
        currentActiveWeapon = currentActiveWeapon < maxWeaponCount ? currentActiveWeapon++ : minWeaponCount;
        activeWeaponStats = weaponStatsList[currentActiveWeapon];
        
        var newRotation = transform.eulerAngles.y - 72;
        transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
    }

    private void Shoot()
    {
        switch (activeWeaponStats.WeaponType)
        {
            case WeaponTypes.WeaponsTypes.Blaster:
                break;
            case WeaponTypes.WeaponsTypes.Shield:
                Shield();
                break;
            case WeaponTypes.WeaponsTypes.Stun:
                Stun();
                break;
            case WeaponTypes.WeaponsTypes.Laser:
                Laser();
                break;
            case WeaponTypes.WeaponsTypes.Normal:
                NormalWeapon();
                break;
            case WeaponTypes.WeaponsTypes.Key:
                Key();
                break;
        }
    }

    private void PowerUpBlaster(float power)
    {
        if (activeWeaponStats.WeaponType != WeaponTypes.WeaponsTypes.Blaster)
        {
            return;
        }

        if (inputHandler.Shoot && blasterPowerUp < blasterMaxPower)
        {
            GameObject bullet = Instantiate(activeWeaponStats.prefab, ShootingPoint.position, Quaternion.identity); 
            Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

        bullet.transform.localScale += new Vector3(power, power, power);

            previousWeaponType = WeaponTypes.WeaponsTypes.Blaster;

          
                blasterPowerUp += Time.deltaTime;
            }
            else if(inputHandler.ReleaseShot)
            {
               // FireBaster(blasterPowerUp, rigidbody);
                blasterPowerUp = 0.0f;
            }
        
    }

    private void FireBaster(float power, Rigidbody rigidbody)
    {
        float damage = activeWeaponStats.Damage * (power * 10);
        float force = activeWeaponStats.ImpactForce * (power * 10);
        float speed =  activeWeaponStats.BulletSpeed * (power * 10);
        
        rigidbody.velocity = ShootingPoint.forward * speed;
        
        //Needs to power up 
        //Scales up as the power increases
        //shoots when the power is released
        //Speed, damage and force are in relation to how much it been powered up
        //this all used energy have to steal energy from NPC/power sources
    }

    private void Shield()
    {
        //Blocks incoming enemy attacks
        //Shoots a force back, consumes a lot of energy
        //energy gets depleted when the shield is hit
    }

    private void Stun()
    {
        //Stun allow the player to drain energy from the NPC when the are stunned.
        //We cannot get energy from them once they are destroyed
    }

    private void Laser()
    {
        //Laser beam that does a lot of damage and is constantly on until the shoot button is felt
        //Consumes a lot of energy, does the most damage over time
    }

    private void NormalWeapon()
    {
        //Shoot constants energy bullets
    }

    private void Key()
    {
        //needed to extract energy from NPC and objects
    }
}

