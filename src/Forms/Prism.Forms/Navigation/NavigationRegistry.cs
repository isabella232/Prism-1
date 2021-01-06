using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Prism.Navigation
{
    public static class NavigationRegistry
    {
        static Dictionary<string, PageNavigationInfo> _pageRegistrationCache = new Dictionary<string, PageNavigationInfo>();

        public static void Register(string name, Type viewType, Type viewModelType)
        {
            var info = new PageNavigationInfo
            {
                Name = name,
                ViewType = viewType,
                ViewModelType = viewModelType
            };

            if (!_pageRegistrationCache.ContainsKey(name))
                _pageRegistrationCache.Add(name, info);
        }

        public static PageNavigationInfo GetPageNavigationInfo(string name)
        {
            if (_pageRegistrationCache.ContainsKey(name))
                return _pageRegistrationCache[name];

            return null;
        }

        public static PageNavigationInfo GetPageNavigationInfo(Type viewType)
        {
            foreach (var item in _pageRegistrationCache)
            {
                if (item.Value.ViewType == viewType)
                    return item.Value;
            }

            return null;
        }

        public static Type GetPageType(string name)
        {
            return GetPageNavigationInfo(name)?.ViewType;
        }

        public static string GetViewKey(Type type) =>
            _pageRegistrationCache.FirstOrDefault(x => x.Value.ViewType == type || x.Value.ViewModelType == type).Value?.Name;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void ClearRegistrationCache()
        {
            _pageRegistrationCache.Clear();
        }
    }
}
