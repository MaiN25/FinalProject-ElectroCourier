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
    }

    // When the user chooses to play a new game and picks a saveslot to override, the saveslot is updated to match the one chosen by the user, the first level is loaded,
    // a new save object is created and data is saved to the proper save file. Score is reset to zero
    public void NewGame(int saveNum)
    {
        saveSlot = saveNum;
        SceneManager.LoadScene("Level");
        playerSave = new PlayerSave
        {
            packages = 0,
            room = "Level",
            playerPosition = new float[] { -0.08f, -2.66f, 0 },
            health = 1,
            score = 0
        };
        string jsonSave = JsonUtility.ToJson(playerSave);
        Debug.Log("Overriding Save " + saveSlot);
        File.WriteAllText(Application.persistentDataPath + "/SaveData_SaveSlot" + saveSlot + ".json", jsonSave);
        ScoreDisplay.Reset();
    }

    // When the user chooses to load a save file, their chosen save slot is saved to the variable, information is loaded from the file, the player's previous room is loaded, player objects are found and the player info is updated
    // The player movement is told it's in the first room, so it won't override the player's saved position
    public void StartLoad(int saveNum)
    {
        saveSlot = saveNum;
        LoadSave();
        SceneManager.LoadScene(playerSave.room);
        UpdateUsedObjects(GameObject.FindObjectOfType<PlayerComponentFinder>());
        SetPlayerFromSave(true);
        PlayerComponentFinder.isFirstRoom = true;
        pcf.gm.ChangeHealthBar();
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
            health = Game_Manager.currentHealth,
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
            Debug.Log("Player Save Null");
            LoadSave();
        }
        ic.packages = playerSave.packages;
        ic.UpdatePackageUI();
        ic.LogPackages();

        if (isFullReset)
        {
            player.transform.position = new Vector3(playerSave.playerPosition[0], playerSave.playerPosition[1], playerSave.playerPosition[2]);
        }
        pcf.pm.rb.velocity = new Vector2(0, 0);
        Game_Manager.currentHealth = playerSave.health;
        pcf.gm.ChangeHealthBar();
        ScoreDisplay.score = playerSave.score;
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
            SaveGame();
    }

    public void Respawn() {
            LoadSave();
            SetPlayerFromSave(true);
    }
    

    // The player objects are found so saving and loading functions don't fail if you move through rooms
    public void UpdateUsedObjects(PlayerComponentFinder playerCF)
    {
        player = playerCF.gameObject;
        pcf = playerCF;
        ic = player.GetComponent<ItemCollection>();
        Debug.Log("Player: " + player.name + ", Component: " + pcf + ", ItemCollection: " + ic + ", Movement: " + pcf.pm);
    }
    //Loads a level according to the name provided
    public void LoadLevelByName(string levelToLoadName)
    {
        SceneManager.LoadScene(levelToLoadName);
        Game_Manager.ResetGamePlayerPrefs();
    }
}
