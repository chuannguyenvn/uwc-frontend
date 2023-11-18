using Commons.Categories;
using Commons.Models;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Vehicles
{
    public class VehiclesView : View
    {
        private ScrollView _scrollView;

        public VehiclesView() : base(nameof(VehiclesView))
        {
            ConfigureUss(nameof(VehiclesView));

            AddToClassList("side-view");

            CreateScrollView();
            CreateEntries();
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollView();
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);
        }

        private void CreateEntries()
        {
            for (int i = 0; i < 30; i++)
            {
                _scrollView.Add(new VehicleListEntry(new VehicleData()
                {
                    LicensePlate = "51F-12345",
                    VehicleType = VehicleType.SideLoader
                }));
            }
        }
    }
}