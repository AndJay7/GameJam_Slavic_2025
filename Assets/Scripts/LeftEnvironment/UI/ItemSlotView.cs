using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotView : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _itemBackground;
    [SerializeField]
    private SpriteRenderer _itemIcon;
    [SerializeField]
    private SpriteRenderer _selection;

    public void SetSelection(bool isActive)
    {
        _selection.gameObject.SetActive(isActive);
    }    

    public void SetItem(Item item)
    {
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
