using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Service;
using SystemGroup.General.CourseEnrollment.Common;

namespace SystemGroup.General.CourseEnrollment.Business
{
    public class PartyMajorValidator : BusinessValidator<PartyMajor>
    {
        public override void Validate(PartyMajor record, EntityActionType action)
        {
            base.Validate(record, action);

            var majorRef = record.MajorRef;
            var PartyRef = record.PartyRef;


            if (majorRef == 0)
            {
                throw this.CreateException("رشته دانشجو را وارد کنید");
            }

            if (PartyRef == 0)
            {
                throw this.CreateException("دانشجو را وارد کنید");
            }

            var result = ServiceFactory.Create<IPartyMajorBusiness>()
                .FetchByFilter(i => i.MajorRef == majorRef && i.PartyRef == PartyRef && i.ID != record.ID);

            if (result.Any())
            {
                throw this.CreateException("هر دانشجو می تواند در هر رشته فقط یکبار ثبت نام کند.");
            }
        }
    }
}
