using UnityEngine;
using UnityEditor;
using System.Collections;

class BuildingTools : EditorWindow {
    private GameObject selectedObject;
    private float snapIntervial = 0.1f;

    [MenuItem ("Tools/SBTools/BuildingTools")]
    public static void  ShowWindow () {
        EditorWindow.GetWindow(typeof(BuildingTools));
    }
    
    void OnGUI() {

        selectedObject = Selection.activeGameObject;

        

        if(selectedObject != null){
            // Show Selected Item -------------------------------------------------
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Selected Object [" + selectedObject.name + "]", EditorStyles.boldLabel);
                EditorGUILayout.ObjectField(selectedObject, typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();

            // Snap selected object to grid-----------------------------------------
            EditorGUILayout.LabelField("Snap Tool:", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
                snapIntervial = EditorGUILayout.FloatField(snapIntervial);
                if(GUILayout.Button("Snap Selection")){
                    SnapToGrid(selectedObject, snapIntervial);
                }

                if(GUILayout.Button("Snap All Selected")){
                    SnapToGrid((Selection.gameObjects), snapIntervial);
                }
            EditorGUILayout.EndHorizontal();

            // Combine selected objects -----------------------------------------
            EditorGUILayout.LabelField("Combine Tool:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Combines all the children of the selected object.", EditorStyles.miniBoldLabel);
            EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Combine Children")){
                    if(EditorUtility.DisplayDialog("Are you sure?", "Are you sure you wand to combine all meshes that are children of this object?", "Go Ahead", "No"))
                        CombineMeshes(Selection.activeGameObject);
                }
            EditorGUILayout.EndHorizontal();

        } else
        // Ask for a selection if nothing selected
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Plese Select An Object to Get Appropriate Tools", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
        }
    }

    void OnSelectionChange() {
        Repaint();
    }

    // Snap Function-------------------------------------------------------------
    // --------------------------------------------------------------------------
    public void SnapToGrid(GameObject go, float intervial){
        float funcAcc = 1/intervial;
        Vector3 oldPos = go.transform.position;

        float newX = (Mathf.Round(oldPos.x * funcAcc))/funcAcc;
        float newY = (Mathf.Round(oldPos.y * funcAcc))/funcAcc;
        float newZ = (Mathf.Round(oldPos.z * funcAcc))/funcAcc;
        Vector3 newPos = new Vector3(newX, newY, newZ);

        go.transform.position = newPos;
    }

    public void SnapToGrid(GameObject[] go, float intervial){
        float funcAcc = 1/intervial;

        foreach (GameObject cgo in go)
        {
            Vector3 oldPos = cgo.transform.position;

            float newX = (Mathf.Round(oldPos.x * funcAcc))/funcAcc;
            float newY = (Mathf.Round(oldPos.y * funcAcc))/funcAcc;
            float newZ = (Mathf.Round(oldPos.z * funcAcc))/funcAcc;
            Vector3 newPos = new Vector3(newX, newY, newZ);

            cgo.transform.position = newPos;
        }
        
    }


    // Combine Mesh Function-----------------------------------------------------------
    // --------------------------------------------------------------------------------

    public void CombineMeshes(GameObject combineParent)
    {   
        Vector3 basePosition = combineParent.transform.position;
        Quaternion baseRotation = combineParent.transform.rotation;
        combineParent.transform.position = Vector3.zero;
        combineParent.transform.rotation = Quaternion.identity;

        ArrayList materials = new ArrayList();
        ArrayList combineInstanceArrays = new ArrayList();
        MeshFilter[] meshFilters = combineParent.GetComponentsInChildren<MeshFilter>();
 
        foreach (MeshFilter meshFilter in meshFilters)
        {
            MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();
 
            if (!meshRenderer ||
                !meshFilter.sharedMesh ||
                meshRenderer.sharedMaterials.Length != meshFilter.sharedMesh.subMeshCount)
            {
                continue;
            }
 
            for (int s = 0; s < meshFilter.sharedMesh.subMeshCount; s++)
            {
                int materialArrayIndex = Contains(materials, meshRenderer.sharedMaterials[s].name);
                if (materialArrayIndex == -1)
                {
                    materials.Add(meshRenderer.sharedMaterials[s]);
                    materialArrayIndex = materials.Count - 1;
                }
                combineInstanceArrays.Add(new ArrayList());
 
                CombineInstance combineInstance = new CombineInstance();
                combineInstance.transform = meshRenderer.transform.localToWorldMatrix;
                combineInstance.subMeshIndex = s;
                combineInstance.mesh = meshFilter.sharedMesh;
                (combineInstanceArrays[materialArrayIndex] as ArrayList).Add(combineInstance);
            }
        }
 
        // Get / Create mesh filter & renderer
        MeshFilter meshFilterCombine = combineParent.GetComponent<MeshFilter>();
        if (meshFilterCombine == null)
        {
            meshFilterCombine = combineParent.AddComponent<MeshFilter>();
        }
        MeshRenderer meshRendererCombine = combineParent.GetComponent<MeshRenderer>();
        if (meshRendererCombine == null)
        {
            meshRendererCombine = combineParent.AddComponent<MeshRenderer>();
        }
 
        // Combine by material index into per-material meshes
        // also, Create CombineInstance array for next step
        Mesh[] meshes = new Mesh[materials.Count];
        CombineInstance[] combineInstances = new CombineInstance[materials.Count];
 
        for (int m = 0; m < materials.Count; m++)
        {
            CombineInstance[] combineInstanceArray = (combineInstanceArrays[m] as ArrayList).ToArray(typeof(CombineInstance)) as CombineInstance[];
            meshes[m] = new Mesh();
            meshes[m].CombineMeshes(combineInstanceArray, true, true);
 
            combineInstances[m] = new CombineInstance();
            combineInstances[m].mesh = meshes[m];
            combineInstances[m].subMeshIndex = 0;
        }
 
        // Combine into one
        meshFilterCombine.sharedMesh = new Mesh();
        meshFilterCombine.sharedMesh.CombineMeshes(combineInstances, false, false);
 
        // Destroy other meshes
        foreach (Mesh oldMesh in meshes)
        {
            oldMesh.Clear();
            DestroyImmediate(oldMesh);
        }
 
        // Assign materials
        Material[] materialsArray = materials.ToArray(typeof(Material)) as Material[];
        meshRendererCombine.materials = materialsArray;
 
        foreach (MeshFilter meshFilter in meshFilters)
        {
            DestroyImmediate(meshFilter.gameObject);
        }

        combineParent.transform.position = basePosition;
        combineParent.transform.rotation = baseRotation;

    }
 
    private int Contains(ArrayList searchList, string searchName)
    {
        for (int i = 0; i < searchList.Count; i++)
        {
            if (((Material)searchList[i]).name == searchName)
            {
                return i;
            }
        }
        return -1;
    }
}