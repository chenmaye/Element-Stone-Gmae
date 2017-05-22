using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class main : MonoBehaviour {

    //[SerializeField]
    //private Text _Text;
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

        //string ANDROID_ID = Secure.getString(getContext().getContentResolver(), Secure.ANDROID_ID)

        //_Text.text = System.Guid.NewGuid().ToString();
        xmlload();
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
        _guidFounding.text = "ADD SUCCESS";
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
        _guidFounding.text = "DELETE SUCCESS";
    }

    public void xmlload()
    {
        XmlTextReader myXmlTextReader = new XmlTextReader(@"Database\GuidMap.xml");

        while (myXmlTextReader.Read())
        {               
             if (myXmlTextReader.NodeType == XmlNodeType.Element)
             {
                   if (myXmlTextReader.Name == "Costomer")
                   {
                        _guidDic.Add(myXmlTextReader.GetAttribute(0), myXmlTextReader.GetAttribute(1));
                   }
             }               
        }
    }

    public void xmlsave()
    {
        if (Directory.Exists("Database") == false)//如果不存在就创建Database文件夹;
        {
            Directory.CreateDirectory("Database");
        }

        XmlTextWriter myXmlTextWriter = new XmlTextWriter(@"Database\GuidMap.xml", null);
        myXmlTextWriter.Formatting = Formatting.Indented; 
        myXmlTextWriter.WriteStartDocument(false);
        myXmlTextWriter.WriteStartElement("GuidMap");
          
        myXmlTextWriter.WriteComment("玩家GUID - 玩家兑换时间");

        foreach (KeyValuePair<string, string> kv in _guidDic)
        {
            myXmlTextWriter.WriteStartElement("Costomer");
            myXmlTextWriter.WriteAttributeString("Guid", kv.Key);
            myXmlTextWriter.WriteAttributeString("Time", kv.Value);

            myXmlTextWriter.WriteEndElement();
        }

        myXmlTextWriter.WriteEndElement();    
        myXmlTextWriter.Flush();
        myXmlTextWriter.Close();
    }

    private void OnApplicationQuit()
    {
        xmlsave();
    }
}
