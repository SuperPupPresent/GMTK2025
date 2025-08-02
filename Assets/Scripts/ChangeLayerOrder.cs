using UnityEngine;
using UnityEngine.U2D;

public class ChangeLayerOrder : MonoBehaviour
{

    public SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        changeOrderInLayer();
    }

    void changeOrderInLayer()
    {
        float yPos = -transform.position.y;
        sprite.sortingOrder = (int)yPos;
    }
}
