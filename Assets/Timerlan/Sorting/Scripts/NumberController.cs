using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NumberController : MonoBehaviour
{
    private Animation animation;
    public float speed;

    private void Start()
    {
        animation = GetComponent<Animation>();
    }

    private void FixedUpdate()
    {
        var direction = new Vector3(0, 1f, 0);
        transform.Translate(direction.normalized * speed);
    }
}
