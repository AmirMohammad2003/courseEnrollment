using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemGroup.Framework.Business;
using SystemGroup.General.CourseEnrollment.Common;
using SystemGroup.Web.UI;
using SystemGroup.Web.UI.Controls;

namespace SystemGroup.General.CourseEnrollment.Web.SemesterCoursePlanPages
{
    public partial class Edit : SgEditorPage<SemesterCoursePlan>
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
                return LoadOptions.With<SemesterCoursePlan>(i => i.SemesterCoursePlanItems)
                    .With<SemesterCoursePlanItem>(i => i.TimeTables);
            }
        }

        protected override IEnumerable<string> ClientSideDetailDataSources
        {
            get
            {
                yield return ".SemesterCoursePlanItems";
                yield return ".SemesterCoursePlanItems.TimeTables";
            }
        }

        #endregion

        #region Methods

        protected override void OnEntityLoaded(object sender, EntityLoadedEventArgs e)
        {
            base.OnEntityLoaded(sender, e);

            SemesterCoursePlanItem.FillExtraProperties(CurrentEntity.SemesterCoursePlanItems);
            foreach (var item in CurrentEntity.SemesterCoursePlanItems)
            {
                TimeTable.FillExtraProperties(item.TimeTables);
            }
            SetUIProperties();
        }

        protected override void OnEntitySaved(object sender, EntitySavedEventArgs e)
        {
            base.OnEntitySaved(sender, e);

            SetUIProperties();
        }

        protected override void OnEditorBinding(EditorBindingEventArgs<SemesterCoursePlan> e)
        {
            base.OnEditorBinding(e);

            e.Context.BindValueTypeProperty(coursePlan => coursePlan.SemesterRef).To(sltSemester);
            e.Context.BindValueTypeProperty(coursePlan => coursePlan.MajorRef).To(sltMajor);
            
        }

        protected void sltCourse_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            var slt = sender as SgSelector;

            slt.ViewParameters[0].Value = Convert.ToInt32(e.Context["id"]);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var ds = FindDataSource(".SemesterCoursePlanItems");
            ds.OnClientInsertedEntity = "ds_insertedEntity";
            ds.OnClientRemovedEntity = "ds_removedEntity";

        }

        private void SetGridEnabled(bool enabled)
        {
            grdCourses.AllowDelete =
            grdCourses.AllowInsert =
            grdCourses.AllowEdit = enabled;

            grdCourses.Rebuild();
        }

        private void SetUIProperties()
        {
            var ds = FindDataSource(".SemesterCoursePlanItems");
            int count = ds.Entities.Count;

            if (sltMajor.SelectedID != null || CurrentEntity.MajorRef != 0)
            {
                SetGridEnabled(true);
                sltMajor.Enabled = count == 0;
            }
            else
            {
                SetGridEnabled(false);
                sltMajor.Enabled = true;
            }
        }

        #endregion
    }
}
