using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQueueSlotView : MonoBehaviour
{
    [SerializeField]
    private Image _itemBackground;
    [SerializeField]
    private Image _itemIcon;

    private Item _item;

    public Item Item => _item;

    public void SetItem(Item item)
    {
        _item = item;
        _itemIcon.sprite = item?.Icon ?? null;
        Sprite backgroundSprite = null;
        switch (item)
        {
            case Weapon weapon:
                {
                    backgroundSprite = GameManager.Instance.ItemDatabase.WeaponBackground;
                    break;
                }
            case Booster booster:
                {
                    backgroundSprite = GameManager.Instance.ItemDatabase.BoosterBackground;
                    break;
                }
            default:
                break;
        }
        _itemBackground.sprite = backgroundSprite;
    }
}
