using Services.StorageService;
using UnityEngine;
public class MenuController : MonoBehaviour
{
    public void StartJourney()
    {
        SceneLoader.instance.LoadScene(SceneNames.Map);
    }
}
