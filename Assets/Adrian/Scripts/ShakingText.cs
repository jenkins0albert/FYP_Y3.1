using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShakingText : MonoBehaviour
{
    private TMP_Text textMesh;

    private Mesh mesh;

    Vector3[] vertices;

    List<int> wordIndexes;
    List<int> wordLengths;

    private void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        /*
        wordIndexes = new List<int> { 0 };
        wordLengths = new List<int>();



        string s = textMesh.text;
        for (int index = s.IndexOf(" "); index > -1; index = s.IndexOf(' ', index + 1))
        {
            wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(index + 1);
        }
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);
        */
    }

    private void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;


        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);
            vertices[i] = vertices[i] + offset;
        }



        mesh.vertices = vertices;
       
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 6.3f), Mathf.Cos(time * 5.5f));
        
    }
}

