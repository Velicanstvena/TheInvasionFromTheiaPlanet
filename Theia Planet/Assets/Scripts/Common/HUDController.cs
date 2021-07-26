using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    private Health player;
    [SerializeField] private Image filler;
    [SerializeField] private TextMeshProUGUI healthText;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<Health>();

        filler.fillAmount = player.HealthValue * 0.002f;
        healthText.text = player.HealthValue + " / 500";

        Health.onHealthChange += DisplayHUD;
    }

    private void OnDisable()
    {
        Health.onHealthChange -= DisplayHUD;
    }

    void Update()
    {
        
    }

    private void DisplayHUD()
    {
        filler.fillAmount = player.HealthValue * 0.002f;
        healthText.text = player.HealthValue + " / 500";
    }
}
