using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwapManager : MonoBehaviour
{
    [SerializeField]private List<GameObject> _bodyList = new List<GameObject>();

    public void SubscibeBody(GameObject body)
    {
        if(!_bodyList.Contains(body))
            _bodyList.Add(body);
    }

    public void UnsubscribeBody(GameObject body)
    {
        if (_bodyList.Contains(body))
            _bodyList.Remove(body);
    }

}
