using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemGroup.General.CourseEnrollment.Common;
using SystemGroup.Web.UI;
using SystemGroup.Web.UI.Controls;

namespace SystemGroup.General.CourseEnrollment.Web.SemesterPages
{
    public partial class Edit : SgEditorPage<Semester>
    {
        #region Properties

        public override SgFormView FormView
        {
            get { return null; }
        }

        public override SgUpdatePanel UpdatePanel
        {
            get { return updMain; }
        }

        public override bool HasFormView
        {
            get
            {
                return false;
            }
        }

        public override Control MainContentContainer
        {
            get
            {
                return dvMain;
            }
        }

        #endregion

        #region Methods

        protected override void OnEditorBinding(EditorBindingEventArgs<Semester> e)
        {
            base.OnEditorBinding(e);

            e.Context.BindProperty(semester => semester.Name).To(txtName);
            e.Context.BindNullableProperty(semester => semester.StartDate).To(dpkStartDate, dt => dt.SelectedDate);
            e.Context.BindNullableProperty(semester => semester.EndDate).To(dpkEndDate, dt => dt.SelectedDate);
            e.Context.BindNullableProperty(semester => semester.EnrollmentStartTime).To(dpkStartEnrollmentTime, dt => dt.SelectedDate);
            e.Context.BindNullableProperty(semester => semester.EnrollmentEndTime).To(dpkEndEnrollmentTime, dt => dt.SelectedDate);
        }

        protected override void OnEntityLoaded(object sender, EntityLoadedEventArgs e)
        {
            base.OnEntityLoaded(sender, e);

            SetUIProperties();
        }

        protected override void OnEntitySaved(object sender, EntitySavedEventArgs e)
        {
            base.OnEntitySaved(sender, e);

            SetUIProperties();
        }

        private void SetUIProperties()
        {
            var txt = FindControl("txtName") as SgTextBox;
            var dtpStartDate = FindControl("dpkStartDate") as SgDatePicker;
            var dtpEndDate = FindControl("dpkEndDate") as SgDatePicker;
            var dtpEnrollmentStartDate = FindControl("dpkStartEnrollmentTime") as SgDatePicker;
            var dtpEnrnollmentEndDate = FindControl("dpkEndEnrollmentTime") as SgDatePicker;
            if (CurrentEntity.State == SemesterStatus.Finished) {
                txt.Enabled = 
                dtpStartDate.Enabled =
                dtpEndDate.Enabled =
                dtpEnrollmentStartDate.Enabled =
                dtpEnrnollmentEndDate.Enabled = false;
            } else if (CurrentEntity.State == SemesterStatus.OnGoing)
            {
                txt.Enabled = 
                dtpEnrollmentStartDate.Enabled =
                dtpEnrnollmentEndDate.Enabled = false;
                dtpStartDate.Enabled =
                dtpEndDate.Enabled = true;
            } else
            {
                txt.Enabled =
                dtpStartDate.Enabled =
                dtpEndDate.Enabled =
                dtpEnrollmentStartDate.Enabled =
                dtpEnrnollmentEndDate.Enabled = true;
            }

        }

        #endregion


    }
}