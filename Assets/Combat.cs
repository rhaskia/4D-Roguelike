using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Combat : MonoBehaviour
{
    public string color;
    string creatureName;

    public int maxHP;
    public int HP;
    public int defense;
    public int power;

    public UnityEvent death = new UnityEvent();

    bool died = false;

    // Start is called before the first frame update
    void Start()
    {
        creatureName = "<color=#" + color + ">" + gameObject.name + "</color>";
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 1 && !died)
        {
            death.Invoke();
            died = true;
        }
    }

    public void Attack(Combat other)
    {
        int damage = power - other.defense;

        if (damage > 0)
        {
            Log.AddLine(creatureName + " attacks " + other.creatureName + " and does " + damage.ToString() + " damage!");
            other.TakeDamage(damage);
        }
        else Log.AddLine(creatureName + " attacks " + other.creatureName + " but it has no effect!");

    }

    void TakeDamage(int v)
    {
        HP -= v;
    }
}
