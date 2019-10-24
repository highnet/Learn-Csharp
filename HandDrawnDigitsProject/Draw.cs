using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

    public Camera mainCamera;
    public Sprite drawingArea;
    public int cameraWidth = 112;
    public int cameraHeight = 112;
    public float colliderBoundaryRange = 0.5f;

    private void Start()
    {

        mainCamera = Camera.main;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Texture2D texture = spriteRenderer.sprite.texture;

        for (int x = 0; x < cameraWidth; x++)
        {
            for (int y = 0; y < cameraHeight; y++)
            {
                texture.SetPixel(x, y, Color.black);
            }
        }
        texture.Apply();
    }

  
    void OnMouseDown()
	{

        Debug.Log("ouch");

        
        RaycastHit hit;
        Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit);

        Debug.Log(hit.point);
        Debug.Log(hit.point.x);
        Debug.Log(hit.point.y);
        SpriteRenderer spriteRenderer = hit.transform.GetComponent<SpriteRenderer>();
        Texture2D texture = spriteRenderer.sprite.texture;

        float mappedX = (hit.point.x + colliderBoundaryRange) * (cameraWidth / (2* colliderBoundaryRange));
        float mappedY = (hit.point.y + colliderBoundaryRange) * (cameraHeight / (2* colliderBoundaryRange));

        texture.SetPixel((int)mappedX, (int)mappedY, Color.white);




        texture.Apply();
    }
}
