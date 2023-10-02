using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            var weaponSwitch = other.GetComponentInParent<WeaponController>().WeaponSwitch;
            weaponSwitch.SetGameObjectPosition(gameObject);
            Destroy(this);
        }
    }
}
