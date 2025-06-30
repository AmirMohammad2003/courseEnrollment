using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemGroup.Web.UI;
using SystemGroup.Web.UI.Controls;
using SystemGroup.General.CourseEnrollment.Common;
using SystemGroup.Framework.Common;

namespace SystemGroup.General.CourseEnrollment.Web.PartyMajorPages
{
    public partial class Edit : SgEditorPage<PartyMajor>
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

        protected override void OnEditorBinding(EditorBindingEventArgs<PartyMajor> e)
        {
            base.OnEditorBinding(e);

            e.Context.BindValueTypeProperty(partyMajor => partyMajor.PartyRef).To(sltParty);
            e.Context.BindValueTypeProperty(partyMajor => partyMajor.MajorRef).To(sltMajor);
            e.Context.BindNullableProperty(partyMajor => partyMajor.ProfessorPartyRef)
                .To(sltProfessorParty);
        }

        protected override void OnEntityLoaded(object sender, EntityLoadedEventArgs e)
        {
            base.OnEntityLoaded(sender, e);
            SetUIProperties();
        }

        private void SetUIProperties()
        {
            if (CurrentEntity.EntityModificationState != Entity.EntityState.New)
            {
                sltMajor.Enabled = false;
                sltParty.Enabled = false;
            }
        }

        #endregion

    }
}