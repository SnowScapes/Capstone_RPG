using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnCtrl : MonoBehaviour
{
    public string[] mob_code;
    public GameObject[] SpawnPoint;
    public GameObject[] Spawned_enemies;
    public List<GameObject> EnemyPool = new List<GameObject>();
    public GameObject[] Enemies;
    public bool[] Spawned;

    private float createTime = 2.0f;
    private Player playerCs;

    void Awake()
    {
        for (int i = 0; i < Spawned.Length; i++)
            Spawned[i] = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerCs = GameObject.FindWithTag("Player").GetComponent<Player>();

        for (int i = 0; i < SpawnPoint.Length; i++)
        {
            GameObject Enemy = (GameObject)Instantiate(Enemies[Random.Range(0,7)]);
            Enemy.name = "Enemy_" + i.ToString();
            Enemy.SetActive(false);
            EnemyPool.Add(Enemy);
            Spawned_enemies[i] = Enemy;
        }

        StartCoroutine(this.CreateEnemy());
    }

    void check_spawn()
    {

    }

    IEnumerator CreateEnemy()
    {
        Debug.Log("Start CreateEnemy Coroutine!");
        while (playerCs.PlayerCurHP != 0)
        {
            for (int i=0; i<Spawned.Length; i++)
            {
                if (!Spawned[i])
                {
                    Spawned[i] = true;
                    yield return new WaitForSeconds(createTime);

                    Debug.Log("Start CreateEnemy Coroutine! 1");

                    if (playerCs.PlayerCurHP == 0) yield break;

                    Debug.Log("Start CreateEnemy Coroutine! 2");

                    foreach (GameObject Enemy in EnemyPool)
                    {
                        Debug.Log("Start CreateEnemy Coroutine! roop");
                        if (!Enemy.activeSelf)
                        {
                            Enemy.transform.position = SpawnPoint[i].transform.position;
                            Enemy.SetActive(true);
                            Debug.Log("Create Enemy : " + Enemy.name);

                            break;
                        }
                    }
                }
            }
            yield return null;
        }
    }
}
