using System;
using Moq;
using Prism.Forms.Tests.Navigation.Mocks.ViewModels;
using Prism.Forms.Tests.Navigation.Mocks.Views;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;
using Xunit;

namespace Prism.Forms.Tests.Navigation
{
    [Collection(nameof(PageNavigation))]
    public class NavigationBuilderFixture : IDisposable
    {
        public NavigationBuilderFixture()
        {
            ContainerLocator.ResetContainer();
            var container = new Mock<IContainerExtension>();
            container.Setup(x => x.CreateScope()).Returns(Mock.Of<IScopedProvider>());
            ContainerLocator.SetContainerExtension(() => container.Object);
            NavigationRegistry.ClearRegistrationCache();

            NavigationRegistry.Register("NavigationPage", typeof(NavigationPage), null);
            NavigationRegistry.Register("TabbedPage", typeof(TabbedPage), null);
            NavigationRegistry.Register("Tab1Mock", typeof(Tab1Mock), typeof(Tab1MockViewModel));
            NavigationRegistry.Register("Tab2Mock", typeof(Tab2Mock), typeof(Tab2MockViewModel));
            NavigationRegistry.Register("Tab3Mock", typeof(Tab3Mock), typeof(Tab3MockViewModel));
        }

        [Fact]
        public void GeneratesBasicRelativeUri()
        {
            var builder = Mock.Of<INavigationService>()
                .CreateBuilder()
                .AddNavigationSegment("SomeView") as NavigationBuilder;

            Assert.Equal(new Uri("SomeView", UriKind.Relative), builder.BuildUri());
        }

        [Fact]
        public void GeneratesBasicAbsoluteUri()
        {
            var builder = Mock.Of<INavigationService>()
                .CreateBuilder()
                .UseAbsoluteNavigation()
                .AddNavigationSegment("SomeView") as NavigationBuilder;

            Assert.Equal(new Uri(NavigationBuilder.RootUri, "SomeView"), builder.BuildUri());
        }

        [Fact]
        public void GetsNavigationPageKey()
        {
            var builder = Mock.Of<INavigationService>()
                .CreateBuilder()
                .AddNavigationPage() as NavigationBuilder;

            Assert.Equal(new Uri("NavigationPage", UriKind.Relative), builder.BuildUri());
        }

        [Fact]
        public void GetsTabbedPageKey()
        {
            var builder = Mock.Of<INavigationService>()
                .CreateBuilder()
                .AddTabbedSegment(o => { }) as NavigationBuilder;

            Assert.Equal(new Uri("TabbedPage", UriKind.Relative), builder.BuildUri());
        }

        [Fact]
        public void GeneratesPerSegmentQueryStrings()
        {
            var builder = Mock.Of<INavigationService>()
                .CreateBuilder()
                .AddNavigationSegment("ViewA", o => o.AddSegmentParameter("id", 3))
                .AddNavigationSegment("ViewB", o => o.AddSegmentParameter("id", 5)) as NavigationBuilder;

            Assert.Equal(new Uri("ViewA?id=3/ViewB?id=5", UriKind.Relative), builder.BuildUri());
        }

        [Fact]
        public void AddsSegmentSpecificModalOverride()
        {
            var builder = Mock.Of<INavigationService>()
               .CreateBuilder()
               .AddNavigationPage()
               .AddNavigationSegment("ViewA")
               .AddNavigationSegment("ViewB", o => o.UseModalNavigation()) as NavigationBuilder;

            Assert.Equal(new Uri("NavigationPage/ViewA/ViewB?useModalNavigation=True", UriKind.Relative), builder.BuildUri());
        }

        [Fact]
        public void ConstructsDynamicTabbedPage()
        {
            var builder = Mock.Of<INavigationService>()
               .CreateBuilder()
               .AddTabbedSegment(o =>
               {
                   o.CreateTab("Tab1Mock");
                   o.CreateTab("Tab2Mock");
               }) as NavigationBuilder;

            Assert.Equal(new Uri("TabbedPage?createTab=Tab1Mock&createTab=Tab2Mock", UriKind.Relative), builder.BuildUri());
        }

        [Fact]
        public void UriEncodesCreateTabQueryString()
        {
            var builder = Mock.Of<INavigationService>()
               .CreateBuilder()
               .AddTabbedSegment(o =>
               {
                   o.CreateTab("Tab1Mock", t => t.AddSegmentParameter("id", 3));
                   o.CreateTab("Tab2Mock");
               }) as NavigationBuilder;

            Assert.Equal(new Uri("TabbedPage?createTab=Tab1Mock%3Fid%3D3&createTab=Tab2Mock", UriKind.Relative), builder.BuildUri());
        }

        public void Dispose()
        {
            ContainerLocator.ResetContainer();
            NavigationRegistry.ClearRegistrationCache();
        }
    }
}
