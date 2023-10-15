using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasketDrag : MonoBehaviour, IDragHandler
{
    [SerializeField] private ManagerUI managerUI;

    private Animator anim;

    private int limitScore= 5;
    private int animIndex =1;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector2(Input.mousePosition.x, transform.position.y);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, 0, Screen.width), transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Apple")){
            Destroy(other.gameObject);
            managerUI.AddScore(1);
        }
        else if (other.gameObject.CompareTag("Leaf")){
            Destroy(other.gameObject);
            managerUI.AddScore(-1);
        }
    }

    private void Update() {
        if(managerUI.score> limitScore){
            anim.SetFloat("animBasket",animIndex);
            limitScore += limitScore;
            animIndex +=1;
        }
    }

}
