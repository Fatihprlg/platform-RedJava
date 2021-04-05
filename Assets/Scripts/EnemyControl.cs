using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class EnemyControl : MonoBehaviour
{
    GameObject[] gidilecekNoktalar;
    GameObject karakter;
    public GameObject mermi;

    bool aradakiMesafeyiAl = true;
    bool ileri_geri = true;

    Vector3 aradakiMesafe;
    
    RaycastHit2D ray;
    
    public LayerMask layerMask;

    public Sprite onTaraf;
    public Sprite arkaTaraf;
    SpriteRenderer spriteRenderer;

    float atesZamani = 0;

    int hiz = 5;
    int mesafeSayac = 0;

    void Start()
    {
        karakter = GameObject.FindGameObjectWithTag("Player");
        gidilecekNoktalar = new GameObject[transform.childCount];
        spriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }


    void FixedUpdate()
    {
        gorduMu();
        
        if(ray.collider.tag == "Player")
        {
            hiz = 8;
            spriteRenderer.sprite = onTaraf;
            atesEt();
        }
        else
        {
            hiz = 4;
            spriteRenderer.sprite = arkaTaraf;
        }

        NoktalaraGit();
    }

    void atesEt()
    {
        float mesafe = Vector3.Distance(karakter.transform.position, transform.position);
        atesZamani += Time.deltaTime;
        if (atesZamani > Random.Range(0.3f, 1) && mesafe <= 50)
        {
            Instantiate(mermi, transform.position, Quaternion.identity);
            atesZamani = 0;
        }

    }
    void gorduMu()
    {
        Vector3 rayYon = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYon, 1000, layerMask);
        Debug.DrawLine(transform.position, ray.point, Color.magenta);
    }

    void NoktalaraGit()
    {
        if (aradakiMesafeyiAl)
        {
            aradakiMesafe = (gidilecekNoktalar[mesafeSayac].transform.position - transform.position).normalized;
            aradakiMesafeyiAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[mesafeSayac].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * hiz;
        if (mesafe < 0.5f)
        {
            aradakiMesafeyiAl = true;
            if (mesafeSayac == gidilecekNoktalar.Length - 1)
            {
                ileri_geri = false;
            }
            else if (mesafeSayac == 0)
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

    public Vector3 getYon()
    {
        return (karakter.transform.position - transform.position).normalized;
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
[CustomEditor(typeof(EnemyControl))]
[System.Serializable]
class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemyControl script = (EnemyControl)target; 
        if (GUILayout.Button("ADD"))
        {
            GameObject yeniObje = new GameObject();
            yeniObje.transform.parent = script.transform;
            yeniObje.transform.position = script.transform.position;
            yeniObje.name = script.transform.childCount.ToString();
        }
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layerMask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("mermi"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();

    }
}
#endif