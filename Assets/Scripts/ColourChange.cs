using UnityEngine;

public class ColourChange : MonoBehaviour
{
    Color _startColour = Color.white;
    Color _touchDownColour = Color.red;


    private void Update()
    {
        //OnTouchDown();
    }

    //This function is supposedly picked up by the message sent out into the void (Dont require reciever)
    void OnTouchDown()
    {
        GetComponent<MeshRenderer>().material.color = _touchDownColour;
    }




}
