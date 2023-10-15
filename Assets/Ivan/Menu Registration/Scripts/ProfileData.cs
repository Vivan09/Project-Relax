using DataManagement;
using System;
using System.IO;
using UnityEngine;

public class ProfileData
{
    private string Name;
    private string Age;
    private int ColorsAva;
    private bool Gender; // false = boy
    private bool IsRegistartion;

    private static ProfileData instance;

    private DataManagementSystem<ProfileData> dataManagementSystemProfile;

    public ProfileData(string _name, bool _gender, string _age)
    {
        Name = _name;
        Gender = _gender;
        Age = _age;
    }

    public static ProfileData GetInstance()
    {
        if (instance == null)
        {
             instance = new ProfileData("", false, 0.ToString());
        }
        return instance;
    }

    #region MethodsData

    public void SetName(string _name)
    {
        Name = _name;
        SaveData();
    }

    public string GetName()
    {
        return Name;
    }

    public void SetGender(bool _gender)
    {
        Gender = _gender;
        SaveData();
    }

    public bool GetGender()
    {
        return Gender;
    }

    public void SetAge(string _age)
    {
        Age = _age;
        SaveData();
    }

    public string GetAge()
    {
        return Age;
    }

    public void SetColorsAva(int _color)
    {
        ColorsAva = _color;
        SaveData();
    }

    public int GetColors()
    {
        return ColorsAva;
    }

    public void SetIsRegistration(bool _isRegistration)
    {
        IsRegistartion = _isRegistration;
        SaveData();
    }

    public bool GetRegistration()
    {
        return IsRegistartion;
    }

    #endregion

    public void SaveData()
    {
        DataProfile data = new DataProfile(Name, Age, ColorsAva, Gender, IsRegistartion);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/saveProfileData.json", json);
    }

    public void LoadData()
    {
        string json = File.ReadAllText(Application.dataPath + "/saveProfileData.json");

        if (json != null)
        {
            DataProfile data = JsonUtility.FromJson<DataProfile>(json);

            Name = data.name;
            Age = data.age;
            ColorsAva = data.colorsAva;
            Gender = data.gender;
            IsRegistartion = data.isRegistration;
        }
    }
}

[Serializable]
public class DataProfile
{
    [SerializeField] public string name;
    [SerializeField] public string age;
    [SerializeField] public int colorsAva;
    [SerializeField] public bool gender;
    [SerializeField] public bool isRegistration;

    public DataProfile(string _name, string _age, int _colorsAva, bool _gender, bool _isRegistration)
    {
        name = _name;
        age = _age;
        colorsAva = _colorsAva;
        gender = _gender;
        isRegistration = _isRegistration;
    }
}
