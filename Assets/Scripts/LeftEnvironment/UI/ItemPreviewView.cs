using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ItemPreviewView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _description;

    private void Start()
    {
        UpdateDescription(GameManager.Instance.Equipment.FocusIndex);
        GameManager.Instance.Equipment.OnFocusChanged += UpdateDescription;
        GameManager.Instance.Equipment.OnItemAdded += UpdateDescription;
    }

    private void UpdateDescription(Vector2Int obj)
    {
        var slot = GameManager.Instance.Equipment.GetSlot(obj);
        var description = string.Empty;

        if(slot.item != null)
        {
            var text = $"Name: {slot.item.Name}\n";
            text += $"Description: {slot.item.Description}\n";

            var additionalDesc = string.Empty;
            var neighbours = GameManager.Instance.Equipment.GetNeighbourIndexes(obj);
            foreach (var n in neighbours)
            {
                var neighbourSlot = GameManager.Instance.Equipment.GetSlot(n);
                if (neighbourSlot.item is Weapon weapon)
                    additionalDesc += $"-{weapon.Name}\n";
            }

            if (!string.IsNullOrEmpty(additionalDesc))
            {
                text += $"Boosted weapons: \n";
                text += additionalDesc;
            }

            description = text;
        }
        _description.text = description;
    }
}
