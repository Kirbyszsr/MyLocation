using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLocation : MonoBehaviour
{
    public GameObject player;
    //待测点
    public GameObject[] aps;
    //代表AP的各个物件的目标
    public GameObject[] aplines;
    //代表定位圆
    public float[] distances;
    //AP离各个物件的距离
    public Vector3[] startLocations;
    //各个AP的中心点坐标（保存原点）

    public GameObject distancetext;
    //显示界面

    // Start is called before the first frame update
    void Start()
    {
        startLocations = new Vector3[aps.Length];
        int n = 0;
        //记录初始位置
        foreach (GameObject ap in aps)
        {
            startLocations[n] = new Vector3(ap.transform.position.x,
                ap.transform.position.y,ap.transform.position.z);
            n = n + 1;
        }

        distances = new float[aps.Length];
        getDistances();
        //更新数据
        setCircles();
    }

    // Update is called once per frame
    void Update()
    {
        getDistances();
        //更新距离
        setCircles();
    }

    void getDistances()
    {
        int n = 0;
        foreach (Vector3 v3 in startLocations)
        {
            distances[n] = Vector3.Distance(v3, player.transform.position);
            n++;
        }

        string textField = "";
        n = 1;
        foreach(float distance in distances)
        {
            textField += "distance for AP " + n + ":" + distance + "\n";
            n++;
        }
        Text text = distancetext.GetComponent<Text>();
        text.text = textField;

    }

    void setCircles()
    {
        //根据初始位置和距离画圈
        int n = 0;
        foreach (GameObject line in aplines)
        {
            if (n != 0) {
                line.transform.position = startLocations[n] + new Vector3(distances[n], 0.5f, 0);
            }
            line.transform.localScale = new Vector3(distances[n], 1, distances[n]);
            n++;
        }
    }
}
