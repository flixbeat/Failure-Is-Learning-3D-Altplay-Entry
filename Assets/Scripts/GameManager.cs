using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action respawn;
    
    [SerializeField] private GameObject player;
    [SerializeField] private CameraMain cameraMain;

    private void Start()
    {
        respawn = Respawn;
    }

    private void Respawn()
    {
        Transform playerInstance = Instantiate(this.player, Vector3.zero, Quaternion.identity).transform;
        Player player = playerInstance.GetComponent<Player>();
        player.enabled = true;
        player.Freeze();
        cameraMain.ChangeFocus(player);
    }
}
