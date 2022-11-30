using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveLoadData : MonoBehaviour
{
    public static SaveLoadData instance;

    // Player objects that need to be used for loading data, called by several functions and the player itself so it needs to be public
    public GameObject player;
    public ItemCollection ic;
    public PlayerComponentFinder pcf;


    public static float[] tempSaves;
    string DefaultLevel = "FinLevel1";
    string TestLevel = "Level";

    private SoundControl sc;

    // The currently selected saveslot, allows for saving and loading to the same file
    int saveSlot = 1;
    public PlayerSave playerSave;

    // SaveLoadData is carried through each level for continuity
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        sc = FindObjectOfType<SoundControl>();
    }

    // When the user chooses to play a new game and picks a saveslot to override, the saveslot is updated to match the one chosen by the user, the first level is loaded,
    // a new save object is created and data is saved to the proper save file. Score is reset to zero
    public void NewGame(int saveNum)
    {
        Time.timeScale = 1;
        saveSlot = saveNum;
        SceneManager.LoadScene(DefaultLevel);
        playerSave = new PlayerSave
        {
            packages = 0,
            room = DefaultLevel,
            playerPosition = new float[] { -13.03f, -3.9f, 0 },
            health = 1,
            score = 0
        };
        tempSaves = new float[] { -999, -999, -999 };
        string jsonSave = JsonUtility.ToJson(playerSave);
        File.WriteAllText(Application.persistentDataPath + "/SaveData_SaveSlot" + saveSlot + ".json", jsonSave);
        ScoreDisplay.Reset();
        sc.PlayBackSFX();
    }

    // When the user chooses to load a save file, their chosen save slot is saved to the variable, information is loaded from the file, the player's previous room is loaded, player objects are found and the player info is updated
    // The player movement is told it's in the first room, so it won't override the player's saved position
    public void StartLoad(int saveNum)
    {
        Time.timeScale = 1;
        saveSlot = saveNum;
        PlayerComponentFinder.isFirstRoom = true;
        tempSaves = new float[] { -999, -999, -999 };
        LoadSave();
        Debug.Log(playerSave.room + " " + playerSave.room.GetType());
        SceneManager.LoadScene(playerSave.room);
        UpdateUsedObjects(GameObject.FindObjectOfType<PlayerComponentFinder>());
        SetPlayerFromSave(true);
        pcf.gm.ChangeHealthBar();
        sc.PlayBackSFX();
    }

    // When the game is saved through exiting or by using the debug key Right Ctrl, the code ensures it knows the player objects and saves various player data to the save file
    public void SaveGame()
    {
        if ((player == null) || (pcf == null) || (ic == null) || (pcf.pm == null))
        {
            UpdateUsedObjects(GameObject.FindObjectOfType<PlayerComponentFinder>());
        }

        Vector3 playerPos = player.transform.position;
        playerSave = new PlayerSave
        {
            packages = ic.packages,
            room = SceneManager.GetActiveScene().name,
            playerPosition = new float[] { playerPos.x, playerPos.y, playerPos.z },
            health = Game_Manager.instance.currentHealth,
            score = ScoreDisplay.score
        };
        string jsonSave = JsonUtility.ToJson(playerSave);
        File.WriteAllText(Application.persistentDataPath + "/SaveData_SaveSlot" + saveSlot + ".json", jsonSave);
    }

    // When data needs to be loaded for the first time or updated throughout the game, it accesses the save file and converts data to the playersave object
    public void LoadSave()
    {
        string saveString = File.ReadAllText(Application.persistentDataPath + "/SaveData_SaveSlot" + saveSlot + ".json");
        playerSave = JsonUtility.FromJson<PlayerSave>(saveString);
    }

    // When player information needs to be updated to show on screen (other than current room), in-game variables and ui elements are changed to reflect the save data
    // This is used to keep continuity between rooms and for loading saves
    public void SetPlayerFromSave(bool isFullReset)
    {
        if ((player == null) || (pcf == null) || (ic == null) || (pcf.pm == null))
        {
            UpdateUsedObjects(GameObject.FindObjectOfType<PlayerComponentFinder>());
        }
        if (playerSave == null)
        {
            Debug.Log("Finding Player Save");
            LoadSave();
        }
        if (tempSaves[0] == -999)
        {
            Game_Manager.instance.currentHealth = playerSave.health;
            ic.packages = playerSave.packages;
            ScoreDisplay.score = playerSave.score;
        } else
        {
            Game_Manager.instance.currentHealth = tempSaves[0];
            ic.packages = Mathf.RoundToInt(tempSaves[1]);
            ScoreDisplay.score = tempSaves[2];
        }
        if (isFullReset)
        {
            player.transform.position = new Vector3(playerSave.playerPosition[0], playerSave.playerPosition[1], playerSave.playerPosition[2]);
        }
        pcf.pm.rb.velocity = new Vector2(0, 0);
        //Update UI to match changes
        pcf.gm.ChangeHealthBar();
        ic.UpdatePackageUI();
    }

    // The player's save data is stored within a special class object
    public class PlayerSave
    {
        public int packages;
        public string room;
        public float[] playerPosition;
        public float health;
        public float score;
    }
    public void CheckpointSave()
    {
        Debug.Log("Saving Game!");
        SaveGame();
    }
    public void TempSave()
    {
        // Health, Packages, Score
        tempSaves = new float[] {Game_Manager.instance.currentHealth, ic.packages, ScoreDisplay.score};
        Debug.Log("Saving Temp: " + tempSaves[0] + ", " + tempSaves[1] + ", " + tempSaves[2]);
    }


    public void Respawn()
    {
        LoadSave();
        SetPlayerFromSave(true);
    }
    
    public void DeleteTemps()
    {
        tempSaves = new float[] { -999, -999, -999 };
        Debug.Log("Deleted Temps: " + tempSaves[0] + ", " + tempSaves[1] + ", " + tempSaves[2]);
    }

    // The player objects are found so saving and loading functions don't fail if you move through rooms
    public void UpdateUsedObjects(PlayerComponentFinder playerCF)
    {
        player = playerCF.gameObject;
        pcf = playerCF;
        ic = player.GetComponent<ItemCollection>();
        Debug.Log("Player: " + player.name + ", Component: " + pcf + ", ItemCollection: " + ic + ", Movement: " + pcf.pm);
    }
}
