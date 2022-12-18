using SpecFlowPlaywright;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly:RuntimePlugin(typeof(PlaywrightRuntimePlugin))]

namespace SpecFlowPlaywright
{
    public class PlaywrightRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object? sender,
            CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<PlaywrightConfiguration, IPlaywrightConfiguration>();
            e.ObjectContainer.RegisterTypeAs<DriverInitialiser, IDriverInitialiser>();
        }
    }
}