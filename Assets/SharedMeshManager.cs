using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedMeshManager : MonoBehaviour {
    public Mesh[] meshes = new Mesh[64]; // 0011 1111

    public static byte MASK_NORTH = 0x01;
    public static byte MASK_SOUTH = 0x02;
    public static byte MASK_EAST =  0x04;
    public static byte MASK_WEST =  0x08;
    public static byte MASK_UP =    0x10;
    public static byte MASK_DOWN =  0x20;

    public static Vector3 TRANSFORM_NORTH = new Vector3(0, 0, 1);
    public static Vector3 TRANSFORM_SOUTH = new Vector3(0, 0, -1);
    public static Vector3 TRANSFORM_EAST = new Vector3(1, 0, 0);
    public static Vector3 TRANSFORM_WEST = new Vector3(-1, 0, 0);
    public static Vector3 TRANSFORM_UP = new Vector3(0, 1, 0);
    public static Vector3 TRANSFORM_DOWN = new Vector3(0, -1, 0);

    private static void buildFace(ref Vector3[] vertices, ref Vector2[] uv, ref int[] tris, ref int vi, ref int triID, Vector3 OffsetF, Vector3 OffsetR, Vector3 OffsetL, Vector3 OffsetT, Vector3 OffsetB)
    {
        vertices[vi] = OffsetF * 0.5f + (OffsetR * 0.5f) + (OffsetT * 0.5f);
        uv[vi] = new Vector2(0, 1);
        int v0 = vi;
        vi++;
        vertices[vi] = OffsetF * 0.5f + (OffsetL * 0.5f) + (OffsetT * 0.5f);
        uv[vi] = new Vector2(1, 1);
        int v1 = vi;
        vi++;
        vertices[vi] = OffsetF * 0.5f + (OffsetL * 0.5f) + (OffsetB * 0.5f);
        uv[vi] = new Vector2(1, 0);
        int v2 = vi;
        vi++;
        vertices[vi] = OffsetF * 0.5f + (OffsetR * 0.5f) + (OffsetB * 0.5f);
        uv[vi] = new Vector2(0, 0);
        int v3 = vi;
        vi++;
        
        tris[triID] = v0;
        triID++;
        tris[triID] = v1;
        triID++;
        tris[triID] = v2;
        triID++;
        tris[triID] = v0;
        triID++;
        tris[triID] = v2;
        triID++;
        tris[triID] = v3;
        triID++;
    }

    // Use this for initialization
    void Start ()
    {
        Mesh mesh = null;
        
        for (int north = 0; north <= 1; north++)
            for (int south = 0; south <= 1; south++)
                for (int east = 0; east <= 1; east++)
                    for (int west = 0; west <= 1; west++)
                        for (int up = 0; up <= 1; up++)
                            for (int down = 0; down <= 1; down++)
                            {
                                int i = (north == 1 ? MASK_NORTH : 0) + 
                                        (south == 1 ? MASK_SOUTH : 0) + 
                                        (east == 1 ? MASK_EAST : 0) + 
                                        (west == 1 ? MASK_WEST : 0) + 
                                        (up == 1 ? MASK_UP : 0) + 
                                        (down == 1 ? MASK_DOWN : 0);

                                Vector3[] vertices = new Vector3[4 * (north + south + east + west + up + down)];
                                Vector2[] uv = new Vector2[4 * (north + south + east + west + up + down)];
                                int[] tris = new int[(north + south + east + west + up + down) * 6];
                                int triID = 0;

                                int vi = 0;
                                if ((i & MASK_NORTH) > 0)
                                {
                                    buildFace(ref vertices, ref uv, ref tris, ref vi, ref triID, 
                                        TRANSFORM_NORTH, 
                                        TRANSFORM_EAST, TRANSFORM_WEST, 
                                        TRANSFORM_UP, TRANSFORM_DOWN);
                                }
                                if ((i & MASK_SOUTH) > 0)
                                {
                                    buildFace(ref vertices, ref uv, ref tris, ref vi, ref triID,
                                        TRANSFORM_SOUTH,
                                        TRANSFORM_WEST, TRANSFORM_EAST,
                                        TRANSFORM_UP, TRANSFORM_DOWN);
                                }
                                if ((i & MASK_EAST) > 0)
                                {
                                    buildFace(ref vertices, ref uv, ref tris, ref vi, ref triID,
                                        TRANSFORM_EAST,
                                        TRANSFORM_SOUTH, TRANSFORM_NORTH,
                                        TRANSFORM_UP, TRANSFORM_DOWN);
                                }
                                if ((i & MASK_WEST) > 0)
                                {
                                    buildFace(ref vertices, ref uv, ref tris, ref vi, ref triID,
                                        TRANSFORM_WEST,
                                        TRANSFORM_NORTH, TRANSFORM_SOUTH,
                                        TRANSFORM_UP, TRANSFORM_DOWN);
                                }
                                if ((i & MASK_UP) > 0)
                                {
                                    buildFace(ref vertices, ref uv, ref tris, ref vi, ref triID,
                                        TRANSFORM_UP,
                                        TRANSFORM_WEST, TRANSFORM_EAST,
                                        TRANSFORM_NORTH, TRANSFORM_SOUTH);
                                }
                                if ((i & MASK_DOWN) > 0)
                                {
                                    buildFace(ref vertices, ref uv, ref tris, ref vi, ref triID,
                                        TRANSFORM_DOWN,
                                        TRANSFORM_EAST, TRANSFORM_WEST,
                                        TRANSFORM_NORTH, TRANSFORM_SOUTH);
                                }

                                mesh = new Mesh();
                                mesh.vertices = vertices;
                                mesh.uv = uv;
                                mesh.triangles = tris;

                                mesh.RecalculateBounds();
                                mesh.RecalculateNormals();
                                mesh.name = "Voxel #" + i;
                                meshes[i] = mesh;
                            }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
