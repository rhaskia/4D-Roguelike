using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Log : MonoBehaviour
{
    public static Log Instance;
    public string text;
    public TextMeshProUGUI front;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        front.text = text;
    }

    public static void AddLine(string line)
    {
        Instance.text = line.ToLower() + "\n\n" + Instance.text;
    }
}
