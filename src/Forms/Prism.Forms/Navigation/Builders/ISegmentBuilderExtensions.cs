namespace Prism.Navigation
{
    public static class ISegmentBuilderExtensions
    {
        public static ISegmentBuilder UseModalNavigation(this ISegmentBuilder builder) =>
            builder.UseModalNavigation(true);
    }
}
