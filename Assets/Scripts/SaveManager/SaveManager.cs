using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUNGAS.Core.Singleton;
using Cloth;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;

    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Bungas";
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }

    #region SAVE
    [NaughtyAttributes.Button]
    private void Save()
    {

        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveItens()
    {
        _saveSetup.coins = Itens.ItemManager.Instance.GetItemByType(Itens.ItemType.COIN).soInt.value;
        _saveSetup.health = Itens.ItemManager.Instance.GetItemByType(Itens.ItemType.LIFE_PACK).soInt.value;
        Save();
    }

    public void SaveOthers()
    {
        _saveSetup.checkPointKey = CheckPointManager.Instance.lastCheckPointKey;
        Debug.Log("Salvando checkPointKey: " + _saveSetup.checkPointKey);

        _saveSetup.life = Player.Instance.healthBase.currentLife;
        Debug.Log("Salvando life: " + _saveSetup.life);

        _saveSetup.playerCloth = Player.Instance._clothChanger.texture;
        Debug.Log("Salvando cloth: " + _saveSetup.playerCloth);

        Save();
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItens();
        SaveOthers();
        Save();
    }
    #endregion

    private void SaveFile(string json)
    {
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    private void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }

        FileLoaded.Invoke(_saveSetup);
    }


    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }

    public void InitializePlayerLife()
    {
        if (_saveSetup.life > 0)
        {
            Player.Instance.healthBase.currentLife = _saveSetup.life;
        }
        else
        {
            Player.Instance.healthBase.ResetLife();
        }
    }

    public void InitializePlayerCheckPoint()
    {
        if (_saveSetup.checkPointKey >= 0)
        {
            CheckPointManager.Instance.lastCheckPointKey = _saveSetup.checkPointKey;
        }
        else
        {
            CheckPointManager.Instance.lastCheckPointKey = 0;
        }
    }

    public void InitializePlayerCloth()
    {
        {
            Player.Instance._clothChanger.texture = _saveSetup.playerCloth;
        }
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public float coins;
    public float health;
    public int checkPointKey;
    public Texture2D playerCloth;
    public float life;

    public string playerName;
}
