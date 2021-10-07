using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventListener : MonoBehaviour
{
    public List<Transform> Transforms = new List<Transform>();
    private List<Vector3> _positionList;
    
    private int _nextPos;

    private void Awake()
    {
        MakeVector3List();
    }

    private void MakeVector3List()
    {
        _positionList = new List<Vector3>();
        for (int i = 0; i < Transforms.Count; i++)
        {
            _positionList.Add(Transforms[i].position);
        }
    }

    private void Update()
    {
        MoveToNextPos();
    }

    private void MoveToNextPos()
    {
        transform.position = Vector2.Lerp(transform.position, _positionList[_nextPos], Time.deltaTime);
    }
    
    public void InitialPos()
    {
        _nextPos = 0;
    }

    public void ActivatedPos()
    {
        _nextPos = 1;
    }

}
