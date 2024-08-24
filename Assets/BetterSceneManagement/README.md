# Better Scene Management
[![openupm](https://img.shields.io/npm/v/com.tdw.better.scenemanagement?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.tdw.better.scenemanagement/)

This plugin allows you to serialize scene asset in inspector for loaing via `SceneLoader`.

## Usage
```c#
[SerializeField] private SceneLoaderAsset nextScene;

private async void Awake()
{
    await SceneLoader.LoadSceneAsync(nextScene);
    //or
    await SceneLoader.LoadSceneAsync(asset, new LoadSceneOptions()
        {
         SceneLoadMode = LoadSceneMode.Additive,
         UseIntermediate = false,
         ProgressChanged = ProgressChanged
        });
}

private static void ProgressChanged(float value)
{ 

}
```

## Install
[How to install](https://github.com/uurha/BetterPluginCollection/wiki/How-to-install)
