using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private Text number;
    private void Start()
    {
        number = GetComponentInChildren<Text>();
    }
    private void Update()
    {
        if (System.Convert.ToInt32(number.text) < 1)
            Destroy(gameObject);
    }
}
