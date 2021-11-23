using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action respawn;
    
    [SerializeField] private GameObject player;
    [SerializeField] private CameraMain cameraMain;
    [SerializeField] private CanvasUI canvasUI;

    private int playerlastZ;
    private float stuckTimePassed;
    
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
        TouchArea.changeFocus.Invoke(player);
        this.player = player.gameObject;
    }

    public void ResetPlayer()
    {
        Player player = this.player.GetComponent<Player>();
        player.Reset();
        canvasUI.HideStuckPanel();
    }

    private void Update()
    {
        CheckIfStuck();
    }

    private void CheckIfStuck()
    {
        stuckTimePassed += Time.deltaTime;
        
        if (stuckTimePassed > 1)
        {
            Player player = this.player.GetComponent<Player>();
            if (!player.isActive) return;
            
            int z = (int) this.player.transform.position.z;
            if (playerlastZ == z)
                canvasUI.ShowStuckPanel();
            
            playerlastZ = z;
            stuckTimePassed = 0;
        }
    }
}
