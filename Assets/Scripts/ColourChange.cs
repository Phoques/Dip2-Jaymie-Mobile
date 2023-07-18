using UnityEngine;

public class ColourChange : MonoBehaviour
{
    Color _startColour = Color.white;
    Color _touchDownColour = Color.red;


    private void Update()
    {
        //OnTouchDown();
    }


    void OnTouchDown()
    {
        GetComponent<MeshRenderer>().material.color = _touchDownColour;
    }




}
