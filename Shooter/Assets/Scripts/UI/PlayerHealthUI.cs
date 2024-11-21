using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private PlayerDamage playerObjectHealth;

    private TextMeshProUGUI healthText;
    private Slider healthSlider;
    private void Start()
    {
        playerObjectHealth = FindFirstObjectByType<PlayerDamage>();

        healthText = GetComponentInChildren<TextMeshProUGUI>();
        healthSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health : " + playerObjectHealth.Health.ToString();
        healthSlider.value = playerObjectHealth.Health;
    }
}
