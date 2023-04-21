using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    //public Vector2 cameraChangeMax;
    //public Vector2 cameraChangeMin;
    public Vector4Value cameraBounds;
    public Vector3 playerChange;
    private CameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !collider.isTrigger)
        {
            // float values method
            /*cam.minPosition.x = cameraChangeMin.x;
            cam.minPosition.y = cameraChangeMin.y;
            cam.maxPosition.x = cameraChangeMax.x;
            cam.maxPosition.y = cameraChangeMax.y;*/

            // vector4 method
            cam.minPosition.x = cameraBounds.initialValue.x;
            cam.minPosition.y = cameraBounds.initialValue.y;
            cam.maxPosition.x = cameraBounds.initialValue.z;
            cam.maxPosition.y = cameraBounds.initialValue.w;

            collider.transform.position += playerChange;

            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(2f);
        placeText.GetComponent<Text>().CrossFadeAlpha(0, 2.5f, false);  // Adds fade to text
        text.SetActive(false);
    }
}
