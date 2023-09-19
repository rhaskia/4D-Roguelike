using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   public static EnemySpawner Instance;

    public bool spawn1st = false;// true = enemy can't respawn
    public int enemiesPerRoom;
    public GameObject[] enemyPrefabs;
    public int[] weights;

    public List<Enemy> enemies = new List<Enemy>();

    public void Start(){Instance = this;}

    void Update(){}

    public void MoveEnemies(){foreach (var enemy in enemies){enemy.Move();}}

    public bool EnemyAtPoint(Vector4 point)
    {   foreach (var enemy in enemies)
        {if (enemy.position == point) return true;}
        return false;
    }
    public Enemy GetEnemy(Vector4 point)
    {   foreach (var enemy in enemies)
        {if (enemy.position == point) return enemy;}
        return null;
    }

    public void SpawnEnemies(hCube room)
    {   string MonstersIncrement = "poise"; //changeable
        int numMonsters = 1; //unassigned local variable must = something

        if (!spawn1st){print("numMonsters start"); spawn1st = true;
            numMonsters = Random.Range(0, enemiesPerRoom);}
        else if (MonstersIncrement == "decay") {numMonsters = Random.Range(0, 1); 
            print("respawned");}
        else if (MonstersIncrement == "poise") {numMonsters = 1; 
            print("respawned");}
        else if (MonstersIncrement == "extra") {numMonsters = Random.Range(1, 2); 
            print("numMonsters+");}
        else if (MonstersIncrement == "swell") {numMonsters = 2;
            print("numMonsters++");}
        else if (MonstersIncrement == "OCEAN") {numMonsters = Random.Range(0, 5);
            print("numMonsters+++");}
        else { print("Monster die forever"); }

        for (int i = 0; i < numMonsters; i++)
        {   Vector4 pos = room.randomPos();

            int random = Random.Range(0, 100);
            int count = 0;
            int behindC = 0;

            for (int j = 0; j < enemyPrefabs.Length; j++)
            {   count += weights[j];
                if (random > behindC && random < count && !EnemyAtPoint(pos))
                {   Enemy objec = Instantiate(enemyPrefabs[j], transform).GetComponent<Enemy>();
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
