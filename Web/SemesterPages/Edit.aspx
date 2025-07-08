<%@ page language="C#" autoeventwireup="true" codebehind="Edit.aspx.cs" 
    inherits="SystemGroup.General.CourseEnrollment.Web.SemesterPages.Edit"
    title="Labels_SemesterPageName" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <sg:sgscriptmanager runat="server" id="scriptManager"></sg:sgscriptmanager>
        <sg:sgupdatepanel runat="server" id="updMain">
            <contenttemplate>
                <div runat="server" id="dvMain">
                    <sg:sgfieldset id="fldsetMain" runat="server">
                        <sg:sgfieldlayout runat="server" LabelCellWidth="130px">
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textKey="Labels_SemesterName" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgtextbox runat="server" id="txtName" width="100px"></sg:sgtextbox>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="txtName" errormessagekey="Messages_EnterSemesterName"></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textKey="Labels_SemesterStartDate" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgdatepicker runat="server" id="dpkStartDate" allowtoggleculture="True" width="100px" />
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="dpkStartDate" errormessagekey="Messages_EnterSemesterStartDate"></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textKey="Labels_SemesterEndDate" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgdatepicker runat="server" id="dpkEndDate" allowtoggleculture="True" width="100px" />
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="dpkEndDate" errormessagekey="Messages_EnterSemesterStartDate"></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textKey="Labels_SemesterEnrollmentStartDate" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgdatepicker runat="server" id="dpkStartEnrollmentTime" allowtoggleculture="True" width="100px" />
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="dpkStartEnrollmentTime" errormessagekey="Messages_EnterEnrollmentStartTime"></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textKey="Labels_SemesterEnrollmentEndDate" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgdatepicker runat="server" id="dpkEndEnrollmentTime" allowtoggleculture="True" width="100px" />
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="dpkEndEnrollmentTime" errormessagekey="Messages_EnterEnrollmentEndTime"></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                        </sg:sgfieldlayout>
                    </sg:sgfieldset>
                </div>
            </contenttemplate>
        </sg:sgupdatepanel>
    </form>
</body>
</html>
