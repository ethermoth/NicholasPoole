using UnityEngine;

public class VRButtonActions : MonoBehaviour
{
    public void InstantiateGameObject(GameObject go)
    {
        Instantiate(go, Vector3.zero, Quaternion.identity);
    }

    public void PlayGame(VRButton blockSpawner)
    {
        blockSpawner.laserActivated = true;
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}