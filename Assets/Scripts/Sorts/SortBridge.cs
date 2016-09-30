using UnityEngine;
using System.Collections;

public class SortBridge : MonoBehaviour {

    public SpecificSortManager _SSM;
    public SortManager _WorshopSM;

    public void RunButton(string sFunction)
    {
        if (_SSM != null)
        {
            if (sFunction.Equals("Add"))
            {
                _SSM.Add();
            }
            else if (sFunction.Equals("Run"))
            {
                _SSM.RunAlgorithm();
            }
            else if (sFunction.Equals("Del"))
            {
                _SSM.Delete();
            }
        }
        else if(_WorshopSM != null)
        {
            if (sFunction.Equals("Add"))
            {
                _WorshopSM.Add();
            }
            else if (sFunction.Equals("Run"))
            {
                _WorshopSM.RunAlgorithm();
            }
            else if (sFunction.Equals("Del"))
            {
                _WorshopSM.Delete();
            }
            else if (sFunction.Equals("Next"))
            {
                _WorshopSM.Next();
            }
            else if (sFunction.Equals("Prev"))
            {
                _WorshopSM.Prev();
            }
        }
        
    }
}
