using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager singleton { get; private set; }
    string filePath;


    private void Awake()
    {
        singleton = this;
    }
    private void Start()
    {
        filePath = Application.dataPath + "/save.gamesave";
    }


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(filePath, FileMode.Create);
        PlayerSave save = new PlayerSave();
        save.Record();
        bf.Serialize(file, save);
        file.Close();
        print("Successful save!");
    }

    public void Load()
    {
        if (!File.Exists(filePath)) return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(filePath, FileMode.Open);
        PlayerSave save = (PlayerSave)bf.Deserialize(file);
        save.Initial();
        file.Close();
        print("Successful load!");
    }

}
