using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnCloudTest : MonoBehaviour
{
    public List<Sprite> cloudList;
    public float StartSpawnHeight;
    public float EndSpawnHeight;
    public Image prefab;
    public float StartSpawnX;
    public float minMoveTime;
    public float maxMoveTime;
    public enum Axis
    {
        Horizontal,
        Vertical
    }
    private void Awake()
    {

    }
    [Button]
    private void SpawnCloud()
    {
        var sprite = cloudList[Random.Range(0, cloudList.Count)];
        var positionY = Random.Range(StartSpawnHeight, EndSpawnHeight);
        var img = Instantiate(prefab, transform);
        img.sprite = sprite;
        img.SetNativeSize();
        var time = Random.Range(minMoveTime, maxMoveTime);

        img.transform.localPosition = new Vector3(StartSpawnX, positionY);
        img.transform.DOLocalMove(new Vector3(-StartSpawnX, positionY), time).OnComplete(() =>
        {
            Destroy(img.gameObject);
        });
    }
}
