using Prism.Forms.Tests.Mocks.Views;
using Prism.Navigation;
using Xunit;

namespace Prism.Forms.Tests.Navigation
{
    public class PageNavigationInfoFixture
    {
        [Fact]
        public void PageNavigationInfoNameIsSet()
        {
            var info = new PageNavigationInfo();

            Assert.Null(info.Name);

            info.Name = "MainPage";

            Assert.NotNull(info.Name);
        }

        [Fact]
        public void PageNavigationInfoTypeIsSet()
        {
            var info = new PageNavigationInfo();

            Assert.Null(info.ViewType);

            var type = typeof(PageMock);
            info.ViewType = type;

            Assert.Equal(info.ViewType, type);
        }
    }
}
