using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SphereMovement : MonoBehaviour
{
    [field:SerializeField] public WeaponHandler WeaponHandler { get; set; }
    
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private float inputMultiplier = 100f;
    [SerializeField] private float rotationalDamping = 0.2f;

    private new Rigidbody rigidbody;

    void Start() => rigidbody = GetComponent<Rigidbody>();
    void FixedUpdate() => ForceMovement();


    private void ForceMovement()
    {
        if (inputHandler.StopMovement)
        {
            rigidbody.velocity = Vector3.Slerp(rigidbody.velocity, Vector3.zero, 0.05f);
            return;
        }

        Vector3 move = new Vector3(Mathf.Lerp(0f, inputHandler.MovementValue.x, rotationalDamping), 0f, 
            Mathf.Lerp(0f, inputHandler.MovementValue.y, rotationalDamping)) * inputMultiplier;

        rigidbody.AddForce(move * Time.deltaTime);
    }
}
