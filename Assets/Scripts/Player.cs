using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform planet;
    public Transform graphics;
    public float movementSpeed = 5f;
    public float flyingHeight;
    public Bullet bulletPrefab;
    public float shootCooldown = 0.2f;
    private float shootTimer;

    private void Start()
    {
        //start position
        var normal = (transform.position - planet.transform.position).normalized;
        transform.position = normal * flyingHeight;
    }

    private void Update()
    {
        //normal from planet center
        var normal = (transform.position - planet.transform.position).normalized;

        //movement
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            //movement direction in local space
            var movementDirection = transform.right * horizontalAxis + transform.forward * verticalAxis;

            //projecting movement direction to the planet normal (follows surface)
            movementDirection = Vector3.ProjectOnPlane(movementDirection, normal);

            //forward direction in local space
            var forwardDirection = transform.forward;

            //projecting forward direction to the planet normal (follows surface)
            forwardDirection = Vector3.ProjectOnPlane(forwardDirection, normal).normalized;

            //rotate graphics to projected movement direction
            graphics.rotation = Quaternion.LookRotation(movementDirection, normal);

            //rotate transform to projected forward direction
            transform.rotation = Quaternion.LookRotation(forwardDirection, normal);

            //move to projected movement direction at the correct height
            transform.position = normal * flyingHeight + movementDirection * movementSpeed * Time.deltaTime;
        }

        //shooting
        float horizontal2Axis = Input.GetAxis("Horizontal2");
        float vertical2Axis = Input.GetAxis("Vertical2");

        if ((horizontal2Axis != 0 || vertical2Axis != 0) && shootTimer < Time.time)
        {
            //shoot direction in local space
            var shootDirection = transform.right * horizontal2Axis + transform.forward * vertical2Axis;

            //instantiating bullet
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as Bullet;

            //bullet stats
            bullet.movementDirection = shootDirection;
            bullet.flyingHeight = flyingHeight;
            bullet.planet = planet;

            shootTimer = Time.time + shootCooldown;
        }
    }
}
