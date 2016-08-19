using UnityEngine;
using System.Collections;

public class PlacedBlock : MonoBehaviour {
    Vector3 worldPosition;

    public int gridX;
    public int gridY;

    // Use this for initialization
    void Start () {
        //worldPosition = transform.position;
    }

    public void moveDown()
    {
        worldPosition = transform.position;
        worldPosition.y -= 0.3f;
        transform.position = worldPosition;
    }

    // Update is called once per frame
    void Update () {
        
    }
}
