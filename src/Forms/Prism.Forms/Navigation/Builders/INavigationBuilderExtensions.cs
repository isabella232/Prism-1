using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace Prism.Navigation
{
    public static class INavigationBuilderExtensions
    {
        internal static string GetNavigationKey<TViewModel>()
        {
            var vmType = typeof(TViewModel);
            if (vmType.IsAssignableFrom(typeof(VisualElement)))
                throw new NavigationException(NavigationException.MvvmPatternBreak, null);

            return NavigationRegistry.GetViewKey(vmType);
        }

        public static INavigationBuilder UseAbsoluteNavigation(this INavigationBuilder builder) =>
            builder.UseAbsoluteNavigation(true);

        public static INavigationBuilder AddNavigationSegment(this INavigationBuilder builder, string segmentName, bool? useModalNavigation = null) =>
            builder.AddNavigationSegment(segmentName, o =>
            {
                if (useModalNavigation.HasValue)
                    o.UseModalNavigation(useModalNavigation.Value);
            });

        public static INavigationBuilder AddNavigationSegment<TViewModel>(this INavigationBuilder builder, Action<ISegmentBuilder> configureSegment)
            where TViewModel : class, INotifyPropertyChanged =>
            builder.AddNavigationSegment(GetNavigationKey<TViewModel>(), configureSegment);

        public static INavigationBuilder AddNavigationSegment<TViewModel>(this INavigationBuilder builder, bool useModalNavigation)
            where TViewModel : class, INotifyPropertyChanged =>
            builder.AddNavigationSegment(GetNavigationKey<TViewModel>(), useModalNavigation);

        // Will check for the Navigation key of a registered NavigationPage
        public static INavigationBuilder AddNavigationPage(this INavigationBuilder builder) =>
            builder.AddNavigationPage(null);

        public static INavigationBuilder AddNavigationPage(this INavigationBuilder builder, Action<ISegmentBuilder> configureSegment)
        {
            var registrationInfo = NavigationRegistry.Cache.FirstOrDefault(x => x.ViewType.IsAssignableFrom(typeof(NavigationPage)));
            if (registrationInfo is null)
                throw new NavigationException(NavigationException.NoPageIsRegistered, null);

            return builder.AddNavigationSegment(registrationInfo.Name, configureSegment);
        }

        public static INavigationBuilder AddNavigationPage(this INavigationBuilder builder, bool useModalNavigation) =>
            builder.AddNavigationPage(o => o.UseModalNavigation(useModalNavigation));


        //public static INavigationBuilder AddNavigationSegment(this INavigationBuilder builder, string segmentName, params string[] createTabs)
        //{
        //    return builder;
        //}

        //public static INavigationBuilder AddNavigationSegment(this INavigationBuilder builder, string segmentName, bool useModalNavigation, params string[] createTabs)
        //{
        //    return builder;
        //}

        //public static INavigationBuilder AddNavigationSegment(this INavigationBuilder builder, string segmentName, string selectTab, bool? useModalNavigation, params string[] createTabs)
        //{
        //    return builder;
        //}

        public static async void Navigate(this INavigationBuilder builder)
        {
            await builder.NavigateAsync();
        }

        public static async void Navigate(this INavigationBuilder builder, Action<Exception> onError)
        {
            await builder.NavigateAsync(onError);
        }

        public static async void Navigate(this INavigationBuilder builder, Action onSuccess)
        {
            await builder.NavigateAsync(onSuccess, null);
        }

        public static async void Navigate(this INavigationBuilder builder, Action onSuccess, Action<Exception> onError)
        {
            await builder.NavigateAsync(onSuccess, onError);
        }
    }
}
