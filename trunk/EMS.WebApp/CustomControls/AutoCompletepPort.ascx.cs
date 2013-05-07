using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Utilities;
using System.Text;

namespace EMS.WebApp.CustomControls
{
    [System.Web.Script.Services.ScriptService]
    public partial class AutoCompletePort : System.Web.UI.UserControl
    {
        private bool? _Enable = true;
        public string RandNo = Guid.NewGuid().ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

            //            StringBuilder sb = new StringBuilder();
            //            sb.AppendFormat(@"
            //            <cc2:AutoCompleteExtender runat=""server""  BehaviorID=""AutoCompleteEx"" ID=""autoCompletePort"" 
            //        TargetControlID=""txtPort"" ServicePath=""AutoComplete.asmx"" ServiceMethod=""GetPortList"" 
            //        MinimumPrefixLength=""2"" CompletionInterval=""1000"" EnableCaching=""true"" CompletionSetCount=""20"" 
            //        CompletionListCssClass=""autocomplete_completionListElement"" CompletionListItemCssClass=""autocomplete_listItem""
            //        CompletionListHighlightedItemCssClass=""autocomplete_highlightedListItem"" DelimiterCharacters="";, :""
            //        ShowOnlyCurrentWordInCompletionListItem=""true"">
            //        <Animations>
            //                    <OnShow>
            //                        <Sequence>
            //                            <%-- Make the completion list transparent and then show it --%>
            //                            <OpacityAction Opacity=""0"" />
            //                            <HideAction Visible=""true"" />
            //                            
            //                            <%--Cache the original size of the completion list the first time
            //                                the animation is played and then set it to zero --%>
            //                            <ScriptAction Script=""
            //                                // Cache the size and setup the initial size
            //                                var behavior = $find('AutoCompleteEx');
            //                                if (!behavior._height) {
            //                                    var target = behavior.get_completionList();
            //                                    behavior._height = target.offsetHeight - 2;
            //                                    target.style.height = '0px';
            //                                }"" />
            //                            
            //                            <%-- Expand from 0px to the appropriate size while fading in --%>
            //                            <Parallel Duration="".4"">
            //                                <FadeIn />
            //                                <Length PropertyKey=""height"" StartValue=""0"" EndValueScript=""$find('AutoCompleteEx')._height"" />
            //                            </Parallel>
            //                        </Sequence>
            //                    </OnShow>
            //                    <OnHide>
            //                        <%-- Collapse down to 0px and fade out --%>
            //                        <Parallel Duration="".4"">
            //                            <FadeOut />
            //                            <Length PropertyKey=""height"" StartValueScript=""$find('AutoCompleteEx')._height"" EndValue=""0"" />
            //                        </Parallel>
            //                    </OnHide>
            //        </Animations>
            //    </cc2:AutoCompleteExtender>");

            //            Response.Write(sb.ToString());

        }

        public bool Enabled
        {
            set
            {
                _Enable = value;
                txtPort.Enabled = _Enable.Value;
            }

        }
    }
}