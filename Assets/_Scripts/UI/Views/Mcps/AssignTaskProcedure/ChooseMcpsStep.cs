using System.Collections.Generic;
using System.Linq;
using Commons.Models;
using Requests;
using UI.Reusables.Procedure;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class ChooseMcpsStep : Step
    {
        private ScrollView _scrollView;

        public ChooseMcpsStep(Flow flow) : base(flow, 1, "Choose the MCPs that you want to be collected:")
        {
            _scrollView = new ScrollView();
            Add(_scrollView);
            for (int i = 0; i < 5; i++)
            {
                var entry = new McpListEntry(new McpData() { Address = "Test" }, Random.Range(0f, 100f));
                _scrollView.Add(entry);
            }
        }
    }
}