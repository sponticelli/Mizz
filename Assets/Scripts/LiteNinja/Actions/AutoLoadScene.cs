using LiteNinja.SOVariable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LiteNinja.Actions
{
    [AddComponentMenu("LiteNinja/Actions/AutoLoadScene")]
    [DefaultExecutionOrder(-150)]
    public class AutoLoadScene : MonoBehaviour
    {
        [SerializeField] private StringVar _sceneName;
        [SerializeField] private bool _isAdditive;

        private void Awake()
        {
            if (string.IsNullOrEmpty(_sceneName.Value))
            {
                Debug.LogError("Scene name is empty");
                return;
            }
            
            //Check if the scene is already loaded
            if (SceneManager.GetSceneByName(_sceneName.Value).isLoaded)
            {
                return;
            }

            SceneManager.LoadScene(_sceneName.Value, _isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        }
    }
}