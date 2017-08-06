using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image receivingImage;
	private Color normalColor;
	public Color highlightColor = Color.yellow;
	public Color normalColored = Color.grey;
	private GameObject instantiatedGO;

	public GameObject pref;
	
	public void OnEnable ()
	{
		//if (containerImage != null)
			//normalColor = containerImage.color;
	}
	
	public void OnDrop(PointerEventData data)
	{
		containerImage.color = normalColor;
		//Debug.Log(containerImage.name);
		nouveSequence.newLabelInSequence+=DragMe.nomObject + "|";
		labelsLoader(DragMe.nomObject);

		if (receivingImage == null)
			Debug.Log("onDrop");
			return;
		
		//Sprite dropSprite = GetDropSprite (data);
		//if (dropSprite != null)
			//receivingImage.overrideSprite = dropSprite;
	}

	public void OnPointerEnter(PointerEventData data)
	{
		
		if (containerImage == null)
			Debug.Log("enter pointer");
			//return;
		
		Sprite dropSprite = GetDropSprite (data);
		if (dropSprite != null){
			containerImage.color = highlightColor;

		}
		else
			containerImage.color = highlightColor;
	}

	public void OnPointerExit(PointerEventData data)
	{
		containerImage.color = normalColored;

		if (containerImage == null)
			Debug.Log("exit pointer");
			//return;
		
	
	}
	
	private Sprite GetDropSprite(PointerEventData data)
	{
		
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;
		
		var dragMe = originalObj.GetComponent<DragMe>();

		var srcImage = receivingImage.GetComponent<Image>();
		if (dragMe == null)
			//srcImage = receivingImage.GetComponent<Image>()
			return null;
		
		srcImage = receivingImage.GetComponent<Image>();

		if (srcImage == null)
			srcImage=containerImage.GetComponent<Image>();
			//return null;
		else
			srcImage = containerImage;

		
		return srcImage.sprite;
	}

	public void labelsLoader(string str){

		GameObject go = GameObject.Find("sequencePool");

		instantiatedGO = Instantiate (pref, transform.position, Quaternion.identity);
		instantiatedGO.transform.parent=go.transform;

		//instantiatedGO.transform.localPosition=new Vector3(transform.parent.GetComponent<RectTransform>().rect.width/2 - (str.IndexOf(s) * instantiatedGO.transform.GetComponent<RectTransform>().rect.width/2), 0,0);//transform.parent.position;

		instantiatedGO.transform.name= str;
		instantiatedGO.GetComponentInChildren<Text>().text=str;

	}
}
