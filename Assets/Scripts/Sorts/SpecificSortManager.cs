using UnityEngine;
using System.Collections;

public class SpecificSortManager : SortManager
{

    public BaseSort _myBaseSort;

    void Start()
    {
        SetExistingNodesData();
        _myBaseSort = GetComponent<BaseSort>();
    }

    public override void RunAlgorithm()
    {
        _myBaseSort.Sort();
    }

    public override void CheckInputNumbers()
    {

    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Add();
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Delete();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            RunAlgorithm();
        }*/
    }

}
