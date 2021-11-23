using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchArea : MonoBehaviour, IPointerDownHandler
{
    public static Action<Player> changeFocus;
    
    public Player player;

    private void Start()
    {
        changeFocus = ChangeFocus;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        player.Jump();
    }

    private void ChangeFocus(Player player)
    {
        this.player = player;
    }
}
