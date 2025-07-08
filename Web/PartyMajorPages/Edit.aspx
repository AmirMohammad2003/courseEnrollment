<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" 
    Inherits="SystemGroup.General.CourseEnrollment.Web.PartyMajorPages.Edit" 
    Title="Labels_Student"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <sg:SgScriptManager runat="server" ID="scriptManager"></sg:SgScriptManager>
        <sg:SgUpdatePanel runat="server" ID="updMain">
            <ContentTemplate>
                <div runat="server" id="dvMain">
                    <sg:sgfieldset id="fldsetMain" runat="server">
                        <sg:sgfieldlayout runat="server" labelcellwidth="130px">
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textkey="Labels_Student" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgselector runat="server" id="sltParty"
                                        componentname="SystemGroup.General.IPartyManagement"
                                        entityname="IParty" viewname="AllStudentParties">
                                    </sg:sgselector>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="sltParty"
                                        errormessagekey="Messages_ChooseStudent">
                                    </sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textkey="Labels_Major" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgselector runat="server" id="sltMajor"
                                        componentname="SystemGroup.General.CourseEnrollment"
                                        entityname="Major" viewname="AllMajor">
                                    </sg:sgselector>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="sltMajor"
                                        errormessagekey="Messages_ChooseMajor">
                                    </sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textkey="Labels_AssignedProfessor"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgselector runat="server" id="sltProfessorParty"
                                        componentname="SystemGroup.General.IPartyManagement"
                                        entityname="IParty" viewname="AllProfessorParties">
                                    </sg:sgselector>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                        </sg:sgfieldlayout>
                    </sg:sgfieldset>
                </div>
            </ContentTemplate>
        </sg:SgUpdatePanel>
    </form>
</body>
</html>
