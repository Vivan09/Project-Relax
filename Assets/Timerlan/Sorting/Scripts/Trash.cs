using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType
{
    Organic, Glass, Plastic, Paper
}

public class Trash : MonoBehaviour
{
    public TrashType colorType;
}