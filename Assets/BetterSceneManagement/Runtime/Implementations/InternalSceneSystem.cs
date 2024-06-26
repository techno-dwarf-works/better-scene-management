using System;
using System.Threading.Tasks;
using Better.SceneManagement.Runtime.Interfaces;
using Better.SceneManagement.Runtime.Transitions;
using UnityEngine.SceneManagement;

namespace Better.SceneManagement.Runtime
{
    public class InternalSceneSystem : ISceneSystem, ITransitionRunner<SingleTransitionInfo>, ITransitionRunner<AdditiveTransitionInfo>
    {
        private readonly SceneSystemSettings _settings;

        public InternalSceneSystem()
        {
            _settings = SceneSystemSettings.Instance;
        }

        public SingleTransitionInfo CreateSingleTransition(SceneReference sceneReference)
        {
            return new(this, sceneReference, _settings.AllowLogs);
        }

        public AdditiveTransitionInfo CreateAdditiveTransition()
        {
            return new(this, _settings.AllowLogs);
        }

        Task ITransitionRunner<SingleTransitionInfo>.RunAsync(SingleTransitionInfo transitionInfo)
        {
            return RunAsync(transitionInfo, LoadSceneMode.Single);
        }

        Task ITransitionRunner<AdditiveTransitionInfo>.RunAsync(AdditiveTransitionInfo transitionInfo)
        {
            return RunAsync(transitionInfo, LoadSceneMode.Additive);
        }

        private Task RunAsync(TransitionInfo transitionInfo, LoadSceneMode mode)
        {
            if (!transitionInfo.OverridenSequence
                || !_settings.TryGetOverridenSequence(transitionInfo.SequenceType, out var sequence))
            {
                sequence = _settings.GetDefaultSequence();
            }
            
            var unloadOperations = transitionInfo.CollectUnloadOperations();
            var loadOperations = transitionInfo.CollectLoadOperations();
            return sequence.Run(unloadOperations, loadOperations, mode, transitionInfo.AllowLogs);
        }
    }
}