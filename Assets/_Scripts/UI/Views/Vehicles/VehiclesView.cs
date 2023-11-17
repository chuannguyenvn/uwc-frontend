using Commons.Categories;
using Commons.Models;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Mcps
{
    public class VehiclesView : View
    {
        private ScrollView _scrollView;

        public VehiclesView() : base(nameof(VehiclesView))
        {
            ConfigureUss(nameof(VehiclesView));

            AddToClassList("side-view");

            _scrollView = new ScrollView();
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);

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