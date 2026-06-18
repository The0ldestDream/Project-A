using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public LevelRenderer levelRenderer;

    public bool levelGenerated = false;


    //Events


    public void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateEntireLevel(LevelData data)
    {
        levelGenerator.GenerateLevel(data);
        levelRenderer.RenderLevel();
        levelGenerated = true;
    }








}
