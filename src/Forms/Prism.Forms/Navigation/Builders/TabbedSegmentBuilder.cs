using System;
using System.Linq;

namespace Prism.Navigation
{
    internal class TabbedSegmentBuilder : ITabbedSegmentBuilder, IConfigurableSegmentName, IUriSegment
    {
        private INavigationParameters _parameters { get; }

        public TabbedSegmentBuilder()
        {
            _parameters = new NavigationParameters();
        }

        public string SegmentName { get; set; }

        public string Segment => BuildSegment();

        public ITabbedSegmentBuilder AddSegmentParameter(string key, object value)
        {
            _parameters.Add(key, value);
            return this;
        }

        public ITabbedSegmentBuilder UseModalNavigation(bool useModalNavigation)
        {
            return AddSegmentParameter(KnownNavigationParameters.UseModalNavigation, useModalNavigation);
        }

        public ITabbedSegmentBuilder CreateTab(Action<ISegmentBuilder> configureSegment)
        {
            var builder = new SegmentBuilder(null);

            throw new NotImplementedException();
        }

        public ITabbedSegmentBuilder SelectedTab(string segmentName)
        {
            return AddSegmentParameter(KnownNavigationParameters.SelectedTab, segmentName);
        }

        private string BuildSegment()
        {
            if (!_parameters.Any())
                return SegmentName;

            return SegmentName + _parameters.ToString();
        }
    }
}
