using UI.Common;
using UI.Views.Status.Name;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Status
{
    public class StatusView : FullScreenView
    {
        private readonly CardsView _cardsView;
        private readonly NameCard _nameCard;
        private readonly VehicleCard _vehicleCard;

        public StatusView() : base("Status")
        {
            _cardsView = new CardsView("StatusCard");
            Add(_cardsView);

            _nameCard = new NameCard();
            _cardsView.Add(_nameCard);

            _vehicleCard = new VehicleCard();
            _cardsView.Add(_vehicleCard);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<StatusView, UxmlTraits>
        {
        }

        #endregion
    }
}