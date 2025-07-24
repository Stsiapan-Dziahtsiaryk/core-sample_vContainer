namespace Shared.Presentation
{
    public interface ICanvasPresenter: IPresenter
    {
        void HandleOpenedWindow();
        void HandleClosedWindow();
    }
}