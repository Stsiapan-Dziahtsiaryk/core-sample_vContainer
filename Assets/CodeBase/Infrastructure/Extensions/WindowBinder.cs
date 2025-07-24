using CodeBase.Shared;
using Infrastructure.UIStateMachine;
using Shared.Presentation;
using VContainer;

namespace Infrastructure.Extensions
{
    public static class WindowBinder
    {
        public static IObjectResolver BindWindow<TPresenter>(this IObjectResolver resolver, WindowType type)
        where TPresenter : class, ICanvasPresenter
        {
            IWindow window = resolver.Resolve<IWindowResolver>().Resolve(type);
            window.InvokeOpen += resolver.Resolve<TPresenter>().HandleOpenedWindow;
            window.InvokeClose += resolver.Resolve<TPresenter>().HandleClosedWindow;
            return resolver;
        }
    }
}