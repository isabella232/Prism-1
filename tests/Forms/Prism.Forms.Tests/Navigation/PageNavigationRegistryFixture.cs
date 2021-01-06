using System;
using System.Reflection;
using Prism.Forms.Tests.Mocks.Views;
using Prism.Navigation;
using Xunit;

namespace Prism.Forms.Tests.Navigation
{
    [Collection(nameof(PageNavigation))]
    public class PageNavigationRegistryFixture : IDisposable
    {
        [Fact]
        public void RegisterPageForNavigation()
        {
            NavigationRegistry.ClearRegistrationCache();

            var name = "MainPage";
            var type = typeof(PageMock);
            NavigationRegistry.Register(name, type, null);

            var info = NavigationRegistry.GetPageNavigationInfo(name);

            Assert.NotNull(info);
        }

        [Fact]
        public void NavigationInfoIsNullForUnregisteredPage()
        {
            var name = "UnRegisteredPage";
            var info = NavigationRegistry.GetPageNavigationInfo(name);

            Assert.Null(info);
        }

        [Fact]
        public void GetPageType()
        {
            NavigationRegistry.ClearRegistrationCache();

            var name = "MainPage";
            var type = typeof(PageMock);
            NavigationRegistry.Register(name, type, null);

            var infoType = NavigationRegistry.GetPageType(name);

            Assert.Equal(type, infoType);
        }

        [Fact]
        public void PageTypeIsNullForUnregisteredPage()
        {
            var name = "UnRegisteredPage";
            var infoType = NavigationRegistry.GetPageType(name);

            Assert.Null(infoType);
        }

        public void Dispose()
        {
            NavigationRegistry.ClearRegistrationCache();
        }
    }
}
