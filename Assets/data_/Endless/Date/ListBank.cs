/* Store the contents for ListBoxes to display.
 */
using UnityEngine;
using System;

public class ListBank : MonoBehaviour
{
	public static ListBank Instance;
	int a;
	private string[] contents_months = {
		"January", "Feburary", "March", "April", "May", "June", "July", "Auguest", "September", "Octomber", "November", "December"
	};
	public string[] contents_years = new string[60];
	public string[] contents_days = new string[31];

	void Awake ()
	{
		Instance = this;
	}

	void Start ()
	{
		//Debug.Log (i);
		int j = 0,m=0;
		for (int i = 2019; i > 1960; i--) {
			contents_years [j++] = i.ToString ();
			//contents_days[m++]= 
		}
		//for (int k = 0; k < 32; k++) 
		//{
		//	contents_days[m++]= DateTime.DaysInMonth(
		//}
	}

	public string getListContent (int index)
	{
		return contents_months [index].ToString ();
	}

	public int getListLength ()
	{
		return contents_months.Length;
	}

	public string getListContent_years (int index)
	{
		return contents_years [index].ToString ();
	}

	public int getListLength_years ()
	{
		return contents_years.Length;
	}

	public string getListContent_days (int index)
	{
		return contents_days [index].ToString ();
	}

	public int getListLength_days ()
	{
		return contents_days.Length;
	}

}
