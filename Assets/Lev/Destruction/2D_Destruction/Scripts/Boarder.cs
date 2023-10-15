using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Destruction
{
    public class Boarder : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other) => Destroy(other.gameObject);
    }
}