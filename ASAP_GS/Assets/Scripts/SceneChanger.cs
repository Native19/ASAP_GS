using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public static void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
