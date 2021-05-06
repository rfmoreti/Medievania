using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatus : MonoBehaviour
{
    [SerializeField] private PlayerController playerHealth;
    [SerializeField] private Image fillImage;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerHealth.currentHealth;
    }
}
