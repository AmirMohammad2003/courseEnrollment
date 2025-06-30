using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.General.CourseEnrollment.Common;
using SystemGroup.Web.UI;
using SystemGroup.Web.UI.Controls;

namespace SystemGroup.General.CourseEnrollment.Web.MajorPages
{
    public partial class Edit : SgEditorPage<Major>
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
                return LoadOptions.With<Major>(i => i.MajorCourses);
            }
        }

        protected override IEnumerable<string> ClientSideDetailDataSources
        {
            get
            {
                yield return ".MajorCourses";
            }
        }

        #endregion

        #region Methods

        protected override void OnEditorBinding(EditorBindingEventArgs<Major> e)
        {
            base.OnEditorBinding(e);

            e.Context.BindProperty(major => major.Name).To(txtName);
            e.Context.BindValueTypeProperty(major => major.Units).To(txtUnits, t => t.Value, 
                entity => (double) entity.Entity.Units, value => (int) value.Target.Value);
        }

        protected override void OnEntityLoaded(object sender, EntityLoadedEventArgs e)
        {
            base.OnEntityLoaded(sender, e);

            MajorCourse.FillExtraProperties(CurrentEntity.MajorCourses);
        }

        protected void slt_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs  e)
        {
			var slt = (SgSelector)sender;
			var ignoredIDs = ((IEnumerable)e.Context["IgnoredIDs"]).
				Cast<object>().
				Select(o => Convert.ToInt64(o)).
				ToList();
			slt.FilterExpression = o => !ignoredIDs.Contains(((Entity)o).ID);
        }
        #endregion
    }
}