using Shared.Presentation;
using VContainer;

namespace Infrastructure.Extensions
{
    public static class ViewBinder
    {
        public static IObjectResolver Bind<TView, TPresenter>(this IObjectResolver objectResolver)
        where TView : class, IView
        where TPresenter : class, IPresenter
        {
            objectResolver.Resolve<TView>().Construct(objectResolver.Resolve<TPresenter>());
            return objectResolver;
        }
    }
}