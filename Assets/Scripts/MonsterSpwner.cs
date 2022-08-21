using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] monsterRef;

    private GameObject spawnMonster;

    [SerializeField]
    private Transform leftPos, rightPos;


    private int randomIndex;
    private int randomSide;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SpawnMonsters());
        
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));

            randomIndex = Random.Range(0, monsterRef.Length);

            randomSide = Random.Range(0, 2);

            spawnMonster = Instantiate(monsterRef[randomIndex]);


            if (randomSide == 0)
            {
                // left side
                spawnMonster.transform.position = leftPos.position;
                spawnMonster.GetComponent<EnemyMove>().speed = Random.Range(4, 10);

            }
            else
            {
                // right side
                spawnMonster.transform.position = rightPos.position;
                spawnMonster.GetComponent<EnemyMove>().speed = -Random.Range(4, 10);
                spawnMonster.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }
}
