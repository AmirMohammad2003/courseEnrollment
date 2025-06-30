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
            get {
                return LoadOptions.With<SemesterCoursePlan>(i => i.SemesterCoursePlanItems);
            }
        }

        protected override IEnumerable<string> ClientSideDetailDataSources
        {
            get
            {
                yield return ".SemesterCoursePlanItems";
            }
        }
        #endregion

        #region Methods

        protected override void OnEntityLoaded(object sender, EntityLoadedEventArgs e)
        {
            base.OnEntityLoaded(sender, e);

            SemesterCoursePlanItem.FillExtraProperties(CurrentEntity.SemesterCoursePlanItems);
        }

        protected override void OnEditorBinding(EditorBindingEventArgs<SemesterCoursePlan> e)
        {
            base.OnEditorBinding(e);

            e.Context.BindValueTypeProperty(coursePlan => coursePlan.SemesterRef).To(sltSemester);
            e.Context.BindValueTypeProperty(coursePlan => coursePlan.MajorRef).To(sltMajor);
        }

        #endregion
    }
}