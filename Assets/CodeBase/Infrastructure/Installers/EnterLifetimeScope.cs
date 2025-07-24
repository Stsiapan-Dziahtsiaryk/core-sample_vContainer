using Domain.Presentation.Presenters;
using Domain.Presentation.Views;
using Infrastructure.Composition;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Installers
{
    public class EnterLifetimeScope : LifetimeScope
    {
        [Header("View's")]
        [SerializeField] private CurtainView _curtainInstance;
        [SerializeField] private InternetAlertView _internetAlertView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EnterCompositionRoot>();

            // Bind View's
            builder.RegisterInstance(_curtainInstance);
            builder.RegisterInstance(_internetAlertView);
            
            // Bind Presenter's
            builder.Register<CurtainPresenter>(Lifetime.Singleton);
            builder.Register<InternetAlertPresenter>(Lifetime.Singleton);
        }
    }
}