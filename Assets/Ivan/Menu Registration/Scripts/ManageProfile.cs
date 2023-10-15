using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageProfile : MonoBehaviour
{
    public ProfileData profileData;

    [SerializeField] private TMP_InputField inputFieldName;
    [SerializeField] private bool checkingRegistration = false;

    private void Awake()
    {
        profileData = ProfileData.GetInstance();
        profileData.LoadData();

        if(profileData != null)
        {
            if (profileData.GetRegistration() == true && checkingRegistration)
            {
                NextRegistration("Menu");
            }
        }

        if (inputFieldName != null)
        {
            GetName();
        }
    }

    public void ChangeName()
    {
        if(profileData != null)
            profileData.SetName(inputFieldName.text);
        Debug.Log("Change");
    }

    public void GetName()
    {
        if(profileData != null)
            inputFieldName.text = profileData.GetName();
    }

    public void ChangeColor(int color)
    {
        if (profileData != null)
            profileData.SetColorsAva(color);
        Debug.Log("Change Color");
    }

    public int GetColor()
    {
        if (profileData != null)
            return profileData.GetColors();
        else
            return 0;
    }

    public void ChangeGender(bool gender) // boy = false
    {
        if (profileData != null)
            profileData.SetGender(gender);
    }

    public void ChangeAge(TrigerAge trigerAge)
    {
        if (trigerAge.textAge != "")
        {
            profileData.SetAge(trigerAge.textAge);
        }
    }

    public void NextRegistration(string name)
    {
        profileData.SetIsRegistration(true);
        SceneManager.LoadScene(name);
    }
}
