using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float topX, topY, speed, rotationSpeed;
    public GameObject shot;
    public float shotInterval = 1;
    private float hAxis, vAxis;
    private bool isShoting = false; 

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        ApplyForces();
        Reposition();    

        if (Input.GetKey(KeyCode.Space))
        {
            CreateShot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           // Destroy(gameObject);
        }
    }

    private void ApplyForces()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        // Rotate with horizontal axis input
        transform.Rotate(Vector3.back * hAxis * Time.deltaTime * rotationSpeed);
        // Impulse with vertical axis input
        Vector3 force = Vector3.up * speed * vAxis;
        rb.AddRelativeForce(force, ForceMode2D.Impulse);
    }

    private void Reposition()
    {
        // Adjust possition when out of bounds
        if (transform.position.y > topY)
        {
            transform.position = new Vector2(transform.position.x, -topY);
        }
        else if (transform.position.y < -topY)
        {
            transform.position = new Vector2(transform.position.x, topY);
        }
        if (transform.position.x > topX)
        {
            transform.position = new Vector2(-topX, transform.position.y);
        }
        if (transform.position.x < -topX)
        {
            transform.position = new Vector2(topX, transform.position.y);
        }
    }
    private void CreateShot()
    {
        if (!isShoting)
        {
            isShoting = true;
            Object newShot = Instantiate(shot, transform.position, transform.rotation);
            StartCoroutine(CheckTimeout());
        }
    }

    IEnumerator CheckTimeout()
    {
        yield return new WaitForSeconds(shotInterval);
        isShoting = false;
    }
}
