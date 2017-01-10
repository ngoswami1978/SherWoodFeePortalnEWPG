using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SherWoodFeePortal.ProgressBarTracker
{
    public partial class TrackerBar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // initialize steps
                AddSteps("Step 1", "Step 2", "Done");
                SetProgress(0);
            }

        }

        protected void AddSteps(params string[] items)
        {
            ProgressBarTracker1.Items.Clear();
            ProgressBarTracker1.Attributes["data-progtrckr-steps"] = items.Length.ToString();

            for (int i = 0; i < items.Length; i++)
            {
                ProgressBarTracker1.Items.Add(new ListItem(items[i]));
            }
        }

        protected void SetProgress(int current)
        {
            for (int i = 0; i < ProgressBarTracker1.Items.Count; i++)
            {
                ProgressBarTracker1.Items[i].Attributes["class"] =
                    (i < current) ? "progtrckr-done" :
                    (i == current) ? "progtrckr-current" : "progtrckr-todo";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SetProgress(1);
        }
    }

}