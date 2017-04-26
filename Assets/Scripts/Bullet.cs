using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform planet;
    public float movementSpeed = 5f;
    public float flyingHeight;
    public Vector3 movementDirection;
    public float lifetime = 10f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        //same movement code as in Player, proper comments there.
        var normal = (transform.position - planet.transform.position).normalized;
        movementDirection = Vector3.ProjectOnPlane(movementDirection, normal).normalized;
        transform.rotation = Quaternion.LookRotation(movementDirection, normal);
        transform.position = normal * flyingHeight + movementDirection * movementSpeed * Time.deltaTime;
    }
}
