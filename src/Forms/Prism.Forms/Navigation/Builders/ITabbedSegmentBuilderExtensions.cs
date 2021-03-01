using System;
using System.ComponentModel;

namespace Prism.Navigation
{
    public static class ITabbedSegmentBuilderExtensions
    {
        public static ICreateTabBuilder AddNavigationSegment(this ICreateTabBuilder builder, string segmentNameOrUri) =>
            builder.AddNavigationSegment(segmentNameOrUri, null);

        public static ITabbedSegmentBuilder CreateTab(this ITabbedSegmentBuilder builder, string segmentName, Action<ISegmentBuilder> configureSegment) =>
            builder.CreateTab(o => o.AddNavigationSegment(segmentName, configureSegment));

        public static ITabbedSegmentBuilder CreateTab(this ITabbedSegmentBuilder builder, string segmentNameOrUri) =>
            builder.CreateTab(o => o.AddNavigationSegment(segmentNameOrUri));

        public static ITabbedSegmentBuilder CreateTab<TViewModel>(this ITabbedSegmentBuilder builder)
            where TViewModel : class, INotifyPropertyChanged
        {
            var navigationKey = INavigationBuilderExtensions.GetNavigationKey<TViewModel>();
            return builder.CreateTab(navigationKey);
        }

        public static ITabbedSegmentBuilder SelectTab<TViewModel>(this ITabbedSegmentBuilder builder)
            where TViewModel : class, INotifyPropertyChanged
        {
            var navigationKey = INavigationBuilderExtensions.GetNavigationKey<TViewModel>();
            return builder.SelectedTab(navigationKey);
        }

        public static ITabbedNavigationBuilder SelectTab(this ITabbedNavigationBuilder builder, params string[] navigationSegments) =>
            builder.SelectTab(string.Join("|", navigationSegments));
    }
}
