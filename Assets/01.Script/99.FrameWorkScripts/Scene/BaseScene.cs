using System.Collections;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public int loadingImgIdx;
    public int enterIdx;
    [SerializeField] protected TransformPos player;
    [SerializeField] protected Transform[] pos;
    public abstract IEnumerator LoadingRoutine();
}
