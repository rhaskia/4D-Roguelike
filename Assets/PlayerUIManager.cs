using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public FourDPlayer plr;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text.text = "health: " + plr.combat.HP + "\ndefense: " + plr.combat.defense + "\npower: " + plr.combat.power;
    }
}
