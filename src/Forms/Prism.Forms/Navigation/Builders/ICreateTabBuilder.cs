using System;

namespace Prism.Navigation
{
    public interface ICreateTabBuilder
    {
        ICreateTabBuilder AddNavigationSegment(string segmentName, Action<ISegmentBuilder> configureSegment);
    }
}
