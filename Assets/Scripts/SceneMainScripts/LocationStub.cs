using Services.StorageService;
using UnityEngine;

public class LocationStub : MonoBehaviour
{
    public void BackToMap()
    {
        SceneLoader.instance.LoadTransistor();
        SceneLoader.instance.LoadScene(SceneNames.Map);
    }
}
