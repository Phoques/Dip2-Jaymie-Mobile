using UnityEngine;
using System.Collections.Generic;
public class TouchInput : MonoBehaviour
{
    [SerializeField] LayerMask _touchMask;
    public List<GameObject> _touchList = new List<GameObject>();
    public GameObject[] _oldTouches;
    RaycastHit _hitInfo;

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR // This allows us to build tests, or in this case add mouse to test swiping, but it will NOT be built into the game.
        //It will only be able to be used in the Unity editor. Good for debugging and testing.

        if (Input.GetMouseButtonDown(0))
        {
            //If the mouse button is clicked on a game object, it adds this gameobject to the touch list count
            _oldTouches =  new GameObject[_touchList.Count];
            //This only works on the second mouse click because the first click adds the GO to the touchlist.
            //Now the touchlist has a GO in the list, so the 'second' click then sends the first click to oldtouches.
            _touchList.CopyTo( _oldTouches);
            _touchList.Clear();
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

            if(Physics.Raycast( ray, out _hitInfo, _touchMask))
            {
                //This is saying that the game object recipient, is the target that the raycast has hit to send the message to (that the script is on?)
                GameObject recipient = _hitInfo.transform.gameObject;
                _touchList.Add(recipient);
                if(Input.GetMouseButtonDown(0))
                {
                    recipient.SendMessage("OnTouchDown", _hitInfo.point, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("Message send on touch down");
                }
            }
        }
#endif
        if (Input.touchCount > 0)
        {
            _oldTouches =  new GameObject[_touchList.Count];
            _touchList.CopyTo(_oldTouches);
            _touchList.Clear();

            foreach (Touch touch in Input.touches)
            {
                //Construct a ray cast from the current touch coords.

                //This takes the main camera, and shots a line straight into the game scene, from the touch input.
                Ray touchRay = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(touchRay, out _hitInfo, _touchMask))
                {
                    GameObject recipient = _hitInfo.transform.gameObject;
                    _touchList.Add(recipient);

                    if (touch.phase == TouchPhase.Began) // Equivilant to get button / key down.
                    {
                        //This sends a message, and he options we chose is that it doesnt require a reciever to send the message.
                        recipient.SendMessage("OnTouchDown", _hitInfo.point, SendMessageOptions.DontRequireReceiver);

                    }
                }
            }
        }


    }
}
