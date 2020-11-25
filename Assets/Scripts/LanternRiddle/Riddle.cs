using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Riddle : MonoBehaviour 
{
    public static Riddle _instance;

    private List<Mystery> mysteries = new List<Mystery>();
    private int[] flags;
    private int index = 0;

    public struct Mystery
    {
        public string mystery;
        public string answer;
        public string tip;
        public int flag;

        public Mystery(string m, string a, string t){
            mystery = m;
            answer = a;
            tip = t;
            flag = 1;
        }

        public void ChangeState()
        {
            flag = 2;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        LoadFile(Application.streamingAssetsPath, "Mystery.txt");
        flags = new int[mysteries.Count];
        for (int i = 0; i < flags.Length; i++)
        {
            flags[i] = 1;
        }
        _instance = this;
    }

    void LoadFile(string sPath, string sName)
    {
        StreamReader sr = null;
        sr = File.OpenText(sPath + "//" + sName);

        string m_Line;
        string a_Line;
        string t_Line;

        while((m_Line = sr.ReadLine()) != null && (a_Line = sr.ReadLine()) != null && (t_Line = sr.ReadLine()) != null)
        {
            mysteries.Add(new Mystery(m_Line, a_Line, t_Line));
        }

        sr.Close();
        sr.Dispose();
    }

    public Mystery GetRiddle()
    {
        while ( flags[index = Random.Range(0, mysteries.Count)] != 1)
        {

        }

        if (flags[index] == 1)
        {
            flags[index] <<= 1;
        }
        return mysteries[index];
    }
}