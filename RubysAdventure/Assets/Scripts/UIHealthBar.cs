using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set;}
    Image healthBar;

    void Awake()
    {
        instance = this;        
    }
    void Start()
    {
    healthBar = gameObject.GetComponent<Image>();
    SetValue(1, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetValue (int currentHealth, int maxHealth) {
        healthBar.fillAmount = currentHealth / (float) maxHealth;

    return;
    }
    
}
