using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchArea : MonoBehaviour, IPointerDownHandler
{
    public static Action<Player> changeFocus;
    public static Action<bool> setActive;
    
    public Player player;
    private bool isActive;

    private void Start()
    {
        changeFocus = ChangeFocus;
        setActive = SetActive;
        isActive = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isActive)
            player.Jump();
    }

    private void ChangeFocus(Player player)
    {
        this.player = player;
    }

    private void SetActive(bool isActive)
    {
        this.isActive = isActive;
    }
}
