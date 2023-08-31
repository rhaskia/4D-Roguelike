using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public int enemiesPerRoom;
    public GameObject[] enemyPrefabs;
    public int[] weights;

    public List<Enemy> enemies = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.Move();
        }
    }

    public bool EnemyAtPoint(Vector4 point)
    {
        foreach (var enemy in enemies)
        {
            if (enemy.position == point) return true;
        }

        return false;
    }
    public Enemy GetEnemy(Vector4 point)
    {
        foreach (var enemy in enemies)
        {
            if (enemy.position == point) return enemy;
        }

        return null;
    }

    public void SpawnEnemies(hCube room)
    {

        print("a");
        int numMonsters = Random.Range(0, enemiesPerRoom);

        for (int i = 0; i < numMonsters; i++)
        {
            Vector4 pos = room.randomPos();

            int random = Random.Range(0, 100);
            int count = 0;
            int behindC = 0;

            for (int j = 0; j < enemyPrefabs.Length; j++)
            {
                count += weights[j];

                if (random > behindC && random < count && !EnemyAtPoint(pos))
                {
                    Enemy objec = Instantiate(enemyPrefabs[j], transform).GetComponent<Enemy>();
                    objec.gameObject.name = enemyPrefabs[j].name;
                    enemies.Add(objec);
                    objec.position = pos;
                }

                behindC = count;
            }
        }

        print(enemies.Count);
    }
}
