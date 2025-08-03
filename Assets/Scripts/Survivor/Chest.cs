using System;
using System.Collections;
using System.Collections.Generic;
using Survivor;
using UnityEngine;
using UnityEngine.Playables;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _pickupTL;
    [SerializeField]
    private AudioSource _pickItemAudio;

    private bool _pickedUp = false;
    private Item _item;
    
    private void Start()
    {
        _item = ItemPool.Instance.Pick();
        
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.01f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_pickedUp)
            return;
        
        var player = other.gameObject.GetComponent<PlayerMovement>();
        
        if(player == null)
            return;
        
        _pickupTL.Play();

        _pickedUp = true;
    }

    public void FinishPickup()
    {
        GameManager.Instance.ItemsQueue.Enqueue(_item);
        _pickItemAudio.Play();
        Destroy(gameObject);
    }
}
