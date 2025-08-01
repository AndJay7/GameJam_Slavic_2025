using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private ItemDatabaseSO _itemsDatabase;

    public ItemsQueue ItemsQueue { get; } = new ItemsQueue();
    public Equipment Equipment { get; } = new Equipment();
}
