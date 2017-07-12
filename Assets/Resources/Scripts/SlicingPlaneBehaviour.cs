using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicingPlaneBehaviour : MonoBehaviour {

    private Mesh slicingPlaneMesh;
    private GameObject gameObjectToSlice;
    private GameObject gOToSRef;
    private List<List<Vector3>> verticesList = new List<List<Vector3>>();
    private List<List<int>> trianglesList = new List<List<int>>();
    private int meshCount;
    private List<GameObject> trianglesRenderers = new List<GameObject>();

    void Awake () {
        Globals.slicingPlane = this;
        gameObject.SetActive(false);
    }

    public void Show (ControllerBehaviour controller)
    {
        transform.position = controller.transform.position;
        transform.rotation = controller.transform.rotation;
        transform.parent = controller.transform;
        transform.localPosition += new Vector3(0, 0, 1.05f);
        gameObject.SetActive(true);
    }

    public void Hide ()
    {
        gameObject.SetActive(false);
        transform.parent = null;
    }

    public bool IsShowing()
    {
        return gameObject.activeSelf;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == Globals.paraviewObj)
        {
            gameObjectToSlice = collider.gameObject;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == Globals.paraviewObj)
        {
            gameObjectToSlice = null;
        }
    }

    public void HighlightIntersection()
    {
        if (gameObjectToSlice != null)
        {
            if (trianglesRenderers.Count > 0)
            {
                foreach (GameObject tr in trianglesRenderers)
                {
                    Destroy(tr);
                }

                trianglesRenderers.Clear();
            }
            

            foreach(MeshFilter m in gameObjectToSlice.GetComponentsInChildren<MeshFilter>())
            {

                GameObject trianglesRenderer = new GameObject("trianglesRenderer");
                trianglesRenderer.AddComponent<LineRenderer>();
                trianglesRenderers.Add(trianglesRenderer);
                LineRenderer lineRenderer = trianglesRenderer.GetComponent<LineRenderer>();

                lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                lineRenderer.receiveShadows = false;
                lineRenderer.widthMultiplier = 0.01f;

                Mesh meshToSlice = m.mesh; //gameObjectToSlice.GetComponent<MeshFilter>().mesh;
                Vector3[] verticesToSlice = meshToSlice.vertices;
                int[] trianglesToSlice = meshToSlice.triangles;

                List<Vector3> verticesToHighlight = new List<Vector3>();

                Mesh slicingPlaneMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
                Vector3[] slicingPlaneVertices = slicingPlaneMesh.vertices;

                var p1 = gameObject.transform.TransformPoint(slicingPlaneVertices[110]);
                var p2 = gameObject.transform.TransformPoint(slicingPlaneVertices[65]);
                var p3 = gameObject.transform.TransformPoint(slicingPlaneVertices[0]);
                var myPlane = new Plane(p1, p2, p3);

                /*
                lineRenderer.positionCount = 3;
                lineRenderer.SetPosition(0, gameObject.transform.TransformPoint(slicingPlaneVertices[110]));
                lineRenderer.SetPosition(1, gameObject.transform.TransformPoint(slicingPlaneVertices[65]));
                lineRenderer.SetPosition(2, gameObject.transform.TransformPoint(slicingPlaneVertices[0]));
                */

                int pos = 0, neg = 0;
                for (int i = 0; i < trianglesToSlice.Length - 2; i += 3)
                {
                    var tmpTriangle = gameObjectToSlice.transform.TransformPoint(verticesToSlice[trianglesToSlice[i]]);
                    var tmpTriangle1 = gameObjectToSlice.transform.TransformPoint(verticesToSlice[trianglesToSlice[i + 1]]);
                    var tmpTriangle2 = gameObjectToSlice.transform.TransformPoint(verticesToSlice[trianglesToSlice[i + 2]]);

                    if (myPlane.GetSide(tmpTriangle) && myPlane.GetSide(tmpTriangle1) && myPlane.GetSide(tmpTriangle2))
                    {
                        pos += 3;
                    }
                    if (!myPlane.GetSide(tmpTriangle) && !myPlane.GetSide(tmpTriangle1) && !myPlane.GetSide(tmpTriangle2))
                    {
                        neg += 3;
                    }

                    if ((myPlane.GetSide(tmpTriangle) || myPlane.GetSide(tmpTriangle1) || myPlane.GetSide(tmpTriangle2)) && !(myPlane.GetSide(tmpTriangle) && myPlane.GetSide(tmpTriangle1) && myPlane.GetSide(tmpTriangle2)))
                    {
                        verticesToHighlight.Add(verticesToSlice[trianglesToSlice[i]]);
                        verticesToHighlight.Add(verticesToSlice[trianglesToSlice[i + 1]]);
                        verticesToHighlight.Add(verticesToSlice[trianglesToSlice[i + 2]]);
                    }
                }

                Debug.Log("verticesToHighligh.Count: " + verticesToHighlight.Count);


                lineRenderer.positionCount = verticesToHighlight.Count;
                for (int j = 0; j < verticesToHighlight.Count; j++)
                {
                    lineRenderer.SetPosition(j, gameObjectToSlice.transform.TransformPoint(verticesToHighlight[j]));
                }
            }

        }
    }

    public void Clip()
    {
        if (gameObjectToSlice != null)
        {
            Debug.Log("Clipping Object");
            /*
            for (int meshIndex = 0; meshIndex < gameObjectToSlice.GetComponentsInChildren<MeshFilter>().Length; meshIndex++)
            {
            */
                gOToSRef = gameObjectToSlice;
            //Mesh meshToSlice = gameObjectToSlice.GetComponentsInChildren<MeshFilter>()[meshIndex].mesh;
            Mesh meshToSlice = gameObjectToSlice.GetComponent<MeshFilter>().mesh;
            Vector3[] verticesToSlice = meshToSlice.vertices;
                int[] trianglesToSlice = meshToSlice.triangles;

                if(verticesList.Count == 0 && trianglesList.Count == 0)
                {
                    verticesList.Add(new List<Vector3>());
                    trianglesList.Add(new List<int>());
                    for (int vertexIndex = 0; vertexIndex < verticesToSlice.Length; vertexIndex++)
                        verticesList[0].Add(verticesToSlice[vertexIndex]);
                    for (int triangleIndex = 0; triangleIndex < trianglesToSlice.Length; triangleIndex++)
                        trianglesList[0].Add(trianglesToSlice[triangleIndex]);
                    meshCount = 1;
                }

                List<Vector3> posVerticesL = new List<Vector3>();
                int[] posVertexIndex = new int[verticesToSlice.Length];
                List<int> posTrianglesL = new List<int>();

                Mesh slicingPlaneMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
                Vector3[] slicingPlaneVertices = slicingPlaneMesh.vertices;

                var p1 = gameObject.transform.TransformPoint(slicingPlaneVertices[110]);
                var p2 = gameObject.transform.TransformPoint(slicingPlaneVertices[65]);
                var p3 = gameObject.transform.TransformPoint(slicingPlaneVertices[0]);
                var myPlane = new Plane(p1, p2, p3);

                Debug.DrawLine(p1, p2, Color.red);
                Debug.DrawLine(p2, p3, Color.green);
                Debug.DrawLine(p3, p1, Color.blue);


                int i = 0;
                while (i < verticesToSlice.Length)
                {
                    var tmpVertices = gameObjectToSlice.transform.TransformPoint(verticesToSlice[i]);

                    if (myPlane.GetSide(tmpVertices))
                    {
                        posVertexIndex[i] = posVerticesL.Count;
                        posVerticesL.Add(verticesToSlice[i]);
                    }
                    else if (!myPlane.GetSide(tmpVertices))
                    {
                        posVertexIndex[i] = -1;
                    }
                    i++;
                }

                for (int j = 0; j < trianglesToSlice.Length - 2; j += 3)
                {
                    var tmpTriangle = gameObjectToSlice.transform.TransformPoint(verticesToSlice[trianglesToSlice[j]]);
                    var tmpTriangle1 = gameObjectToSlice.transform.TransformPoint(verticesToSlice[trianglesToSlice[j + 1]]);
                    var tmpTriangle2 = gameObjectToSlice.transform.TransformPoint(verticesToSlice[trianglesToSlice[j + 2]]);

                    if (myPlane.GetSide(tmpTriangle) && myPlane.GetSide(tmpTriangle1) && myPlane.GetSide(tmpTriangle2))
                    {
                        posTrianglesL.Add(posVertexIndex[trianglesToSlice[j]]);
                        posTrianglesL.Add(posVertexIndex[trianglesToSlice[j + 1]]);
                        posTrianglesL.Add(posVertexIndex[trianglesToSlice[j + 2]]);
                    }
                }
                verticesList.Add(new List<Vector3>());
                trianglesList.Add(new List<int>());
                for (int vertexIndex = 0; vertexIndex < posVerticesL.Count; vertexIndex++)
                    verticesList[meshCount].Add(posVerticesL[vertexIndex]);
                for (int triangleIndex = 0; triangleIndex < posTrianglesL.Count; triangleIndex++)
                    trianglesList[meshCount].Add(posTrianglesL[triangleIndex]);
                meshCount += 1;

                meshToSlice.triangles = posTrianglesL.ToArray();
                meshToSlice.vertices = posVerticesL.ToArray();
                meshToSlice.RecalculateBounds();
            //}
        }
        else if(gameObjectToSlice == null && meshCount > 1)
        {
            Debug.Log("Undo");
            Mesh meshToShow = gOToSRef.GetComponent<MeshFilter>().mesh;
            meshToShow.triangles = trianglesList[meshCount - 2].ToArray();
            meshToShow.vertices = verticesList[meshCount - 2].ToArray();
            meshToShow.RecalculateBounds();

            verticesList[meshCount - 1] = new List<Vector3>();
            trianglesList[meshCount - 1] = new List<int>();
            meshCount -= 1;
        }
    }

    public void Undo()
    {
        Debug.Log("Undo function called");
    }
}
