using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameObject panelExample;
    [SerializeField] private Button restartButton;

    private Panel[] panels;
    private int limit = 16;
    private List<int> poolOfNumber = new List<int>() { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };

    public static List<Panel> CurrentCount = new List<Panel>(2);

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(()=> 
        {
            SceneManager.LoadScene("SampleScene");
        });

        float x = -170, y = 200;
        panels = new Panel[16];
        int index = 0;

        for (int i = 0; i < 4; i++)
        {

            for (int ii = 0; ii < 4; ii++)
            {
                GameObject temp = Instantiate(panelExample, new Vector3(x, y, 0), Quaternion.identity, GameObject.Find("MainCanvas").transform);
                temp.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(x, y, 0);
                panels[index] = temp.GetComponent<Panel>();
                x += 110;
                index++;
            }
            x = -170;
            y -= 110;
        }

        print(panels.Length + " &&");

        for (int i = 0; i < panels.Length; i++)
        {
            int getNumber = Random.Range(0, poolOfNumber.Count);
            
            panels[i].SetNumber(poolOfNumber[getNumber]);
            poolOfNumber.Remove(poolOfNumber[getNumber]);
        }

        for (int i = 0; i < poolOfNumber.Count; i++)
        {
            print(poolOfNumber[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (CurrentCount.Count == 2 && CurrentCount[0].number == CurrentCount[1].number)
        {
            CurrentCount[0].ShowAndStay();
            CurrentCount[0].StopAllCoroutines();
            CurrentCount[1].ShowAndStay();
            CurrentCount[1].StopAllCoroutines();
            CurrentCount.Clear();
        }
    }
}
