using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector4 position;
    public Combat combat;

    MapDrawer gridSize;
    FourDPlayer plr;

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        gridSize = FindObjectOfType<MapDrawer>();
        plr = FindObjectOfType<FourDPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(position.x / gridSize.gridSize + position.w, position.y / gridSize.gridSize + position.z, 0);

        sprite.color = gridSize.Within5(position, plr.position) ? Color.white : new Color(255, 255, 255, 0);

        if (combat.HP < 1)
        {
            print("died");
            EnemySpawner.Instance.enemies.Remove(this);
            Destroy(gameObject);
        }
    }

    public void Move()
    {
        if (!MapDrawer.Instance.Within5(plr.position, position))
            return;

        Vector4 dx = plr.position - position;
        float distance = Vector4.Distance(plr.position, position);

        Vector4 d = GetDirection(new Vector4(Mathf.RoundToInt(dx.x / distance), Mathf.RoundToInt(dx.y / distance),
                                Mathf.RoundToInt(dx.z / distance), Mathf.RoundToInt(dx.w / distance)));

        print(d);

        if (position + d == plr.position)
        {
            combat.Attack(plr.combat);
        }
        else if (gridSize.tilemap.GetTile(FourToTwo(position + d)) != null && !EnemySpawner.Instance.EnemyAtPoint(position + d))
        {
            position += d;
            gridSize.UpdateScreen();
        }
    }

    Vector4 GetDirection(Vector4 d)
    {
        if (Mathf.Abs(d.x) + Mathf.Abs(d.y) + Mathf.Abs(d.z) + Mathf.Abs(d.w) == 1) return d;

        d.x = 0;

        if (Mathf.Abs(d.x) + Mathf.Abs(d.y) + Mathf.Abs(d.z) + Mathf.Abs(d.w) == 1) return d;

        d.y = 0;

        if (Mathf.Abs(d.x) + Mathf.Abs(d.y) + Mathf.Abs(d.z) + Mathf.Abs(d.w) == 1) return d;

        d.z = 0;

        return d;
    }

    public Vector3Int FourToTwo(Vector4 v4)
    {
        return new Vector3Int((int)v4.x + (int)v4.w * gridSize.gridSize, (int)v4.y + (int)v4.z * gridSize.gridSize, 0);
    }
}
