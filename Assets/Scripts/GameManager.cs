using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action respawn;
    public static Action startGame;
    public static Action endGame;
    public static Action backToTitleScreen;
    
    public static bool changeLevel;
    
    [SerializeField] private GameObject player;
    [SerializeField] private CameraMain cameraMain;
    [SerializeField] private CanvasUI canvasUI;
    [SerializeField] private List<GameObject> levels = new List<GameObject>();

    private Queue<GameObject> inactivePlayers = new Queue<GameObject>();
    private Player playerScript;
    private int playerlastZ;
    private float stuckTimePassed;
    private GameObject level;
    private int currentLevel;

    private void Start()
    {
        respawn = Respawn;
        startGame = StartGame;
        endGame = EndGame;
        backToTitleScreen = BackToTitleScreen;
        playerScript = player.GetComponent<Player>();
    }

    private void Respawn()
    {
        inactivePlayers.Enqueue(this.player);
        
        if (changeLevel)
        {
            Destroy(level);
            currentLevel += 1;
            InstantiateLevel(currentLevel);
            canvasUI.RestoreLife();
            changeLevel = false;
            
            while(inactivePlayers.Count > 0)
                Destroy(inactivePlayers.Dequeue());
        }
        else
            canvasUI.DeductLife();
        
        Transform playerInstance = Instantiate(this.player, Vector3.zero, Quaternion.identity).transform;
        Player player = playerInstance.GetComponent<Player>();
        player.enabled = true;
        player.Freeze();
        cameraMain.ChangeFocus(player);
        TouchArea.changeFocus.Invoke(player);
        this.player = player.gameObject;
        playerScript = player;
    }

    public void ResetPlayer()
    {
        playerScript.Reset();
        canvasUI.HideStuckPanel();
    }

    private void InstantiateLevel(int levelIdx)
    {
        level = Instantiate(levels[levelIdx], levels[levelIdx].transform.position, Quaternion.identity);
    }

    private void Update()
    {
        CheckIfStuck();
    }

    private void CheckIfStuck()
    {
        stuckTimePassed += Time.deltaTime;

        if (!playerScript.IsActive)
            return;

        if (stuckTimePassed > 1)
        {
            int z = (int) player.transform.position.z;
            if (playerlastZ == z)
                canvasUI.ShowStuckPanel();
            
            playerlastZ = z;
            stuckTimePassed = 0;
        }
    }

    private void StartGame()
    {
        currentLevel = 0;
        InstantiateLevel(currentLevel);
        playerScript.IsActive = true;
        playerScript.Unfreeze();
        canvasUI.ShowMenuBar();
        canvasUI.RestoreLife();
        stuckTimePassed = 0;
    }

    private void EndGame()
    {
        canvasUI.ShowGameOver();
    }

    private void BackToTitleScreen()
    {
        canvasUI.ShowTitleScreen();
    }

}
