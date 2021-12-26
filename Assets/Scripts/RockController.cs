using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public float maxSpeed, maxRotationSpeed;
    public int life = 1;
    private Rigidbody2D rb;
    public RockCreator rockCreator;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        rockCreator = GameObject.Find("RocksCreator").GetComponent<RockCreator>();
        float forceX = Random.Range(-maxSpeed , maxSpeed);
        float forcey = Random.Range(-maxSpeed , maxSpeed);
        float rotationX = Random.Range(maxRotationSpeed / 2, maxRotationSpeed);
        float rotationY = Random.Range(maxRotationSpeed / 2, maxRotationSpeed);
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector3(forceX, forcey, 0));
        //rb.AddTorque(new Vector3(rotationX, rotationY, 0));
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust possition when out of bounds
        if (transform.position.y > player.topY)
        {
            transform.position = new Vector2(transform.position.x, -player.topY);
        }
        else if (transform.position.y < -player.topY)
        {
            transform.position = new Vector2(transform.position.x, player.topY);
        }
        if (transform.position.x > player.topX)
        {
            transform.position = new Vector2(-player.topX, transform.position.y);
        }
        if (transform.position.x < -player.topX)
        {
            transform.position = new Vector2(player.topX, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shot"))
        {
            Destroy(collision.gameObject);
            rockCreator.RockDestroyed(this);
        }
    }

    public void SetCreator(RockCreator creator)
    {
        rockCreator = creator;
    }
}
