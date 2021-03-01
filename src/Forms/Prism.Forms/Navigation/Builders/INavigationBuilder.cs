using System;
using System.Threading.Tasks;

namespace Prism.Navigation
{
    public interface INavigationBuilder
    {
        INavigationBuilder AddNavigationSegment(string segmentName, Action<ISegmentBuilder> configureSegment);

        INavigationBuilder AddTabbedSegment(Action<ITabbedSegmentBuilder> configureSegment);

        // Will check for the Navigation key of a registered TabbedPage
        //ITabbedNavigationBuilder AddTabbedSegment(bool? useModalNavigation = null);
        //ITabbedNavigationBuilder AddTabbedSegment(string segmentName, bool? useModalNavigation = null);
        //ITabbedNavigationBuilder AddTabbedSegment<TViewModel>(bool? useModalNavigation = null)
        //    where TViewModel : class, INotifyPropertyChanged;

        INavigationBuilder WithParameters(INavigationParameters parameters);
        INavigationBuilder AddParameter(string key, object value);

        INavigationBuilder UseAbsoluteNavigation(bool absolute); // allows conditional logic to be placed here
        INavigationBuilder UseRelativeNavigation();

        Task<INavigationResult> NavigateAsync();
        Task NavigateAsync(Action<Exception> onError);
        Task NavigateAsync(Action onSuccess, Action<Exception> onError);
    }
}
