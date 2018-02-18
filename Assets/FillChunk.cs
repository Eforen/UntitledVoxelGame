using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillChunk : MonoBehaviour {
    public GameObject template = null;
    public int X = 16;
    public int Z = 16;
    public int Y = 256;

    public float X_Offset = 1;
    public float Z_Offset = 1;
    public float Y_Offset = 1;

    public string NamePrefix = "Voxel";

    // Use this for initialization
    void Start () {
        if(template == null)
        {
            Debug.LogError("No Template Assigned!", this);
        }
        else
        {
            if (template.activeSelf) template.SetActive(false); //Hide the template so we can see the template in the editor if we want to.

            GameObject go = null;
            for (int x = 0; x < X; x++)
            {
                for (int z = 0; z < Z; z++)
                {
                    for (int y = 0; y < Y; y++)
                    {
                        go = GameObject.Instantiate(template, this.transform);
                        go.name = NamePrefix + " ("+x+","+z+","+y+")";
                        go.transform.localPosition = new Vector3(
                            (X * X_Offset / 2 * -1) + (x * X_Offset), //x
                            (Y * Y_Offset / 2 * -1) + (y * Y_Offset), //y
                            (Z * Z_Offset / 2 * -1) + (z * Z_Offset) //z
                        );
                        go.SetActive(true);
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
