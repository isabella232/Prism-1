using System.ComponentModel;

namespace Prism.Navigation
{
    public static class ITabbedSegmentBuilderExtensions
    {
        public static ITabbedSegmentBuilder CreateTab(this ITabbedSegmentBuilder builder, string segmentNameOrUri)
        {
            return builder.CreateTab(o =>
            {
                if (o is IConfigurableSegmentName c)
                    c.SegmentName = segmentNameOrUri;
            });
        }

        public static ITabbedSegmentBuilder CreateTab<TViewModel>(this ITabbedSegmentBuilder builder)
            where TViewModel : class, INotifyPropertyChanged
        {
            var navigationKey = INavigationBuilderExtensions.GetNavigationKey<TViewModel>();
            return builder.CreateTab(navigationKey);
        }

        //public static ITabbedSegmentBuilder SelectTab(this ITabbedSegmentBuilder builder, string segmentNameOrUri)
        //{
        //    return builder;
        //}

        public static ITabbedSegmentBuilder SelectTab<TViewModel>(this ITabbedSegmentBuilder builder)
            where TViewModel : class, INotifyPropertyChanged
        {
            var navigationKey = INavigationBuilderExtensions.GetNavigationKey<TViewModel>();
            return builder.SelectedTab(navigationKey);
        }
    }
}
