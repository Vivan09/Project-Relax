using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public TrashType binColorType;
    public GameObject numberPrefab;
    public TMP_Text scoreText;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Trash>().colorType == binColorType)
        {
            GameObject clone = Instantiate(numberPrefab, 
            other.transform.position, Quaternion.identity, gameObject.transform);
            Destroy(other.gameObject);
            StartCoroutine(DestroyNumber(clone));
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

    IEnumerator DestroyNumber(GameObject number)
    {
        yield return new WaitForSeconds(3.3f);
        Destroy(number);
        int result = int.Parse(scoreText.text);
        result++;
        scoreText.text = result.ToString();
    }
}
