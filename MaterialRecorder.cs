using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialRecorder : MonoBehaviour
{
    public new GameObject gameObject;
    public static Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        materials = gameObject.GetComponent<MeshRenderer>().materials;
        Debug.Log("Materials Length is " + materials.Length);
    }
    public static int DEVICENORMAL = 0;
    public static int DEVICESELECTED = 1;

}
