using UnityEngine;

public static class Save
{
    public static PlayerData playerData { get; private set; }
    

    public static bool isLoad = false;
    public static float saveTimer;
    public const float SAVETIME = 120;
    public static bool isSaving = false;
    public static float saveCoolDown;
    public const float SAVECOOLDOWN = 0.5f;


    public static void SetPlayerData(PlayerData data)
    {
        playerData = data;
    }

    public static void SaveUpdate()
    {
        if (isSaving)
        {
            saveCoolDown += Time.deltaTime;
            if (saveCoolDown >= SAVECOOLDOWN)
            {
                isSaving = false;
            }
        }
        AutoSave();
    }

    private static void AutoSave()
    {
        saveTimer += Time.deltaTime;

        if (saveTimer >= SAVETIME)
        {
            saveTimer = 0;
            SaveData();
        }
    }
    public static void LoadData()
    {
        playerData.LoadData();
    }

    public static void SaveData()
    {
        if (isSaving)
            return;

        playerData.SaveData();
        isSaving = true;
        saveCoolDown = 0f;
    }
}
