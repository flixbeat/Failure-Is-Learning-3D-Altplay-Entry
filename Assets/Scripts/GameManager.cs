using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action startGame;
    public static Action endGame;
    public static Action<int> loadGame;
    public static Action respawn;

    public static Action toTitleScreen;
    
    public static Action playZoomSFX;
    public static Action resumeBGM;
    public static Action stopBGM;
    
    public static bool changeLevel;
    
    [SerializeField] private GameObject player;
    [SerializeField] private CameraMain cameraMain;
    [SerializeField] private CanvasUI canvasUI;
    [SerializeField] private List<GameObject> levels = new List<GameObject>();
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource zoom;
    
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
        loadGame = LoadGame;
        toTitleScreen = BackToTitleScreen;
        playZoomSFX = PlayZoomSFX;
        resumeBGM = ResumeBGM;
        stopBGM = StopBGM;
        playerScript = player.GetComponent<Player>();
        SpawnCurrentLevel();
    }

    private void Respawn()
    {
        inactivePlayers.Enqueue(this.player);
        
        // when changing level
        if (changeLevel)
        {
            Destroy(level);
            currentLevel += 1;
            InstantiateLevel(currentLevel);
            canvasUI.RestoreLife();
            changeLevel = false;
            SaveLevel();
            
            while(inactivePlayers.Count > 0)
                Destroy(inactivePlayers.Dequeue());
        }
        else
            canvasUI.DeductLife();
        
        // respawn
        Transform playerInstance = Instantiate(this.player, Player.SPAWN_POINT, Quaternion.identity).transform;
        Player player = playerInstance.GetComponent<Player>();
        player.Freeze();
        cameraMain.ChangeFocus(player);
        TouchArea.changeFocus.Invoke(player);
        this.player = player.gameObject;
        playerScript = player;
    }

    public void ResetPlayer()
    {
        playerScript.Reset();
        playerScript.Unfreeze();
        canvasUI.HideStuckPanel();
    }

    public void RestartLevel()
    {
        canvasUI.DisplayRestartButton(false);
        
        playerScript.Freeze();
        inactivePlayers.Enqueue(player);
        while (inactivePlayers.Count > 0)
            Destroy(inactivePlayers.Dequeue());
        
        canvasUI.RestoreLife();
        Respawn();
        ResetPlayer();
        StartCoroutine(ShowRestartButton());

        IEnumerator ShowRestartButton()
        {
            yield return new WaitForSeconds(0.5f);
            canvasUI.DisplayRestartButton(true);
        }
    }

    private void InstantiateLevel(int levelIdx)
    {
        // show finish screen when last level reached
        if (levelIdx > levels.Count-1)
        {
            canvasUI.ShowFinishScreen();
            return;
        }
        
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

        // check if z position is not moving
        if (stuckTimePassed > 1f)
        {
            int z = (int) player.transform.position.z;
            if (playerlastZ == z)
            {
                playerScript.Freeze();
                canvasUI.ShowStuckPanel();
            }
            
            playerlastZ = z;
            stuckTimePassed = 0;
        }
    }

    private void SpawnCurrentLevel()
    {
        currentLevel = PlayerPrefs.HasKey("level") ? PlayerPrefs.GetInt("level") : 0; 
        InstantiateLevel(currentLevel);
    }

    private void StartGame()
    {
        SaveLevel();
        Begin();
    }

    private void LoadGame(int level)
    {
        Destroy(this.level);
        currentLevel = level;
        InstantiateLevel(currentLevel);
        Begin();
    }

    private void Begin()
    {
        playerScript.IsActive = true;
        playerScript.Unfreeze();
        canvasUI.ShowMenuBar();
        canvasUI.RestoreLife();
        stuckTimePassed = 0;
        canvasUI.HideTitleScreen();
    }

    private void EndGame()
    {
        canvasUI.ShowGameOver();
    }

    private void SaveLevel()
    {
        // prevent saving level higher than actual number of levels
        if (currentLevel >= levels.Count)
            return;
        
        // get save file
        int latestLevel = PlayerPrefs.GetInt("level");

        // save only when current level is higher than stored level, or if there's no save file
        if (currentLevel >= latestLevel || !PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", currentLevel);
            PlayerPrefs.Save();
        }
    }

    private void BackToTitleScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayZoomSFX()
    {
        bgm.Stop();
        zoom.Play();
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void ResumeBGM()
    {
        bgm.Play();
    }
}
