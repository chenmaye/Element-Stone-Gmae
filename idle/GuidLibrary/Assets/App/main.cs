using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class main : MonoBehaviour {

    [SerializeField]
    private Text _Text;
    [SerializeField]
    private Dictionary<string, string> _guidDic;
    [SerializeField]
    private InputField _guidInput;
    [SerializeField]
    private Text _guidFounding;
    [SerializeField]
    private GameObject _add;
    [SerializeField]
    private GameObject _delete;


    private bool noEnter;
    // Use this for initialization
    void Start () {
        _guidDic = new Dictionary<string, string>();
        _Text.text = System.Guid.NewGuid().ToString();
        _add.SetActive(false);
        _delete.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public void Enter()
    {
        string tes = _guidInput.text;
        if (_guidDic.ContainsKey(tes))
        {
            string te = "Data found  \r\n";
            te += _guidInput.text;
            te += "       ";
            te += _guidDic[_guidInput.text];
            _guidFounding.text = te;
        }
        else
        {
            _guidFounding.text = "GUID NOT FOUND IN DATA BASE";
            _add.SetActive(true);
        }
    }

    public void Add()
    {
        string tes = _guidInput.text;
        if (_guidDic.ContainsKey(tes))
        {
            _guidFounding.text = "ADD ERROR : GUID NOT FOUND";
            return;
        }
        
        _guidDic.Add(tes, System.DateTime.Now.ToString());
        _add.SetActive(false);
        _delete.SetActive(true);
    }

    public void Delete()
    {
        string tes = _guidInput.text;
        if (!_guidDic.ContainsKey(tes))
        {
            _guidFounding.text = "DELETE ERROR : GUID NOT FOUND";
            return;
        }

        _guidDic.Remove(tes);     
        _guidDic.Add(tes, System.DateTime.Now.ToString());
        _delete.SetActive(false);
    }
}
