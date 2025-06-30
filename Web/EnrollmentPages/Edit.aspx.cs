using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Logging;
using SystemGroup.Framework.Security;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.Utilities;
using SystemGroup.General.CourseEnrollment.Common;
using SystemGroup.Web.UI;
using SystemGroup.Web.UI.Controls;
using SystemGroup.Web.UI.Shell;

namespace SystemGroup.General.CourseEnrollment.Web.EnrollmentPages
{
    public partial class Edit : SgEditorPage<Enrollment>
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

        public override DetailLoadOptions EntityLoadOptions
        {
            get
            {
                return LoadOptions.With<Enrollment>(i => i.EnrollmentItems);
            }
        }

        protected override IEnumerable<string> ClientSideDetailDataSources
        {
            get
            {
                yield return ".EnrollmentItems";
            }
        }   

        #endregion

        #region Methods

        protected override void OnEditorBinding(EditorBindingEventArgs<Enrollment> e)
        {
            base.OnEditorBinding(e);

            e.Context.BindValueTypeProperty(enrollment => enrollment.SemesterCoursePlanRef).To(sltSemesterCoursePlan);
        }

        protected override void OnEntityLoaded(object sender, EntityLoadedEventArgs e)
        {
            base.OnEntityLoaded(sender, e);

            EnrollmentItem.FillExtraProperties(CurrentEntity.EnrollmentItems);
            SetUIProperties();
        }

        protected override void OnEntitySaved(object sender, EntitySavedEventArgs e)
        {
            base.OnEntitySaved(sender, e);

            SetUIProperties();
        }
        

        private void SetGridEnabled(bool enabled)
        {
            SgGrid grd = FindControl("grdCourses") as SgGrid;

            grd.AllowDelete =
            grd.AllowInsert =
            grd.AllowEdit = enabled;

            grd.Rebuild();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var ds = FindDataSource(".EnrollmentItems");
            ds.OnClientInsertedEntity = "ds_insertedEntity";
            ds.OnClientRemovedEntity = "ds_removedEntity";
            SgGrid grd = FindControl("grdCourses") as SgGrid;
            grd.MenuVisibility = SgGridMenuVisibility.Visible;
            grd.ToolBarVisibility = SgGridToolBarVisibility.Visible;
        }

        private void SetUIProperties()
        {
            var ds = FindDataSource(".EnrollmentItems");
            SgSelector slt = FindControl("sltSemesterCoursePlan") as SgSelector;
            if (CurrentEntity.State != EnrollmentStatus.Registering || 
                !ServiceFactory.Create<IAuthorizationService>().HasAccess("CourseEnrollment.Enrollment.Edit")) {
                SetGridEnabled(false);
                slt.Enabled = false;
                return;
            }

            int count = ds.Entities.Count;
            if (slt.SelectedID != null || CurrentEntity.SemesterCoursePlanRef != 0)
            {
                SetGridEnabled(true);
                slt.Enabled = count == 0;
            } else
            {
                SetGridEnabled(false);
                slt.Enabled = true;
            }
        }

        protected void sltCourse_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs  e)
        {
            var slt = (SgSelector)sender;
            var ignoredIDs = ((IEnumerable)e.Context["IgnoredIDs"]).
                Cast<object>().
                Select(o => Convert.ToInt64(o)).
                ToList();
            slt.FilterExpression = o => !ignoredIDs.Contains(((Entity)o).ID);

            slt.ViewParameters[0].Value = Convert.ToInt64(e.Context["id"]);
        }

        #endregion
    }
}