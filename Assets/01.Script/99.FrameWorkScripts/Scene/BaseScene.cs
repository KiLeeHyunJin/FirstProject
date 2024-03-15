using System.Collections;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public int loadingImgIdx;
    public int enterIdx;
    [SerializeField] protected AudioClip bgm;
    [SerializeField] protected TransformPos player;
    [SerializeField] protected Transform[] pos;
    public abstract IEnumerator LoadingRoutine();
    public void StartBGM() => Manager.Sound.PlayBGM(bgm);
}
