using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoSingleton<SceneController>
{
    public SceneModel CurrentScene => currentScene;
    public UnityEvent<SceneModel> OnSceneLoad;
    public UnityEvent<SceneModel> OnSceneUnload;
    [SerializeField] SceneModel[] scenes;
    [SerializeField] SceneModel mainScene;
    [SerializeField] private SceneLoadingViewModel loadingScreen;
    private SceneModel currentScene;
    
    public override void Initialize()
    {
        destroyGameObjectOnDuplicate = true;
        base.Initialize();
        if(destroyed) return;
        currentScene = mainScene;
    }

    public void NextScene()
    {
        if (currentScene.ID + 1 < scenes.Length)
            StartCoroutine(LoadAsynchronously(scenes[currentScene.ID +1]));
        else
            StartCoroutine(LoadAsynchronously(mainScene));;
    }

    public void LoadScene(SceneModel scene)
    {
        StartCoroutine(LoadAsynchronously(scene));
    }

    [EditorButton]
    public void RestartScene()
    {
        LoadScene(currentScene);
    }

    
    private IEnumerator LoadAsynchronously(SceneModel sceneModel)
    {
        GameController.Instance.ChangeState(GameStates.Loading);
        float timer = 0f;
        float minLoadTime = loadingScreen.MinLoadTime;
        float displayedProgress = 0;
        loadingScreen.UpdateLoadingBar(0);
        DOTween.To(() => displayedProgress, (a) => displayedProgress = a, .9f, minLoadTime).OnUpdate(() =>
        {
            loadingScreen.UpdateLoadingBar(displayedProgress);
        });
        loadingScreen.Show();
        OnSceneUnload?.Invoke(currentScene);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneModel.SceneField);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.95f);
            timer += Time.deltaTime;
            if (timer > minLoadTime && progress >= displayedProgress)
            {
                loadingScreen.UpdateLoadingBar(progress);
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
        loadingScreen.Hide();
        currentScene = sceneModel;
        OnSceneLoad?.Invoke(currentScene);
        yield return null;
    }
}
