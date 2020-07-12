using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static int health;
    public static int money;

    public int maxHealth;
    public int startingMoney;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI moneyText;

    private void Awake()
    {
        health = maxHealth;
        money = startingMoney;
    }

    private void Update()
    {
        if(health <= 0)
        {
            Debug.Log("No health");
            SceneManager.LoadScene("Menu");
        }

        if(healthText != null)
        {
            healthText.text = health + "";
            moneyText.text = money + "";
        }
    }
}
