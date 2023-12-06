using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFix : MonoBehaviour
{
    public BoxCollider2D col;
    public SpriteRenderer spr;

    void Start()
    {
        col.size = spr.size; // This is such a bandaid fix
    }
}
