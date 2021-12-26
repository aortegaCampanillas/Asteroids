using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public float speed = 1;
    public float timeout = 2;
    public float topX, topY;
    private Rigidbody2D rb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        Quaternion rot = transform.rotation;
        //Vector2 direction = new Vector2(transform.rotation.z, transform.rotation.z).normalized;
        rb.AddRelativeForce(Vector3.up * speed, ForceMode2D.Impulse);
        StartCoroutine(CheckTimeout());
    }

    // Update is called once per frame
    void Update()
    {
        Reposition();
    }

    IEnumerator CheckTimeout()
    {
        yield return new WaitForSeconds(timeout);
        Destroy(gameObject);
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
}
