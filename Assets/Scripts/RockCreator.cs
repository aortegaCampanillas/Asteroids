using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCreator : MonoBehaviour
{
    public GameObject rock;
    public int rockCount = 5;
    public int rocksAlive = 0;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        CreateRocks();
        
    }

    void CreateRocks()
    {
        for (int i = 0; i < rockCount; i++)
        {
            float posX = Random.Range(-player.topX, player.topX);
            float posY = Random.Range(-player.topY, player.topY);

            GameObject newRock = Instantiate(rock, new Vector3(posX, posY, 0), rock.transform.rotation);
            rocksAlive++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RockDestroyed(RockController rockDestroyed)
    {
        int life = rockDestroyed.life;
        Vector3 pos = rockDestroyed.gameObject.transform.position;
        Quaternion rot = rockDestroyed.gameObject.transform.rotation;
        float size = rockDestroyed.gameObject.transform.localScale.x;
        Vector3 scale = new Vector3(size / 2, size / 2, 1);

        if (rockDestroyed.life < 3)
        {
            Quaternion rot1 = rot;
            Quaternion rot2 = rot;
            rot1.x += 5;
            rot1.y += 5;
            rot2.x -= 5;
            rot2.y -= 5;

            GameObject newRock1 = Instantiate(rock, pos, rot1);
            GameObject newRock2 = Instantiate(rock, pos, rot1);
            newRock1.transform.localScale = scale;
            newRock2.transform.localScale = scale;
            newRock1.GetComponent<RockController>().life = life + 1;
            newRock2.GetComponent<RockController>().life = life + 1;
            rocksAlive += 2;
        } 
        
        Destroy(rockDestroyed.gameObject);
        rocksAlive--;

        if (rocksAlive == 0)
        {
            rockCount++;
            CreateRocks();
        }
    }
}
