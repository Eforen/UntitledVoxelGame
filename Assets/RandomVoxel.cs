using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class RandomVoxel : MonoBehaviour {
    public SharedMeshManager meshes;
    public MeshFilter mesh = null;
    //public float waitTime;

	// Use this for initialization
	void Start () {
        if (mesh == null) this.mesh = GetComponent<MeshFilter>();
        this.mesh.sharedMesh = meshes.meshes[Random.Range(0, meshes.meshes.Length)];
	}

    float counter = 0;
	// Update is called once per frame
	void Update () {
        if(counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            this.mesh.sharedMesh = meshes.meshes[Random.Range(0, meshes.meshes.Length)];
            counter = Random.Range(0.5f, 5f);
        }
    }
}
