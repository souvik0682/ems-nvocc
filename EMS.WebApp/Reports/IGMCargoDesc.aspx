<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IGMCargoDesc.aspx.cs" Inherits="EMS.WebApp.Reports.IGMCargoDesc" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <asp:UpdateProgress ID="uProgressLoc" runat="server" AssociatedUpdatePanelID="upLoc">
        <ProgressTemplate>
            <div class="progress">
                <div id="image">
                    <img src="../../Images/PleaseWait.gif" alt="" /></div>
                <div id="text">
                    Please Wait...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <center>

 <div id="headercaption">
         C A R G O    D E C L A R A T I O N </div>
    <center>
        <fieldset style="width:800px; ">
            <legend> Form-II Cargo Declaration </legend>
             <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlVoyage" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlVessel" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlLoc" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="3" width="100%">
            
            <tr>
              <td style="width:45%" ><table width="100%">
                    <tr>
                    <td style="text-align:right">Line:</td>
                    <td>
                     <asp:DropDownList ID="ddlLine" runat="server" Width="100%">
                         
                      </asp:DropDownList>
                    </td>
           
                    <td style="text-align:right">Location:<%--<span class="errormessage1">*</span>--%></td>
                    <td>
                      <asp:DropDownList ID="ddlLoc" runat="server" Width="100%" onselectedindexchanged="ddlLoc_SelectedIndexChanged" 
                            ></asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errormessage" 
                            ControlToValidate="ddlLoc" Display="Dynamic" InitialValue="0" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
              
              </table></td>
              <td style="width:45%" ><table width="100%">
                <tr>
                    <td style="text-align:right">Vessel:</td>
                    <td>
                      <asp:DropDownList ID="ddlVessel" runat="server" Width="100%" AutoPostBack="True" onselectedindexchanged="ddlVessel_SelectedIndexChanged" 
                            ></asp:DropDownList>
                    </td>
             
                    <td style="text-align:right">Voyage:</td>
                    <td>
                      <asp:DropDownList ID="ddlVoyage" runat="server" Width="100%" 
                            onselectedindexchanged="ddlVoyage_SelectedIndexChanged"      >
                         
                            </asp:DropDownList>
                    </td>
                </tr>
              </table></td>
                    
                <td colspan="2" style="text-align:right; width:10%">
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Show" 
                        ValidationGroup="Save" />
                    &nbsp;&nbsp;
                </td>
            </tr>
    
            </table>
             </ContentTemplate>
       </asp:UpdatePanel>
        </fieldset>
    </center>

    <div style="padding-top: 10px;">
        <div style="padding-left:5px;width:98%;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" 
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" >
                <LocalReport ReportPath="RDLC\IGMCargoDesc.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>        
        </div>    
    </div>
</center>
</asp:Content>
