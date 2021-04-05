using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class testere : MonoBehaviour
{
    GameObject[] gidilecekNoktalar;

    bool aradakiMesafeyiAl = true;
    bool ileri_geri = true;
    Vector3 aradakiMesafe;

    int mesafeSayac = 0;
    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
        for(int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }

    
    void FixedUpdate()
    {
        transform.Rotate(0, 0, -5);
        NoktalaraGit();
    }

    void NoktalaraGit()
    {
        if (aradakiMesafeyiAl)
        {
            aradakiMesafe = (gidilecekNoktalar[mesafeSayac].transform.position - transform.position).normalized;
            aradakiMesafeyiAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[mesafeSayac].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * 10;
        if (mesafe < 0.5f)
        {
            aradakiMesafeyiAl = true;
            if(mesafeSayac == gidilecekNoktalar.Length - 1)
            {
                ileri_geri = false;   
            }
            else if(mesafeSayac == 0)
            {
                ileri_geri = true;
            }
            if (ileri_geri)
            {
                mesafeSayac++;
            }
            else
            {
                mesafeSayac--;
            }
        }

    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}


#if UNITY_EDITOR
[CustomEditor(typeof(testere))]
[System.Serializable]
class testereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        testere script = (testere)target; //yukaridaki classa erisim
        if (GUILayout.Button("URET"))
        {
            GameObject yeniObje = new GameObject(); 
            yeniObje.transform.parent = script.transform;
            yeniObje.transform.position = script.transform.position;
            yeniObje.name = script.transform.childCount.ToString();
        }

    }
}
#endif