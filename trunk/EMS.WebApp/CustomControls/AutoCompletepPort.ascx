<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoCompletepPort.ascx.cs"
    Inherits="EMS.WebApp.CustomControls.AutoCompletePort" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<link href="../CustomControls/StyleSheet.css" rel="stylesheet" type="text/css" />
<style type="text/css">
  
</style>
<table width="100%" cellpadding="0" cellspacing="0" style="vertical-align: bottom">
    <tr>
        <td style="vertical-align: bottom">
            <asp:TextBox runat="server" ID="txtPort" Width="98%" autocomplete="off" CssClass="textboxuppercase" />
            <cc2:TextBoxWatermarkExtender ID="txtWMEName1" runat="server" TargetControlID="txtPort"
                WatermarkText="TYPE PORT" WatermarkCssClass="watermark">
            </cc2:TextBoxWatermarkExtender>
            <cc2:AutoCompleteExtender runat="server" ID="AutoPort" TargetControlID="txtPort"
                ServicePath="AutoComplete.asmx" ServiceMethod="GetPortList" MinimumPrefixLength="2"
                CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                DelimiterCharacters=";, :" ShowOnlyCurrentWordInCompletionListItem="true">
                <%-- <Animations>
                    <OnShow>
                        <Sequence>
                           
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find(AutoPort;);
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            
                            <Parallel Duration=".4">
                                <FadeIn />--%>
                <%--       <%--<Length PropertyKey="height" StartValue="0" EndValueScript="$get('<%=RandNo %>').._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                      
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('<%=RandNo %>')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
        </Animations>--%>
            </cc2:AutoCompleteExtender>
        </td>
    </tr>
</table>
<%--<script type="text/javascript">
    function GetID() {
        return '<%=RandNo %>';
    }
    function GetEID() {
        var ed = '<%=RandNo %>';
        return ed;
    }
    var rid = GetID();
    alert(Math.random());
</script>--%>