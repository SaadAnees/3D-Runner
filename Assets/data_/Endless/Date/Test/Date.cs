using System;
using UnityEngine;
using UnityEngine.UI;


public class Date : MonoBehaviour {

    public static Date instance;

    public string date;
    public GameObject[] scrollViews;
    public GameObject textPrefab;
    public int startYear, endYear;

    int month, year;

    private void Awake()
    {
        AddYear();
        //AddDate();
        year = month = 1;
    }

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddYear() {
        for (int i = startYear; i < endYear; i++) {
            GameObject g = Instantiate(textPrefab, scrollViews[2].GetComponent<ScrollRect>().content.transform);
            g.GetComponent<Text>().text = i.ToString();
        }

        //for (int i = endYear; i < endYear; i--)
        //{
        //    GameObject g = Instantiate(textPrefab, scrollViews[2].GetComponent<ScrollRect>().content.transform);
        //    g.GetComponent<Text>().text = i.ToString();
        //}

    }

    public void UpdateMonth() {
        month = (int)(scrollViews[0].GetComponent<ScrollRect>().content.gameObject.GetComponent<RectTransform>().localPosition.y / 270) + 1;
        AddDate();
        scrollViews[1].GetComponent<ScrollRect>().GetComponent<SnapScriptYear>().CallStart();
    }

    public void UpdateYear()
    {
        year = Int32.Parse(scrollViews[2].GetComponent<SnapScriptYear>()._screensContainer_year.GetChild((int)(scrollViews[2].GetComponent<ScrollRect>().content.gameObject.GetComponent<RectTransform>().localPosition.y / 270)).GetComponent<Text>().text);
        AddDate();
        scrollViews[1].GetComponent<ScrollRect>().GetComponent<SnapScriptYear>().CallStart();
    }

    void AddDate() {

        foreach (Transform t in scrollViews[1].GetComponent<ScrollRect>().content.transform) {
            t.gameObject.SetActive(false);
        }

        print(month +"   "+ DateTime.DaysInMonth(year, month));

        for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
        {
            GameObject g = Instantiate(textPrefab, scrollViews[1].GetComponent<ScrollRect>().content.transform);
            g.GetComponent<Text>().text = i.ToString();
            Vector2 temp = g.GetComponent<RectTransform>().pivot = new Vector2(0,1);
        }
    }

    public void ShowDate() {
        print(month + "-" + ((int)(scrollViews[1].GetComponent<ScrollRect>().content.gameObject.GetComponent<RectTransform>().localPosition.y / 270) + 1) + "-" + year);
    }
}
