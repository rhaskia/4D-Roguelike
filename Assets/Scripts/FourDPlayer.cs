using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class KeyAxis
{
    public KeyCode p;
    public KeyCode n;
}

[System.Serializable]
public class FourDkeys
{
    public KeyAxis X;
    public KeyAxis Y;
    public KeyAxis Z;
    public KeyAxis W;
}

public class FourDPlayer : MonoBehaviour
{
    public Combat combat;

    [Header("Movement")]
    public Vector4 position;
    public FourDkeys keys;
    public Tilemap tilemap;
    public Tile wallTile;

    public Vector4 xOne, yOne, zOne, wOne;

    MapDrawer gridSize;

    [Header("Sprites")]
    public SpriteRenderer sprite;
    public Sprite deathSprite;
    bool dead;

    void Start()
    {
        gridSize = FindObjectOfType<MapDrawer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;

        InputManager();

        transform.position = new Vector3(position.x / gridSize.gridSize + position.w, position.y / gridSize.gridSize + position.z, 0);
    }

    public void InputManager()
    {
        if (Input.GetKeyDown(keys.X.p)) { Move(xOne); }
        if (Input.GetKeyDown(keys.X.n)) { Move(-xOne); }

        if (Input.GetKeyDown(keys.Y.p)) { Move(yOne); }
        if (Input.GetKeyDown(keys.Y.n)) { Move(-yOne); }

        if (Input.GetKeyDown(keys.Z.p)) { Move(zOne); }
        if (Input.GetKeyDown(keys.Z.n)) { Move(-zOne); }

        if (Input.GetKeyDown(keys.W.p)) { Move(wOne); }
        if (Input.GetKeyDown(keys.W.n)) { Move(-wOne); }

        if (Input.GetKeyDown(KeyCode.X)) { EnemySpawner.Instance.MoveEnemies(); }
    }

    void Move(Vector4 newPosition)
    {
        if (EnemySpawner.Instance.EnemyAtPoint(position + newPosition))
        {
            combat.Attack(EnemySpawner.Instance.GetEnemy(position + newPosition).combat);
        }
        else if (tilemap.GetTile(FourToTwo(position + newPosition)) != null)
        {
            position += newPosition;
            gridSize.UpdateScreen();
        }

        EnemySpawner.Instance.MoveEnemies();
    }

    public void Death()
    {
        dead = true;
        sprite.sprite = deathSprite;

        Log.AddLine("You Died...");
    }

    public Vector3Int FourToTwo(Vector4 v4)
    {
        return new Vector3Int((int)v4.x + (int)v4.w * gridSize.gridSize, (int)v4.y + (int)v4.z * gridSize.gridSize, 0);
    }
}
