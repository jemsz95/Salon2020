using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SortManager : MonoBehaviour
{

    public float fXMargin = 0.5f;
    public float fHeight = 1.5f;
    public GameObject prefSphere;

    public Text tSelectedAlgorithm;
    public int iSelectedAlgorithm;

    public BubbleSort _BS;
    public IntercambioSort _IS;
    public SeleccionDirectaSort _SDS;
    public InsercionDirectaSort _IDS;

    void Start()
    {
        SetExistingNodesData();
    }

    // Update is called once per frame
    void Update()
    {
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
        }

        CheckInputNumbers();
    }

    void RunAlgorithm()
    {
        if (iSelectedAlgorithm == 1)
        {
            if(_BS != null)
            _BS.Sort();
            
        }
        if (iSelectedAlgorithm == 2)
        {
            if (_IS != null)
                _IS.Sort();
        }
        if (iSelectedAlgorithm == 3)
        {
            if (_SDS != null)
                _SDS.Sort();
        }
        if (iSelectedAlgorithm == 4)
        {
            if (_IDS != null)
                _IDS.Sort();
        }
    }

    public virtual void CheckInputNumbers()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            iSelectedAlgorithm = 1;
            tSelectedAlgorithm.text = "Burbuja";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            iSelectedAlgorithm = 2;
            tSelectedAlgorithm.text = "Intercambio";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            iSelectedAlgorithm = 3;
            tSelectedAlgorithm.text = "Seleccion directa";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            iSelectedAlgorithm = 4;
            tSelectedAlgorithm.text = "Insercion directa";
        }
    }

    public void SetExistingNodesData()
    {
        foreach (Transform child in transform)
        {
            Node nNode = child.GetComponent<Node>();
            int iValue = -1;
            string sNodeText = nNode.DataText.text;
            if (sNodeText.Length > 0)
            {
                int.TryParse(sNodeText, out iValue);
            }
            
            if(iValue != -1)
            {
                nNode.SetData(iValue);
            }

        }
    }

    public void Delete()
    {
        if (transform.childCount > 0)
        {
            Transform tTmp = transform.GetChild(0);
            float fMayor = transform.position.x;
            foreach (Transform child in transform)
            {
                if (child.position.x > fMayor)
                {
                    fMayor = child.position.x;
                    tTmp = child;
                }
            }

            Destroy(tTmp.gameObject);

        }
    }

    public void Add()
    {
        if (transform.childCount < 8 && transform.childCount > 0)
        {
            float fMayor = 0.0f;
            foreach (Transform child in transform)
            {
                if (child.position.x > fMayor)
                {
                    fMayor = child.position.x;
                }
            }

            GameObject goSphere = Instantiate(prefSphere, Vector3.zero, Quaternion.identity) as GameObject;
            goSphere.transform.SetParent(this.transform);
            goSphere.transform.position = new Vector3(transform.position.x - fXMargin + fXMargin * transform.childCount, fHeight, transform.position.z);

            Node nNode = goSphere.GetComponent<Node>();
            System.Random rnd = new System.Random();
            nNode.SetData(rnd.Next(31));
        }else if(transform.childCount == 0)
        {
            GameObject goSphere = Instantiate(prefSphere, Vector3.zero, Quaternion.identity) as GameObject;
            goSphere.transform.SetParent(this.transform);
            goSphere.transform.position = new Vector3(transform.position.x, fHeight, transform.position.z);

            Node nNode = goSphere.GetComponent<Node>();
            System.Random rnd = new System.Random();
            nNode.SetData(rnd.Next(31));
        }
    }

}
