using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFlyHpLoss : MonoBehaviour
{

    private TextMesh text;
    private Vector3 startPos;
	// Use this for initialization
	void Start ()
	{
	    startPos = transform.localPosition;
        text = transform.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fly(string val)
    {
        text.text = val;

        transform.localPosition = startPos;
        var midPoint = transform.position + new Vector3(0, 0.25f, 0);
        Hashtable args = new Hashtable();

        //这里是设置类型，iTween的类型又很多种，在源码中的枚举EaseType中
        //例如移动的特效，先震动在移动、先后退在移动、先加速在变速、等等
        args.Add("easeType", iTween.EaseType.easeOutBack);

        //移动的速度，
        //  args.Add("speed", 0.3f);
        //移动的整体时间。如果与speed共存那么优先speed
        args.Add("time", 1.2f);
        //延迟执行时间
        args.Add("delay", 0.3f);
        //移动的过程中面朝一个点
        //   args.Add("looktarget", Vector3.zero);

        //当然也可以写Vector3
         args.Add("position", midPoint);

        args.Add("onstart", "OnStart");
        args.Add("oncomplete", "OnComplete");//动画完成后执行什么方法，OnComplete在下面

        iTween.MoveTo(gameObject, args);

    }

    private void OnStart()
    {
        text.color = Color.red;
    }

    private void OnComplete()
    {
        text.color = new Color(0, 0, 0, 0);//透明

    }
}
