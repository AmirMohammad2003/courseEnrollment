<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditClass.aspx.cs" 
    Inherits="SystemGroup.General.CourseEnrollment.Web.SemesterCoursePlanPages.EditClass"
    Title="Labels_Scoring"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <sg:SgScriptManager runat="server" ID="scriptManager">
            <Scripts>
                <asp:ScriptReference Path="EditClass.js"/>
            </Scripts>
        </sg:SgScriptManager>
        <sg:SgUpdatePanel runat="server" ID="updMain">
            <ContentTemplate>
                <div runat="server" id="dvMain">
                    <sg:SgGrid runat="server" id="grdParties" gridtype="ClientSide" allowscroll="true"
                        allowedit="true" allowinsert="false" allowdelete="false" width="800px"
                        datasourceid="edsParties" validationgroup="vgGrid"
                        ShowStandardCommands="false" OnCommand="GrdParties_Command">
                        <Columns>
                            <sg:SgTextGridColumn PropertyName="PartyName" HeaderText="Labels_Name" AllowEdit="false" />
                            <sg:SgNumericGridColumn PropertyName="Score" HeaderText="Labels_Score" AllowEdit="true" >
                                <EditItemTemplate>
                                    <sg:sgnumerictextbox runat="server" id="txtScore" value="1" minvalue="0" maxvalue="20"
                                        numberformat-decimaldigits="0" datatype="System.Int" cbvalue="{binding Score}" />
                                </EditItemTemplate>
                            </sg:SgNumericGridColumn>
                        </Columns>
                        <Commands>
                            <sg:SgGridCommand TextKey="Labels_Save" Multiplicity="Any" UniqueName="Save" ImageUrl="~/Icons/Save.gif" />
                            <sg:SgGridCommand TextKey="Labels_Reload" Multiplicity="Any" UniqueName="Reload" ImageUrl="~/Icons/Reload.gif" />
                        </Commands>
                    </sg:SgGrid>
                </div> 
            </ContentTemplate>
        </sg:SgUpdatePanel>
    </form>
</body>
</html>
