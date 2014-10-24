<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoCompleteCountry.ascx.cs"
    Inherits="EMS.WebApp.CustomControls.AutoCompleteExtender" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    function SetMaxLength(obj, maxLen) {
        return (obj.value.length < maxLen);
    }

    function CountryAutoCompleteItemSelected(sender, e) {

        if (sender._id == "AutoCompleteEx1Country") {
            var hdnFromLocation = $get('<%=hdnCountryId.ClientID %>');
            hdnFromLocation.value = e.get_value();
            //  alert(hdnFromLocation.value);
        }
    }
    </script>

<style type="text/css" >
   
    

</style>
<div>

    <asp:TextBox runat="server" ID="txtCountry" Width="98%" autocomplete="off" style="color: #747862;" CssClass="textboxuppercase" />
    <cc1:TextBoxWatermarkExtender ID="txtWMEName" runat="server" TargetControlID="txtCountry"
        WatermarkText="TYPE COUNTRY" WatermarkCssClass="watermark">
    </cc1:TextBoxWatermarkExtender>
      <asp:HiddenField ID="hdnCountryId" runat="server" Value="0" />
    <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx1Country" ID="autoCompleteCopuntry"
        TargetControlID="txtCountry" ServicePath="AutoComplete.asmx" ServiceMethod="GetCountryListWithId"
        MinimumPrefixLength="2" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
         OnClientItemSelected="CountryAutoCompleteItemSelected" ShowOnlyCurrentWordInCompletionListItem="true"
        >
        <Animations>
                    <OnShow>
                        <Sequence>
                            <%-- Make the completion list transparent and then show it --%>
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            <%--Cache the original size of the completion list the first time
                                the animation is played and then set it to zero --%>
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx1Country');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            <%-- Expand from 0px to the appropriate size while fading in --%>
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx1Country')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        <%-- Collapse down to 0px and fade out --%>
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx1Country')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
        </Animations>
    </cc1:AutoCompleteExtender>
</div>
