using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static int health;
    public static int money;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI moneyText;

    private void Start()
    {
        health = 3;
        money = 5;
    }

    private void Update()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
        healthText.text = health + "";
        moneyText.text = money + "";

    }
}
