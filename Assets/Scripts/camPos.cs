using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camPos : MonoBehaviour
{
    public Transform camPos2;
    void Update()
    {
        transform.position = camPos2.position;
    }
}
