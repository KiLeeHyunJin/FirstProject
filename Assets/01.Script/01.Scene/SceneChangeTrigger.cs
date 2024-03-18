using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    [SerializeField] string SceneName;
    BaseScene scene;
    public int SetImgIdx;
    public int enterIdx;
    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Portal");
        scene = GetComponentInParent<BaseScene>();
        if (scene == null)
            scene = FindObjectOfType<BaseScene>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            scene.enterIdx = enterIdx;
            scene.loadingImgIdx = SetImgIdx;
            Manager.Scene.LoadScene(SceneName);
        }
    }

}
