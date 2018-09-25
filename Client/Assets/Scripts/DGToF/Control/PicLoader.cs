using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PicLoader
{
    private static PicLoader instance;
    public static PicLoader Instance { get { return instance ?? (instance = new PicLoader()); } }

    public IEnumerator Load(Image imgC, string path)
    {
        double startTime = (double) Time.time;

        var fileAddress = System.IO.Path.Combine( Application.streamingAssetsPath, path);
        Debug.Log(fileAddress);
        //请求WWW
        WWW www = new WWW("file:///" + fileAddress);
        
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("WWW加载错误:" + www.error);
            yield return null;
        }
        Texture2D texture = www.texture;

        //创建Sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        imgC.sprite = sprite;

        startTime = (double)Time.time - startTime;
        Debug.Log("WWW加载用时:" + startTime);
        yield return null;
    }

    public IEnumerator Load(SpriteRenderer sprR, string path)
    {
        double startTime = (double)Time.time;

        var fileAddress = System.IO.Path.Combine(Application.streamingAssetsPath, path);
        Debug.Log(fileAddress);
        //请求WWW
        WWW www = new WWW("file:///" + fileAddress);

        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("WWW加载错误:" + www.error);
            yield return null;
        }
        Texture2D texture = www.texture;

        //创建Sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        sprR.sprite = sprite;

        startTime = (double)Time.time - startTime;
        Debug.Log("WWW加载用时:" + startTime);
        yield return null;
    }
}
