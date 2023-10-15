using TMPro;
using UnityEngine;

public class TrigerAge : MonoBehaviour
{
    public string textAge;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.GetComponent<TMP_Text>() != null)
            {
                textAge = collision.GetComponent<TMP_Text>().text;
            }
        }
    }
}
