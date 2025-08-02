using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TMP_Text TextComponent;


    public void DestroyMe() { Destroy(gameObject); }


    void Start()
    {
        TextComponent = GetComponent<TMP_Text>();
        
    }

    public void Number(float damage)
    {
        TextComponent.text = damage.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
