using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName ="ItemDatabase_SO",menuName ="Game/Items/Database")]
public class ItemDatabaseSO : ScriptableObject
{
    [SerializeField]
    private Sprite _weaponBackground;
    [SerializeField]
    private Sprite _boosterBackground;
    [SerializeField]
    private List<ItemSO> _list = new List<ItemSO>();

    public Sprite WeaponBackground => _weaponBackground;
    public Sprite BoosterBackground => _boosterBackground;

    private List<Item> _listCache = null;

    public List<Item> GetItems()
    {
        if (_listCache == null)
            _listCache = _list.Select(i => i.Item).ToList();
        return _listCache;
    }
}
