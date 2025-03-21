using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor.Experimental;
using UnityEngine;

[CreateAssetMenu(fileName = "CSVSO", menuName = "Data/CSVSO")]

public class ReadCSV : SerializedScriptableObject
{
    const string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRYxPviAE9-hb7RtwYhWAxkzYNoJYons_bucFZLuOr-t5FHM8ydPAXVkuIFM3-sQaur6W18LZ_Nnn3n/pub?output=csv";
    [field: SerializeField] public Dictionary<int, TemplateCSV> csvDic { get; private set; }

#if UNITY_EDITOR
    [Button()]
    void LoadData()
    {
        csvDic = new Dictionary<int, TemplateCSV>();
        System.Action<string> readAction = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            for (int i = 2; i < data.Count; i++) // 2: start at row 
            {
                var _data = data[i];
                if (!string.IsNullOrEmpty(_data[0]))
                {
                    // data[row][collumn]
                    // _data[collumn]
                    ReadData(_data[0], _data[1], _data[2], _data[3], _data[4],
                        _data[5], _data[6], _data[7], _data[8], _data[9],
                        _data[10], _data[11]);
                }
            }
            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(IELoadData(url, readAction));
    }
    void ReadData(string stringNum, string stringValue, string poolType, string goType, string hp, string damage,string mSpeed,
        string atkSpeed, string atkRange, string mana, string cooldown, string sight)
    {
        int index = 0;
        int key = ParseInt(stringNum);
        float hpValue = ParseFloat(hp);
        float damageValue = ParseFloat(damage);
        float mSpeedValue = ParseFloat(mSpeed);
        float atkSpeedValue = ParseFloat(atkSpeed);
        float atkRangeValue = ParseFloat(atkRange);
        float manaValue = ParseFloat(mana);
        float cooldownValue = ParseFloat(cooldown);
        float sightValue = ParseFloat(sight);
        Sprite[] spriteTemplate = Resources.LoadAll<Sprite>("Sprite/");
        if (!csvDic.ContainsKey(key))
        {
            //csvDic.Add(key, new TemplateCSV(key, stringValue, poolType, goType, hpValue, damageValue,
            //           mSpeedValue, atkSpeedValue, atkRangeValue, manaValue, cooldownValue, sightValue, spriteTemplate[index]));
            //index++;
            for(int i = 0; i < 14; i++)
            {
                csvDic.Add(key, new TemplateCSV(key, stringValue, poolType, goType, hpValue, damageValue,
                      mSpeedValue, atkSpeedValue, atkRangeValue, manaValue, cooldownValue, sightValue));
            }
            //for (int i = 0; i < spriteTemplate.Length; i++)
            //{
            //    if (key == ParseInt(spriteTemplate[i].name))
            //    {
            //        csvDic.Add(key, new TemplateCSV(spriteTemplate[i]));
            //    }
            //}
        }
        
    }
#endif

    #region Utility
    public IEnumerator IELoadData(string urlData, System.Action<string> actionComplete, bool showAlert = false)
    {
        var www = new WWW(urlData);
        float time = 0;
        //TextAsset fileCsvLevel = null;
        while (!www.isDone)
        {
            time += 0.001f;
            if (time > 10000)
            {
                yield return null;
                Debug.Log("Downloading...");
                time = 0;
            }
        }
        if (!string.IsNullOrEmpty(www.error))
        {
            UnityEditor.EditorUtility.DisplayDialog("Notice", "Load CSV Fail", "OK");
            yield break;
        }
        yield return null;
        actionComplete?.Invoke(www.text);
        yield return null;
        UnityEditor.AssetDatabase.SaveAssets();
        if (showAlert)
            UnityEditor.EditorUtility.DisplayDialog("Notice", "Load Data Success", "OK");
        else
            Debug.Log("<color=yellow>Download Data Complete</color>");
    }
    public float ParseFloat(string value)
    {
        if (value.Contains(",")) value = value.Replace(",", "");
        if (value.Contains("%")) value = value.Replace("%", "");
        float tmpFloat = float.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat);
        return tmpFloat;
    }
    public int ParseInt(string data)
    {
        if (data.Contains(",")) data = data.Replace(",", "");
        int val = 0;
        if (int.TryParse(data, out val))
            return val;
        Debug.LogError("Wrong Input " + data);
        return val;
    }
    public bool ParseBool(string data)
    {
        bool val = false;
        if (bool.TryParse(data, out val))
            return val;
        Debug.LogError("Wrong Input" + data);
        return val;
    }
    #endregion
}

public class TemplateCSV
{
    private int id;
    private string name;
    private string poolType;
    private string goType;
    private float hp;
    private float damage;
    private float moveSpeed;
    private float atkSpeed;
    private float mana;
    private float cooldownSpawn;
    private float sight;
    private float atkRange;
    [PreviewField] public Sprite sprite;

    public int ID => id;
    public string Name => name;
    public string PoolTypeObject => poolType;
    public string GOType => goType;
    public float HP => hp;
    public float Damage => damage;
    public float MoveSpeed => moveSpeed;
    public float AtkSpeed => atkSpeed;
    public float CooldownSpawn => cooldownSpawn;
    public float Sight => sight;
    public float AtkRange => atkRange;
    public float Mana => mana;

    public TemplateCSV(int num, string value, string poolType, string goType, float hp, float damage, float moveSpeed, 
        float atkSpeed, float atkRange, float mana, float cooldown, float sight)
    {
        this.id = num;
        this.name = value;
        this.poolType = poolType;
        this.goType = goType;
        this.hp = hp;
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.atkSpeed = atkSpeed;
        this.atkRange = atkRange;
        this.mana = mana;
        this.cooldownSpawn = cooldown;
        this.sight = sight;
        //this.sprite = sprite;
    }
    public TemplateCSV(Sprite sprite)
    {
        this.sprite = sprite;
    }
}


