using LibContent.Entities;
using LibUniversal.StaticServices;

namespace ContentApi.StaticServices
{
    public static class PageTreeService
    {
        public static string ResolveSlug(Page page)
        {
            var slugSource = page.Title;
            if (page.NavigationTitle != null) slugSource = page.NavigationTitle;

            var slug = TextService.GetSlug(slugSource);

            if (page.slug == null) return slug;

            if (!page.LockSlug && page.slug != slug) return slug;

            return page.slug;
        }
        public static string GetFullRelativePath(Page page)
        {
            var path = "/";
            if (page.IsRoot) return $"{page.Domain ?? ""}{path}";

            path = $"{path}{page.slug ?? ResolveSlug(page)}";

            if (!page.IsRoot && page.Parent != null)
            {
                path = $"{GetFullRelativePath(page.Parent)}{path}";
            }

            return path;
        }
    }
}
