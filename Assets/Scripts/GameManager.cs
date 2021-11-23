using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action respawn;
    public static bool changeLevel;
    
    [SerializeField] private GameObject player;
    [SerializeField] private CameraMain cameraMain;
    [SerializeField] private CanvasUI canvasUI;
    [SerializeField] private List<GameObject> levels = new List<GameObject>();

    private Queue<GameObject> inactivePlayers = new Queue<GameObject>();
    private int playerlastZ;
    private float stuckTimePassed;
    private GameObject level;
    private int currentLevel;
    
    private void Start()
    {
        respawn = Respawn;
        InstantiateLevel();
    }

    private void Respawn()
    {
        inactivePlayers.Enqueue(this.player);
        
        if (changeLevel)
        {
            Destroy(level);
            currentLevel++;
            InstantiateLevel();
            changeLevel = false;

            while(inactivePlayers.Count > 0)
                Destroy(inactivePlayers.Dequeue());
        }
        
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

    private void InstantiateLevel()
    {
        level = Instantiate(levels[currentLevel], levels[currentLevel].transform.position, Quaternion.identity);
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
